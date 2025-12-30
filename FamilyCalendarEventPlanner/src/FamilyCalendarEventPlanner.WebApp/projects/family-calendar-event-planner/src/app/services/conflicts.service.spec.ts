import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { ConflictsService } from './conflicts.service';
import { ScheduleConflict, ConflictSeverity } from './models';

describe('ConflictsService', () => {
  let service: ConflictsService;
  let httpMock: HttpTestingController;
  const baseUrl = 'http://localhost:3200';

  const mockConflict: ScheduleConflict = {
    conflictId: '1',
    conflictingEventIds: ['event1', 'event2'],
    affectedMemberIds: ['member1'],
    conflictSeverity: ConflictSeverity.High,
    isResolved: false,
    resolvedAt: null
  };

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [ConflictsService]
    });
    service = TestBed.inject(ConflictsService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  describe('getConflicts', () => {
    it('should get all conflicts', () => {
      service.getConflicts().subscribe(conflicts => {
        expect(conflicts).toEqual([mockConflict]);
      });

      const req = httpMock.expectOne(`${baseUrl}/api/conflicts`);
      expect(req.request.method).toBe('GET');
      req.flush([mockConflict]);
    });

    it('should filter by memberId', () => {
      service.getConflicts('member1').subscribe(conflicts => {
        expect(conflicts).toEqual([mockConflict]);
      });

      const req = httpMock.expectOne(`${baseUrl}/api/conflicts?memberId=member1`);
      expect(req.request.method).toBe('GET');
      req.flush([mockConflict]);
    });

    it('should filter by isResolved', () => {
      service.getConflicts(undefined, false).subscribe(conflicts => {
        expect(conflicts).toEqual([mockConflict]);
      });

      const req = httpMock.expectOne(`${baseUrl}/api/conflicts?isResolved=false`);
      expect(req.request.method).toBe('GET');
      req.flush([mockConflict]);
    });
  });

  describe('resolveConflict', () => {
    it('should resolve a conflict', () => {
      const resolvedConflict = { ...mockConflict, isResolved: true };

      service.resolveConflict('1').subscribe(conflict => {
        expect(conflict.isResolved).toBe(true);
      });

      const req = httpMock.expectOne(`${baseUrl}/api/conflicts/1/resolve`);
      expect(req.request.method).toBe('POST');
      req.flush(resolvedConflict);
    });
  });
});
