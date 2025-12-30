import { ComponentFixture, TestBed } from '@angular/core/testing';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { RecipientCard } from './recipient-card';
import { Recipient } from '../../models';

describe('RecipientCard', () => {
  let component: RecipientCard;
  let fixture: ComponentFixture<RecipientCard>;

  const mockRecipient: Recipient = {
    recipientId: 'recipient-1',
    userId: 'user-1',
    name: 'John Doe',
    relationship: 'Friend',
    createdAt: '2025-01-01T00:00:00Z'
  };

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RecipientCard, NoopAnimationsModule]
    }).compileComponents();

    fixture = TestBed.createComponent(RecipientCard);
    component = fixture.componentInstance;
    component.recipient = mockRecipient;
    component.giftIdeasCount = 5;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should display recipient name', () => {
    const compiled = fixture.nativeElement;
    expect(compiled.textContent).toContain('John Doe');
  });

  it('should display recipient relationship', () => {
    const compiled = fixture.nativeElement;
    expect(compiled.textContent).toContain('Friend');
  });

  it('should display gift ideas count', () => {
    const compiled = fixture.nativeElement;
    expect(compiled.textContent).toContain('5 ideas');
  });

  it('should calculate initials correctly', () => {
    expect(component.getInitials()).toBe('JD');
  });

  it('should calculate single letter initial for single name', () => {
    component.recipient = { ...mockRecipient, name: 'Jane' };
    expect(component.getInitials()).toBe('J');
  });

  it('should limit initials to 2 characters', () => {
    component.recipient = { ...mockRecipient, name: 'John Michael Doe' };
    expect(component.getInitials()).toBe('JM');
  });

  it('should emit viewIdeasClick when view ideas button is clicked', () => {
    jest.spyOn(component.viewIdeasClick, 'emit');
    component.onViewIdeasClick();
    expect(component.viewIdeasClick.emit).toHaveBeenCalledWith(mockRecipient);
  });

  it('should emit editClick when edit button is clicked', () => {
    jest.spyOn(component.editClick, 'emit');
    component.onEditClick();
    expect(component.editClick.emit).toHaveBeenCalledWith(mockRecipient);
  });

  it('should emit deleteClick when delete button is clicked', () => {
    jest.spyOn(component.deleteClick, 'emit');
    component.onDeleteClick();
    expect(component.deleteClick.emit).toHaveBeenCalledWith(mockRecipient);
  });

  it('should have action buttons', () => {
    const compiled = fixture.nativeElement;
    expect(compiled.textContent).toContain('View Ideas');
    expect(compiled.textContent).toContain('Edit');
    expect(compiled.textContent).toContain('Delete');
  });
});
