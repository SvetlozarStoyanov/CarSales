const priceValidationSpan = document.querySelector('#priceValidation');

const newPrice = document.querySelector('#price');
const oldPrice = document.querySelector('#oldPrice');
const submitBtn = document.querySelector('#submitBtn');

newPrice.addEventListener('change', function () {
    if (parseFloat(newPrice.value) > parseFloat(oldPrice.value)) {
        submitBtn.disabled = true;
        priceValidationSpan.textContent = `Price cannot be more than the original price (${oldPrice.value})!`;
    } else {
        submitBtn.disabled = false;
        priceValidationSpan.textContent = '';
    }
});