import { TestBed } from '@angular/core/testing';
import { ActivatedRoute } from '@angular/router';
import { Dashboard } from './dashboard.component';
import { ContactsService, FollowUpsService, InteractionsService } from '../../services';
import { BehaviorSubject } from 'rxjs';
import { describe, it, expect, beforeEach } from 'vitest';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';

describe('Dashboard', () => {
  let component: Dashboard;
  let contactsService: any;
  let followUpsService: any;
  let interactionsService: any;

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

    const interactionsSubject = new BehaviorSubject([
      {
        interactionId: '1',
        contactId: '1',
        type: 'meeting',
        notes: 'Test meeting',
        date: new Date().toISOString(),
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

    interactionsService = {
      interactions$: interactionsSubject.asObservable()
    };

    const activatedRoute = {
      snapshot: {
        params: {}
      }
    };

    await TestBed.configureTestingModule({
      imports: [Dashboard, NoopAnimationsModule],
      providers: [
        { provide: ContactsService, useValue: contactsService },
        { provide: FollowUpsService, useValue: followUpsService },
        { provide: InteractionsService, useValue: interactionsService },
        { provide: ActivatedRoute, useValue: activatedRoute }
      ]
    }).compileComponents();

    const fixture = TestBed.createComponent(Dashboard);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should calculate correct stats from services', async () => {
    const viewModel = await new Promise<any>((resolve) => {
      component.viewModel$.subscribe(vm => resolve(vm));
    });

    expect(viewModel.contactsCount).toBe(2);
    expect(viewModel.priorityContacts).toBe(1);
    expect(viewModel.pendingFollowUps).toBe(1);
    expect(viewModel.interactionsCount).toBe(1);
  });

  it('should update when contacts change', async () => {
    const viewModel = await new Promise<any>((resolve) => {
      component.viewModel$.subscribe(vm => {
        if (vm.contactsCount === 2) {
          resolve(vm);
        }
      });
    });

    expect(viewModel.priorityContacts).toBe(1);
  });
});
