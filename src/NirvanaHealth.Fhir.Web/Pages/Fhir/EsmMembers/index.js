$(function () {
    var l = abp.localization.getResource("Fhir");
	
	var esmMemberService = window.nirvanaHealth.fhir.esmMembers.esmMember;
	
	
    var createModal = new abp.ModalManager({
        viewUrl: abp.appPath + "Fhir/EsmMembers/CreateModal",
        scriptUrl: "/Pages/Fhir/EsmMembers/createModal.js",
        modalClass: "esmMemberCreate"
    });

	var editModal = new abp.ModalManager({
        viewUrl: abp.appPath + "Fhir/EsmMembers/EditModal",
        scriptUrl: "/Pages/Fhir/EsmMembers/editModal.js",
        modalClass: "esmMemberEdit"
    });

	var getFilter = function() {
        return {
            filterText: $("#FilterText").val(),
            mCPMember_IDMin: $("#MCPMember_IDFilterMin").val(),
			mCPMember_IDMax: $("#MCPMember_IDFilterMax").val(),
			lastName: $("#LastNameFilter").val(),
			firstName: $("#FirstNameFilter").val(),
			mIddleName: $("#MIddleNameFilter").val(),
			suffix: $("#SuffixFilter").val(),
			preFix: $("#PreFixFilter").val(),
			dOBMin: $("#DOBFilterMin").data().datepicker.getFormattedDate('yyyy-mm-dd'),
			dOBMax: $("#DOBFilterMax").data().datepicker.getFormattedDate('yyyy-mm-dd'),
			gender: $("#GenderFilter").val(),
			dateofDeathMin: $("#DateofDeathFilterMin").data().datepicker.getFormattedDate('yyyy-mm-dd'),
			dateofDeathMax: $("#DateofDeathFilterMax").data().datepicker.getFormattedDate('yyyy-mm-dd'),
			race: $("#RaceFilter").val()
        };
    };

    var dataTable = $("#EsmMembersTable").DataTable(abp.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        paging: true,
        searching: false,
        scrollX: true,
        autoWidth: false,
        scrollCollapse: true,
        order: [[1, "asc"]],
        ajax: abp.libs.datatables.createAjax(esmMemberService.getList, getFilter),
        columnDefs: [
            {
                rowAction: {
                    items:
                        [
                            {
                                text: l("Edit"),
                                visible: abp.auth.isGranted('Fhir.EsmMembers.Edit'),
                                action: function (data) {
                                    editModal.open({
                                     id: data.record.id
                                     });
                                }
                            },
                            {
                                text: l("Delete"),
                                visible: abp.auth.isGranted('Fhir.EsmMembers.Delete'),
                                confirmMessage: function () {
                                    return l("DeleteConfirmationMessage");
                                },
                                action: function (data) {
                                    esmMemberService.delete(data.record.id)
                                        .then(function () {
                                            abp.notify.info(l("SuccessfullyDeleted"));
                                            dataTable.ajax.reload();
                                        });
                                }
                            }
                        ]
                }
            },
			{ data: "mCPMember_ID" },
			{ data: "lastName" },
			{ data: "firstName" },
			{ data: "mIddleName" },
			{ data: "suffix" },
			{ data: "preFix" },
            {
                data: "dob",
                render: function (dob) {
                    if (!dob) {
                        return "";
                    }
                    
					var date = Date.parse(dob);
                    return (new Date(date)).toLocaleDateString(abp.localization.currentCulture.name);
                }
            },
            {
                data: "gender",
                render: function (gender) {
                    if (gender === undefined ||
                        gender === null) {
                        return "";
                    }

                    var localizationKey = "Enum:Gender:" + gender;
                    var localized = l(localizationKey);

                    if (localized === localizationKey) {
                        abp.log.warn("No localization found for " + localizationKey);
                        return "";
                    }

                    return localized;
                }
            },
            {
                data: "dateofDeath",
                render: function (dateofDeath) {
                    if (!dateofDeath) {
                        return "";
                    }
                    
					var date = Date.parse(dateofDeath);
                    return (new Date(date)).toLocaleDateString(abp.localization.currentCulture.name);
                }
            },
            {
                data: "race",
                render: function (race) {
                    if (race === undefined ||
                        race === null) {
                        return "";
                    }

                    var localizationKey = "Enum:Race:" + race;
                    var localized = l(localizationKey);

                    if (localized === localizationKey) {
                        abp.log.warn("No localization found for " + localizationKey);
                        return "";
                    }

                    return localized;
                }
            }
        ]
    }));

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $("#NewEsmMemberButton").click(function (e) {
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
