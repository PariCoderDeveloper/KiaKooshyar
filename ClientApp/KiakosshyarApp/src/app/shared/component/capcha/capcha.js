import { __decorate } from "tslib";
import { Component, Output, EventEmitter } from '@angular/core';
import { FormsModule } from '@angular/forms';
let Capcha = class Capcha {
    captchaService;
    imageBase64 = '';
    captchaId = '';
    userInput = '';
    captchaChange = new EventEmitter();
    constructor(captchaService) {
        this.captchaService = captchaService;
    }
    ngOnInit() {
        this.refresh();
    }
    refresh() {
        this.captchaService.generate().subscribe(result => {
            this.captchaId = result.captchaId;
            this.imageBase64 = result.imageBase64;
            this.userInput = '';
            this.onValueChange();
        });
    }
    onValueChange() {
        this.captchaChange.emit({ captchaId: this.captchaId, code: this.userInput });
    }
};
__decorate([
    Output()
], Capcha.prototype, "captchaChange", void 0);
Capcha = __decorate([
    Component({
        imports: [
            FormsModule,
        ],
        selector: 'app-captcha',
        styleUrl: './capcha.css',
        templateUrl: './capcha.html',
        standalone: true
    })
], Capcha);
export { Capcha };
