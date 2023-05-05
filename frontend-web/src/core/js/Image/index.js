// Lazy loading support for browsers that don't support loading attribute
// Check for browser support.

if ('loading' in HTMLImageElement.prototype) {
  const images = document.querySelectorAll('.lazyload');
  // copy the value of the data-src to the src.
  images.forEach((img) => {
    // eslint-disable-next-line no-param-reassign
    img.src = img.dataset.src;
  });
} else {
  // if no support, async load the lazysizes plugin
  const script = document.createElement('script');
  script.async = true;
  script.src = 'https://cdnjs.cloudflare.com/ajax/libs/lazysizes/4.1.8/lazysizes.min.js';
  document.body.appendChild(script);
}
