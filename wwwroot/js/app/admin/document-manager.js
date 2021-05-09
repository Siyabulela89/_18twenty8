document.addEventListener('DOMContentLoaded', () => {
    var currentUrl = window.location.href;
    var userId = currentUrl.split("/").pop();
    var urlSegment = currentUrl.split("/");
    var sisterDocTableBody = document.querySelector("#tbl-sister-documents");
   
    function getDocumentHandler() {
        var data = {};
        data.userId = userId;

        var page = urlSegment[urlSegment.length - 2];
        if (page === 'AdminStatusBigsis') {
            data.sisterType = 'Big';
        } else if (page === 'AdminStatusLilsis' || page === 'AdminStatusLilSis') {
            data.sisterType = 'Little';
        } else {
            page = urlSegment[urlSegment.length - 3];
            if (page === 'BigSisterDetails') {
                data.sisterType = 'Big';
            } else if (page === 'LittleSisterDetails') {
                data.sisterType = 'Little';
            }
        }

        getData(data);
    }

    function getDocumentDirectory(docName) {

        if (docName.substring(0, 2).toLowerCase() === "cv")
            return "CV";

        if (docName.substring(0, 3).toLowerCase() === "cid")
            return "CertifiedID";

        if (docName.substring(0, 4).toLowerCase() === "qual")
            return "Qualifications";

    }

    function getData(data = {}) {

        const options = {
            method: 'POST',
            body: JSON.stringify(data),
            headers: {
                'Content-Type': 'application/json'
            }
        }
        sisterDocTableBody.innerHTML = '<tr><td colspan="4" style="text-align:center"><i class="fas fa-spinner fa-spin"></i> Busy...</td></tr>';
        fetch(`${window.location.origin}/Administration/GetSisterProfile`, options)
            .then((response) => {
                return response.json();
            })
            .then((response) => {
                renderDocTable(response);
            });
    }

    function renderDocTable(response) {
        var tableRow = "";
        tableRow += `<tr>
                <th scope="row">1</th>
                <td>Curriculum Vitae (CV)</td>
                <td>${response.profile.dateCreated}</td>
                <td><a onclick="return false" data-doc-url="${response.profile.cvUrl}" class="btn-view-document" href="#"><i class="fas fa-external-link-alt"></i></a>&nbsp;&nbsp;<a onclick="return false" class="btn-delete-document" href="#"><i class="fas fa-trash"></i></a></td>
            </tr>`;

        tableRow += `<tr>
                <th scope="row">2</th>
                <td>Certified Identity Document (ID)</td>
                <td>${response.profile.dateCreated}</td>
                <td><a onclick="return false" data-doc-url="${response.profile.certifiedID}" class="btn-view-document" href="#"><i class="fas fa-external-link-alt"></i></a>&nbsp;&nbsp;<a onclick="return false" data-doc-url="${response.profile.certifiedID}" class="btn-delete-document" href="#"><i class="fas fa-trash"></i></a></td>
            </tr>`;

        var startIndex = 2;
        response.academicRecords.forEach((item) => {
            startIndex++;
            var qualificationName = item.qualificationDocname.length > 60 ? `${item.qualificationDocname.substring(0, 57)}...` : item.qualificationDocname;
            tableRow += `<tr>
                <th scope="row">${startIndex}</th>
                <td>${qualificationName}</td>
                <td>${item.dateCreated}</td>
                <td><a onclick="return false" data-doc-url="${item.qualificationurl}" class="btn-view-document" href="#"><i class="fas fa-external-link-alt"></i></a>&nbsp;&nbsp;<a onclick="return false" data-doc-url="${item.qualificationurl}" class="btn-delete-document" href="#"><i class="fas fa-trash"></i></a></td>
            </tr>`;
        });

        sisterDocTableBody.innerHTML = tableRow;
    }

    getDocumentHandler();

    document.body.addEventListener('click', (e) => {
        if (e.target.parentElement.classList.contains('btn-view-document')) {
            var docName = e.target.parentElement.getAttribute('data-doc-url');
            var dir = getDocumentDirectory(docName);
            document.querySelector("#documentViewer").src = `${window.location.origin}/Uploads/${dir}/${docName}`;
            $('#viewDocumentModal').modal('show');
        }

        if (e.target.parentElement.classList.contains('btn-delete-document')) {
            alert('Deleted');
        }
    });

    $('#viewDocumentModal').on('hidden.bs.modal', function () {
        $('#viewDocumentModal').modal('dispose');
    });
});