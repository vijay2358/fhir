$(function () {
    var l = abp.localization.getResource("Fhir");
	
	var businessService = window.nirvanaHealth.fhir.businesses.business;
	
	
    var createModal = new abp.ModalManager({
        viewUrl: abp.appPath + "Fhir/Businesses/CreateModal",
        scriptUrl: "/Pages/Fhir/Businesses/createModal.js",
        modalClass: "businessCreate"
    });

	var editModal = new abp.ModalManager({
        viewUrl: abp.appPath + "Fhir/Businesses/EditModal",
        scriptUrl: "/Pages/Fhir/Businesses/editModal.js",
        modalClass: "businessEdit"
    });

	var getFilter = function() {
        return {
            filterText: $("#FilterText").val(),
            business_IDMin: $("#Business_IDFilterMin").val(),
			business_IDMax: $("#Business_IDFilterMax").val(),
			businessName: $("#BusinessNameFilter").val(),
			businessCode: $("#BusinessCodeFilter").val(),
			businessCatMin: $("#BusinessCatFilterMin").val(),
			businessCatMax: $("#BusinessCatFilterMax").val(),
			trackerCode: $("#TrackerCodeFilter").val(),
			dBA: $("#DBAFilter").val(),
			fEIN: $("#FEINFilter").val()
        };
    };

    var dataTable = $("#BusinessesTable").DataTable(abp.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        paging: true,
        searching: false,
        scrollX: true,
        autoWidth: false,
        scrollCollapse: true,
        order: [[1, "asc"]],
        ajax: abp.libs.datatables.createAjax(businessService.getList, getFilter),
        columnDefs: [
            {
                rowAction: {
                    items:
                        [
                            {
                                text: l("Edit"),
                                visible: abp.auth.isGranted('Fhir.Businesses.Edit'),
                                action: function (data) {
                                    editModal.open({
                                     id: data.record.id
                                     });
                                }
                            },
                            {
                                text: l("Delete"),
                                visible: abp.auth.isGranted('Fhir.Businesses.Delete'),
                                confirmMessage: function () {
                                    return l("DeleteConfirmationMessage");
                                },
                                action: function (data) {
                                    businessService.delete(data.record.id)
                                        .then(function () {
                                            abp.notify.info(l("SuccessfullyDeleted"));
                                            dataTable.ajax.reload();
                                        });
                                }
                            }
                        ]
                }
            },
			{ data: "business_ID" },
			{ data: "businessName" },
			{ data: "businessCode" },
			{ data: "businessCat" },
			{ data: "trackerCode" },
			{ data: "dba" },
			{ data: "fein" }
        ]
    }));

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $("#NewBusinessButton").click(function (e) {
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
