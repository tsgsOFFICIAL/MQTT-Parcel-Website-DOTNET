function requestPermission() {
    if (!('Notification' in window)) {
        alert('Notification API not supported!');
        return;
    }

    Notification.requestPermission((result) => {
        console.log(result);
    });
}

function showNonPersistentNotification(notifyTitle, notifyBody) {
    const notifyImg = `./assets/icons/icon-128x128.png`;

    const payload = {
        body: notifyBody,
        icon: notifyImg,
    };

    if (!('Notification' in window)) {
        alert('Notification API not supported!');
        return;
    }

    try {
        const notification = new Notification(notifyTitle, payload);

        // Event listener when the user clicks on the notification
        notification.onclick = () => {
            console.log('The notification was clicked');
        };
    } catch (err) {
        showPersistentNotification(notifyTitle, notifyBody);
        //alert('Notification API error: ' + err);
    }
}

function showPersistentNotification(notifyTitle, notifyBody) {
    const notifyImg = `./assets/icons/icon-128x128.png`;

    const payload = {
        body: notifyBody,
        icon: notifyImg,
    };

    if (!('Notification' in window) || !('ServiceWorkerRegistration' in window)) {
        alert('Persistent Notification API not supported!');
        return;
    }

    try {
        navigator.serviceWorker.getRegistration()
            .then((reg) => reg.showNotification(notifyTitle))//, payload))
            .catch((err) => alert('Service Worker registration error: ' + err));
    } catch (err) {
        alert('Notification API error: ' + err);
    }
}

requestPermission();