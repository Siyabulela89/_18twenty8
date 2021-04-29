document.addEventListener('DOMContentLoaded', () => {

    var btnApprove = document.querySelector("#btn-approve-profile");
    var btnDecline = document.querySelector("#btn-decline-profile");
    var btnCorrect = document.querySelector("#btn-correct-profile");

    btnApprove.addEventListener("click", approveProfileHandler);
    btnDecline.addEventListener("click", declineProfileHandler);
    btnCorrect.addEventListener("click", correctProfileHandler);

    function approveProfileHandler(e) {
        e.preventDefault();
        var data = {};
        btnApprove.innerHTML = '<i class="fas fa-spinner fa-spin"></i> Busy...';
        data.action = btnApprove.getAttribute('data-action');
        data.userId = btnApprove.getAttribute('data-user-id');
        submitData(data);


    }

    function declineProfileHandler(e) {
        e.preventDefault();
        var data = {};
        btnDecline.innerHTML = '<i class="fas fa-spinner fa-spin"></i> Busy...';
        data.action = btnDecline.getAttribute('data-action');
        data.userId = btnDecline.getAttribute('data-user-id');
        submitData(data);
    }

    function correctProfileHandler(e) {
        e.preventDefault();
        btnCorrect.innerHTML = '<i class="fas fa-spinner fa-spin"></i> Busy...';
        var data = {};
        data.action = btnCorrect.getAttribute('data-action');
        data.userId = btnCorrect.getAttribute('data-user-id');
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
                console.log(response);
                if (data.action == "Approved")
                    btnApprove.innerHTML = '<i class="fas fa-thumbs-up"></i> Approve';
                if (data.action == "Sent for Correction")
                    btnCorrect.innerHTML = '<i class="fas fa-reply"></i> Send for correction';
                if (data.action == "Declined")
                    btnDecline.innerHTML = '<i class="fas fa-thumbs-down"></i> Decline';
            });
    }
});