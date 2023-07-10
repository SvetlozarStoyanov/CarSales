const formInputs = document.querySelectorAll('.form-control');
const submitBtn = document.querySelector('button[type="submit"]');
const profilePicture = document.querySelector('#profilePicture');
const defaultProfilePictureSrc = document.querySelector('#defaultProfilePicture');
const urlRegex = new RegExp("(www|http:|https:)+[^\s]+[\w]");

console.log(formInputs.length);
for (const input of formInputs) {
    input.addEventListener('input', function (e) {
        submitBtn.disabled = false;
        if (e.currentTarget.id === 'imageUrl') {
            let imageUrlInput = e.currentTarget;
            if (imageUrlInput.value.match(urlRegex)) {
                profilePicture.src = e.currentTarget.value;
            } else {
                profilePicture.src = defaultProfilePictureSrc.value;
            }

        }
    })
}

//for (const btn of submitBtns) {
//    btn.addEventListener('click', function (e) {
//        e.preventDefault();
//        console.log('submit clicked!');
//    });
//}