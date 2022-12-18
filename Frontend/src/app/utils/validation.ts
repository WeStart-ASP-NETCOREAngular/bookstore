import { AbstractControl, ValidatorFn } from '@angular/forms';

export default class Validation {
  static match(controlName: string, checkControlName: string): ValidatorFn {
    return (controls: AbstractControl) => {
      const control = controls.get(controlName);
      const checkControl = controls.get(checkControlName);

      if (checkControl?.errors && !checkControl.errors['mustMatch']) {
        return null;
      }

      if (control?.value !== checkControl?.value) {
        controls.get(checkControlName)?.setErrors({ mustMatch: true });
        return { mustMatch: true };
      } else {
        return null;
      }
    };
  }
}
