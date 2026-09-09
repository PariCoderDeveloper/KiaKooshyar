import { __decorate } from "tslib";
import { Component, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterOutlet, RouterLink, RouterLinkActive } from '@angular/router';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatListModule } from '@angular/material/list';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
let AdminLayoutComponent = class AdminLayoutComponent {
    drawer;
    toggleSidenav() {
        this.drawer.toggle();
    }
};
__decorate([
    ViewChild('drawer')
], AdminLayoutComponent.prototype, "drawer", void 0);
AdminLayoutComponent = __decorate([
    Component({
        selector: 'app-admin-layout',
        standalone: true,
        imports: [
            CommonModule,
            RouterOutlet,
            RouterLink,
            RouterLinkActive,
            MatSidenavModule,
            MatToolbarModule,
            MatListModule,
            MatIconModule,
            MatButtonModule
        ],
        templateUrl: './admin-layout.html',
        styleUrls: ['./admin-layout.css']
    })
], AdminLayoutComponent);
export { AdminLayoutComponent };
