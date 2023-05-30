const userCredits = parseFloat(document.querySelector('#userCredits').textContent);

const price = document.querySelector('#price');

const submitBtn = document.querySelector('#submitBtn');

price.addEventListener('change', function () {
    let priceParsed = parseFloat(price.value);
    if (userCredits >= priceParsed && priceParsed >= 0) {
        submitBtn.disabled = false;
    } else {
        submitBtn.disabled = true;
    }
});