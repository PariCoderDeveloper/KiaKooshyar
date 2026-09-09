import { __decorate, __param } from "tslib";
import { Injectable, Inject, PLATFORM_ID } from "@angular/core";
import { isPlatformBrowser } from '@angular/common';
import { HubConnectionBuilder, LogLevel, HttpTransportType } from '@microsoft/signalr';
import { Subject } from "rxjs";
let SignalRService = class SignalRService {
    platformId;
    hubConnection;
    notification = new Subject();
    notification$ = this.notification.asObservable();
    constructor(platformId) {
        this.platformId = platformId;
    }
    startConnection() {
        if (isPlatformBrowser(this.platformId)) {
            console.log('🔥 startConnection CALLED (Browser Environment)');
            this.hubConnection = new HubConnectionBuilder()
                .withUrl('/hubs/notification', {
                withCredentials: true,
                transport: HttpTransportType.LongPolling
            })
                .configureLogging(LogLevel.Trace)
                .build();
            this.hubConnection.on('ForceLogout', (message) => {
                this.notification.next(message);
            });
            this.hubConnection.onclose((error) => {
                throw error;
            });
            this.hubConnection.start()
                .then(() => console.log('🟢 SIGNALR CONNECTED'))
                .catch(error => console.error('🔴 SIGNALR CONNECTION ERROR:', error));
        }
    }
};
SignalRService = __decorate([
    Injectable({ providedIn: 'root' }),
    __param(0, Inject(PLATFORM_ID))
], SignalRService);
export { SignalRService };
