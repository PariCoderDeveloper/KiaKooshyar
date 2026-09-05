import { Component } from '@angular/core';
import { ReactiveFormsModule,FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { Capcha } from '../../../shared/component/capcha/capcha';
import { CommonModule } from '@angular/common';
import { AuthStateService } from '../../services/auth.state.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.html',
  styleUrls: ['./login.css'],
  standalone:true,
  imports:[
    ReactiveFormsModule,
    CommonModule,
    RouterLink,
    Capcha
  ]
})
export class LoginComponent {
  loginForm: FormGroup;
  errorMessage: string = '';
  isLoading: boolean = false;
  showPassword: boolean = false;
  captchaId: string = '';
  captchaCode: string = '';

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router,
    private authStateService : AuthStateService
  ) {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.minLength(3)]],
      password: ['', [Validators.required, Validators.minLength(3)]]
    });
  }

  get email() {
    return this.loginForm.get('email');
  }

  get password() {
    return this.loginForm.get('password');
  }

  onCaptchaChange(event: { captchaId: string; code: string }): void {
    this.captchaId = event.captchaId;
    this.captchaCode = event.code;
  }

  togglePasswordVisibility(): void {
    this.showPassword = !this.showPassword;
  }

  onSubmit(): void {
    if (this.loginForm.invalid) {
      this.loginForm.markAllAsTouched();
      return;
    }

    if (!this.captchaCode || this.captchaCode.trim() === '') {
      this.errorMessage = 'Please ener captcha';
      return;
    }

    this.isLoading = true;
    this.errorMessage = '';

    const loginValue = this.loginForm.value;

    const loginPayload = {
      email: loginValue.email,
      password: loginValue.password,
      captchaId: this.captchaId,
      captchaCode: this.captchaCode
    };
    this.authService.login(loginPayload).subscribe({
      next: (response) => {
        this.isLoading = false;
        var user = response.data.user;
        this.authStateService.setRoles(user.roles);
        this.authStateService.setPermissions(user.permissions);
        this.router.navigate(['/dashboard']);
      },
      error: (error) => {  
        this.isLoading = false;
        this.errorMessage = error.message;
      }
    });
  }
}