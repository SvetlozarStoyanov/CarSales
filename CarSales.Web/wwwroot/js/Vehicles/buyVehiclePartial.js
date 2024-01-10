const cancelBuyBtn = document.querySelector('#cancelBuyBtn');
const creditsAfterBuyingInput = document.querySelector('#creditsAfterBuyingInput');
const buyPrice = parseFloat(document.querySelector('#buyPrice').value);
const userCurrentCredits = parseFloat(document.querySelector('#userCredits').textContent);
creditsAfterBuyingInput.value = (userCurrentCredits - buyPrice).toFixed(2);

//document.addEventListener('load', function () {
//});

cancelBuyBtn.addEventListener('click', function () {
    e.preventDefault();
})