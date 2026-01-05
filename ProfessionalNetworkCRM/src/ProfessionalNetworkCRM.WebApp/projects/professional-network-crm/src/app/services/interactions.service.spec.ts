import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { InteractionsService } from './interactions.service';
import { Interaction, CreateInteractionRequest } from '../models';
import { environment } from '../environments';
import { describe, it, expect, beforeEach } from 'vitest';

describe('InteractionsService', () => {
  let service: InteractionsService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [InteractionsService]
    });

    service = TestBed.inject(InteractionsService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should load interactions', () => {
    const mockInteractions: Interaction[] = [
      {
        interactionId: '1',
        contactId: '1',
        type: 'meeting',
        notes: 'Test meeting',
        date: new Date().toISOString(),
        createdAt: new Date().toISOString(),
        updatedAt: new Date().toISOString()
      }
    ];

    service.loadInteractions().subscribe(interactions => {
      expect(interactions).toEqual(mockInteractions);
    });

    const req = httpMock.expectOne(`${environment.baseUrl}/api/interactions`);
    expect(req.request.method).toBe('GET');
    req.flush(mockInteractions);
  });

  it('should get interaction by id', () => {
    const mockInteraction: Interaction = {
      interactionId: '1',
      contactId: '1',
      type: 'meeting',
      notes: 'Test meeting',
      date: new Date().toISOString(),
      createdAt: new Date().toISOString(),
      updatedAt: new Date().toISOString()
    };

    service.getInteractionById('1').subscribe(interaction => {
      expect(interaction).toEqual(mockInteraction);
    });

    const req = httpMock.expectOne(`${environment.baseUrl}/api/interactions/1`);
    expect(req.request.method).toBe('GET');
    req.flush(mockInteraction);
  });

  it('should create interaction', () => {
    const createRequest: CreateInteractionRequest = {
      contactId: '1',
      type: 'call',
      notes: 'Phone call',
      date: new Date().toISOString()
    };

    const createdInteraction: Interaction = {
      interactionId: '2',
      ...createRequest,
      createdAt: new Date().toISOString(),
      updatedAt: new Date().toISOString()
    };

    service.createInteraction(createRequest).subscribe(interaction => {
      expect(interaction).toEqual(createdInteraction);
    });

    const req = httpMock.expectOne(`${environment.baseUrl}/api/interactions`);
    expect(req.request.method).toBe('POST');
    req.flush(createdInteraction);
  });

  it('should update interaction', () => {
    const updateRequest = {
      interactionId: '1',
      contactId: '1',
      type: 'call',
      notes: 'Updated notes',
      date: new Date().toISOString()
    };

    const updatedInteraction: Interaction = {
      ...updateRequest,
      createdAt: new Date().toISOString(),
      updatedAt: new Date().toISOString()
    };

    service['interactionsSubject'].next([
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

    service.updateInteraction(updateRequest).subscribe(interaction => {
      expect(interaction).toEqual(updatedInteraction);
    });

    const req = httpMock.expectOne(`${environment.baseUrl}/api/interactions/1`);
    expect(req.request.method).toBe('PUT');
    req.flush(updatedInteraction);
  });

  it('should delete interaction', () => {
    service['interactionsSubject'].next([
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

    service.deleteInteraction('1').subscribe();

    const req = httpMock.expectOne(`${environment.baseUrl}/api/interactions/1`);
    expect(req.request.method).toBe('DELETE');
    req.flush(null);
  });

  it('should clear selected interaction', () => {
    service.clearSelectedInteraction();
    service.selectedInteraction$.subscribe(interaction => {
      expect(interaction).toBeNull();
    });
  });
});
