const selectedReviewTypeIndexInput = document.querySelector('#selectedReviewTypeIndex');


const reviewPlanArticles = document.querySelectorAll('.review-plan');
const reviewPlanButtons = document.querySelectorAll('.review-plan>div>button');
let currSelectedPlanIndex = -1;

window.addEventListener('load', async function () {
    await setTimeout(() => {
        if (currSelectedPlanIndex === -1) {
            selectReviewPlan(1);
        }
    }, 600);
})


for (let btn of reviewPlanButtons) {
    btn.addEventListener('click', async function (e) {
        e.preventDefault();
        e.currentTarget.blur();

        const currReviewPlanArticle = e.currentTarget.parentNode.parentNode;
        let currReviewPlanArticleIndex = parseInt(currReviewPlanArticle.querySelector('input').value);
        if (currSelectedPlanIndex !== -1) {
            await resetOldReviewPlan(currSelectedPlanIndex);
        }
        await selectReviewPlan(currReviewPlanArticleIndex);

    });
}

async function selectReviewPlan(index) {
    currSelectedPlanIndex = index;
    selectedReviewTypeIndexInput.value = parseInt(currSelectedPlanIndex);

    const currSelectedReviewPlanArticle = reviewPlanArticles[currSelectedPlanIndex];
    const currSelectedReviewPlanArticleIcon = currSelectedReviewPlanArticle.querySelector('div>button>i');
    currSelectedReviewPlanArticleIcon.classList.remove('bi-dash-circle-fill');
    currSelectedReviewPlanArticleIcon.classList.add('bi-check-circle-fill');
    currSelectedReviewPlanArticle.classList.add('bg-success');
    currSelectedReviewPlanArticle.classList.remove('bg-info');
}

async function resetOldReviewPlan(index) {
    const currSelectedReviewPlanArticle = reviewPlanArticles[index];
    const currSelectedReviewPlanArticleIcon = currSelectedReviewPlanArticle.querySelector('div>button>i');
    currSelectedReviewPlanArticleIcon.classList.remove('bi-check-circle-fill');
    currSelectedReviewPlanArticleIcon.classList.add('bi-dash-circle-fill');
    currSelectedReviewPlanArticle.classList.remove('bg-success');
    currSelectedReviewPlanArticle.classList.add('bg-info');
}
