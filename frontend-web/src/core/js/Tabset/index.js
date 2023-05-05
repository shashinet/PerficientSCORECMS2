import TabNavButtons from './TabWithNavigationButtons';

class Tabset {
  isInitialized = false;

  init() {
    if (!this.isInitialized) {
      this.addHandlers();
    }
    this.isInitialized = true;
  }

  addHandlers = () => {
    if (!document.querySelector('.score-tab')) return;
    const tabs = document.querySelectorAll('.score-tab');
    const tabButton = document.querySelectorAll('.tab-buttonLink');
    const contents = document.querySelectorAll('.content');

    tabs.forEach((tab) => {
      tab.addEventListener('click', (e) => {
        const { id } = e.target.dataset;

        if (id) {
          tabButton.forEach((btn) => {
            btn.classList.remove('active');
          });

          e.target.classList.add('active');

          contents.forEach((content) => {
            content.classList.remove('active');
          });

          const element = document.getElementById(id);
          element.classList.add('active');
          if (document.querySelector('.tab-block')) {
            TabNavButtons.sortElements();
          }
        }
      });
    });
  };
}

const instance = new Tabset();
export default instance;
