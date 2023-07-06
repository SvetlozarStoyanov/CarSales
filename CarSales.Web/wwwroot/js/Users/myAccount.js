const changeButtons = document.querySelectorAll('button[type="button"]');
const submitBtns = document.querySelectorAll('button[type="submit"]');



console.log(submitBtns.length);
for (const btn of changeButtons) {
    btn.addEventListener('click', function (e) {
        e.preventDefault();
        const parentDiv = e.currentTarget.parentElement;
        const input = parentDiv.querySelector('input');
        const submitBtn = parentDiv.querySelector('button[type="submit"]');
        e.currentTarget.classList.add('d-none');
        input.disabled = false;
        submitBtn.classList.remove('d-none');
    });
}

//for (const btn of submitBtns) {
//    btn.addEventListener('click', function (e) {
//        e.preventDefault();
//        console.log('submit clicked!');
//    });
//}