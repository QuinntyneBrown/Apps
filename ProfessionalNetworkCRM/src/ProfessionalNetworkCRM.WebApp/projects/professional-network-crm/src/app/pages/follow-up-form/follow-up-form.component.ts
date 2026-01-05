import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { FollowUpsService, ContactsService } from '../../services';
import { map, switchMap, of, take } from 'rxjs';

@Component({
  selector: 'app-follow-up-form',
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
  templateUrl: './follow-up-form.component.html',
  styleUrl: './follow-up-form.component.scss'
})
export class FollowUpForm {
  private fb = inject(FormBuilder);
  private followUpsService = inject(FollowUpsService);
  private contactsService = inject(ContactsService);
  private router = inject(Router);
  private route = inject(ActivatedRoute);

  private followUpId$ = of(this.route.snapshot.paramMap.get('id')).pipe(
    map(id => id && id !== 'new' ? id : null)
  );

  followUpForm = this.fb.group({
    contactId: ['', Validators.required],
    description: ['', Validators.required],
    dueDate: [new Date(), Validators.required],
    priority: ['Medium', Validators.required],
    notes: ['']
  });

  viewModel$ = this.followUpId$.pipe(
    switchMap(id => {
      return this.contactsService.contacts$.pipe(
        switchMap(contacts => {
          if (id) {
            return this.followUpsService.getFollowUpById(id).pipe(
              map(followUp => {
                this.followUpForm.patchValue({
                  contactId: followUp.contactId,
                  description: followUp.description,
                  dueDate: followUp.dueDate ? new Date(followUp.dueDate) : new Date(),
                  priority: followUp.priority,
                  notes: followUp.notes
                });
                return { isEditMode: true, followUpId: id, contacts };
              })
            );
          }
          return of({ isEditMode: false, followUpId: null, contacts });
        })
      );
    })
  );

  onSubmit(): void {
    if (this.followUpForm.valid) {
      const formValue = this.followUpForm.value;

      this.followUpId$.pipe(take(1)).subscribe(followUpId => {
        if (followUpId) {
          const updateRequest = {
            followUpId: followUpId,
            description: formValue.description!,
            dueDate: formValue.dueDate!.toISOString(),
            priority: formValue.priority!,
            notes: formValue.notes || undefined
          };
          this.followUpsService.updateFollowUp(updateRequest).subscribe(() => {
            this.router.navigate(['/follow-ups']);
          });
        } else {
          const createRequest = {
            userId: '00000000-0000-0000-0000-000000000000',
            contactId: formValue.contactId!,
            description: formValue.description!,
            dueDate: formValue.dueDate!.toISOString(),
            priority: formValue.priority!,
            notes: formValue.notes || undefined
          };
          this.followUpsService.createFollowUp(createRequest).subscribe(() => {
            this.router.navigate(['/follow-ups']);
          });
        }
      });
    }
  }

  onCancel(): void {
    this.router.navigate(['/follow-ups']);
  }
}
