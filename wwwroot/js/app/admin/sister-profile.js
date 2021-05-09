document.addEventListener('DOMContentLoaded', () => {
    var selectedLittleSister = "";
    var bigSisterAssignTableBody = document.querySelector("#tbl-big-sister-assign-body");
    var littleSisterProfileWrapper = document.querySelector("#mentee-profile-wrapper");


    function getData(data = {}) {

        const options = {
            method: 'POST',
            body: JSON.stringify(data),
            headers: {
                'Content-Type': 'application/json'
            }
        }
        bigSisterAssignTableBody.innerHTML = '<tr><td colspan="5" style="text-align:center"><i class="fas fa-spinner fa-spin"></i> Busy...</td></tr>';
        littleSisterProfileWrapper.innerHTML = '<span style="width:100px; margin-left:auto; margin-right: auto"><i class="fas fa-spinner fa-spin"></i> Busy...</span>';
        fetch(`${window.location.origin}/Administration/GetSisterAssignment`, options)
            .then((response) => {
                return response.json();
            })
            .then((response) => {
                renderMenteeProfile(response);
                renderMentorsTable(response);
            });
    }

    function renderMenteeProfile(response) {
        var menteeProfile = `<div class="col-md-2">
                                    <img src="/Uploads/ProimagesLilsis/${response.mentee.imageurl}" class="img-thumbnail" alt="...">
                                </div>
                                <div class="col-md-5" style="text-align:left">
                                    <p><b>Sister Type:</b> Little Sister</p>
                                    <p><b>Full Names:</b> ${response.mentee.name} ${response.mentee.surname}</p>
                                    <p><b>Nickname/Preferred name:</b> ${response.mentee.nickname}</p>
                                    <p><b>ID/Passport:</b> ${response.mentee.idPassport}</p>
                                    <p><b>Date of birth:</b> ${response.mentee.dateofBirth}</p>
                                </div>
                                <div class="col-md-5" style="text-align:left">
                                    <p><b>Email:</b> ${response.mentee.email}</p>
                                    <p><b>Contact number (Cell):</b> ${response.mentee.phonenumber}</p>
                                    <p><b>Physical Address:</b> ${response.mentee.addressStreet} ${response.mentee.addressStreetlinetwo}</p>
                                    <p><b>Postal Code:</b> ${response.mentee.postalCode}</p>
                                    <p><a class="btn-view-sister-profile" data-sister-type="little" data-sister-id="${response.mentee.userID}" href="#" onclick="return false">Click to view profile &nbsp;<i class="fas fa-external-link-alt"></i></a></p>
                                </div>`;

        littleSisterProfileWrapper.innerHTML = menteeProfile;

    }

    function renderMentorsTable(response) {
        $('#tbl-big-sister-assign').DataTable({
            "bDestroy": true,
            "data": response.mentors,
            "columns": [
                {
                    "data": null,
                    render: function (data, type, full, meta) {
                        return meta.row + meta.settings._iDisplayStart + 1;
                    }

                },
                {
                    "data": null,
                    render: function (data, type, full, meta) {
                        return `${full.name} ${full.surname}`;
                    }
                },
                { "data": "phonenumber" },
                { "data": "email" },
                {
                    "data": null,
                    render: function (data, type, full, meta) {
                        return `<a class="btn-view-sister-profile" data-sister-type="big" data-sister-id="${full.userID}" onclick="return false" href="#"><i class="fas fa-user"></i>&nbsp;Profile</a> &nbsp;&nbsp; <a class="btn-assign-big-sister" data-big-sister-id="${full.userID}" onclick="return false" href="#"><i class="fas fa-link"></i>&nbsp;Assign</a>`;
                    }
                }
            ]
        });
    }

    function loadAssignmentScreen() {
        var data = {};
        data.userId = selectedLittleSister;
        getData(data);
    }


    var sisterprofileViewer = document.querySelector("#sisterprofileViewer");
    document.body.addEventListener('click', (e) => {
        if (e.target.parentElement.classList.contains('btn-assign-sister')) {
            selectedLittleSister = e.target.parentElement.getAttribute('data-little-sister-id');
            loadAssignmentScreen();
            $('#sisterAssignModal').modal('show');
        }

        if (e.target.className === 'btn-assign-big-sister') {
            var assignData = {};
            assignData.littleSisterUserId = selectedLittleSister;
            assignData.bigSisterUserId = e.target.getAttribute("data-big-sister-id");
            const options = {
                method: 'POST',
                body: JSON.stringify(assignData),
                headers: {
                    'Content-Type': 'application/json'
                }
            }
            fetch(`${window.location.origin}/Administration/AssignSister`, options)
                .then((response) => {
                    return response.json();
                })
                .then((response) => {
                    toastr.success(`Sister assigned successfully`, 'Success Message')
                });
        }

        if (e.target.className === 'btn-view-sister-profile') {
            var sisterType = e.target.getAttribute('data-sister-type');
            var sisterId = e.target.getAttribute('data-sister-id');
            var src = "";
            console.log('eee', e);
            if (sisterType === "little") {
                src = `${window.location.origin}/LittleSisterDetails/LittleSisterProfile/${sisterId}`;
            }

            if (sisterType === "big") {
                src = `${window.location.origin}/BigSisterDetails/BigSisterProfile/${sisterId}`;
            }

            sisterprofileViewer.src = src;
            $('#sisterprofileViewModal').modal('show');
        }
    });

    //$('#viewDocumentModal').on('hidden.bs.modal', function () {
    //    $('#viewDocumentModal').modal('dispose');
    //});

});