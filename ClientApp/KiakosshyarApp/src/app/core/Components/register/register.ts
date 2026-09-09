import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service'; 
import { Capcha } from '../../../shared/component/capcha/capcha'; 

@Component({
  selector: 'app-register',
  standalone: true,
  templateUrl: './register.html',
  styleUrls: ['./register.css'],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    Capcha
  ]
})
export class RegisterComponent {
  registerForm: FormGroup;
  errorMessage: string = '';
  successMessage: string = '';
  isLoading: boolean = false;
  
  captchaId: string = '';
  captchaCode: string = '';

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router
  ) {
    this.registerForm = this.fb.group({
      firstname: ['', [Validators.required, Validators.minLength(3)]],
      lastname: ['', [Validators.required, Validators.minLength(3)]],
      username: ['', [Validators.required, Validators.minLength(3)]],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
      confirmPassword: ['', [Validators.required]]
    });
      console.log('REGISTER CREATED');

  }

  get firstname() { return this.registerForm.get('firstname'); }
  get lastname() { return this.registerForm.get('lastname'); }
  get username() { return this.registerForm.get('username'); }
  get email() { return this.registerForm.get('email'); }
  get password() { return this.registerForm.get('password'); }
  get confirmPassword() { return this.registerForm.get('confirmPassword'); }

  onCaptchaChange(event: { captchaId: string; code: string }): void {
    this.captchaId = event.captchaId;
    this.captchaCode = event.code;
  }

  onSubmit(): void {
    if (this.registerForm.invalid) {
      this.registerForm.markAllAsTouched();
      return;
    }

    if (this.password?.value !== this.confirmPassword?.value) {
      this.errorMessage = 'Passwords do not match.';
      return;
    }

    if (!this.captchaCode || this.captchaCode.trim() === '') {
      this.errorMessage = 'Please enter the security code (captcha).';
      return;
    }

    this.isLoading = true;
    this.errorMessage = '';
    this.successMessage = '';

    const registerPayload = {
      firstname: this.firstname?.value,
      lastname: this.lastname?.value,
      username: this.username?.value,
      email: this.email?.value,
      password: this.password?.value,
      captchaId: this.captchaId,
      captchaCode: this.captchaCode
    };
  this.authService.register(registerPayload).subscribe({
    next: (response) => {
      this.isLoading = false;
      this.successMessage = 'Registration successful. Redirecting to the login page...';
    
      setTimeout(() => {
        this.router.navigate(['/login']);
      }, 2000);
   },
    error: (error) => {
      this.isLoading = false;
      this.errorMessage = error.error?.message || 'Registration failed. Please try again.';
      }
    });
  }
}