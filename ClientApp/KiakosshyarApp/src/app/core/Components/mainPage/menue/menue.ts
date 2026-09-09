import { Component, OnInit } from '@angular/core'; 
import { Router, RouterLink, RouterLinkActive } from '@angular/router'; 
import { CommonModule } from '@angular/common'; 
import { AuthStateService } from '../../../services/auth.state.service'; 
import { AuthService } from '../../../services/auth.service';

export interface MenuItem { 
  title: string; 
  icon: string; 
  route?: string; 
  roles?: string[]; 
  children?: MenuItem[]; 
  expanded?: boolean; 
} 

@Component({ 
  selector: 'app-menu', 
  standalone: true, 
  imports: [ 
    CommonModule, 
    RouterLink, 
    RouterLinkActive 
  ], 
  templateUrl: './menue.html', 
  styleUrl: './menue.css' 
}) 
export class MenuComponent implements OnInit { 
  private userRoles: string[] = []; 
  
  menus: MenuItem[] = [ 
    { 
      title: 'صفحه اصلی', 
      icon: '🏠', 
      roles: ['Admin', 'User', 'Manager'] 
    },
    { 
      title: 'حساب کاربری', 
      icon: '👤', 
      route: '/profile', 
      roles: ['Admin', 'User', 'Manager'] 
    },
    { 
      title: 'فعالیت‌های من', 
      icon: '📋', 
      roles: ['User', 'Manager', 'Admin'], 
      children: [
        { 
          title: 'درخواست‌ها', 
          icon: '📝', 
          route: '/my-requests', 
          roles: ['User', 'Manager', 'Admin'] 
        }, 
        { 
          title: 'پیام‌ها و اعلان‌ها', 
          icon: '🔔', 
          route: '/notifications', 
          roles: ['User', 'Manager', 'Admin'] 
        }
      ] 
    },
    { 
      title: 'پشتیبانی', 
      icon: '🎧', 
      route: '/support', 
      roles: ['User', 'Manager', 'Admin'] 
    },
    { 
      title: 'مدیریت سیستم', 
      icon: '⚙️', 
      roles: ['Admin', 'Manager'], 
      children: [
        { 
          title: 'لیست کاربران', 
          icon: '👥', 
          route: '/users', 
          roles: ['Admin', 'Manager'] 
        }, 
        { 
          title: 'مدیریت نقش‌ها و دسترسی‌ها', 
          icon: '🔒', 
          route: '/roles', 
          roles: ['Admin'] 
        }
      ] 
    }, 
    { 
      title: 'گزارش‌ها و آمار', 
      icon: '📊', 
      route: '/reports', 
      roles: ['Admin', 'Manager'] 
    },
    { 
      title: 'تنظیمات پیشرفته', 
      icon: '🛠️', 
      route: '/system-settings', 
      roles: ['Admin'] // فقط ادمین
    }
  ]; 

  constructor( 
    private router: Router, 
    private aithService:AuthService,
    private authState: AuthStateService 
  ) {} 

  ngOnInit(): void { 
    // دریافت نقش‌های کاربر از سرویس احراز هویت
    this.userRoles = this.authState.getRoles() || []; 
  } 

  // متد کمکی برای بررسی دسترسی کاربر به یک آیتم منو
  hasAccess(itemRoles?: string[]): boolean { 
    if (!itemRoles || itemRoles.length === 0) return true; 
    return itemRoles.some(role => this.userRoles.includes(role)); 
  } 

  get filteredMenus(): MenuItem[] { 
    return this.menus
      .filter(menu => this.hasAccess(menu.roles))
      .map(menu => { 
        if (menu.children) { 
          return { 
            ...menu, 
            children: menu.children.filter(child => this.hasAccess(child.roles)) 
          }; 
        } 
        return menu; 
      })
      .filter(menu => !menu.children || menu.children.length > 0); 
  } 

  toggleMenu(menu: MenuItem): void { 
    if (menu.children) { 
      menu.expanded = !menu.expanded; 
    } 
  } 

  logout(): void { 
    this.aithService.logout(); 
    this.router.navigate(['/login']); 
  } 
}