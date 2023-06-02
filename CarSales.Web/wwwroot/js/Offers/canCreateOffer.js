const availableCredits = parseFloat(document.querySelector('#availableCredits').value);

const price = document.querySelector('#price');

const submitBtn = document.querySelector('#submitBtn');

window.addEventListener('load', function () {
    let priceParsed = parseFloat(price.value);
    if (availableCredits >= priceParsed && priceParsed >= 0) {
        submitBtn.disabled = false;
    } else {
        submitBtn.disabled = true;
    }
});

price.addEventListener('change', function () {
    let priceParsed = parseFloat(price.value);
    if (availableCredits >= priceParsed && priceParsed >= 0) {
        submitBtn.disabled = false;
    } else {
        submitBtn.disabled = true;
    }
});