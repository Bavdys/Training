var exampleModal = document.querySelector('#trainerInformationModal')

$(document).on('show.bs.modal', exampleModal, function (event) {
    var button = event.relatedTarget
    var id = button.getAttribute('data-bs-id')

    GetDataFromServer(`https://${window.location.host}/api/trainers/${id}`).then(response => {
        exampleModal.querySelector('.modal-title').textContent = response.fullName
        exampleModal.querySelector('#photo').src = "data:image/jpeg;base64," + response.photo
        exampleModal.querySelector('#description').textContent = response.description
    })
})