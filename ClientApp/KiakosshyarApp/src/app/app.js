import { __decorate } from "tslib";
import { Component, inject, signal } from '@angular/core';
import { Router, RouterOutlet } from '@angular/router';
import { SignalRService } from './core/services/signalr.service';
import { AuthService } from './core/services/auth.service';
let App = class App {
    title = signal('kiakooshyarApp');
    signalRService = inject(SignalRService);
    authService = inject(AuthService);
    router = inject(Router);
    ngOnInit() {
        this.signalRService.notification$.subscribe(reason => {
            alert(reason);
            this.authService.logout();
            this.router.navigate(["/login"]);
        });
        this.signalRService.startConnection();
    }
};
App = __decorate([
    Component({
        imports: [RouterOutlet],
        selector: 'app-root',
        styleUrl: './app.css',
        templateUrl: './app.html',
    })
], App);
export { App };
