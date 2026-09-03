import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { Observable, tap } from "rxjs";
import { ApiSrvice } from "./api.service";

@Injectable({providedIn:"root"})
export class AuthService {
    private loggedIn = false;

    constructor(
       private api:ApiSrvice,
       private router : Router
    ){}
    public login(credentials: {username:string, password:string})
       :Observable<any>{
            return this.api.post(
                "Authentication",
                "login",
                credentials
            ).pipe(tap(()=>(this.loggedIn = true)));
    }
    public refresh_token()
        :Observable<any>{
            return this.api.post(
                "Authentication",
                "refresh-token"
            );
    }
    public logout():void{
        this.api.post(
            "Authentication",
            "logout"
        ).subscribe({
            next:()=> this.handleLoggedOut(),
            error:()=> this.handleLoggedOut()
        });
    }
    
    private handleLoggedOut():void{
        this.loggedIn = false;
        this.router.
    }
}