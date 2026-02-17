export interface LoginRequest {
  username: string;
  password: string;
}

export interface LoginResponse {
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
