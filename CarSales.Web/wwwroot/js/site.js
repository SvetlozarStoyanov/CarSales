// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


const navbar = document.querySelector('.navbar')
const body = document.querySelector('body');

navbarPositionChange();

window.addEventListener('resize', navbarPositionChange);

function navbarPositionChange() {
    const windowHeight = window.innerHeight;
    const windowWidth = window.innerWidth;


    if (windowHeight < 600) {
        if (navbar.classList.contains('fixed-top')) {
            navbar.classList.remove('fixed-top');
            body.classList.remove('pt-5');
            body.classList.remove('mt-4');
        }
    } else {
        if (!navbar.classList.contains('fixed-top')) {
            navbar.classList.add('fixed-top');
            body.classList.add('pt-5');
            body.classList.add('mt-4');
        }
    }
}