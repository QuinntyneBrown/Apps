import { TestBed } from '@angular/core/testing';
import { ReactiveFormsModule } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { Login } from './login.component';
import { AuthService } from '../../services';
import { of, throwError } from 'rxjs';
import { describe, it, expect, beforeEach, vi } from 'vitest';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';

describe('Login', () => {
  let component: Login;
  let authService: any;
  let router: any;
  let activatedRoute: any;

  beforeEach(async () => {
    authService = {
      login: vi.fn()
    };

    router = {
      navigate: vi.fn()
    };

    activatedRoute = {
      snapshot: {
        queryParams: {}
      }
    };

    await TestBed.configureTestingModule({
      imports: [Login, ReactiveFormsModule, NoopAnimationsModule],
      providers: [
        { provide: AuthService, useValue: authService },
        { provide: Router, useValue: router },
        { provide: ActivatedRoute, useValue: activatedRoute }
      ]
    }).compileComponents();

    const fixture = TestBed.createComponent(Login);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should initialize login form with empty values', () => {
    expect(component.loginForm.value).toEqual({
      username: '',
      password: ''
    });
  });

  it('should mark form as invalid when empty', () => {
    expect(component.loginForm.valid).toBe(false);
  });

  it('should mark form as valid when filled correctly', () => {
    component.loginForm.patchValue({
      username: 'testuser',
      password: 'password123'
    });
    expect(component.loginForm.valid).toBe(true);
  });

  it('should toggle password visibility', () => {
    expect(component.hidePassword).toBe(true);
    component.togglePasswordVisibility();
    expect(component.hidePassword).toBe(false);
    component.togglePasswordVisibility();
    expect(component.hidePassword).toBe(true);
  });

  it('should not submit if form is invalid', () => {
    component.onSubmit();
    expect(authService.login).not.toHaveBeenCalled();
  });

  it('should login successfully and navigate to home', () => {
    const loginResponse = {
      token: 'test-token',
      user: {
        userId: '1',
        username: 'testuser',
        email: 'test@example.com',
        roles: ['user']
      }
    };

    authService.login.mockReturnValue(of(loginResponse));

    component.loginForm.patchValue({
      username: 'testuser',
      password: 'password123'
    });

    component.onSubmit();

    expect(component.isLoading).toBe(true);
    expect(authService.login).toHaveBeenCalledWith({
      username: 'testuser',
      password: 'password123'
    });
    expect(router.navigate).toHaveBeenCalledWith(['/']);
  });

  it('should navigate to returnUrl after login', () => {
    activatedRoute.snapshot.queryParams = { returnUrl: '/dashboard' };
    
    const loginResponse = {
      token: 'test-token',
      user: {
        userId: '1',
        username: 'testuser',
        email: 'test@example.com',
        roles: ['user']
      }
    };

    authService.login.mockReturnValue(of(loginResponse));

    component.loginForm.patchValue({
      username: 'testuser',
      password: 'password123'
    });

    component.onSubmit();

    expect(router.navigate).toHaveBeenCalledWith(['/dashboard']);
  });

  it('should handle login error', () => {
    authService.login.mockReturnValue(
      throwError(() => ({
        error: { error: 'Invalid credentials' }
      }))
    );

    component.loginForm.patchValue({
      username: 'testuser',
      password: 'wrongpassword'
    });

    component.onSubmit();

    expect(component.isLoading).toBe(false);
    expect(component.errorMessage).toBe('Invalid credentials');
  });

  it('should show default error message when error details not provided', () => {
    authService.login.mockReturnValue(
      throwError(() => ({}))
    );

    component.loginForm.patchValue({
      username: 'testuser',
      password: 'wrongpassword'
    });

    component.onSubmit();

    expect(component.errorMessage).toBe('Invalid username or password. Please try again.');
  });
});
