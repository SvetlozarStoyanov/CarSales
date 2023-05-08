const userCredits = document.querySelector('#userCredits');

const price = document.querySelector('#price');

const submitBtn = document.querySelector('#submitBtn');

price.addEventListener('change', function () {
    let priceCalc = parseFloat(price.value) * 0.9;
    console.log(priceCalc);
    if (parseFloat(userCredits.textContent) >= priceCalc) {
        submitBtn.disabled = false;
    } else {
        submitBtn.disabled = true;
    }
});