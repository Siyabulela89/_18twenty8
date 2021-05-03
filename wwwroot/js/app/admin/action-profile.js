document.addEventListener('DOMContentLoaded', () => {
    // Action Buttons
    var btnApprove = document.querySelector("#btn-approve-profile");
    var btnDecline = document.querySelector("#btn-decline-profile");
    var btnCorrect = document.querySelector("#btn-correct-profile");
    var btnSaveProfileStatus = document.querySelector("#btn-save-profile-status-reason");
    var taProfileStatusReason = document.querySelector("#ta-profile-Status-reason");

    var action = "";
    // Stage Status
    var phase2 = document.querySelector("#big-sis-phase-2");


    btnApprove.addEventListener("click", approveProfileHandler);
    btnDecline.addEventListener("click", openProfileStatusReasonModalHandler);
    btnCorrect.addEventListener("click", openProfileStatusReasonModalHandler);
    btnSaveProfileStatus.addEventListener("click", saveProfileStatusReasonHandler);

    function openProfileStatusReasonModalHandler(e) {
        e.preventDefault();
        action = e.target.dataset.action
        $('#profileStatusReasonModal').modal('show');
    }

    function saveProfileStatusReasonHandler() {
        btnSaveProfileStatus.innerHTML = '<i class="fas fa-spinner fa-spin"></i> Busy...';
        if (action == "Declined")
            declineProfileHandler();

        if (action == "Sent for Correction")
            correctProfileHandler();
    }


    function approveProfileHandler(e) {
        e.preventDefault();
        var data = {};
        btnApprove.innerHTML = '<i class="fas fa-spinner fa-spin"></i> Busy...';
        data.action = btnApprove.getAttribute('data-action');
        data.userId = btnApprove.getAttribute('data-user-id');
        data.role = btnApprove.getAttribute('data-role');
        submitData(data);
    }

    function declineProfileHandler() {
        var data = {};
        data.action = btnDecline.getAttribute('data-action');
        data.userId = btnDecline.getAttribute('data-user-id');
        data.role = btnDecline.getAttribute('data-role');
        data.reason = taProfileStatusReason.value;
        submitData(data);
    }

    function correctProfileHandler() {
        var data = {};
        data.action = btnCorrect.getAttribute('data-action');
        data.userId = btnCorrect.getAttribute('data-user-id');
        data.role = btnCorrect.getAttribute('data-role');
        data.reason = taProfileStatusReason.value;
        submitData(data);
    }

    function submitData(data = {}) {
        const options = {
            method: 'POST',
            body: JSON.stringify(data),
            headers: {
                'Content-Type': 'application/json'
            }
        }

        fetch(`${window.location.origin}/Administration/ActionProfile`, options)
            .then((response) => {
                return response.json();
            })
            .then((response) => {
                if (data.action == "Approved") {
                    btnApprove.innerHTML = '<i class="fas fa-thumbs-up"></i> Approve';
                    phase2.innerHTML = `<i class="fas fa-check"></i> ${data.action}`
                    btnApprove.disabled = true;
                    btnApprove.style.cursor = 'default';
                }

                if (data.action == "Sent for Correction") {
                    phase2.innerHTML = `<i class="fas fa-exclamation"></i> ${data.action}`
                    btnCorrect.disabled = true;
                    btnCorrect.style.cursor = 'default';
                }

                if (data.action == "Declined") {
                    phase2.innerHTML = `<i class="fas fa-times"></i> ${data.action}`
                    btnDecline.disabled = true;
                    btnDecline.style.cursor = 'default';
                }
                btnSaveProfileStatus.innerHTML = '<i class="fas fa-save"></i> Save';
                $('#profileStatusReasonModal').modal('hide');
                toastr.success(`${data.action} successfully`, 'Success Message')
            });
    }
    $('#profileStatusReasonModal').on('hidden.bs.modal', function () {
        taProfileStatusReason.value = "";
    });
});