class Accordion {
  isInitialized = false;

  init() {
    if (!this.isInitialized) {
      this.addHandlers();
    }
    this.isInitialized = true;
  }

  addHandlers = () => {
    if (document.querySelector('.score-accordion')) {
      const accordionParent = document.querySelectorAll('.score-accordion');

      accordionParent.forEach((parent) => {
        parent.addEventListener('click', (e) => {
          if (e.target && e.target.matches('.accordion-buttonLink')) {
            let panelContent = e.target.dataset.target;
            panelContent = panelContent.substring(1);

            const panel = document.getElementById(panelContent);
            panel.classList.toggle('show');

            e.target.setAttribute(
              'aria-expanded',
              e.target.getAttribute('aria-expanded') === 'false' ? 'true' : 'false',
            );
          }
        });
      });
    }
  };
}

const instance = new Accordion();
export default instance;
