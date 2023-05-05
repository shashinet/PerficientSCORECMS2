class ScoreVideoLoader {
  isInitialized = false;

  init() {
    if (!this.isInitialized) {
      this.addLoaders();
    }
    this.isInitialized = true;
  }

  addLoaders = () => {
    if (document.querySelector('.score-video')) {
      const videoWrapper = document.querySelectorAll('.score-video');

      videoWrapper.forEach((vid) => {
        const iframe = vid.querySelector('iframe');

        if (!iframe) {
          return;
        }

        const vidThumb = iframe.dataset.thumbnailurl;

        // eslint-disable-next-line no-param-reassign
        vid.style.backgroundImage = `url(${vidThumb})`;
        // eslint-disable-next-line no-param-reassign

        vid.addEventListener('click', (e) => {
          if (e.target && e.target.matches('.score-video')) {
            const vidFrame = e.target.querySelector('iframe');

            vidFrame.src = `${vidFrame.dataset.src}?autoplay=1&enablejsapi=1`;
            e.target.classList.add('loaded');
            vidFrame.contentWindow.postMessage({ event: 'command', func: 'playVideo' });
          }
        });
      });
    }
  };
}

const instance = new ScoreVideoLoader();
export default instance;
