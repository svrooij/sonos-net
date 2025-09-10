// Caution! Be sure you understand the caveats before publishing an application with
// offline support. See https://aka.ms/blazor-offline-considerations

self.importScripts('./service-worker-assets.js');
self.addEventListener('install', event => event.waitUntil(onInstall(event)));
self.addEventListener('activate', event => event.waitUntil(onActivate(event)));
self.addEventListener('fetch', event => event.respondWith(onFetch(event)));

const cacheNamePrefix = 'offline-cache-';
const cacheName = `${cacheNamePrefix}${self.assetsManifest.version}`;
const offlineAssetsInclude = [ /\.dll$/, /\.pdb$/, /\.wasm/, /\.html/, /\.js$/, /\.json$/, /\.css$/, /\.woff$/, /\.png$/, /\.jpe?g$/, /\.gif$/, /\.ico$/, /\.blat$/, /\.dat$/ ];
const offlineAssetsExclude = [ /^service-worker\.js$/ ];

// URLs to skip caching (always fetch from network)
const skipCachePatterns = [ /^\/api\//, /^\/getaa/, /^\/scalar\//, /^\/\.easyauth/ ];

// Replace with your base path if you are hosting on a subfolder. Ensure there is a trailing '/'.
const base = "/";
const baseUrl = new URL(base, self.origin);
const manifestUrlList = self.assetsManifest.assets.map(asset => new URL(asset.url, baseUrl).href);

async function onInstall(event) {
    console.info('Service worker: Install');

    // Fetch and cache all matching items from the assets manifest
    const assetsRequests = self.assetsManifest.assets
        .filter(asset => offlineAssetsInclude.some(pattern => pattern.test(asset.url)))
        .filter(asset => !offlineAssetsExclude.some(pattern => pattern.test(asset.url)))
        .map(asset => new Request(asset.url, { integrity: asset.hash, cache: 'no-cache' }));
    await caches.open(cacheName).then(cache => cache.addAll(assetsRequests));
}

async function onActivate(event) {
    console.info('Service worker: Activate');

    // Delete unused caches
    const cacheKeys = await caches.keys();
    await Promise.all(cacheKeys
        .filter(key => key.startsWith(cacheNamePrefix) && key !== cacheName)
        .map(key => caches.delete(key)));
}

async function checkForMandatoryRedirect(request) {
    try {
        // Make a HEAD request to check for redirects without downloading the full response
        const headRequest = new Request(request.url, {
            method: 'HEAD',
            headers: request.headers,
            credentials: request.credentials
        });
        
        const response = await fetch(headRequest);
        
        // If we get a redirect response, follow it
        if (response.status >= 300 && response.status < 400) {
            return fetch(request); // Let the browser handle the redirect normally
        }
        
        return null; // No redirect, proceed with normal caching logic
    } catch (error) {
        console.warn('Service worker: Error checking for mandatory redirect:', error);
        return null; // On error, proceed with normal logic
    }
}

async function onFetch(event) {
    const requestUrl = new URL(event.request.url);
    const shouldSkipCache = skipCachePatterns.some(pattern => pattern.test(requestUrl.pathname));
    
    if (shouldSkipCache) {
        // Always fetch from network for API calls and excluded URLs
        return fetch(event.request);
    }

    // For navigation requests, check for mandatory redirects first
    if (event.request.mode === 'navigate') {
        const redirectResponse = await checkForMandatoryRedirect(event.request);
        if (redirectResponse) {
            return redirectResponse;
        }
    }

    let cachedResponse = null;
    if (event.request.method === 'GET') {
        // For all navigation requests, try to serve index.html from cache,
        // unless that request is for an offline resource.
        // If you need some URLs to be server-rendered, edit the following check to exclude those URLs
        const shouldServeIndexHtml = event.request.mode === 'navigate'
            && !manifestUrlList.some(url => url === event.request.url);

        const request = shouldServeIndexHtml ? 'index.html' : event.request;
        const cache = await caches.open(cacheName);
        cachedResponse = await cache.match(request);
    }

    // If we have a cached response, check if we should validate it against the server
    if (cachedResponse && event.request.mode === 'navigate') {
        // For navigation requests, always check the server for redirects even if we have a cached response
        try {
            const networkResponse = await fetch(event.request);
            
            // If the server responds with a redirect, use that instead of cache
            if (networkResponse.status >= 300 && networkResponse.status < 400) {
                return networkResponse;
            }
            
            // If the network response is successful, use it (fresher than cache)
            if (networkResponse.ok) {
                return networkResponse;
            }
        } catch (error) {
            // Network error, fall back to cache
            console.warn('Service worker: Network error, using cached response:', error);
        }
    }

    return cachedResponse || fetch(event.request);
}