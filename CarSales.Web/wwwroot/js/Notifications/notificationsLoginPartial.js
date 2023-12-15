const areaInput = document.querySelector('#areaInput');
const notificationsDropdownIcon = document.querySelector('#notificationsDropdownIcon');
const notificationsDropdownButton = document.querySelector('#notificationsDropdownButton');
const notificationsDropdownListItem = document.querySelector('#notificationsDropdownListItem');
const notificationsDropdownList = document.querySelector('#notificationsDropdownList');

window.addEventListener('load', async function () {
    await checkIfUserHasUnreadNotifications();

    setInterval(checkIfUserHasUnreadNotifications, 30000)
})

window.addEventListener('click', function (e) {
    let currTarget = e.target;
    if (currTarget.matches('#notificationsDropdownButton')
        || currTarget.matches('#notificationsDropdownList')
        || currTarget.matches('#notificationsDropdownIcon') ) {
        return;
    }
    if (currTarget.closest('#notificationsDropdownList')) {
        return;
    }
    if (notificationsDropdownList.classList.contains('custom-show-dropdown')) {
        notificationsDropdownList.classList.remove('custom-show-dropdown');
    }
})

notificationsDropdownButton.addEventListener('click', async function () {
    notificationsDropdownButton.blur();
    notificationsDropdownList.classList.toggle('custom-show-dropdown');
    if (notificationsDropdownList.classList.contains('custom-show-dropdown')) {
        await $.ajax({
            type: 'GET',
            url: `/${areaInput.value}/Notifications/GetNotificationsDropdownPartial`,
            success: async function (viewText) {
                let viewHtml = jQuery.parseHTML(viewText);
                let dropdownMarkAsReadButtons = $(viewHtml).find('.dropdown-mark-as-read-button');
                if (dropdownMarkAsReadButtons.length > 0) {
                    dropdownMarkAsReadButtons.each(function () {
                        $(this).on('click', dropdownMarkAsReadButtonClick);
                    });
                }
                await $(notificationsDropdownList).html(viewHtml);
            },
            failure: function () {
                console.log('Notifications get request failed')
            }
        });
    }
})

async function checkIfUserHasUnreadNotifications() {
    await fetch(`/${areaInput.value}/Notifications/DoesUserHaveUnreadNotifications`, {
        method: 'GET',
    })
        .then(res => res.json())
        .then(body => {
            if (body === false) {
                notificationsDropdownIcon.classList.remove('bi-bell-fill');
                notificationsDropdownIcon.classList.add('bi-bell');
            } else {
                notificationsDropdownIcon.classList.add('bi-bell-fill');
                notificationsDropdownIcon.classList.remove('bi-bell');
            }
        });
}

async function dropdownMarkAsReadButtonClick(e) {
    e.preventDefault();
    let currBtn = e.currentTarget;
    if (!currBtn.disabled) {
        let btnParent = currBtn.parentNode;
        let notificationId = btnParent.querySelector('.notification-dropdown-id').value;
        await fetch(`/${areaInput.value}/Notifications/MarkAsRead/${notificationId}`);

        let btnIcon = currBtn.querySelector('i');
        btnIcon.classList.remove('bi-toggle-on');
        btnIcon.classList.remove('text-success');
        currBtn.disabled = true;
        btnIcon.classList.add('bi-toggle-off');
        btnIcon.classList.add('text-black');
        btnParent.classList.remove('bg-info');
        btnParent.classList.add('text-black');
    }
}
