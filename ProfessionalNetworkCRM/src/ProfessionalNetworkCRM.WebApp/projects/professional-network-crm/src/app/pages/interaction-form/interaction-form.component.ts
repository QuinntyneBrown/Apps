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
import { InteractionsService, ContactsService } from '../../services';
import { map, switchMap, of, take } from 'rxjs';

@Component({
  selector: 'app-interaction-form',
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
  templateUrl: './interaction-form.component.html',
  styleUrl: './interaction-form.component.scss'
})
export class InteractionForm {
  private fb = inject(FormBuilder);
  private interactionsService = inject(InteractionsService);
  private contactsService = inject(ContactsService);
  private router = inject(Router);
  private route = inject(ActivatedRoute);

  private interactionId$ = of(this.route.snapshot.paramMap.get('id')).pipe(
    map(id => id && id !== 'new' ? id : null)
  );

  interactionForm = this.fb.group({
    contactId: ['', Validators.required],
    interactionType: ['', Validators.required],
    interactionDate: [new Date(), Validators.required],
    subject: [''],
    durationMinutes: [null as number | null],
    notes: [''],
    outcome: ['']
  });

  viewModel$ = this.interactionId$.pipe(
    switchMap(id => {
      return this.contactsService.contacts$.pipe(
        switchMap(contacts => {
          if (id) {
            return this.interactionsService.getInteractionById(id).pipe(
              map(interaction => {
                this.interactionForm.patchValue({
                  contactId: interaction.contactId,
                  interactionType: interaction.interactionType,
                  interactionDate: interaction.interactionDate ? new Date(interaction.interactionDate) : new Date(),
                  subject: interaction.subject,
                  durationMinutes: interaction.durationMinutes,
                  notes: interaction.notes,
                  outcome: interaction.outcome
                });
                return { isEditMode: true, interactionId: id, contacts };
              })
            );
          }
          return of({ isEditMode: false, interactionId: null, contacts });
        })
      );
    })
  );

  onSubmit(): void {
    if (this.interactionForm.valid) {
      const formValue = this.interactionForm.value;

      this.interactionId$.pipe(take(1)).subscribe(interactionId => {
        if (interactionId) {
          const updateRequest = {
            interactionId: interactionId,
            interactionType: formValue.interactionType!,
            interactionDate: formValue.interactionDate!.toISOString(),
            subject: formValue.subject || undefined,
            durationMinutes: formValue.durationMinutes || undefined,
            notes: formValue.notes || undefined,
            outcome: formValue.outcome || undefined
          };
          this.interactionsService.updateInteraction(updateRequest).subscribe(() => {
            this.router.navigate(['/interactions']);
          });
        } else {
          const createRequest = {
            userId: '00000000-0000-0000-0000-000000000000',
            contactId: formValue.contactId!,
            interactionType: formValue.interactionType!,
            interactionDate: formValue.interactionDate!.toISOString(),
            subject: formValue.subject || undefined,
            durationMinutes: formValue.durationMinutes || undefined,
            notes: formValue.notes || undefined,
            outcome: formValue.outcome || undefined
          };
          this.interactionsService.createInteraction(createRequest).subscribe(() => {
            this.router.navigate(['/interactions']);
          });
        }
      });
    }
  }

  onCancel(): void {
    this.router.navigate(['/interactions']);
  }
}
