import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { FamilyMembersService } from './family-members.service';
import { FamilyMember, CreateFamilyMemberRequest, MemberRole, RelationType } from './models';

describe('FamilyMembersService', () => {
  let service: FamilyMembersService;
  let httpMock: HttpTestingController;
  const baseUrl = 'http://localhost:3200';

  const mockMember: FamilyMember = {
    memberId: '1',
    familyId: 'family1',
    name: 'John Doe',
    email: 'john@example.com',
    color: '#3b82f6',
    role: MemberRole.Admin,
    isImmediate: true,
    relationType: RelationType.Self
  };

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [FamilyMembersService]
    });
    service = TestBed.inject(FamilyMembersService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  describe('getFamilyMembers', () => {
    it('should get all family members', () => {
      service.getFamilyMembers().subscribe(members => {
        expect(members).toEqual([mockMember]);
      });

      const req = httpMock.expectOne(`${baseUrl}/api/familymembers`);
      expect(req.request.method).toBe('GET');
      req.flush([mockMember]);
    });

    it('should get family members with familyId filter', () => {
      service.getFamilyMembers({ familyId: 'family1' }).subscribe(members => {
        expect(members).toEqual([mockMember]);
      });

      const req = httpMock.expectOne(`${baseUrl}/api/familymembers?familyId=family1`);
      expect(req.request.method).toBe('GET');
      req.flush([mockMember]);
    });

    it('should get immediate family members only', () => {
      service.getFamilyMembers({ isImmediate: true }).subscribe(members => {
        expect(members).toEqual([mockMember]);
      });

      const req = httpMock.expectOne(`${baseUrl}/api/familymembers?isImmediate=true`);
      expect(req.request.method).toBe('GET');
      req.flush([mockMember]);
    });

    it('should get extended family members only', () => {
      service.getFamilyMembers({ isImmediate: false }).subscribe(members => {
        expect(members).toEqual([mockMember]);
      });

      const req = httpMock.expectOne(`${baseUrl}/api/familymembers?isImmediate=false`);
      expect(req.request.method).toBe('GET');
      req.flush([mockMember]);
    });

    it('should get family members with multiple filters', () => {
      service.getFamilyMembers({ familyId: 'family1', isImmediate: true }).subscribe(members => {
        expect(members).toEqual([mockMember]);
      });

      const req = httpMock.expectOne(`${baseUrl}/api/familymembers?familyId=family1&isImmediate=true`);
      expect(req.request.method).toBe('GET');
      req.flush([mockMember]);
    });
  });

  describe('getFamilyMemberById', () => {
    it('should get member by id', () => {
      service.getFamilyMemberById('1').subscribe(member => {
        expect(member).toEqual(mockMember);
      });

      const req = httpMock.expectOne(`${baseUrl}/api/familymembers/1`);
      expect(req.request.method).toBe('GET');
      req.flush(mockMember);
    });
  });

  describe('createFamilyMember', () => {
    it('should create a new family member', () => {
      const createRequest: CreateFamilyMemberRequest = {
        familyId: 'family1',
        name: 'Jane Doe',
        email: 'jane@example.com',
        color: '#10b981',
        role: MemberRole.Member,
        isImmediate: true,
        relationType: RelationType.Spouse
      };

      service.createFamilyMember(createRequest).subscribe(member => {
        expect(member).toEqual(mockMember);
      });

      const req = httpMock.expectOne(`${baseUrl}/api/familymembers`);
      expect(req.request.method).toBe('POST');
      expect(req.request.body).toEqual(createRequest);
      req.flush(mockMember);
    });

    it('should create a family member without email', () => {
      const createRequest: CreateFamilyMemberRequest = {
        familyId: 'family1',
        name: 'Baby Doe',
        color: '#10b981',
        role: MemberRole.Member,
        isImmediate: true,
        relationType: RelationType.Child
      };

      service.createFamilyMember(createRequest).subscribe(member => {
        expect(member).toBeTruthy();
      });

      const req = httpMock.expectOne(`${baseUrl}/api/familymembers`);
      expect(req.request.method).toBe('POST');
      expect(req.request.body).toEqual(createRequest);
      req.flush({ ...mockMember, email: null });
    });
  });

  describe('updateFamilyMember', () => {
    it('should update a family member', () => {
      const updateRequest = { memberId: '1', name: 'Updated Name' };

      service.updateFamilyMember(updateRequest).subscribe(member => {
        expect(member).toEqual(mockMember);
      });

      const req = httpMock.expectOne(`${baseUrl}/api/familymembers/1`);
      expect(req.request.method).toBe('PUT');
      req.flush(mockMember);
    });

    it('should update isImmediate and relationType', () => {
      const updateRequest = {
        memberId: '1',
        isImmediate: false,
        relationType: RelationType.Cousin
      };

      service.updateFamilyMember(updateRequest).subscribe(member => {
        expect(member).toBeTruthy();
      });

      const req = httpMock.expectOne(`${baseUrl}/api/familymembers/1`);
      expect(req.request.method).toBe('PUT');
      req.flush({ ...mockMember, isImmediate: false, relationType: RelationType.Cousin });
    });
  });

  describe('changeMemberRole', () => {
    it('should change member role', () => {
      const roleRequest = { memberId: '1', role: MemberRole.Admin };

      service.changeMemberRole(roleRequest).subscribe(member => {
        expect(member).toEqual(mockMember);
      });

      const req = httpMock.expectOne(`${baseUrl}/api/familymembers/1/role`);
      expect(req.request.method).toBe('PUT');
      req.flush(mockMember);
    });
  });
});
