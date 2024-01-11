const reviewImageAnchorTags = document.querySelectorAll('.custom-review-image-anchor');



window.addEventListener('load', function (e) {

    //reviewImageAnchorTags.forEach(achorTag => achorTag.addEventListener('mouseenter', function (e) {
    //    const backgroundDiv = e.currentTarget.querySelector('.custom-review-image-background-div');
    //    backgroundDiv.classList.add('custom-review-image-background-div-show');
    //}));

    //reviewImageAnchorTags.forEach(achorTag => achorTag.addEventListener('mouseleave', function (e) {
    //    const backgroundDiv = e.currentTarget.querySelector('.custom-review-image-background-div');
    //    backgroundDiv.classList.remove('custom-review-image-background-div-show');
    //}));

    reviewImageAnchorTags.forEach(anchorTag => {
        anchorTag.addEventListener('mouseenter', function (e) {
            const backgroundDiv = e.currentTarget.querySelector('.custom-review-image-background-div');
            backgroundDiv.classList.add('custom-review-image-background-div-show');
        });
        anchorTag.addEventListener('mouseleave', function (e) {
            const backgroundDiv = e.currentTarget.querySelector('.custom-review-image-background-div');
            backgroundDiv.classList.remove('custom-review-image-background-div-show');
        })
        setTimeout(() => {
            anchorTag.dispatchEvent(new Event('mouseenter'))
        }, 1500);

        setTimeout(() => {
            anchorTag.dispatchEvent(new Event('mouseleave'))
        }, 5000);
    });

})

