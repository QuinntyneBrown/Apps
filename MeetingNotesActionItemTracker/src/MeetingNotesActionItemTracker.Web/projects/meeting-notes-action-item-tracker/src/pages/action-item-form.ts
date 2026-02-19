import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { ActionItemService, MeetingService } from '../services';
import { CreateActionItemDto, UpdateActionItemDto, Priority, ActionItemStatus, PRIORITY_LABELS, ACTION_ITEM_STATUS_LABELS } from '../models';

@Component({
  selector: 'app-action-item-form',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatSelectModule,
    MatDatepickerModule,
    MatNativeDateModule
  ],
  templateUrl: './action-item-form.html',
  styleUrl: './action-item-form.scss'
})
export class ActionItemForm implements OnInit {
  private fb = inject(FormBuilder);
  private actionItemService = inject(ActionItemService);
  private meetingService = inject(MeetingService);
  private route = inject(ActivatedRoute);
  private router = inject(Router);

  actionItemForm!: FormGroup;
  isEditMode = false;
  actionItemId?: string;
  meetings$ = this.meetingService.meetings$;

  priorities = Object.keys(Priority)
    .filter(k => !isNaN(Number(k)))
    .map(k => ({
      value: Number(k),
      label: PRIORITY_LABELS[Number(k) as Priority]
    }));

  statuses = Object.keys(ActionItemStatus)
    .filter(k => !isNaN(Number(k)))
    .map(k => ({
      value: Number(k),
      label: ACTION_ITEM_STATUS_LABELS[Number(k) as ActionItemStatus]
    }));

  ngOnInit(): void {
    this.actionItemForm = this.fb.group({
      meetingId: ['', Validators.required],
      description: ['', Validators.required],
      responsiblePerson: [''],
      dueDate: [''],
      priority: [Priority.Medium, Validators.required],
      status: [ActionItemStatus.NotStarted, Validators.required],
      notes: ['']
    });

    this.meetingService.getMeetings().subscribe();

    this.route.params.subscribe(params => {
      if (params['id'] && params['id'] !== 'new') {
        this.isEditMode = true;
        this.actionItemId = params['id'];
        this.loadActionItem(this.actionItemId);
      }
    });
  }

  loadActionItem(id: string): void {
    this.actionItemService.getActionItemById(id).subscribe(actionItem => {
      this.actionItemForm.patchValue({
        meetingId: actionItem.meetingId,
        description: actionItem.description,
        responsiblePerson: actionItem.responsiblePerson,
        dueDate: actionItem.dueDate ? new Date(actionItem.dueDate) : null,
        priority: actionItem.priority,
        status: actionItem.status,
        notes: actionItem.notes
      });
    });
  }

  onSubmit(): void {
    if (this.actionItemForm.valid) {
      const formValue = this.actionItemForm.value;
      const dueDate = formValue.dueDate ? new Date(formValue.dueDate).toISOString() : undefined;

      if (this.isEditMode && this.actionItemId) {
        const dto: UpdateActionItemDto = {
          actionItemId: this.actionItemId,
          description: formValue.description,
          responsiblePerson: formValue.responsiblePerson || undefined,
          dueDate: dueDate,
          priority: formValue.priority,
          status: formValue.status,
          notes: formValue.notes || undefined
        };
        this.actionItemService.updateActionItem(dto).subscribe(() => {
          this.router.navigate(['/action-items']);
        });
      } else {
        const dto: CreateActionItemDto = {
          userId: '00000000-0000-0000-0000-000000000000', // Replace with actual user ID
          meetingId: formValue.meetingId,
          description: formValue.description,
          responsiblePerson: formValue.responsiblePerson || undefined,
          dueDate: dueDate,
          priority: formValue.priority,
          status: formValue.status,
          notes: formValue.notes || undefined
        };
        this.actionItemService.createActionItem(dto).subscribe(() => {
          this.router.navigate(['/action-items']);
        });
      }
    }
  }

  cancel(): void {
    this.router.navigate(['/action-items']);
  }
}
