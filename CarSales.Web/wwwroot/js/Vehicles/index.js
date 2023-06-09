﻿const selectedVehicleTypesInput = document.querySelector('#selectedVehicleTypes');

const allVehicleTypesCheckBox = document.querySelector('#vehicleTypesDiv>.form-check>#allVehicleTypes');

const vehicleTypesCheckboxes = document.querySelectorAll('#vehicleTypesDiv>.form-check>.vehicle-types');

const filterBtn = document.querySelector('#filterBtn');
const filterIcon = filterBtn.querySelector('i');

const submitFormBtn = document.querySelector('#submitBtn');
const form = document.querySelector('#form');

filterBtn.addEventListener('click', function (e) {
    e.preventDefault();
    e.currentTarget.blur();
    filterIcon.classList.toggle('bi-filter-circle');
    filterIcon.classList.toggle('bi-filter-circle-fill');
});

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

});

allVehicleTypesCheckBox.addEventListener('change', function (e) {
    for (let checkbox of vehicleTypesCheckboxes) {
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


form.addEventListener('submit', function (e) {

    selectedVehicleTypesInput.value = '';
    for (let checkbox of vehicleTypesCheckboxes) {
        if (checkbox.checked) {
            selectedVehicleTypesInput.value += `${checkbox.value};`;
        }
    }

});