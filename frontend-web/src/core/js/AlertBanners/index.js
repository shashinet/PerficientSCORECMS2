class AlertBanners {
  isInitialized = false;

  init() {
    if (!this.isInitialized) {
      this.dismissAlert();
    }
    this.isInitialized = true;
  }

  /**
   *
   * @param id
   * @param expDays
   */

  setCookie = (id, expDays) => {
    const date = new Date();
    date.setTime(date.getTime() + expDays * 24 * 60 * 60 * 1000);
    const dateString = `${date.getUTCFullYear()}-${date.getUTCMonth() + 1}-${date.getUTCDate()}T${date.getUTCHours()}:${date.getUTCMinutes()}:${date.getUTCSeconds()}.000Z`;
    const expires = `expires=${date.toUTCString()}`;
    document.cookie = `alert-${id}=${dateString}; ${expires}; path=/`;
  };

  dismissAlert = () => {
    if (document.querySelector('.score-alert-dismissible')) {
      const banner = document.querySelectorAll('.score-alert-dismissible');

      banner.forEach((item) => {
        const closeButton = item.querySelector('.close');
        const cValue = item.dataset.value;
        const cExpires = item.dataset.expires;
        closeButton.addEventListener('click', () => {
          // console.log(item + 'clicked');
          item.classList.add('dismiss');
          this.setCookie(cValue, cExpires);
        });
      });
    }
  };
}

const instance = new AlertBanners();
export default instance;
