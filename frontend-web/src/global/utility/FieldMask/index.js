class FieldMask {
  isInitialized = false;

  init = () => {
    if (!this.isInitialized) {
      this.addHandlers();
    }
    this.isInitialized = true;
  }

  addHandlers = () => {
    const phoneInputs = document.querySelectorAll("input.FormTextbox__Input[placeholder='(###) ###-####']");
    phoneInputs.forEach((p) => {
      p.addEventListener('input', (e) => {
        const x = e.target.value.replace(/\D/g, '').match(/(\d{0,3})(\d{0,3})(\d{0,4})/);
        const x3 = x[3] ? `-${x[3]}` : '';
        e.target.value = !x[2] ? x[1] : `(${x[1]}) ${x[2]}${x3}`;
      });
    });
  }
}

const instance = new FieldMask();
export default instance;
