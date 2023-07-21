const submitBtn = document.querySelector('button[type="submit"]');
const profilePicture = document.querySelector('#profilePicture');
const genderSelect = document.querySelector('#gender');
const form = document.querySelector('form');
const imageUrlInput = document.querySelector('#imageUrl');
const imageErrorHeader = document.querySelector('#imageErrorHeader');
const urlRegex = /(www|http:|https:)+[^\s]+[\w*]/g;
let defaultProfilePicture = `/img/Profile/${genderSelect.value}Profile.png`;

window.addEventListener('load', function (e) {
    profilePicture.src = defaultProfilePicture;
});

genderSelect.addEventListener('change', function (e) {
    defaultProfilePicture = `/img/Profile/${genderSelect.value}Profile.png`
    if (imageUrlInput.value.length === 0) {
        profilePicture.src = defaultProfilePicture;
    }
});

imageUrlInput.addEventListener('input', function (e) {
    let imageUrl = e.currentTarget.value;
    if (imageUrl.length === 0) {
        if (profilePicture.classList.contains('d-none')) {
            profilePicture.classList.remove('d-none');
            imageErrorHeader.classList.add('d-none');
        }
        profilePicture.src = defaultProfilePicture;
    } else if (imageUrl.match(urlRegex)) {
        let request = new XMLHttpRequest();
        request.open('GET', imageUrlInput.value, true);
        request.onreadystatechange = function () {
            if (request.readyState === 4) {
                console.log('image loaded');
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
        profilePicture.src = defaultProfilePicture;
    }
});

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
})