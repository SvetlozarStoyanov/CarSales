const areaInput = document.querySelector('#areaInput');
const notificationsListItem = document.querySelector('#notificationsListItem');
const notificationsList = document.querySelector('#notificationsList');

notificationsListItem.addEventListener('click', function () {
    if (notificationsList.classList.contains('show')) {
        $(notificationsList).load(`/${areaInput.value}/Notifications/GetNotificationsPartial`);
        console.log('get request');
    }
})


