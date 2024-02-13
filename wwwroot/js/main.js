const registration = await navigator.serviceWorker.getRegistration();

requestPermission();


const sendNotification = async (notifyTitle, notifyBody) => {
    if (Notification.permission === 'granted') {
        showNotification(notifyTitle, notifyBody);
    } else {
        if (Notification.permission !== 'denied') {
            const permission = await Notification.requestPermission();

            if (permission === 'granted') {
                showNotification(notifyTitle, notifyBody);
            }
        }
    }
};

const showNotification = (notifyTitle, notifyBody) => {
    const notifyImg = `./assets/icons/icon-128x128.png`;

    const payload = {
        body: notifyBody,
        icon: notifyImg,
    };

    if ('showNotification' in registration) {
        registration.showNotification(notifyTitle, payload);
    } else {
        let notification = new Notification(notifyTitle, payload);

        // Eventlistener to react, when a user clicks on a notification
        notification.onclick = () => {
            console.log('The notification was clicked');
        };
    }
};

function requestPermission() {
    if (Notification.permission !== 'denied') {
        await Notification.requestPermission();
    }
}