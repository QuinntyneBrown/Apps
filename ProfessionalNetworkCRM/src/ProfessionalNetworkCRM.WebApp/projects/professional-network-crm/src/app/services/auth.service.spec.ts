import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { Router } from '@angular/router';
import { AuthService } from './auth.service';
import { LoginRequest, LoginResponse } from '../models';
import { environment } from '../environments';
import { describe, it, expect, beforeEach, vi } from 'vitest';

describe('AuthService', () => {
  let service: AuthService;
  let httpMock: HttpTestingController;
  let router: Router;

  beforeEach(() => {
    const routerMock = {
      navigate: vi.fn()
    };

    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [
        AuthService,
        { provide: Router, useValue: routerMock }
      ]
    });

    service = TestBed.inject(AuthService);
    httpMock = TestBed.inject(HttpTestingController);
    router = TestBed.inject(Router);

    // Clear localStorage before each test
    localStorage.clear();
  });

  afterEach(() => {
    httpMock.verify();
    localStorage.clear();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  describe('login', () => {
    it('should login successfully and store token', () => {
      const loginRequest: LoginRequest = {
        username: 'testuser',
        password: 'password123'
      };

      const loginResponse: LoginResponse = {
        token: 'test-token',
        user: {
          userId: '1',
          username: 'testuser',
          email: 'test@example.com',
          roles: ['user']
        }
      };

      service.login(loginRequest).subscribe(response => {
        expect(response).toEqual(loginResponse);
        expect(service.isAuthenticated()).toBe(true);
        expect(service.currentUser()).toEqual(loginResponse.user);
        expect(localStorage.getItem('auth_token')).toBe('test-token');
      });

      const req = httpMock.expectOne(`${environment.baseUrl}/api/auth/login`);
      expect(req.request.method).toBe('POST');
      expect(req.request.body).toEqual(loginRequest);
      req.flush(loginResponse);
    });
  });

  describe('logout', () => {
    it('should clear authentication state and navigate to login', () => {
      // Set up initial state
      localStorage.setItem('auth_token', 'test-token');
      localStorage.setItem('user_info', JSON.stringify({ userId: '1', username: 'test' }));
      
      service.logout();

      expect(localStorage.getItem('auth_token')).toBeNull();
      expect(localStorage.getItem('user_info')).toBeNull();
      expect(service.isAuthenticated()).toBe(false);
      expect(service.currentUser()).toBeNull();
      expect(router.navigate).toHaveBeenCalledWith(['/login']);
    });
  });

  describe('getToken', () => {
    it('should return token from localStorage', () => {
      localStorage.setItem('auth_token', 'test-token');
      expect(service.getToken()).toBe('test-token');
    });

    it('should return null if no token exists', () => {
      expect(service.getToken()).toBeNull();
    });
  });

  describe('hasRole', () => {
    it('should return true if user has the role', () => {
      service.currentUser.set({
        userId: '1',
        username: 'test',
        email: 'test@example.com',
        roles: ['admin', 'user']
      });

      expect(service.hasRole('admin')).toBe(true);
      expect(service.hasRole('user')).toBe(true);
    });

    it('should return false if user does not have the role', () => {
      service.currentUser.set({
        userId: '1',
        username: 'test',
        email: 'test@example.com',
        roles: ['user']
      });

      expect(service.hasRole('admin')).toBe(false);
    });

    it('should return false if user is null', () => {
      service.currentUser.set(null);
      expect(service.hasRole('admin')).toBe(false);
    });
  });

  describe('hasAnyRole', () => {
    it('should return true if user has any of the roles', () => {
      service.currentUser.set({
        userId: '1',
        username: 'test',
        email: 'test@example.com',
        roles: ['user']
      });

      expect(service.hasAnyRole(['admin', 'user'])).toBe(true);
    });

    it('should return false if user has none of the roles', () => {
      service.currentUser.set({
        userId: '1',
        username: 'test',
        email: 'test@example.com',
        roles: ['user']
      });

      expect(service.hasAnyRole(['admin', 'moderator'])).toBe(false);
    });
  });
});
