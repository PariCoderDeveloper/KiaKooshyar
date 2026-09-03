import { Component,Injectable,OnInit } from "@angular/core";
import * as signalR from '@microsoft/signalR'
import { Subject } from "rxjs";

@Injectable({providedIn:'root'})
export class SignalRService{
    private hubConnection :signalR.HubConnection;
    public messageRecived = new Subject<{user:string,me}
}
