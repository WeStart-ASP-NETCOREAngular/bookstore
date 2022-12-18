import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { RegisterRequest } from 'src/app/Interfaces/AuthenticationRequest';
import { AuthService } from 'src/app/services/auth.service';
import Validation from 'src/app/utils/validation';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
})
export class RegisterComponent implements OnInit {
  constructor(private router: Router, private authService: AuthService) {}

  showError: boolean;
  errorMessage: string;
  registerForm: FormGroup;

  ngOnInit(): void {
    this.registerForm = new FormGroup(
      {
        firstName: new FormControl('', [Validators.required]),
        lastName: new FormControl('', [Validators.required]),
        email: new FormControl('', [Validators.email, Validators.required]),
        password: new FormControl('', [Validators.required]),
        confirm: new FormControl('', [Validators.required]),
      },
      {
        validators: [Validation.match('password', 'confirm')],
      }
    );
  }

  validateControl(controlName: string) {
    return (
      this.registerForm.get(controlName)?.invalid &&
      this.registerForm.get(controlName)?.touched
    );
  }

  hasError(controlName: string, errorName: string) {
    return this.registerForm.get(controlName)?.hasError(errorName);
  }

  registerUser() {
    this.showError = false;
    const formValues = { ...this.registerForm.value };

    const user: RegisterRequest = {
      FirstName: formValues.firstName,
      LastName: formValues.lastName,
      Email: formValues.email,
      Password: formValues.password,
    };

    this.authService.registerUser(user).subscribe({
      next: (data) => {
        console.log(data);
        this.router.navigate(['/auth/login']);
      },
      error: (error: HttpErrorResponse) => {
        this.errorMessage = error.error.errors;
        this.showError = true;
      },
    });
  }
}
