const priceValidationSpan = document.querySelector('#priceValidation');

const newPrice = document.querySelector('#newPrice');
const oldPrice = document.querySelector('#oldPrice');
const submitBtn = document.querySelector('#submitBtn');
const cancelBtn = document.querySelector('#cancelBtn');

newPrice.addEventListener('input', function () {
    console.log('price changed');
    if (parseFloat(newPrice.value) > parseFloat(oldPrice.value)) {
        submitBtn.disabled = true;
        priceValidationSpan.textContent = `Price cannot be more than the original price (${oldPrice.value})!`;
    } else {
        submitBtn.disabled = false;
        priceValidationSpan.textContent = '';
    }
});

cancelBtn.addEventListener('click', function (e) {
    e.preventDefault();
})