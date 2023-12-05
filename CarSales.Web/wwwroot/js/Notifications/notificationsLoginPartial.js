const areaInput = document.querySelector('#areaInput');
const notificationsIcon = document.querySelector('#notificationsIcon');
const notificationsListItem = document.querySelector('#notificationsListItem');
const notificationsList = document.querySelector('#notificationsList');

window.addEventListener('load', async function () {
    await checkIfUserHasUnreadNotifications();

    setInterval(checkIfUserHasUnreadNotifications, 30000)

})

notificationsListItem.addEventListener('click', function () {
    if (notificationsList.classList.contains('show')) {
        $(notificationsList).load(`/${areaInput.value}/Notifications/GetNotificationsPartial`);
    }
})

async function checkIfUserHasUnreadNotifications() {
    await fetch(`/${areaInput.value}/Notifications/DoesUserHaveUnreadNotifications`, {
        method: 'GET',
    })
        .then(res => res.json())
        .then(body => {
            if (body === false) {
                notificationsIcon.classList.remove('bi-bell-fill');
                notificationsIcon.classList.add('bi-bell');
            } else {
                notificationsIcon.classList.add('bi-bell-fill');
                notificationsIcon.classList.remove('bi-bell');
            }
        });
}


