import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { RecipientsService } from './recipients.service';
import { Recipient, CreateRecipientRequest, UpdateRecipientRequest } from '../models';

describe('RecipientsService', () => {
  let service: RecipientsService;
  let httpMock: HttpTestingController;
  const baseUrl = 'http://localhost:5200';

  const mockRecipient: Recipient = {
    recipientId: 'recipient-1',
    userId: 'user-1',
    name: 'John Doe',
    relationship: 'Friend',
    createdAt: '2025-01-01T00:00:00Z'
  };

  const mockRecipients: Recipient[] = [
    mockRecipient,
    {
      recipientId: 'recipient-2',
      userId: 'user-1',
      name: 'Jane Smith',
      relationship: 'Family',
      createdAt: '2025-01-02T00:00:00Z'
    }
  ];

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [RecipientsService]
    });
    service = TestBed.inject(RecipientsService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  describe('getRecipients', () => {
    it('should return all recipients', () => {
      service.getRecipients().subscribe(recipients => {
        expect(recipients).toEqual(mockRecipients);
        expect(recipients.length).toBe(2);
      });

      const req = httpMock.expectOne(`${baseUrl}/api/recipients`);
      expect(req.request.method).toBe('GET');
      req.flush(mockRecipients);
    });

    it('should return empty array when no recipients exist', () => {
      service.getRecipients().subscribe(recipients => {
        expect(recipients).toEqual([]);
        expect(recipients.length).toBe(0);
      });

      const req = httpMock.expectOne(`${baseUrl}/api/recipients`);
      req.flush([]);
    });
  });

  describe('getRecipient', () => {
    it('should return a single recipient by id', () => {
      service.getRecipient('recipient-1').subscribe(recipient => {
        expect(recipient).toEqual(mockRecipient);
        expect(recipient.name).toBe('John Doe');
      });

      const req = httpMock.expectOne(`${baseUrl}/api/recipients/recipient-1`);
      expect(req.request.method).toBe('GET');
      req.flush(mockRecipient);
    });
  });

  describe('createRecipient', () => {
    it('should create a new recipient', () => {
      const createRequest: CreateRecipientRequest = {
        name: 'New Person',
        relationship: 'Colleague'
      };

      service.createRecipient(createRequest).subscribe(recipient => {
        expect(recipient.name).toBe('New Person');
        expect(recipient.relationship).toBe('Colleague');
      });

      const req = httpMock.expectOne(`${baseUrl}/api/recipients`);
      expect(req.request.method).toBe('POST');
      expect(req.request.body).toEqual(createRequest);
      req.flush({
        ...mockRecipient,
        name: 'New Person',
        relationship: 'Colleague'
      });
    });

    it('should create a recipient without relationship', () => {
      const createRequest: CreateRecipientRequest = {
        name: 'No Relationship'
      };

      service.createRecipient(createRequest).subscribe(recipient => {
        expect(recipient.name).toBe('No Relationship');
      });

      const req = httpMock.expectOne(`${baseUrl}/api/recipients`);
      expect(req.request.body).toEqual(createRequest);
      req.flush({
        ...mockRecipient,
        name: 'No Relationship',
        relationship: null
      });
    });
  });

  describe('updateRecipient', () => {
    it('should update an existing recipient', () => {
      const updateRequest: UpdateRecipientRequest = {
        recipientId: 'recipient-1',
        name: 'Updated Name',
        relationship: 'Updated Relationship'
      };

      service.updateRecipient(updateRequest).subscribe(recipient => {
        expect(recipient.name).toBe('Updated Name');
      });

      const req = httpMock.expectOne(`${baseUrl}/api/recipients/recipient-1`);
      expect(req.request.method).toBe('PUT');
      expect(req.request.body).toEqual(updateRequest);
      req.flush({
        ...mockRecipient,
        name: 'Updated Name',
        relationship: 'Updated Relationship'
      });
    });
  });

  describe('deleteRecipient', () => {
    it('should delete a recipient', () => {
      service.deleteRecipient('recipient-1').subscribe(result => {
        expect(result).toBeUndefined();
      });

      const req = httpMock.expectOne(`${baseUrl}/api/recipients/recipient-1`);
      expect(req.request.method).toBe('DELETE');
      req.flush(null);
    });
  });
});
