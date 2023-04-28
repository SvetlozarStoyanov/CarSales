const newRatingInput = document.querySelector('#newRating');
const currRatingHeading = document.querySelector('#currRating');
const starButtons = document.querySelectorAll('.rating-change-button');

const vehicleRating = ['Unreliable', 'Underwhelming', 'Average', 'Reliable', 'Good'];

for (const btn of starButtons) {
    btn.addEventListener('click', function (e) {
        e.preventDefault();
        newRatingInput.value = e.currentTarget.value;
        currRatingHeading.textContent = vehicleRating[parseInt(newRatingInput.value)];
        updateStarButtonsStatus(e.currentTarget.value);
    });
    btn.addEventListener('mouseover', function (e) {
        currRatingHeading.textContent = vehicleRating[parseInt(e.currentTarget.value)];
        updateStarButtonsStatus(e.currentTarget.value);
    });
    btn.addEventListener('mouseleave', function (e) {
        currRatingHeading.textContent = vehicleRating[parseInt(newRatingInput.value)];
        updateStarButtonsStatus(newRating.value);
    });
}

function updateStarButtonsStatus(value) {
    const buttonIcons = document.querySelectorAll('.rating-change-button>.bi');
    const selectedButtonIcons = Array.from(buttonIcons).slice(0, parseInt(value) + 1);
    const otherButtonIcons = Array.from(buttonIcons).slice(parseInt(value) + 1, buttonIcons.length)
    for (let icon of selectedButtonIcons) {
        icon.classList.add('bi-star-fill');
        icon.classList.remove('bi-star');
    }
    for (let icon of otherButtonIcons) {
        icon.classList.remove('bi-star-fill');
        icon.classList.add('bi-star');
    }
}