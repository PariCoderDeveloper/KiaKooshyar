import { __decorate } from "tslib";
import { Injectable } from "@angular/core";
import { environments } from "../../environments/environment";
let ApiService = class ApiService {
    http;
    baseUrl = environments.apiUrl;
    constructor(http) {
        this.http = http;
    }
    get(controller, action = '', params) {
        const url = this.buildUrl(controller, action);
        return this.http.get(url, {
            withCredentials: true,
            params
        });
    }
    post(controller, action = '', params) {
        const url = this.buildUrl(controller, action);
        return this.http.post(url, params, {
            withCredentials: true
        });
    }
    put(controller, action = '', params) {
        const url = this.buildUrl(controller, action);
        return this.http.put(url, params, {
            withCredentials: true,
        });
    }
    patch(controller, action = '', params) {
        const url = this.buildUrl(controller, action);
        return this.http.patch(url, params, {
            withCredentials: true,
        });
    }
    delete(controller, action = '', params) {
        const url = this.buildUrl(controller, action);
        return this.http.delete(url, {
            withCredentials: true,
            params
        });
    }
    buildUrl(controller, action) {
        return action
            ? `${this.baseUrl}/api/V1/${controller}/${action}`
            : `${this.baseUrl}/api/V1/${controller}`;
    }
};
ApiService = __decorate([
    Injectable({ providedIn: "root" })
], ApiService);
export { ApiService };
