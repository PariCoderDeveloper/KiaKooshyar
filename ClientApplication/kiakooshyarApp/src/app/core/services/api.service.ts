import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { environment } from "../../environments/environment.prod";
import { Observable } from "rxjs";

@Injectable({providedIn:"root"})
export class ApiSrvice{
    private readonly baseUrl = environment.apiUrl;
    constructor(
        private http:HttpClient
    ){}
    public get<T>(
        controller : string,
        action : string = '',
        params? : any 
    ):Observable<T>{
        const url = this.buildUrl(controller,action);
        return this.http.get<T>(url,{
            withCredentials:true,
            params
        });
    }
    public post<T>(
        controller : string,
        action : string = '',
        params? : any 
    ):Observable<T>{
        const url = this.buildUrl(controller,action);
        return this.http.post<T>(url,{
            withCredentials:true,
            params
        });
    }
    public put<T>(
        controller : string,
        action : string = '',
        params? : any 
    ):Observable<T>{
        const url = this.buildUrl(controller,action);
        return this.http.put<T>(url,{
            withCredentials:true,
            params
        });
    }
    public patch<T>(
        controller : string,
        action : string = '',
        params? : any 
    ):Observable<T>{
        const url = this.buildUrl(controller,action);
        return this.http.patch<T>(url,{
            withCredentials:true,
            params
        });
    }
    public delete<T>(
        controller : string,
        action : string = '',
        params? : any 
    ):Observable<T>{
        const url = this.buildUrl(controller,action);
        return this.http.delete<T>(url,{
            withCredentials:true,
            params
        });
    }
    private buildUrl(
        controller : string,
        action : string 
    ):string{
        return action
          ? `${this.baseUrl}/api/V1/${controller}/${action}`
          : `${this.baseUrl}/api/V1/${controller}`;
    }
}