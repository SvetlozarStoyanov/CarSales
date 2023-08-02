const availableCredits = parseFloat(document.querySelector('#availableCredits').value);

const offeredPrice = document.querySelector('#offeredPrice');

const submitBtn = document.querySelector('#submitBtn');
const cancelBtn = document.querySelector('#cancelBtn');
const returnUrlInput = document.querySelector('#returnUrl');

window.addEventListener('load', function () {
    let priceParsed = parseFloat(offeredPrice.value);
    if (availableCredits >= priceParsed && priceParsed >= 0) {
        submitBtn.disabled = false;
    } else {
        submitBtn.disabled = true;
    }
    returnUrlInput.value = window.location.href;
});

offeredPrice.addEventListener('input', function () {
    let priceParsed = parseFloat(offeredPrice.value);
    if (availableCredits >= priceParsed && priceParsed >= 0) {
        submitBtn.disabled = false;
    } else {
        submitBtn.disabled = true;
    }
});

cancelBtn.addEventListener('click', function (e) {
    e.preventDefault();
})