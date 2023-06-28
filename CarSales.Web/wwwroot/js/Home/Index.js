const reviewContainerArticles = document.querySelectorAll('.review-container');
const highlightedReviewContainerArticle = document.querySelector('.review-hightlighted-container');



for (let i = 0; i < reviewContainerArticles.length; i++) {
    const currReviewContainer = reviewContainerArticles[i];
    if (i % 2 !== 0) {
        currReviewContainer.classList.add('flex-row-reverse');
    }
}


