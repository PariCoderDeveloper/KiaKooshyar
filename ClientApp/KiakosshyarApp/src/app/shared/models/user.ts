export enum UserStatus {
  Pending = 1,
  Active = 2,
  Inactive = 3,
  Suspended = 4,
  Locked = 5,
  Block = 6,
  Unblock = 7
}

export interface User {
  id: number;
  firstName: string;
  lastName: string;
  userName: string;
  email?: string;
  phoneNumber?: string;
  roles: string[];
  permissions: string[];
  rolePermissions: string[];
  status: UserStatus;
}
