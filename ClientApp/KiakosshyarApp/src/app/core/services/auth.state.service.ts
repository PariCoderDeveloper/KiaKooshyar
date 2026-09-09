import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthStateService {

  private roles: string[] = [];
  private permissions: string[] = [];
  private userName: string = 'Guest';
  private userId: number | null = null;

  setRoles(roles: string[]): void {
    this.roles = roles;
  }

  setPermissions(permissions: string[]): void {
    this.permissions = permissions;
  }

  setUserInfo(name: string, id: number): void {
    this.userName = name;
    this.userId = id;
  }

  getRoles(): string[] {
    return this.roles;
  }

  getPermissions(): string[] {
    return this.permissions;
  }

  getUserName(): string {
    return this.userName;
  }

  getUserId(): number | null {
    return this.userId;
  }

  hasRole(role: string): boolean {
    return this.roles.includes(role);
  }

  hasPermission(permission: string): boolean {
    return this.permissions.includes(permission);
  }

  isAdmin(): boolean {
    return this.roles.includes('Admin');
  }

  clear(): void {
    this.roles = [];
    this.permissions = [];
    this.userName = 'Guest';
    this.userId = null;
  }
}