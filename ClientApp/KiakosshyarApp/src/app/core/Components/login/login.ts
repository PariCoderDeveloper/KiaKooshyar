import { Component, ViewChild } from '@angular/core';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { Capcha } from '../../../shared/component/capcha/capcha';
import { CommonModule } from '@angular/common';
import { AuthStateService } from '../../services/auth.state.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.html',
  styleUrls: ['./login.css'],
  standalone: true,
  imports: [
    ReactiveFormsModule,
    CommonModule,
    RouterLink,
    Capcha
  ]
})
export class LoginComponent {
  @ViewChild(Capcha) captchaComponent!: Capcha;

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
    private authStateService: AuthStateService
  ) {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]]
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
    }

    if (!this.captchaCode || this.captchaCode.trim() === '') {
      this.errorMessage = 'Please enter the captcha code';
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

        if (!response?.data?.user) {
          this.errorMessage = 'Invalid response from server';
          this.refreshCaptcha();
          return;
        }

        const user = response.data.user;

        const roles = user.roles || ['User'];
        const permissions = user.permissions || [];

        this.authStateService.setRoles(roles);
        this.authStateService.setPermissions(permissions);
        this.authStateService.setUserInfo(
          `${user.firstname || ''} ${user.lastname || ''}`.trim() || 'User',
          user.id
        );

        if (roles.includes('Admin')) {
          this.router.navigate(['/admin/dashboard']);
        }else {
          this.router.navigate(['/dashboard']);
        }
      },
      error: (error) => {
        this.isLoading = false;
        console.error('❌ Login error:', error);

        if (error.status === 401) {
          this.errorMessage = 'Invalid email or password';
        } else if (error.status === 403) {
          this.errorMessage = 'Your account has been blocked';
        } else if (error.status === 400) {
          this.errorMessage = error.error?.message || 'Invalid captcha code';
        } else if (error.status === 0) {
          this.errorMessage = 'Connection error. Please check your internet connection';
        } else {
          this.errorMessage = error.error?.message || error.message || 'An unknown error occurred';
        }

        this.refreshCaptcha();
      }
    });
  }

  private refreshCaptcha(): void {
    if (this.captchaComponent && this.captchaComponent.refresh) {
      this.captchaComponent.refresh();
    }
    this.captchaCode = '';
    this.captchaId = '';
  }
}