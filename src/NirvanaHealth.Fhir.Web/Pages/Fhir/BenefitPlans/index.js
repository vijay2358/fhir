$(function () {
    var l = abp.localization.getResource("Fhir");
	
	var benefitPlanService = window.nirvanaHealth.fhir.benefitPlans.benefitPlan;
	
	
    var createModal = new abp.ModalManager({
        viewUrl: abp.appPath + "Fhir/BenefitPlans/CreateModal",
        scriptUrl: "/Pages/Fhir/BenefitPlans/createModal.js",
        modalClass: "benefitPlanCreate"
    });

	var editModal = new abp.ModalManager({
        viewUrl: abp.appPath + "Fhir/BenefitPlans/EditModal",
        scriptUrl: "/Pages/Fhir/BenefitPlans/editModal.js",
        modalClass: "benefitPlanEdit"
    });

	var getFilter = function() {
        return {
            filterText: $("#FilterText").val(),
            benefitPlan_IDMin: $("#BenefitPlan_IDFilterMin").val(),
			benefitPlan_IDMax: $("#BenefitPlan_IDFilterMax").val(),
			benefitName: $("#BenefitNameFilter").val(),
			benefitCode: $("#BenefitCodeFilter").val(),
			description: $("#DescriptionFilter").val(),
			versionNbr: $("#VersionNbrFilter").val()
        };
    };

    var dataTable = $("#BenefitPlansTable").DataTable(abp.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        paging: true,
        searching: false,
        scrollX: true,
        autoWidth: false,
        scrollCollapse: true,
        order: [[1, "asc"]],
        ajax: abp.libs.datatables.createAjax(benefitPlanService.getList, getFilter),
        columnDefs: [
            {
                rowAction: {
                    items:
                        [
                            {
                                text: l("Edit"),
                                visible: abp.auth.isGranted('Fhir.BenefitPlans.Edit'),
                                action: function (data) {
                                    editModal.open({
                                     id: data.record.id
                                     });
                                }
                            },
                            {
                                text: l("Delete"),
                                visible: abp.auth.isGranted('Fhir.BenefitPlans.Delete'),
                                confirmMessage: function () {
                                    return l("DeleteConfirmationMessage");
                                },
                                action: function (data) {
                                    benefitPlanService.delete(data.record.id)
                                        .then(function () {
                                            abp.notify.info(l("SuccessfullyDeleted"));
                                            dataTable.ajax.reload();
                                        });
                                }
                            }
                        ]
                }
            },
			{ data: "benefitPlan_ID" },
			{ data: "benefitName" },
			{ data: "benefitCode" },
			{ data: "description" },
			{ data: "versionNbr" }
        ]
    }));

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $("#NewBenefitPlanButton").click(function (e) {
        e.preventDefault();
        createModal.open();
    });

	$("#SearchForm").submit(function (e) {
        e.preventDefault();
        dataTable.ajax.reload();
    });

    $('#AdvancedFilterSectionToggler').on('click', function (e) {
        $('#AdvancedFilterSection').toggle();
    });

    $('#AdvancedFilterSection').on('keypress', function (e) {
        if (e.which === 13) {
            dataTable.ajax.reload();
        }
    });

    $('#AdvancedFilterSection select').change(function() {
        dataTable.ajax.reload();
    });
    
    
});
