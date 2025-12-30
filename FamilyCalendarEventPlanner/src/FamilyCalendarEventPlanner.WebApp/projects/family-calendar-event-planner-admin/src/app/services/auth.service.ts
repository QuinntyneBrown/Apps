import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../../environments';
import { LoginCommand, LoginResult, UserInfo } from '../models/user-dto';

const TOKEN_KEY = 'auth_token';
const USER_KEY = 'auth_user';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = environment.apiBaseUrl;

  private currentUserSubject = new BehaviorSubject<UserInfo | null>(this.getStoredUser());
  currentUser$ = this.currentUserSubject.asObservable();

  get isLoggedIn(): boolean {
    return !!this.getToken();
  }

  get currentUser(): UserInfo | null {
    return this.currentUserSubject.value;
  }

  login(command: LoginCommand): Observable<LoginResult> {
    return this.http.post<LoginResult>(`${this.baseUrl}/api/auth/login`, command).pipe(
      tap(result => {
        this.setToken(result.token);
        this.setStoredUser(result.user);
        this.currentUserSubject.next(result.user);
      })
    );
  }

  logout(): void {
    localStorage.removeItem(TOKEN_KEY);
    localStorage.removeItem(USER_KEY);
    this.currentUserSubject.next(null);
  }

  getToken(): string | null {
    return localStorage.getItem(TOKEN_KEY);
  }

  private setToken(token: string): void {
    localStorage.setItem(TOKEN_KEY, token);
  }

  private getStoredUser(): UserInfo | null {
    const userJson = localStorage.getItem(USER_KEY);
    if (userJson) {
      try {
        return JSON.parse(userJson);
      } catch {
        return null;
      }
    }
    return null;
  }

  private setStoredUser(user: UserInfo): void {
    localStorage.setItem(USER_KEY, JSON.stringify(user));
  }

  hasRole(role: string): boolean {
    return this.currentUser?.roles?.includes(role) ?? false;
  }

  isAdmin(): boolean {
    return this.hasRole('Admin');
  }
}
