const availableCredits = parseFloat(document.querySelector('#availableCredits').value);

const price = parseFloat(document.querySelector('#price').value);

const buyBtn = document.querySelector('#buyBtn');


if (availableCredits < price) {
    buyBtn.classList.add('disabled');
} else {
    buyBtn.classList.remove('disabled');
}