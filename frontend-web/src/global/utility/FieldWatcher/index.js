class FieldWatcher {
    isInitialized = false;

    init = () => {
      if (!this.isInitialized) {
        this.addHandlers();
      }
      this.isInitialized = true;
    }

    addHandlers = () => {
      document
        .querySelectorAll('.Form__Element input, .Form__Element select, .Form__Element textarea')
        .forEach((f) => {
          const label = document.querySelector(`label[for='${f.id}']`);
          if (f.value !== '') {
            f.classList.add('has-data');
            label?.classList.add('has-data');
          } else {
            f.classList.remove('has-data');
            label?.classList.remove('has-data');
          }
          f.addEventListener(
            'blur',
            () => {
              if (f.value !== '') {
                f.classList.add('has-data');
                label?.classList.add('has-data');
              } else {
                f.classList.remove('has-data');
                label?.classList.remove('has-data');
              }
            },
          );
        });
    };
}

const instance = new FieldWatcher();
export default instance;
