const formInputs = document.querySelectorAll('.form-control');
const submitBtn = document.querySelector('button[type="submit"]');
const profilePicture = document.querySelector('#profilePicture');
const defaultProfilePictureSrc = document.querySelector('#defaultProfilePicture');
const form = document.querySelector('form');

const imageErrorHeader = document.querySelector('#imageErrorHeader');
const urlRegex = new RegExp("(www|http:|https:)+[^\s]+[\w]");



for (const input of formInputs) {
    input.addEventListener('input', function (e) {
        submitBtn.disabled = false;
        if (e.currentTarget.id === 'imageUrl') {
            let imageUrlInput = e.currentTarget;
            if (imageUrlInput.value.match(urlRegex)) {
                let request = new XMLHttpRequest();
                request.open('GET', imageUrlInput.value, true);
                request.onreadystatechange = function () {
                    if (request.readyState === 4) {
                        if (request.status !== 200) {
                            imageErrorHeader.classList.remove('d-none');
                            profilePicture.classList.add('d-none');
                        } else {
                            imageErrorHeader.classList.add('d-none');
                            profilePicture.classList.remove('d-none');
                            profilePicture.src = imageUrlInput.value;
                        }
                    }
                };
                request.send();
            } else {
                profilePicture.src = defaultProfilePictureSrc.value;
            }

        }
    })
}

form.addEventListener('submit', async function (e) {
    e.preventDefault();
    console.log(e.currentTarget);
    const imageUrlInput = document.querySelector('#imageUrl');
    let request = new XMLHttpRequest();
    await request.open('GET', imageUrlInput.value, true);
    request.onreadystatechange = async function () {
        if (request.readyState === 4) {
            if (request.status !== 200) {
                imageUrlInput.value = null;
            }
            
            await form.submit();
        }

    };
    await request.send();
    /*e.currentTarget.submit();*/
})