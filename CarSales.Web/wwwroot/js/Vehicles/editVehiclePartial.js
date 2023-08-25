﻿const priceValidationSpan = document.querySelector('#priceValidation');

const newPrice = document.querySelector('#newPrice');
const oldPrice = document.querySelector('#oldPrice');
const submitBtn = document.querySelector('#submitBtn');
const cancelBtn = document.querySelector('#cancelBtn');

const imageUrlInput = document.querySelector('#imageUrl');
const defaultVehicleImageSrc = document.querySelector('#defaultVehicleImage');
const vehiclePreviewImage = document.querySelector('#vehiclePreviewImage');
const imageErrorHeader = document.querySelector('#imageErrorHeader');
const urlRegex = /(www|http:|https:)+[^\s]+[\w*]/g;
const originalImageUrl = document.querySelector('#originalVehicleImage');

const form = document.querySelector('#editForm');

newPrice.addEventListener('change', function () {
    console.log('price changed');
    if (parseFloat(newPrice.value) > parseFloat(oldPrice.value)) {
        submitBtn.disabled = true;
        priceValidationSpan.textContent = `Price cannot be more than the original price (${oldPrice.value})!`;
    } else {
        submitBtn.disabled = false;
        priceValidationSpan.textContent = '';
    }
});

imageUrlInput.addEventListener('input', function (e) {
    let imageUrlInput = e.currentTarget;
    if (imageUrlInput.value.match(urlRegex)) {
        let request = new XMLHttpRequest();
        request.open('GET', imageUrlInput.value, true);
        request.onreadystatechange = function () {
            if (request.readyState === 4) {
                if (request.status !== 200) {
                    imageErrorHeader.classList.remove('d-none');
                    vehiclePreviewImage.classList.add('d-none');
                } else {
                    imageErrorHeader.classList.add('d-none');
                    vehiclePreviewImage.classList.remove('d-none');
                    vehiclePreviewImage.src = imageUrlInput.value;
                }
            }
        };
        request.send();
    } else {
        vehiclePreviewImage.src = defaultVehicleImageSrc.value;
    }
});

cancelBtn.addEventListener('click', function (e) {
    imageUrlInput.value = originalImageUrl.value;
    vehiclePreviewImage.src = originalImageUrl.value;
    imageErrorHeader.classList.add('d-none');
    vehiclePreviewImage.classList.remove('d-none');
    e.preventDefault();
});

form.addEventListener('submit', async function (e) {
    e.preventDefault();
    if (imageUrlInput.value.length > 0) {
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
    }
    //} else {
    //    e.currentTarget.submit();
    //}
});