// In development, always fetch from the network and do not enable offline support.
// This is because caching would make development more difficult (changes would not
// be reflected on the first load after each change).

//TODO add for presentation
//self.addEventListener('fetch', () => { });

// DEVELOPMENT MODE — ne kesira nista
//self.addEventListener('fetch', event => {
//    event.respondWith(fetch(event.request));
//});

const API_CACHE = 'nutrilab-api-v1';
const API_EXCLUDE_PREFIXES = [
    '/auth',
    '/login',
    '/logout',
    '/register',
    '/token'
];

const CACHE_NAME = 'demo-pwa-cache-v2';
const FILES_TO_CACHE = [
    '/index.html',
    '/manifest.json',
    '/icon-192.jpg',
    '/icon-512.jpg'
];

self.addEventListener('install', event => {
    event.waitUntil((async () => {
        const cache = await caches.open(CACHE_NAME);
        for (const file of FILES_TO_CACHE) {
            try {
                await cache.add(file);
                console.log('Cached:', file);
            } catch (err) {
                console.warn('Failed to cache:', file, err);
            }
        }
    })());
    self.skipWaiting();
});

self.addEventListener('activate', event => {
    event.waitUntil(
        caches.keys().then(keys =>
            Promise.all(keys.map(key => key !== CACHE_NAME ? caches.delete(key) : null))
        )
    );
    self.clients.claim();
});

self.addEventListener('fetch', event => {
    const url = new URL(event.request.url);

    const isApiCall =
        event.request.method === 'GET' &&
        (url.hostname === 'localhost' && url.port === '7049');

    const isExcluded = API_EXCLUDE_PREFIXES.some(prefix =>
        url.pathname.toLowerCase().startsWith(prefix)
    );

    if (isApiCall && !isExcluded) {
        event.respondWith(
            caches.open(API_CACHE).then(async cache => {
                try {
                    // Network first — always try live data
                    const response = await fetch(event.request);
                    if (response.ok) {
                        cache.put(event.request, response.clone());
                    }
                    return response;
                } catch {
                    // Offline fallback — return cached version
                    const cached = await cache.match(event.request);
                    if (cached) {
                        console.log('[SW] Offline fallback for:', url.pathname);
                        return cached;
                    }
                    // Nothing cached — return a clean JSON error
                    return new Response(
                        JSON.stringify({ error: 'Offline — no cached data available' }),
                        { status: 503, headers: { 'Content-Type': 'application/json' } }
                    );
                }
            })
        );
        return;
    }

    event.respondWith(
        caches.match(event.request).then(response => response || fetch(event.request))
    );
});