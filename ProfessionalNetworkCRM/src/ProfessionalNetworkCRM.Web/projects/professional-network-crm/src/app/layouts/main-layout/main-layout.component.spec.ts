import { TestBed } from '@angular/core/testing';
import { MainLayout } from './main-layout.component';
import { describe, it, expect, beforeEach } from 'vitest';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { ActivatedRoute } from '@angular/router';
import { ContactsService, FollowUpsService } from '../../services';
import { BehaviorSubject } from 'rxjs';

describe('MainLayout', () => {
  let component: MainLayout;

  beforeEach(async () => {
    const contactsSubject = new BehaviorSubject([]);
    const followUpsSubject = new BehaviorSubject([]);

    const contactsService = {
      contacts$: contactsSubject.asObservable()
    };

    const followUpsService = {
      followUps$: followUpsSubject.asObservable()
    };

    const activatedRoute = {
      snapshot: {
        params: {}
      }
    };

    await TestBed.configureTestingModule({
      imports: [MainLayout, NoopAnimationsModule],
      providers: [
        { provide: ContactsService, useValue: contactsService },
        { provide: FollowUpsService, useValue: followUpsService },
        { provide: ActivatedRoute, useValue: activatedRoute }
      ]
    }).compileComponents();

    const fixture = TestBed.createComponent(MainLayout);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
