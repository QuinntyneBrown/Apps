import { ComponentFixture, TestBed } from '@angular/core/testing';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { GiftIdeaDialog, GiftIdeaDialogData, GiftIdeaDialogResult } from './gift-idea-dialog';
import { GiftIdea, Recipient, Occasion } from '../../models';

describe('GiftIdeaDialog', () => {
  let component: GiftIdeaDialog;
  let fixture: ComponentFixture<GiftIdeaDialog>;
  let dialogRef: jest.Mocked<MatDialogRef<GiftIdeaDialog>>;

  const mockRecipients: Recipient[] = [
    {
      recipientId: 'recipient-1',
      userId: 'user-1',
      name: 'John Doe',
      relationship: 'Friend',
      createdAt: '2025-01-01T00:00:00Z'
    },
    {
      recipientId: 'recipient-2',
      userId: 'user-1',
      name: 'Jane Smith',
      relationship: 'Family',
      createdAt: '2025-01-02T00:00:00Z'
    }
  ];

  const mockGiftIdea: GiftIdea = {
    giftIdeaId: 'gift-1',
    userId: 'user-1',
    recipientId: 'recipient-1',
    name: 'New iPhone',
    occasion: Occasion.Birthday,
    estimatedPrice: 999,
    isPurchased: false,
    createdAt: '2025-01-01T00:00:00Z'
  };

  const createComponent = (data: GiftIdeaDialogData) => {
    dialogRef = {
      close: jest.fn()
    } as unknown as jest.Mocked<MatDialogRef<GiftIdeaDialog>>;

    TestBed.configureTestingModule({
      imports: [GiftIdeaDialog, NoopAnimationsModule],
      providers: [
        { provide: MatDialogRef, useValue: dialogRef },
        { provide: MAT_DIALOG_DATA, useValue: data }
      ]
    });

    fixture = TestBed.createComponent(GiftIdeaDialog);
    component = fixture.componentInstance;
    fixture.detectChanges();
  };

  describe('Create Mode', () => {
    beforeEach(() => {
      createComponent({ recipients: mockRecipients });
    });

    it('should create', () => {
      expect(component).toBeTruthy();
    });

    it('should be in create mode when no gift idea provided', () => {
      expect(component.isEditMode).toBe(false);
    });

    it('should have default values in create mode', () => {
      expect(component.form.get('name')?.value).toBe('');
      expect(component.form.get('recipientId')?.value).toBe('');
      expect(component.form.get('occasion')?.value).toBe(Occasion.Other);
      expect(component.form.get('estimatedPrice')?.value).toBe(null);
    });

    it('should display add title in create mode', () => {
      const compiled = fixture.nativeElement;
      expect(compiled.textContent).toContain('Add Gift Idea');
    });

    it('should have all occasions available', () => {
      expect(component.occasions).toEqual(Object.values(Occasion));
    });

    it('should close dialog on cancel', () => {
      component.onCancel();
      expect(dialogRef.close).toHaveBeenCalledWith();
    });

    it('should not submit if form is invalid', () => {
      component.form.get('name')?.setValue('');
      component.onSubmit();
      expect(dialogRef.close).not.toHaveBeenCalled();
    });

    it('should submit create request when form is valid', () => {
      component.form.get('name')?.setValue('New Gift');
      component.form.get('recipientId')?.setValue('recipient-1');
      component.form.get('occasion')?.setValue(Occasion.Birthday);
      component.form.get('estimatedPrice')?.setValue(100);
      component.onSubmit();

      expect(dialogRef.close).toHaveBeenCalledWith({
        action: 'create',
        data: {
          name: 'New Gift',
          recipientId: 'recipient-1',
          occasion: Occasion.Birthday,
          estimatedPrice: 100
        }
      } as GiftIdeaDialogResult);
    });

    it('should submit create request without recipient and price', () => {
      component.form.get('name')?.setValue('General Gift');
      component.form.get('recipientId')?.setValue('');
      component.form.get('occasion')?.setValue(Occasion.Other);
      component.form.get('estimatedPrice')?.setValue(null);
      component.onSubmit();

      expect(dialogRef.close).toHaveBeenCalledWith({
        action: 'create',
        data: {
          name: 'General Gift',
          recipientId: undefined,
          occasion: Occasion.Other,
          estimatedPrice: undefined
        }
      } as GiftIdeaDialogResult);
    });
  });

  describe('Edit Mode', () => {
    beforeEach(() => {
      createComponent({ giftIdea: mockGiftIdea, recipients: mockRecipients });
    });

    it('should be in edit mode when gift idea provided', () => {
      expect(component.isEditMode).toBe(true);
    });

    it('should populate form with gift idea data', () => {
      expect(component.form.get('name')?.value).toBe('New iPhone');
      expect(component.form.get('recipientId')?.value).toBe('recipient-1');
      expect(component.form.get('occasion')?.value).toBe(Occasion.Birthday);
      expect(component.form.get('estimatedPrice')?.value).toBe(999);
    });

    it('should display edit title in edit mode', () => {
      const compiled = fixture.nativeElement;
      expect(compiled.textContent).toContain('Edit Gift Idea');
    });

    it('should submit update request when form is valid', () => {
      component.form.get('name')?.setValue('Updated Gift');
      component.form.get('occasion')?.setValue(Occasion.Anniversary);
      component.onSubmit();

      expect(dialogRef.close).toHaveBeenCalledWith({
        action: 'update',
        data: {
          giftIdeaId: 'gift-1',
          name: 'Updated Gift',
          recipientId: 'recipient-1',
          occasion: Occasion.Anniversary,
          estimatedPrice: 999
        }
      } as GiftIdeaDialogResult);
    });
  });

  describe('Form Validation', () => {
    beforeEach(() => {
      createComponent({ recipients: mockRecipients });
    });

    it('should require name field', () => {
      const nameControl = component.form.get('name');
      nameControl?.setValue('');
      expect(nameControl?.hasError('required')).toBe(true);
    });

    it('should require occasion field', () => {
      const occasionControl = component.form.get('occasion');
      occasionControl?.setValue(null);
      expect(occasionControl?.hasError('required')).toBe(true);
    });

    it('should not require recipientId field', () => {
      const recipientIdControl = component.form.get('recipientId');
      recipientIdControl?.setValue('');
      expect(recipientIdControl?.hasError('required')).toBeFalsy();
    });

    it('should validate estimatedPrice is positive', () => {
      const priceControl = component.form.get('estimatedPrice');
      priceControl?.setValue(-10);
      expect(priceControl?.hasError('min')).toBe(true);
    });

    it('should accept zero price', () => {
      const priceControl = component.form.get('estimatedPrice');
      priceControl?.setValue(0);
      expect(priceControl?.hasError('min')).toBeFalsy();
    });

    it('should be valid with required fields', () => {
      component.form.get('name')?.setValue('Valid Name');
      component.form.get('occasion')?.setValue(Occasion.Birthday);
      expect(component.form.valid).toBe(true);
    });
  });
});
