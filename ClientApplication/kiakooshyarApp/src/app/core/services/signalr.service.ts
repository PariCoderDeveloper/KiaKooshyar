import { Component,Injectable,OnInit } from "@angular/core";
import {HubConnection, HubConnectionBuilder, LogLevel} from '@microsoft/signalR'
import { Subject } from "rxjs";

@Injectable({providedIn:'root'})
export class SignalRService{
    private hubConnection! :HubConnection;
    private notification = new Subject<any>();

    notification$ = this.notification.asObservable();

    startConnection():void{
        this.hubConnection = new HubConnectionBuilder()
            .withUrl('https://localhost:44316/')
            .withAutomaticReconnect()
            .configureLogging(LogLevel.Information)
            .build();
        this.hubConnection.on("ForceLogout",(message)=>{
            this.notification.next(message);
        });
        this.hubConnection
            .start()
            .then(() => {
                console.log("SignalR connected");
            })
            .catch((error)=>{
                console.error(error);
            });
    }
}
