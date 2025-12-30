import { Component, inject, Inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatDialogModule, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { FamilyMemberDto, RelationType } from '../../models/family-member-dto';
import { CreateFamilyMemberCommand } from '../../models/create-family-member-command';
import { HouseholdsService } from '../../services/households.service';
import { HouseholdDto } from '../../models/household-dto';
import { Observable } from 'rxjs';

export interface CreateOrEditFamilyMemberDialogData {
  member?: FamilyMemberDto;
  familyId: string;
}

export interface CreateOrEditFamilyMemberDialogResult {
  action: 'create' | 'update' | 'cancel';
  data?: CreateFamilyMemberCommand & { memberId?: string };
}

@Component({
  selector: 'app-create-or-edit-family-member-dialog',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatSelectModule,
    MatCheckboxModule
  ],
  templateUrl: './create-or-edit-family-member-dialog.html',
  styleUrls: ['./create-or-edit-family-member-dialog.scss']
})
export class CreateOrEditFamilyMemberDialog implements OnInit {
  private fb = inject(FormBuilder);
  private householdsService = inject(HouseholdsService);

  form: FormGroup;
  households$!: Observable<HouseholdDto[]>;

  availableColors = [
    { value: '#ef4444', label: 'Red' },
    { value: '#f59e0b', label: 'Orange' },
    { value: '#10b981', label: 'Green' },
    { value: '#3b82f6', label: 'Blue' },
    { value: '#8b5cf6', label: 'Purple' },
    { value: '#ec4899', label: 'Pink' },
    { value: '#06b6d4', label: 'Cyan' },
    { value: '#f97316', label: 'Amber' }
  ];

  availableRoles = [
    { value: 0, label: 'Admin' },
    { value: 1, label: 'Member' },
    { value: 2, label: 'View Only' }
  ];

  availableRelationTypes: { value: RelationType; label: string }[] = [
    { value: 'Self', label: 'Self' },
    { value: 'Spouse', label: 'Spouse' },
    { value: 'Child', label: 'Child' },
    { value: 'Parent', label: 'Parent' },
    { value: 'Sibling', label: 'Sibling' },
    { value: 'Grandparent', label: 'Grandparent' },
    { value: 'Grandchild', label: 'Grandchild' },
    { value: 'AuntUncle', label: 'Aunt/Uncle' },
    { value: 'NieceNephew', label: 'Niece/Nephew' },
    { value: 'Cousin', label: 'Cousin' },
    { value: 'InLaw', label: 'In-Law' },
    { value: 'Other', label: 'Other' }
  ];

  constructor(
    public dialogRef: MatDialogRef<CreateOrEditFamilyMemberDialog>,
    @Inject(MAT_DIALOG_DATA) public data: CreateOrEditFamilyMemberDialogData
  ) {
    this.form = this.fb.group({
      name: [data.member?.name || '', Validators.required],
      email: [data.member?.email || '', [Validators.email]],
      householdId: [data.member?.householdId || null],
      color: [data.member?.color || '#3b82f6', Validators.required],
      role: [data.member?.role ?? 1, Validators.required],
      isImmediate: [data.member?.isImmediate ?? true],
      relationType: [data.member?.relationType || 'Self', Validators.required]
    });
  }

  ngOnInit(): void {
    this.households$ = this.householdsService.getHouseholds();
  }

  get isEditMode(): boolean {
    return !!this.data.member;
  }

  get dialogTitle(): string {
    return this.isEditMode ? 'Edit Family Member' : 'Create Family Member';
  }

  onSubmit(): void {
    if (this.form.valid) {
      const formValue = this.form.value;
      // Map relationType string to enum number
      const relationTypeMap: Record<string, number> = {
        Self: 0,
        Spouse: 1,
        Child: 2,
        Parent: 3,
        Sibling: 4,
        Grandparent: 5,
        Grandchild: 6,
        AuntUncle: 7,
        NieceNephew: 8,
        Cousin: 9,
        InLaw: 10,
        Other: 11
      };
      const mappedRelationType = relationTypeMap[(formValue.relationType as string)] ?? 0;

      const result: CreateOrEditFamilyMemberDialogResult = {
        action: this.isEditMode ? 'update' : 'create',
        data: {
          familyId: this.data.familyId,
          name: formValue.name,
          email: formValue.email || null,
          householdId: formValue.householdId || null,
          color: formValue.color,
          role: formValue.role, // already number
          isImmediate: formValue.isImmediate,
          relationType: mappedRelationType,
          ...(this.isEditMode && { memberId: this.data.member!.memberId })
        }
      };

      this.dialogRef.close(result);
    }
  }

  onCancel(): void {
    this.dialogRef.close({ action: 'cancel' } as CreateOrEditFamilyMemberDialogResult);
  }
}
