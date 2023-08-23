const formArticle = document.querySelector('#formArticle');
const previewArticle = document.querySelector('#previewArticle');

const createForm = document.querySelector('#createForm');

const submitButton = document.querySelector('#submitBtn');
const previewButton = document.querySelector('#previewButton');

const previewOverviewParagraph = document.querySelector('#previewOverview');
const previewTitleHeading = document.querySelector('#previewTitle');
const previewPerformanceParagraph = document.querySelector('#previewPerformance');
const previewInteriorParagraph = document.querySelector('#previewInterior');
const previewLongevityParagraph = document.querySelector('#previewLongevity');
const previewFeaturesParagraph = document.querySelector('#previewFeatures');

const ratingStarIcons = document.querySelectorAll('.rating-icon');

previewButton.addEventListener('click', async function (e) {
    let currBtn = e.currentTarget;
    currBtn.blur();
    e.preventDefault();
    formArticle.classList.toggle('d-none');
    previewArticle.classList.toggle('d-none');
    if (formArticle.classList.contains('d-none')) {
        currBtn.classList.toggle('btn-warning');
        currBtn.classList.toggle('btn-danger');
        currBtn.textContent = 'Back';
        let formData = collectData(createForm);
        let data = new URLSearchParams(formData);
        await fetch(`/Reviewer/Reviews/CreatePreviewModel`, {
            method: 'POST',
            body: data
        })
            .then((res) => res.json())
            .then((responseObj) => {
                previewTitleHeading.textContent = responseObj['title'];
                previewOverviewParagraph.textContent = responseObj['overview'];
                previewPerformanceParagraph.textContent = responseObj['performance'];
                if (previewInteriorParagraph !== null) {
                    previewInteriorParagraph.textContent = responseObj['interior'];
                }
                if (previewLongevityParagraph !== null) {
                    previewLongevityParagraph.textContent = responseObj['longevity'];
                }
                if (previewFeaturesParagraph !== null) {
                    previewFeaturesParagraph.textContent = responseObj['features'];
                }
                let rating = parseInt(responseObj['vehicleRatingAsInt']);
                for (var i = 0; i < rating; i++) {
                    ratingStarIcons[i].classList.toggle('bi-star');
                    ratingStarIcons[i].classList.toggle('bi-star-fill');
                }
            });
    } else {
        currBtn.classList.toggle('btn-warning');
        currBtn.classList.toggle('btn-danger');
        currBtn.textContent = 'Preview';
        ratingStarIcons.forEach(icon => {
            if (icon.classList.contains('bi-star-fill')) {
                icon.classList.toggle('bi-star');
                icon.classList.toggle('bi-star-fill');
            }
        })
    }
});



function collectData(currentForm) {
    const data = new FormData(currentForm);
    return data;
}


submitButton.addEventListener('click', function () {
    createForm.submit();
});

