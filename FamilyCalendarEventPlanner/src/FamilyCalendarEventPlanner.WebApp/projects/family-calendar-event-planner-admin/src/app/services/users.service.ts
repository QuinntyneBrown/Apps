import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments';
import { UserDto, CreateUserCommand, UpdateUserCommand } from '../models/user-dto';

@Injectable({ providedIn: 'root' })
export class UsersService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = environment.apiBaseUrl;

  getUsers(): Observable<UserDto[]> {
    return this.http.get<UserDto[]>(`${this.baseUrl}/api/users`);
  }

  getUserById(userId: string): Observable<UserDto> {
    return this.http.get<UserDto>(`${this.baseUrl}/api/users/${userId}`);
  }

  createUser(command: CreateUserCommand): Observable<UserDto> {
    return this.http.post<UserDto>(`${this.baseUrl}/api/users`, command);
  }

  updateUser(userId: string, command: UpdateUserCommand): Observable<UserDto> {
    return this.http.put<UserDto>(`${this.baseUrl}/api/users/${userId}`, command);
  }

  deleteUser(userId: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/api/users/${userId}`);
  }

  addRoleToUser(userId: string, roleId: string): Observable<UserDto> {
    return this.http.post<UserDto>(`${this.baseUrl}/api/users/${userId}/roles`, { roleId });
  }

  removeRoleFromUser(userId: string, roleId: string): Observable<UserDto> {
    return this.http.delete<UserDto>(`${this.baseUrl}/api/users/${userId}/roles/${roleId}`);
  }
}
