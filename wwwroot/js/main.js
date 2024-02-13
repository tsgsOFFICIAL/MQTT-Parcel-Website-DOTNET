let registration;

topLevelFunction();
requestPermission();

async function topLevelFunction() {
    registration = await navigator.serviceWorker.getRegistration();
}

function requestPermission() {
    if (!('Notification' in window)) {
        alert('Notification API not supported!');
        return;
    }

    Notification.requestPermission(() => { });
}

function showNotification(notifyTitle, notifyBody) {
    const notifyImg = `./assets/icons/icon-128x128.png`;

    const payload = {
        body: notifyBody,
        icon: notifyImg,
    };

    if (!('Notification' in window)) {
        alert('Notification API not supported!');
        return;
    }

    if (Notification.permission === 'granted') {
        let notification = new Notification(notifyTitle, payload);

        // Eventlistener to react, when a user clicks on a notification
        notification.onclick = () => {
            console.log('The notification was clicked');
        };
    }
}