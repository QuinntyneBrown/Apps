import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { FollowUpsService } from './follow-ups.service';
import { FollowUp, CreateFollowUpRequest } from '../models';
import { environment } from '../environments';
import { describe, it, expect, beforeEach } from 'vitest';

describe('FollowUpsService', () => {
  let service: FollowUpsService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [FollowUpsService]
    });

    service = TestBed.inject(FollowUpsService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should load follow-ups', () => {
    const mockFollowUps: FollowUp[] = [
      {
        followUpId: '1',
        contactId: '1',
        dueDate: new Date().toISOString(),
        notes: 'Test follow-up',
        isCompleted: false,
        createdAt: new Date().toISOString(),
        updatedAt: new Date().toISOString()
      }
    ];

    service.loadFollowUps().subscribe(followUps => {
      expect(followUps).toEqual(mockFollowUps);
    });

    const req = httpMock.expectOne(`${environment.baseUrl}/api/followups`);
    expect(req.request.method).toBe('GET');
    req.flush(mockFollowUps);
  });

  it('should get follow-up by id', () => {
    const mockFollowUp: FollowUp = {
      followUpId: '1',
      contactId: '1',
      dueDate: new Date().toISOString(),
      notes: 'Test follow-up',
      isCompleted: false,
      createdAt: new Date().toISOString(),
      updatedAt: new Date().toISOString()
    };

    service.getFollowUpById('1').subscribe(followUp => {
      expect(followUp).toEqual(mockFollowUp);
    });

    const req = httpMock.expectOne(`${environment.baseUrl}/api/followups/1`);
    expect(req.request.method).toBe('GET');
    req.flush(mockFollowUp);
  });

  it('should create follow-up', () => {
    const createRequest: CreateFollowUpRequest = {
      contactId: '1',
      dueDate: new Date().toISOString(),
      notes: 'New follow-up'
    };

    const createdFollowUp: FollowUp = {
      followUpId: '2',
      ...createRequest,
      isCompleted: false,
      createdAt: new Date().toISOString(),
      updatedAt: new Date().toISOString()
    };

    service.createFollowUp(createRequest).subscribe(followUp => {
      expect(followUp).toEqual(createdFollowUp);
    });

    const req = httpMock.expectOne(`${environment.baseUrl}/api/followups`);
    expect(req.request.method).toBe('POST');
    req.flush(createdFollowUp);
  });

  it('should complete follow-up', () => {
    const completedFollowUp: FollowUp = {
      followUpId: '1',
      contactId: '1',
      dueDate: new Date().toISOString(),
      notes: 'Test follow-up',
      isCompleted: true,
      completedAt: new Date().toISOString(),
      createdAt: new Date().toISOString(),
      updatedAt: new Date().toISOString()
    };

    service['followUpsSubject'].next([
      {
        followUpId: '1',
        contactId: '1',
        dueDate: new Date().toISOString(),
        notes: 'Test follow-up',
        isCompleted: false,
        createdAt: new Date().toISOString(),
        updatedAt: new Date().toISOString()
      }
    ]);

    service.completeFollowUp('1').subscribe(followUp => {
      expect(followUp).toEqual(completedFollowUp);
    });

    const req = httpMock.expectOne(`${environment.baseUrl}/api/followups/1/complete`);
    expect(req.request.method).toBe('POST');
    req.flush(completedFollowUp);
  });

  it('should delete follow-up', () => {
    service['followUpsSubject'].next([
      {
        followUpId: '1',
        contactId: '1',
        dueDate: new Date().toISOString(),
        notes: 'Test follow-up',
        isCompleted: false,
        createdAt: new Date().toISOString(),
        updatedAt: new Date().toISOString()
      }
    ]);

    service.deleteFollowUp('1').subscribe();

    const req = httpMock.expectOne(`${environment.baseUrl}/api/followups/1`);
    expect(req.request.method).toBe('DELETE');
    req.flush(null);
  });

  it('should clear selected follow-up', () => {
    service.clearSelectedFollowUp();
    service.selectedFollowUp$.subscribe(followUp => {
      expect(followUp).toBeNull();
    });
  });
});
