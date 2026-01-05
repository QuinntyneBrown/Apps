import { TestBed } from '@angular/core/testing';
import { Header } from './header.component';
import { AuthService } from '../../services';
import { describe, it, expect, beforeEach, vi } from 'vitest';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { ActivatedRoute } from '@angular/router';

describe('Header', () => {
  let component: Header;
  let authService: any;

  beforeEach(async () => {
    authService = {
      logout: vi.fn(),
      currentUser: vi.fn(() => ({ userId: '1', username: 'testuser' })),
      isAuthenticated: vi.fn(() => true)
    };

    const activatedRoute = {
      snapshot: {
        params: {}
      }
    };

    await TestBed.configureTestingModule({
      imports: [Header, NoopAnimationsModule],
      providers: [
        { provide: AuthService, useValue: authService },
        { provide: ActivatedRoute, useValue: activatedRoute }
      ]
    }).compileComponents();

    const fixture = TestBed.createComponent(Header);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should have authService', () => {
    expect(component.authService).toBeDefined();
  });

  it('should call authService.logout when onLogout is called', () => {
    component.onLogout();
    expect(authService.logout).toHaveBeenCalled();
  });
});
