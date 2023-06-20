const selectedReviewTypesInput = document.querySelector('#selectedReviewTypes');
const allReviewTypesCheckBox = document.querySelector('#reviewTypesDiv>.form-check>#all');
const reviewTypesCheckboxes = document.querySelectorAll('#reviewTypesDiv>.form-check>.review-types');

const submitFormBtn = document.querySelector('#submitBtn');
const form = document.querySelector('#form');
console.log(reviewTypesCheckboxes.length);

window.addEventListener('load', function () {
    if (selectedReviewTypesInput.value === '') {
        allReviewTypesCheckBox.checked = true;
    } else {
        for (let checkbox of reviewTypesCheckboxes) {
            if (selectedReviewTypesInput.value.includes(checkbox.value)) {
                checkbox.checked = true;
            }
        }
    }
});

allReviewTypesCheckBox.addEventListener('change', function (e) {
    for (let checkbox of reviewTypesCheckboxes) {
        checkbox.checked = false;
    }
});

for (let checkbox of reviewTypesCheckboxes) {
    checkbox.addEventListener('change', function (e) {
        let currCheckBox = e.currentTarget;
        if (currCheckBox.checked) {
            if (allReviewTypesCheckBox.checked) {
                allReviewTypesCheckBox.checked = false;
            }
        }
    });
}

form.addEventListener('submit', function (e) {
    selectedReviewTypesInput.value = '';
    for (let checkbox of reviewTypesCheckboxes) {
        if (checkbox.checked) {
            selectedReviewTypesInput.value += `${checkbox.value};`;
        }
    }
});