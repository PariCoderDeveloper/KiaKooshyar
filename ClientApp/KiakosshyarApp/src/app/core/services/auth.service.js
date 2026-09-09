import { __decorate } from "tslib";
import { Injectable } from "@angular/core";
import { tap } from "rxjs";
let AuthService = class AuthService {
    api;
    router;
    loggedIn = false;
    constructor(api, router) {
        this.api = api;
        this.router = router;
    }
    login(credentials) {
        return this.api.post("Authentication", "login", credentials).pipe(tap(() => (this.loggedIn = true)));
    }
    refresh_token() {
        return this.api.post("Authentication", "refresh-token");
    }
    register(credentials) {
        return this.api.post("user", "registeruser", credentials);
    }
    logout() {
        this.api.post("Authentication", "logout").subscribe({
            next: () => this.handleLoggedOut(),
            error: () => this.handleLoggedOut()
        });
    }
    handleLoggedOut() {
        this.loggedIn = false;
        this.router.navigate(['/login']);
    }
};
AuthService = __decorate([
    Injectable({ providedIn: "root" })
], AuthService);
export { AuthService };
