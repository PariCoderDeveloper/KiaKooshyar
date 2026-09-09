import { __decorate } from "tslib";
import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
let Dashboard = class Dashboard {
};
Dashboard = __decorate([
    Component({
        imports: [
            MatCardModule,
            MatIconModule,
            CommonModule
        ],
        selector: 'app-dashboard',
        styleUrl: './dashboard.css',
        templateUrl: './dashboard.html',
    })
], Dashboard);
export { Dashboard };
