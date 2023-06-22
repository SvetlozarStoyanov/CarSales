﻿const selectedVehicleTypesInput = document.querySelector('#selectedVehicleTypes');
const selectedReviewTypesInput = document.querySelector('#selectedReviewTypes');

const allVehicleTypesCheckBox = document.querySelector('#vehicleTypesDiv>.form-check>#allVehicleTypes');
const allReviewTypesCheckBox = document.querySelector('#reviewTypesDiv>.form-check>#allReviewTypes');

const vehicleTypesCheckboxes = document.querySelectorAll('#vehicleTypesDiv>.form-check>.vehicle-types');
const reviewTypesCheckboxes = document.querySelectorAll('#reviewTypesDiv>.form-check>.review-types');

const submitFormBtn = document.querySelector('#submitBtn');
const form = document.querySelector('#form');

console.log(vehicleTypesCheckboxes.length);
console.log(reviewTypesCheckboxes.length);

window.addEventListener('load', function () {
    if (selectedVehicleTypesInput.value === '') {
        allVehicleTypesCheckBox.checked = true;
    } else {
        for (let checkbox of vehicleTypesCheckboxes) {
            if (selectedVehicleTypesInput.value.includes(checkbox.value)) {
                checkbox.checked = true;
            }
        }
    }
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

allVehicleTypesCheckBox.addEventListener('change', function (e) {
    for (let checkbox of vehicleTypesCheckboxes) {
        checkbox.checked = false;
    }
});

allReviewTypesCheckBox.addEventListener('change', function (e) {
    for (let checkbox of reviewTypesCheckboxes) {
        checkbox.checked = false;
    }
});

for (let checkbox of vehicleTypesCheckboxes) {
    checkbox.addEventListener('change', function (e) {
        let currCheckBox = e.currentTarget;
        if (currCheckBox.checked) {
            if (allVehicleTypesCheckBox.checked) {
                allVehicleTypesCheckBox.checked = false;
            }
        }
    });
}

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

    selectedVehicleTypesInput.value = '';
    for (let checkbox of vehicleTypesCheckboxes) {
        if (checkbox.checked) {
            selectedVehicleTypesInput.value += `${checkbox.value};`;
        }
    }

    selectedReviewTypesInput.value = '';
    for (let checkbox of reviewTypesCheckboxes) {
        if (checkbox.checked) {
            selectedReviewTypesInput.value += `${checkbox.value};`;
        }
    }
});