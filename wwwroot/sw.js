const cacheName = 'mqtt_parcel_cache';
const assets = [
    './',
    './index.html',
    './manifest.json',
    './logo.svg',
    './js/cookie.js',
    './js/main.js',
    './img/cookie.svg',
    './img/cookie.svg',
    './img/postman_icon.svg',
    './css/animations.css',
    './css/cookie.css',
    './css/index.css',
    './css/main.css',
    './css/variables.css'
];

// Cache all the files to make a PWA
self.addEventListener('install', installEvent => {
    installEvent.waitUntil(
        caches.open(cacheName).then(cache => {
            return Promise.all(
                assets.map(url => {
                    return fetch(url)
                        .then(response => {
                            if (!response.ok) {
                                throw new Error(`Failed to fetch "${url}", status: ${response.status}`);
                            }

                            return cache.put(url, response);
                        })
                        .catch(error => console.error(`Failed to cache "${url}": ${error.message}`));
                })
            );
        }).catch(error => console.error('Cache open failed:', error))
    );
});

//self.addEventListener('install', installEvent => {
//    installEvent.waitUntil(
//        caches.open(cacheName).then(cache => {
//            return cache.addAll(assets);
//        })
//    );
//});

// Our service worker will intercept all fetch requests
// and check if we have cached the file
// if so it will serve the cached file
self.addEventListener("fetch", fetchEvent => {
    fetchEvent.respondWith(
        caches.match(fetchEvent.request).then(res => {
            return res || fetch(fetchEvent.request);
        })
    );
});