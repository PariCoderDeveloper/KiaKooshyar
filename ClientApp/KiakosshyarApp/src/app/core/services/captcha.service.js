import { __decorate } from "tslib";
import { Injectable } from '@angular/core';
let CaptchaService = class CaptchaService {
    api;
    constructor(api) {
        this.api = api;
    }
    generate() {
        return this.api.get('captcha', 'generate');
    }
};
CaptchaService = __decorate([
    Injectable({ providedIn: 'root' })
], CaptchaService);
export { CaptchaService };
