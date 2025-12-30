import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments';
import { RoleDto, CreateRoleCommand, UpdateRoleCommand } from '../models/role-dto';

@Injectable({ providedIn: 'root' })
export class RolesService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = environment.apiBaseUrl;

  getRoles(): Observable<RoleDto[]> {
    return this.http.get<RoleDto[]>(`${this.baseUrl}/api/roles`);
  }

  getRoleById(roleId: string): Observable<RoleDto> {
    return this.http.get<RoleDto>(`${this.baseUrl}/api/roles/${roleId}`);
  }

  createRole(command: CreateRoleCommand): Observable<RoleDto> {
    return this.http.post<RoleDto>(`${this.baseUrl}/api/roles`, command);
  }

  updateRole(roleId: string, command: UpdateRoleCommand): Observable<RoleDto> {
    return this.http.put<RoleDto>(`${this.baseUrl}/api/roles/${roleId}`, command);
  }

  deleteRole(roleId: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/api/roles/${roleId}`);
  }
}
