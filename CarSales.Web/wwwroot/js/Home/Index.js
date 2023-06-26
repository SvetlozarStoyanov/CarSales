const reviewContainerArticles = document.querySelectorAll('.review-container');
const highlightedReviewContainerArticle = document.querySelector('.review-hightlighted-container');


window.addEventListener('load', function () {
    highlightedReviewContainerArticle.addEventListener('mouseenter', function () {
        highlightedReviewContainerArticle.classList.add('border-primary');
        highlightedReviewContainerArticle.classList.remove('border-dark');
    });

    highlightedReviewContainerArticle.addEventListener('mouseleave', function () {
        highlightedReviewContainerArticle.classList.remove('border-primary');
        highlightedReviewContainerArticle.classList.add('border-dark');
    });
});


for (let i = 0; i < reviewContainerArticles.length; i++) {
    const currReviewContainer = reviewContainerArticles[i];
    if (i % 2 !== 0) {
        currReviewContainer.classList.add('flex-row-reverse');
    }

    currReviewContainer.addEventListener('mouseenter', function () {
        currReviewContainer.classList.add('border-primary');
        currReviewContainer.classList.remove('border-dark');
    });

    currReviewContainer.addEventListener('mouseleave', function () {
        currReviewContainer.classList.remove('border-primary');
        currReviewContainer.classList.add('border-dark');
    });
}


