import { __decorate } from "tslib";
import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, Validators } from '@angular/forms';
import { Capcha } from '../../../shared/component/capcha/capcha';
let RegisterComponent = class RegisterComponent {
    fb;
    authService;
    router;
    registerForm;
    errorMessage = '';
    successMessage = '';
    isLoading = false;
    captchaId = '';
    captchaCode = '';
    constructor(fb, authService, router) {
        this.fb = fb;
        this.authService = authService;
        this.router = router;
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
    onCaptchaChange(event) {
        this.captchaId = event.captchaId;
        this.captchaCode = event.code;
    }
    onSubmit() {
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
};
RegisterComponent = __decorate([
    Component({
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
], RegisterComponent);
export { RegisterComponent };
