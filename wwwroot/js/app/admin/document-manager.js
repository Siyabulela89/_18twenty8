document.addEventListener('DOMContentLoaded', () => {
    var currentUrl = window.location.href;
    var userId = currentUrl.split("/").pop();

    function getDocumentHandler() {
        var data = {};
        data.userId = userId;
        getData(data);
    }

    var sisterDocTableBody = document.querySelector("#tbl-sister-documents");
    function getData(data = {}) {
        sisterDocTableBody.innerHTML = '<tr><td colspan="4" style="text-align:center"><i class="fas fa-spinner fa-spin"></i> Busy...</td></tr>';
        const options = {
            method: 'POST',
            body: JSON.stringify(data),
            headers: {
                'Content-Type': 'application/json'
            }
        }

        fetch(`${window.location.origin}/Administration/GetBigSisterProfile`, options)
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
            tableRow += `<tr>
                <th scope="row">${startIndex}</th>
                <td>${item.qualificationDocname}</td>
                <td>${item.dateCreated}</td>
                <td><a onclick="return false" data-doc-url="${item.qualificationurl}" class="btn-view-document" href="#"><i class="fas fa-external-link-alt"></i></a>&nbsp;&nbsp;<a onclick="return false" data-doc-url="${item.qualificationurl}" class="btn-delete-document" href="#"><i class="fas fa-trash"></i></a></td>
            </tr>`;
        });

        sisterDocTableBody.innerHTML = tableRow;
    }
    getDocumentHandler();
});