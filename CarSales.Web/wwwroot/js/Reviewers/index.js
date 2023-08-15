const filterBtn = document.querySelector('#filterBtn');
const filterIcon = filterBtn.querySelector('i');

const submitFormBtn = document.querySelector('#submitBtn');

filterBtn.addEventListener('click', function (e) {
    e.preventDefault();
    e.currentTarget.blur();
    filterIcon.classList.toggle('bi-filter-circle');
    filterIcon.classList.toggle('bi-filter-circle-fill');
});