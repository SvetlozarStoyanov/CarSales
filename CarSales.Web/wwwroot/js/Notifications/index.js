const notificationsList = document.querySelector('#notificationsList');
let timer;
let allNotificationsLoaded = false;

window.addEventListener('load', async function () {
    await loadMoreNotifications();
    let height = window.innerHeight;
    if (height >= 1070) {
        while (!allNotificationsLoaded) {
            await loadMoreNotifications();
        }
    }
});

window.addEventListener('scroll', async function () {
    if (timer) {
        window.clearTimeout(timer);
    }
    timer = window.setTimeout(async function () {
        let windowHeight = $(window).height();;
        let windowScrollTop = $(window).scrollTop();
        let scrollTopAndHeight = windowHeight + windowScrollTop;
        let documentHeight = getDocHeight();
        if (scrollTopAndHeight >= documentHeight - 200 && !allNotificationsLoaded) {
            await loadMoreNotifications();
        }
    }, 100)

});

window.addEventListener('resize', async function (e) {
    let height = window.innerHeight;
    let test = 0;
    if (height >= 1070) {
        while (!allNotificationsLoaded) {
            await loadMoreNotifications();
        }
    }
})

async function loadMoreNotifications() {
    let skipped = notificationsList.querySelectorAll('.notification-list-item').length;
    await $.ajax({
        type: 'GET',
        url: `./Notifications/GetNotifications`,
        data: jQuery.param({ skipped: skipped }),
        success: async function (viewText) {
            let viewHtml = jQuery.parseHTML(viewText);
            let markAsReadButtons = $(viewHtml).find('.mark-as-read-button');
            if (markAsReadButtons.length > 0) {
                markAsReadButtons.each(function () {
                    $(this).on('click', markAsReadButtonClick);
                });
            }

            await $(notificationsList).append(viewHtml);
            let currentNotificationCount = notificationsList.querySelectorAll('.notification-list-item').length;
            if (currentNotificationCount === 0) {
                allNotificationsLoaded = true;
                let noNotificationsHeading = notificationsList.querySelector('#noNotificationsHeading');
                noNotificationsHeading.classList.remove('d-none');
                return;
            }
            if (currentNotificationCount === skipped) {
                allNotificationsLoaded = true;
                return;
            }
        },
        failure: function () {
            console.log('Notifications get request failed')
        }
    });
}

async function markAsReadButtonClick(e) {
    let currBtn = e.currentTarget;
    if (!currBtn.disabled) {
        let btnParent = currBtn.parentNode;
        let notificationId = btnParent.querySelector('.notification-id').value;
        await fetch(`./Notifications/MarkAsRead/${notificationId}`);

        let btnIcon = currBtn.querySelector('i');
        btnIcon.classList.remove('bi-toggle-on');
        btnIcon.classList.remove('text-success');
        btnIcon.classList.add('bi-toggle-off');
        currBtn.disabled = true;
        btnParent.classList.remove('bg-info');
        btnParent.classList.remove('bg-opacity-50');
        btnParent.classList.add('bg-secondary');
        btnParent.classList.add('bg-opacity-10');
    }
}

function getDocHeight() {
    var D = document;
    return Math.max(
        D.body.scrollHeight, D.documentElement.scrollHeight,
        D.body.offsetHeight, D.documentElement.offsetHeight,
        D.body.clientHeight, D.documentElement.clientHeight
    );
}
