const userCredits = document.querySelector('#userCredits');
const vehicleImg = document.querySelector('#vehicleImg');
const vehicleImgUrlInput = document.querySelector('#vehicleImgUrlInput');
const price = document.querySelector('#price');

const vehicleTypeSelect = document.querySelector('#vehicleType');

const imageErrorHeader = document.querySelector('#imageErrorHeader');
const urlRegex = /(www|http:|https:)+[^\s]+[\w*]/g;
let defaultVehicleImgSrc = `/img/VehicleTypes/${vehicleTypeSelect.value}.png`;
const submitBtn = document.querySelector('#submitBtn');

window.addEventListener('load', function (e) {
    let imageUrlInput = vehicleImgUrlInput;
    if (imageUrlInput.value.match(urlRegex)) {
        let request = new XMLHttpRequest();
        request.open('GET', imageUrlInput.value, true);
        request.onreadystatechange = function () {
            if (request.readyState === 4) {

                if (request.status !== 200) {
                    imageErrorHeader.classList.remove('d-none');
                    vehicleImg.classList.add('d-none');
                } else {
                    imageErrorHeader.classList.add('d-none');
                    vehicleImg.classList.remove('d-none');
                    vehicleImg.src = imageUrlInput.value;
                }
            }
        };
        request.send();
    } else {
        vehicleImg.src = defaultVehicleImgSrc;
    }
});


price.addEventListener('change', function () {
    let priceCalc = parseFloat(price.value) * 0.9;

    if (parseFloat(userCredits.textContent) >= priceCalc) {
        submitBtn.disabled = false;
    } else {
        submitBtn.disabled = true;
    }
});

vehicleImgUrlInput.addEventListener('input', function (e) {
    let imageUrlInput = e.currentTarget;
    if (imageUrlInput.value.match(urlRegex) && imageUrlInput.value.length > 0) {
        let request = new XMLHttpRequest();
        request.open('GET', imageUrlInput.value, true);
        request.onreadystatechange = function () {
            if (request.readyState === 4) {
                if (request.status !== 200) {
                    imageErrorHeader.classList.remove('d-none');
                    vehicleImg.classList.add('d-none');
                } else {
                    imageErrorHeader.classList.add('d-none');
                    vehicleImg.classList.remove('d-none');
                    vehicleImg.src = imageUrlInput.value;
                }
            }
        };
        request.send();
    } else {
        imageErrorHeader.classList.add('d-none');
        vehicleImg.classList.remove('d-none');
        vehicleImg.src = defaultVehicleImgSrc;
    }
});

vehicleTypeSelect.addEventListener('change', function (e) {
    defaultVehicleImgSrc = `/img/VehicleTypes/${e.currentTarget.value}.png`;
    if (vehicleImgUrlInput.value.length === 0) {
        vehicleImg.src = defaultVehicleImgSrc;
    }
})