const userCredits = document.querySelector('#userCredits');

const price = document.querySelector('#price');

const buyBtn = document.querySelector('#buyBtn');

if (parseFloat(userCredits.textContent) >= parseFloat(price.textContent)) {
    buyBtn.disabled = false;
} else {
    buyBtn.disabled = true;
}