import { debounce } from 'lodash';

class TabNavButtons {
  isInitialized = false;

  init() {
    if (!this.isInitialized) {
      this.addHandlers();
    }
    this.isInitialized = true;
  }

  addHandlers = () => {
    if (!document.querySelector('.tab-block')) return;
    this.sortElements();
    this.checkWidth();
    window.addEventListener('resize', debounce(this.checkWidth, 100), { capture: true });
  };

  sortElements = () => {
    const active = document.querySelector('.active');

    if (document.querySelector('.prevButton')) {
      document.querySelector('.prevButton')
        .classList
        .remove('prevButton');
    }

    if (document.querySelector('.nextButton')) {
      document.querySelector('.nextButton')
        .classList
        .remove('nextButton');
    }

    if (active.previousElementSibling) {
      active.previousElementSibling.classList.add('prevButton');
      setTimeout(() => {
        document.querySelector('.prev')
          ?.classList
          .remove('disabled');
      }, 200);
    }
    if (active.nextElementSibling) {
      active.nextElementSibling.classList.add('nextButton');
      setTimeout(() => {
        document.querySelector('.next')
          ?.classList
          .remove('disabled');
      }, 200);
    }

    if (!document.querySelector('.nextButton')) {
      setTimeout(() => {
        document.querySelector('.next')
          ?.classList
          .add('disabled');
      }, 200);
    }

    if (!document.querySelector('.prevButton')) {
      setTimeout(() => {
        document.querySelector('.prev')
          ?.classList
          .add('disabled');
      }, 200);
    }
  };

  scrollTabButtons = (elem) => {
    const tabWrapper = document.querySelector('.tab-buttons-wrapper');
    tabWrapper.querySelectorAll('.tab-buttonLink');
    tabWrapper.scrollTo({
      left: elem,
      behavior: 'smooth',
    });
  };

  nextTab = () => {
    if (!document.querySelector('.nextButton')) return;
    const next = document.querySelector('.nextButton');
    this.scrollTabButtons(next.offsetLeft);
    setTimeout(() => {
      next.click();
      this.sortElements();
    }, 300);
  };

  prevTab = () => {
    if (!document.querySelector('.prevButton')) return;
    const prev = document.querySelector('.prevButton');
    this.scrollTabButtons(prev.offsetLeft);
    setTimeout(() => {
      prev.click();
      this.sortElements();
    }, 300);
  };

  addNavButtons = () => {
    const scrollTab = document.querySelector('.score-tab');
    const buttons = new DocumentFragment();
    const tabWrapper = document.querySelector('.tab-buttons-wrapper');
    const navButton = document.createElement('div');
    const prev = document.createElement('button');
    const next = document.createElement('button');
    const arrows = '<svg xmlns="http://www.w3.org/2000/svg" width="32" height="32" viewBox="0 0 32 32" fill="none">\n'
      + '<circle cx="16" cy="16" r="15.5" transform="rotate(-180 16 16)" stroke="#512D6D"/>\n'
      + '<path fill-rule="evenodd" clip-rule="evenodd" d="M15.1565 22.8447C14.973 23.0204 14.6942 23.0503 14.4776 22.9176C14.1652 22.726 14.1185 22.2907 14.3831 22.0373L20.7555 15.9336L14.3831 9.82995C14.1185 9.57646 14.1652 9.14116 14.4776 8.94963C14.6942 8.81688 14.973 8.84682 15.1565 9.02253L21.7436 15.3318C22.0861 15.6599 22.0861 16.2073 21.7436 16.5354L15.1565 22.8447Z" fill="#512D6D"/>\n'
      + '</svg>';
    navButton.classList.add('nav-buttons-wrapper');
    prev.type = 'button';
    prev.classList.add('prev');
    prev.innerHTML = arrows;
    next.type = 'button';
    next.classList.add('next');
    next.innerHTML = arrows;
    next.onclick = this.nextTab;
    prev.onclick = this.prevTab;
    navButton.appendChild(prev);
    navButton.appendChild(next);
    buttons.appendChild(navButton);
    scrollTab.insertBefore(buttons, tabWrapper);
  };

  checkWidth = () => {
    if (window.innerWidth < 600) return;
    const tabWrapper = document.querySelector('.tab-buttons-wrapper');
    const tabButtons = tabWrapper.querySelectorAll('.tab-buttonLink');
    let width = 0;

    // eslint-disable-next-line no-return-assign
    tabButtons.forEach((el) => width += el.offsetWidth + 40);

    if (tabWrapper.offsetWidth > width) return;
    if (document.querySelector('.nav-buttons-wrapper')) return;
    this.addNavButtons();
  };
}

const instance = new TabNavButtons();
export default instance;
