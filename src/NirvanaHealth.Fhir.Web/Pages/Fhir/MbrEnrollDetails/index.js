$(function () {
    var l = abp.localization.getResource("Fhir");
	
	var mbrEnrollDetailService = window.nirvanaHealth.fhir.mbrEnrollDetails.mbrEnrollDetail;
	
	
    var createModal = new abp.ModalManager({
        viewUrl: abp.appPath + "Fhir/MbrEnrollDetails/CreateModal",
        scriptUrl: "/Pages/Fhir/MbrEnrollDetails/createModal.js",
        modalClass: "mbrEnrollDetailCreate"
    });

	var editModal = new abp.ModalManager({
        viewUrl: abp.appPath + "Fhir/MbrEnrollDetails/EditModal",
        scriptUrl: "/Pages/Fhir/MbrEnrollDetails/editModal.js",
        modalClass: "mbrEnrollDetailEdit"
    });

	var getFilter = function() {
        return {
            filterText: $("#FilterText").val(),
            mbrEnrollDetail_IDMin: $("#MbrEnrollDetail_IDFilterMin").val(),
			mbrEnrollDetail_IDMax: $("#MbrEnrollDetail_IDFilterMax").val(),
			mCPMember_IDMin: $("#MCPMember_IDFilterMin").val(),
			mCPMember_IDMax: $("#MCPMember_IDFilterMax").val(),
			benefitPlan_IDMin: $("#BenefitPlan_IDFilterMin").val(),
			benefitPlan_IDMax: $("#BenefitPlan_IDFilterMax").val(),
			member_ID: $("#Member_IDFilter").val(),
			subscriber_ID: $("#Subscriber_IDFilter").val(),
			personCode: $("#PersonCodeFilter").val()
        };
    };

    var dataTable = $("#MbrEnrollDetailsTable").DataTable(abp.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        paging: true,
        searching: false,
        scrollX: true,
        autoWidth: false,
        scrollCollapse: true,
        order: [[1, "asc"]],
        ajax: abp.libs.datatables.createAjax(mbrEnrollDetailService.getList, getFilter),
        columnDefs: [
            {
                rowAction: {
                    items:
                        [
                            {
                                text: l("Edit"),
                                visible: abp.auth.isGranted('Fhir.MbrEnrollDetails.Edit'),
                                action: function (data) {
                                    editModal.open({
                                     id: data.record.id
                                     });
                                }
                            },
                            {
                                text: l("Delete"),
                                visible: abp.auth.isGranted('Fhir.MbrEnrollDetails.Delete'),
                                confirmMessage: function () {
                                    return l("DeleteConfirmationMessage");
                                },
                                action: function (data) {
                                    mbrEnrollDetailService.delete(data.record.id)
                                        .then(function () {
                                            abp.notify.info(l("SuccessfullyDeleted"));
                                            dataTable.ajax.reload();
                                        });
                                }
                            }
                        ]
                }
            },
			{ data: "mbrEnrollDetail_ID" },
			{ data: "mCPMember_ID" },
			{ data: "benefitPlan_ID" },
			{ data: "member_ID" },
			{ data: "subscriber_ID" },
			{ data: "personCode" }
        ]
    }));

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $("#NewMbrEnrollDetailButton").click(function (e) {
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
