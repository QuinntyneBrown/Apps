import { TestBed } from '@angular/core/testing';
import { Sidebar } from './sidebar.component';
import { ContactsService, FollowUpsService } from '../../services';
import { BehaviorSubject } from 'rxjs';
import { describe, it, expect, beforeEach } from 'vitest';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { ActivatedRoute } from '@angular/router';

describe('Sidebar', () => {
  let component: Sidebar;
  let contactsService: any;
  let followUpsService: any;

  beforeEach(async () => {
    const contactsSubject = new BehaviorSubject([
      {
        contactId: '1',
        name: 'John Doe',
        email: 'john@example.com',
        phone: '123-456-7890',
        company: 'Test Company',
        position: 'Developer',
        isPriority: true,
        notes: '',
        createdAt: new Date().toISOString(),
        updatedAt: new Date().toISOString()
      },
      {
        contactId: '2',
        name: 'Jane Doe',
        email: 'jane@example.com',
        phone: '987-654-3210',
        company: 'Another Company',
        position: 'Manager',
        isPriority: false,
        notes: '',
        createdAt: new Date().toISOString(),
        updatedAt: new Date().toISOString()
      }
    ]);

    const followUpsSubject = new BehaviorSubject([
      {
        followUpId: '1',
        contactId: '1',
        dueDate: new Date().toISOString(),
        notes: 'Test follow-up',
        isCompleted: false,
        createdAt: new Date().toISOString(),
        updatedAt: new Date().toISOString()
      },
      {
        followUpId: '2',
        contactId: '2',
        dueDate: new Date().toISOString(),
        notes: 'Completed follow-up',
        isCompleted: true,
        createdAt: new Date().toISOString(),
        updatedAt: new Date().toISOString()
      }
    ]);

    contactsService = {
      contacts$: contactsSubject.asObservable()
    };

    followUpsService = {
      followUps$: followUpsSubject.asObservable()
    };

    const activatedRoute = {
      snapshot: {
        params: {}
      }
    };

    await TestBed.configureTestingModule({
      imports: [Sidebar, NoopAnimationsModule],
      providers: [
        { provide: ContactsService, useValue: contactsService },
        { provide: FollowUpsService, useValue: followUpsService },
        { provide: ActivatedRoute, useValue: activatedRoute }
      ]
    }).compileComponents();

    const fixture = TestBed.createComponent(Sidebar);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should have menu items', () => {
    expect(component.menuItems).toBeDefined();
    expect(component.menuItems.length).toBeGreaterThan(0);
  });

  it('should calculate contacts count', async () => {
    const count = await new Promise<number>((resolve) => {
      component.contactsCount$.subscribe(count => resolve(count));
    });
    expect(count).toBe(2);
  });

  it('should calculate pending follow-ups count', async () => {
    const count = await new Promise<number>((resolve) => {
      component.followUpsCount$.subscribe(count => resolve(count));
    });
    expect(count).toBe(1); // Only non-completed follow-ups
  });

  it('should have dashboard menu item with exact route matching', () => {
    const dashboardItem = component.menuItems.find(item => item.path === '/');
    expect(dashboardItem).toBeDefined();
    expect(dashboardItem?.exact).toBe(true);
  });

  it('should have contacts menu item with count observable', () => {
    const contactsItem = component.menuItems.find(item => item.path === '/contacts');
    expect(contactsItem).toBeDefined();
    expect(contactsItem?.count$).toBeDefined();
  });

  it('should have follow-ups menu item with count observable', () => {
    const followUpsItem = component.menuItems.find(item => item.path === '/follow-ups');
    expect(followUpsItem).toBeDefined();
    expect(followUpsItem?.count$).toBeDefined();
  });
});
