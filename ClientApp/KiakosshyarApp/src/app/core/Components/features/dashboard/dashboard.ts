import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { AuthStateService } from '../../../services/auth.state.service';
import { AdminManagmentService } from '../../../services/admin.managment.service';
import { AdminAuthorizationService } from '../../../services/admin.authorization.service';

interface StatCard {
  title: string;
  value: string | number;
  icon: string;
  color: string;
  bgColor: string;
  description: string;
  route?: string;
}

interface Activity {
  id: number;
  title: string;
  time: string;
  status: 'success' | 'pending' | 'info';
}

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.css'
})
export class DashboardComponent implements OnInit {
  userName: string = 'کاربر عزیز';
  userRole: string = '';
  isAdmin: boolean = false;
  
  totalUsers: number = 0;
  activeUsers: number = 0;
  totalRoles: number = 0;
  
  myRequests: number = 0;
  myNotifications: number = 0;
  myTickets: number = 0;

  recentActivities: Activity[] = [];

  constructor(
    private authState: AuthStateService,
    private adminService: AdminManagmentService,
    private authService: AdminAuthorizationService
  ) {}

  ngOnInit(): void {
    this.userName = this.authState.getUserName();
    this.isAdmin = this.authState.isAdmin();
    this.userRole = this.authState.getRoles()[0] || 'User';

    if (this.isAdmin) {
      this.loadAdminDashboard();
    } else {
      this.loadUserDashboard();
    }
  }

  loadAdminDashboard(): void {
    this.adminService.getAllUsers({
      searchKey: '',
      paginationRequest: {
        pageNumber: 1,
        pageSize: 1,
        sortBy: '',
        sortDescending: false
      }
    }).subscribe({
      next: (response) => {
        if (response.data) {
          this.totalUsers = response.data.totalCount || 0;
        }
      },
      error: (err) => console.error('Error in getting all of users', err)
    });

    this.authService.getAllPermissions().subscribe({
      next: (response) => {
        this.totalRoles = 5; 
      },
      error: (err) => console.error('Error in getting all of permissions', err)
    });

    this.recentActivities = [
      { id: 1, title: 'کاربر جدید ثبت‌نام کرد', time: '۵ دقیقه پیش', status: 'info' },
      { id: 2, title: 'نقش "مدیر" به کاربر علی اختصاص یافت', time: '۱ ساعت پیش', status: 'success' },
      { id: 3, title: 'درخواست پشتیبانی جدید دریافت شد', time: '۲ ساعت پیش', status: 'pending' },
    ];
  }

  loadUserDashboard(): void {
    this.myRequests = 3;
    this.myNotifications = 5;
    this.myTickets = 1;

    this.recentActivities = [
      { id: 1, title: 'درخواست مرخصی ثبت شد', time: '۲ ساعت پیش', status: 'pending' },
      { id: 2, title: 'پروفایل کاربری به‌روزرسانی شد', time: 'دیروز', status: 'success' },
      { id: 3, title: 'پاسخ پشتیبانی دریافت شد', time: '۲ روز پیش', status: 'info' },
    ];
  }

  getStatusLabel(status: string): string {
    const labels: Record<string, string> = {
      'success': 'انجام شده',
      'pending': 'در انتظار بررسی',
      'info': 'اطلاع‌رسانی'
    };
    return labels[status] || status;
  }

  getRoleDisplayName(role: string): string {
    const roleMap: Record<string, string> = {
      'Admin': 'مدیر سیستم',
      'Manager': 'مدیر',
      'User': 'کاربر'
    };
    return roleMap[role] || role;
  }
}