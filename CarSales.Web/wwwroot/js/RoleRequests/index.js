const selectedRoleNamesInput = document.querySelector('#selectedRoleNames');

const allRoleNamesCheckBox = document.querySelector('#roleNamesDiv>.form-check>#allRoleNames');

const roleNamesCheckboxes = document.querySelectorAll('#roleNamesDiv>.form-check>.role-names');

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
    if (selectedRoleNamesInput.value === '') {
        allRoleNamesCheckBox.checked = true;
    } else {
        for (let checkbox of roleNamesCheckboxes) {
            if (selectedRoleNamesInput.value.includes(checkbox.value)) {
                checkbox.checked = true;
            }
        }
    }
});

allRoleNamesCheckBox.addEventListener('change', function (e) {
    for (let checkbox of roleNamesCheckboxes) {
        checkbox.checked = false;
    }
});


for (let checkbox of roleNamesCheckboxes) {
    checkbox.addEventListener('change', function (e) {
        let currCheckBox = e.currentTarget;
        if (currCheckBox.checked) {
            if (allRoleNamesCheckBox.checked) {
                allRoleNamesCheckBox.checked = false;
            }
        }
    });
}


form.addEventListener('submit', function (e) {

    selectedRoleNamesInput.value = '';
    for (let checkbox of roleNamesCheckboxes) {
        if (checkbox.checked) {
            selectedRoleNamesInput.value += `${checkbox.value};`;
        }
    }

});