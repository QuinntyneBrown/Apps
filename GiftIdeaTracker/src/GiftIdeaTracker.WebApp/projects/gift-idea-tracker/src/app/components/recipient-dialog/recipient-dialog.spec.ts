import { ComponentFixture, TestBed } from '@angular/core/testing';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { RecipientDialog, RecipientDialogData, RecipientDialogResult } from './recipient-dialog';
import { Recipient } from '../../models';

describe('RecipientDialog', () => {
  let component: RecipientDialog;
  let fixture: ComponentFixture<RecipientDialog>;
  let dialogRef: jest.Mocked<MatDialogRef<RecipientDialog>>;

  const mockRecipient: Recipient = {
    recipientId: 'recipient-1',
    userId: 'user-1',
    name: 'John Doe',
    relationship: 'Friend',
    createdAt: '2025-01-01T00:00:00Z'
  };

  const createComponent = (data: RecipientDialogData) => {
    dialogRef = {
      close: jest.fn()
    } as unknown as jest.Mocked<MatDialogRef<RecipientDialog>>;

    TestBed.configureTestingModule({
      imports: [RecipientDialog, NoopAnimationsModule],
      providers: [
        { provide: MatDialogRef, useValue: dialogRef },
        { provide: MAT_DIALOG_DATA, useValue: data }
      ]
    });

    fixture = TestBed.createComponent(RecipientDialog);
    component = fixture.componentInstance;
    fixture.detectChanges();
  };

  describe('Create Mode', () => {
    beforeEach(() => {
      createComponent({});
    });

    it('should create', () => {
      expect(component).toBeTruthy();
    });

    it('should be in create mode when no recipient provided', () => {
      expect(component.isEditMode).toBe(false);
    });

    it('should have empty form fields in create mode', () => {
      expect(component.form.get('name')?.value).toBe('');
      expect(component.form.get('relationship')?.value).toBe('');
    });

    it('should display add title in create mode', () => {
      const compiled = fixture.nativeElement;
      expect(compiled.textContent).toContain('Add Recipient');
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
      component.form.get('name')?.setValue('New Person');
      component.form.get('relationship')?.setValue('Family');
      component.onSubmit();

      expect(dialogRef.close).toHaveBeenCalledWith({
        action: 'create',
        data: {
          name: 'New Person',
          relationship: 'Family'
        }
      } as RecipientDialogResult);
    });

    it('should submit create request without relationship', () => {
      component.form.get('name')?.setValue('New Person');
      component.form.get('relationship')?.setValue('');
      component.onSubmit();

      expect(dialogRef.close).toHaveBeenCalledWith({
        action: 'create',
        data: {
          name: 'New Person',
          relationship: undefined
        }
      } as RecipientDialogResult);
    });
  });

  describe('Edit Mode', () => {
    beforeEach(() => {
      createComponent({ recipient: mockRecipient });
    });

    it('should be in edit mode when recipient provided', () => {
      expect(component.isEditMode).toBe(true);
    });

    it('should populate form with recipient data', () => {
      expect(component.form.get('name')?.value).toBe('John Doe');
      expect(component.form.get('relationship')?.value).toBe('Friend');
    });

    it('should display edit title in edit mode', () => {
      const compiled = fixture.nativeElement;
      expect(compiled.textContent).toContain('Edit Recipient');
    });

    it('should submit update request when form is valid', () => {
      component.form.get('name')?.setValue('Updated Name');
      component.form.get('relationship')?.setValue('Updated Relationship');
      component.onSubmit();

      expect(dialogRef.close).toHaveBeenCalledWith({
        action: 'update',
        data: {
          recipientId: 'recipient-1',
          name: 'Updated Name',
          relationship: 'Updated Relationship'
        }
      } as RecipientDialogResult);
    });
  });

  describe('Form Validation', () => {
    beforeEach(() => {
      createComponent({});
    });

    it('should require name field', () => {
      const nameControl = component.form.get('name');
      nameControl?.setValue('');
      expect(nameControl?.hasError('required')).toBe(true);
    });

    it('should not require relationship field', () => {
      const relationshipControl = component.form.get('relationship');
      relationshipControl?.setValue('');
      expect(relationshipControl?.hasError('required')).toBeFalsy();
    });

    it('should be valid when name is provided', () => {
      component.form.get('name')?.setValue('Valid Name');
      expect(component.form.valid).toBe(true);
    });
  });
});
