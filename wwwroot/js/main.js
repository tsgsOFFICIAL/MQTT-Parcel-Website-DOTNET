let registration;

topLevelFunction();

async function topLevelFunction() {
    registration = await navigator.serviceWorker.getRegistration();
}

// Function to send a notification
async function sendNotification(notifyTitle, notifyBody) {
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
}

// Function to show a notification
function showNotification(notifyTitle, notifyBody) {
    const notifyImg = `./assets/icons/icon-128x128.png`;

    const payload = {
        body: notifyBody,
        icon: notifyImg,
    };

    if ('showNotification' in registration) {
        registration.showNotification(notifyTitle, payload);
    } else {
        new Notification(notifyTitle, payload);
    }
}







//let registration;


//const sendNotification = async (notifyTitle, notifyBody) => {
//    const permission = await Notification.requestPermission();

//    if (permission === 'granted') {
//        const notifyImg = `./assets/icons/icon-128x128.png`;

//        const payload = {
//            body: notifyBody,
//            icon: notifyImg,
//        };

//        if ('showNotification' in registration) {
//            registration.showNotification(notifyTitle, payload);
//        } else {
//            const notification = new Notification(notifyTitle, payload);

//            // Event listener when the user clicks on the notification
//            notification.onclick = () => {
//                console.log('The notification was clicked');
//            };
//        }
//    }
//};


//const requestPermission = async () => {
//    if (Notification.permission !== 'granted') {
//        await Notification.requestPermission();
//    }
//};

//// Function to get service worker registration and request permission
//const initialize = async () => {
//    try {
//        registration = await navigator.serviceWorker.getRegistration();
//        await requestPermission();
//    } catch (error) {
//        console.error('Error initializing service worker:', error);
//    }
//};

//// Initialize the service worker and request permission
//initialize();
