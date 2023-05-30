const userCredits = parseFloat(document.querySelector('#userCredits').textContent);

const price = parseFloat(document.querySelector('#price').value);

const buyBtn = document.querySelector('#buyBtn');


if (userCredits < price) {
    buyBtn.disabled = true;
} else {
    buyBtn.disabled = false;
}