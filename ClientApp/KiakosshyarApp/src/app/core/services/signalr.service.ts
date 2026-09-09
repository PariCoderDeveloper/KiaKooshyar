import { Injectable, Inject } from "@angular/core";
import { HubConnection, HubConnectionBuilder, LogLevel, HttpTransportType } from '@microsoft/signalr';
import { Subject } from "rxjs";

@Injectable({ providedIn: 'root' })
export class SignalRService {
  private hubConnection!: HubConnection;
  private notification = new Subject<any>();
  notification$ = this.notification.asObservable();

  startConnection(): void {
      this.hubConnection = new HubConnectionBuilder()
        .withUrl('/hubs/notification', {
          withCredentials: true,
        })
        .configureLogging(LogLevel.Trace)
        .build();

      this.hubConnection.on('ForceLogout', (message) => {
        this.notification.next(message);
      });

        this.hubConnection.onclose((error) => {
          console.error('SignalR disconnected:', error);
      });
       this.hubConnection.start()
        .then(() => console.log('SignalR connected'))
        .catch(error => console.error('SignalR connection error:', error));
    }
  }
