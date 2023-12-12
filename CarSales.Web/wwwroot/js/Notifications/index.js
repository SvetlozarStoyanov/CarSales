const notificationsSection = document.querySelector('#notificationsSection');
let allNotificationsLoaded = false;

window.addEventListener('load', async function () {
    await loadMoreNotifications();
});

window.addEventListener('scrollend', async function () {
    if (!allNotificationsLoaded) {
        await loadMoreNotifications();
    }
});

async function loadMoreNotifications() {
    let skipped = notificationsSection.querySelectorAll('article').length;

    $.ajax({
        type: 'GET',
        url: `./Notifications/GetNotifications`,
        data: jQuery.param({ skipped: skipped }),
        success: function (viewText) {
            let viewHtml = jQuery.parseHTML(viewText);
            let markAsReadButtons = $(viewHtml).find('.mark-as-read-button');

            if (markAsReadButtons.length > 0) {
                markAsReadButtons.each(function () {
                    $(this).on('click', markAsReadButtonClick);
                });
            }

            $(notificationsSection).append(viewHtml);
            let currentNotificationCount = notificationsSection.querySelectorAll('article').length;
            if (currentNotificationCount === 0) {
                allNotificationsLoaded = true;
                let noNotificationsHeading = notificationsSection.querySelector('#noNotificationsHeading');
                noNotificationsHeading.classList.remove('d-none');
                return;
            }
            if (currentNotificationCount === skipped) {
                allNotificationsLoaded = true;
                return;
            }
        }
    }
    );
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


