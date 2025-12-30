import { ComponentFixture, TestBed } from '@angular/core/testing';
import { MemberCard } from './member-card';
import { FamilyMember, MemberRole, RelationType } from '../../services/models';

describe('MemberCard', () => {
  let component: MemberCard;
  let fixture: ComponentFixture<MemberCard>;

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

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MemberCard]
    }).compileComponents();

    fixture = TestBed.createComponent(MemberCard);
    component = fixture.componentInstance;
    component.member = mockMember;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should display member name', () => {
    const compiled = fixture.nativeElement;
    expect(compiled.textContent).toContain('John Doe');
  });

  it('should display member email', () => {
    const compiled = fixture.nativeElement;
    expect(compiled.textContent).toContain('john@example.com');
  });

  it('should not display email when null', () => {
    component.member = { ...mockMember, email: null };
    fixture.detectChanges();
    const compiled = fixture.nativeElement;
    expect(compiled.querySelector('.member-card__email')).toBeFalsy();
  });

  it('should get correct initials', () => {
    expect(component.getInitials()).toBe('JD');
  });

  it('should get initials for single name', () => {
    component.member = { ...mockMember, name: 'Madonna' };
    expect(component.getInitials()).toBe('M');
  });

  it('should return admin role badge class for Admin', () => {
    expect(component.getRoleBadgeClass()).toBe('member-card__role--admin');
  });

  it('should return empty role badge class for Member', () => {
    component.member = { ...mockMember, role: MemberRole.Member };
    expect(component.getRoleBadgeClass()).toBe('');
  });

  it('should return correct role icon for Admin', () => {
    expect(component.getRoleIcon()).toBe('admin_panel_settings');
  });

  it('should return correct role icon for Member', () => {
    component.member = { ...mockMember, role: MemberRole.Member };
    expect(component.getRoleIcon()).toBe('person');
  });

  it('should return correct role icon for ViewOnly', () => {
    component.member = { ...mockMember, role: MemberRole.ViewOnly };
    expect(component.getRoleIcon()).toBe('visibility');
  });

  it('should return correct relation type label for Self', () => {
    expect(component.getRelationTypeLabel()).toBe('Self');
  });

  it('should return correct relation type label for Spouse', () => {
    component.member = { ...mockMember, relationType: RelationType.Spouse };
    expect(component.getRelationTypeLabel()).toBe('Spouse');
  });

  it('should return correct relation type label for AuntUncle', () => {
    component.member = { ...mockMember, relationType: RelationType.AuntUncle };
    expect(component.getRelationTypeLabel()).toBe('Aunt/Uncle');
  });

  it('should display immediate badge when isImmediate is true', () => {
    component.member = { ...mockMember, isImmediate: true };
    fixture.detectChanges();
    const compiled = fixture.nativeElement;
    expect(compiled.textContent).toContain('Immediate');
  });

  it('should not display immediate badge when isImmediate is false', () => {
    component.member = { ...mockMember, isImmediate: false };
    fixture.detectChanges();
    const compiled = fixture.nativeElement;
    const immediateBadge = compiled.querySelector('.member-card__immediate-badge');
    expect(immediateBadge).toBeFalsy();
  });

  it('should emit editClick event', () => {
    jest.spyOn(component.editClick, 'emit');
    component.onEditClick();
    expect(component.editClick.emit).toHaveBeenCalledWith(mockMember);
  });

  it('should emit viewScheduleClick event', () => {
    jest.spyOn(component.viewScheduleClick, 'emit');
    component.onViewScheduleClick();
    expect(component.viewScheduleClick.emit).toHaveBeenCalledWith(mockMember);
  });

  it('should display event count', () => {
    component.eventCount = 5;
    fixture.detectChanges();
    const compiled = fixture.nativeElement;
    expect(compiled.textContent).toContain('5');
  });
});
