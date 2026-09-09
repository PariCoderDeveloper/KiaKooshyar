import { __decorate } from "tslib";
import { Component } from '@angular/core';
import { ReactiveFormsModule, Validators } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { Capcha } from '../../../shared/component/capcha/capcha';
import { CommonModule } from '@angular/common';
let LoginComponent = class LoginComponent {
    fb;
    authService;
    router;
    authStateService;
    loginForm;
    errorMessage = '';
    isLoading = false;
    showPassword = false;
    captchaId = '';
    captchaCode = '';
    constructor(fb, authService, router, authStateService) {
        this.fb = fb;
        this.authService = authService;
        this.router = router;
        this.authStateService = authStateService;
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
    onCaptchaChange(event) {
        this.captchaId = event.captchaId;
        this.captchaCode = event.code;
    }
    togglePasswordVisibility() {
        this.showPassword = !this.showPassword;
    }
    onSubmit() {
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
                if (user.roles.includes('Admin')) {
                    this.router.navigate(['/admin/dashboard']);
                }
                else {
                    this.router.navigate(['/user/dashboard']);
                }
            },
            error: (error) => {
                this.isLoading = false;
                this.errorMessage = error.message;
            }
        });
    }
};
LoginComponent = __decorate([
    Component({
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
], LoginComponent);
export { LoginComponent };
