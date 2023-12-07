const markAsReadBtns = document.querySelectorAll('.mark-as-read-button');

markAsReadBtns.forEach(b => b.addEventListener('click', async function (e) {
    let currBtn = e.currentTarget;
    if (!currBtn.disabled) {
        let btnParent = currBtn.parentNode;
        let notificationId = btnParent.parentNode.querySelector('.notification-id').value;
        await fetch(`./Notifications/MarkAsRead/${notificationId}`);

        currBtn.classList.remove('btn-warning');
        currBtn.classList.add('btn-primary');
        currBtn.disabled = true;

        btnParent.classList.remove('bg-info');
        btnParent.classList.remove('bg-opacity-50');
        btnParent.classList.add('bg-secondary');
        btnParent.classList.add('bg-opacity-10');
    }
}))