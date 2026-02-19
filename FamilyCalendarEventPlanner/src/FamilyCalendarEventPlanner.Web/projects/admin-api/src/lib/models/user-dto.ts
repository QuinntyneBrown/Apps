export interface UserDto {
  userId: string;
  userName: string;
  email: string;
  roles: RoleDto[];
}

export interface RoleDto {
  roleId: string;
  name: string;
}

export interface CreateUserCommand {
  userName: string;
  email: string;
  password: string;
  roleIds?: string[];
}

export interface UpdateUserCommand {
  userId: string;
  userName?: string;
  email?: string;
  password?: string;
}

export interface LoginCommand {
  username: string;
  password: string;
}

export interface LoginResult {
  token: string;
  expiresAt: string;
  user: UserInfo;
}

export interface UserInfo {
  userId: string;
  userName: string;
  email: string;
  roles: string[];
}
