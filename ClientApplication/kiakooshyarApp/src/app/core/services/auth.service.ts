import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { Observable, tap } from "rxjs";
import { ApiService } from "./api.service";

@Injectable({providedIn:"root"})
export class AuthService {
    private loggedIn = false;

    constructor(
       private api:ApiService,
       private router : Router
    ){}
    public login(credentials: {
        email:string,
        password:string,
        captchaId:string,
        captchaCode:string
    })
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
      //  this.router.
    }
}