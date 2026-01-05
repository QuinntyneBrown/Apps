import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { ContactsService } from './contacts.service';
import { Contact, CreateContactRequest, UpdateContactRequest } from '../models';
import { environment } from '../environments';
import { describe, it, expect, beforeEach, afterEach } from 'vitest';

describe('ContactsService', () => {
  let service: ContactsService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [ContactsService]
    });

    service = TestBed.inject(ContactsService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  describe('loadContacts', () => {
    it('should fetch and update contacts', async () => {
      const mockContacts: Contact[] = [
        {
          contactId: '1',
          name: 'John Doe',
          email: 'john@example.com',
          phone: '123-456-7890',
          company: 'Test Company',
          position: 'Developer',
          isPriority: false,
          notes: '',
          createdAt: new Date().toISOString(),
          updatedAt: new Date().toISOString()
        }
      ];

      const loadPromise = new Promise<void>((resolve) => {
        service.loadContacts().subscribe(contacts => {
          expect(contacts).toEqual(mockContacts);
          
          service.contacts$.subscribe(contacts => {
            expect(contacts).toEqual(mockContacts);
            resolve();
          });
        });
      });

      const req = httpMock.expectOne(`${environment.baseUrl}/api/contacts`);
      expect(req.request.method).toBe('GET');
      req.flush(mockContacts);

      await loadPromise;
    });
  });

  describe('getContactById', () => {
    it('should fetch a single contact and set as selected', () => {
      const mockContact: Contact = {
        contactId: '1',
        name: 'John Doe',
        email: 'john@example.com',
        phone: '123-456-7890',
        company: 'Test Company',
        position: 'Developer',
        isPriority: false,
        notes: '',
        createdAt: new Date().toISOString(),
        updatedAt: new Date().toISOString()
      };

      service.getContactById('1').subscribe(contact => {
        expect(contact).toEqual(mockContact);
      });

      service.selectedContact$.subscribe(contact => {
        if (contact) {
          expect(contact).toEqual(mockContact);
        }
      });

      const req = httpMock.expectOne(`${environment.baseUrl}/api/contacts/1`);
      expect(req.request.method).toBe('GET');
      req.flush(mockContact);
    });
  });

  describe('createContact', () => {
    it('should create a new contact and add it to the list', () => {
      const createRequest: CreateContactRequest = {
        name: 'Jane Doe',
        email: 'jane@example.com',
        phone: '987-654-3210',
        company: 'Another Company',
        position: 'Manager',
        isPriority: true,
        notes: 'Important contact'
      };

      const createdContact: Contact = {
        contactId: '2',
        ...createRequest,
        createdAt: new Date().toISOString(),
        updatedAt: new Date().toISOString()
      };

      service.createContact(createRequest).subscribe(contact => {
        expect(contact).toEqual(createdContact);
      });

      const req = httpMock.expectOne(`${environment.baseUrl}/api/contacts`);
      expect(req.request.method).toBe('POST');
      expect(req.request.body).toEqual(createRequest);
      req.flush(createdContact);
    });
  });

  describe('updateContact', () => {
    it('should update an existing contact', () => {
      const updateRequest: UpdateContactRequest = {
        contactId: '1',
        name: 'John Updated',
        email: 'johnupdated@example.com',
        phone: '123-456-7890',
        company: 'Updated Company',
        position: 'Senior Developer',
        isPriority: true,
        notes: 'Updated notes'
      };

      const updatedContact: Contact = {
        ...updateRequest,
        createdAt: new Date().toISOString(),
        updatedAt: new Date().toISOString()
      };

      // Set initial contacts
      service['contactsSubject'].next([{
        contactId: '1',
        name: 'John Doe',
        email: 'john@example.com',
        phone: '123-456-7890',
        company: 'Test Company',
        position: 'Developer',
        isPriority: false,
        notes: '',
        createdAt: new Date().toISOString(),
        updatedAt: new Date().toISOString()
      }]);

      service.updateContact(updateRequest).subscribe(contact => {
        expect(contact).toEqual(updatedContact);
      });

      const req = httpMock.expectOne(`${environment.baseUrl}/api/contacts/1`);
      expect(req.request.method).toBe('PUT');
      expect(req.request.body).toEqual(updateRequest);
      req.flush(updatedContact);
    });
  });

  describe('deleteContact', () => {
    it('should delete a contact and remove it from the list', () => {
      // Set initial contacts
      service['contactsSubject'].next([
        {
          contactId: '1',
          name: 'John Doe',
          email: 'john@example.com',
          phone: '123-456-7890',
          company: 'Test Company',
          position: 'Developer',
          isPriority: false,
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
          isPriority: true,
          notes: '',
          createdAt: new Date().toISOString(),
          updatedAt: new Date().toISOString()
        }
      ]);

      service.deleteContact('1').subscribe();

      const req = httpMock.expectOne(`${environment.baseUrl}/api/contacts/1`);
      expect(req.request.method).toBe('DELETE');
      req.flush(null);

      service.contacts$.subscribe(contacts => {
        expect(contacts.length).toBe(1);
        expect(contacts[0].contactId).toBe('2');
      });
    });
  });

  describe('clearSelectedContact', () => {
    it('should clear the selected contact', () => {
      service['selectedContactSubject'].next({
        contactId: '1',
        name: 'John Doe',
        email: 'john@example.com',
        phone: '123-456-7890',
        company: 'Test Company',
        position: 'Developer',
        isPriority: false,
        notes: '',
        createdAt: new Date().toISOString(),
        updatedAt: new Date().toISOString()
      });

      service.clearSelectedContact();

      service.selectedContact$.subscribe(contact => {
        expect(contact).toBeNull();
      });
    });
  });
});
