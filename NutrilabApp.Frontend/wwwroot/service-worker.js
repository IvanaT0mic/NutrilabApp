// In development, always fetch from the network and do not enable offline support.
// This is because caching would make development more difficult (changes would not
// be reflected on the first load after each change).

//TODO add for presentation
//self.addEventListener('fetch', () => { });

// DEVELOPMENT MODE — ne kesira nista
self.addEventListener('fetch', event => {
    event.respondWith(fetch(event.request));
});
