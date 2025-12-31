import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { ContributionService, RetirementScenarioService } from '../services';
import { CreateContribution, UpdateContribution } from '../models';

@Component({
  selector: 'app-contribution-form',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatCheckboxModule,
    MatSelectModule,
    MatDatepickerModule,
    MatNativeDateModule
  ],
  template: `
    <div class="contribution-form">
      <mat-card class="contribution-form__card">
        <mat-card-header>
          <mat-card-title>{{ isEditMode ? 'Edit Contribution' : 'New Contribution' }}</mat-card-title>
        </mat-card-header>
        <mat-card-content>
          <form [formGroup]="form" class="contribution-form__form">
            <mat-form-field appearance="outline" class="contribution-form__field">
              <mat-label>Retirement Scenario</mat-label>
              <mat-select formControlName="retirementScenarioId">
                <mat-option *ngFor="let scenario of (scenarios$ | async)" [value]="scenario.retirementScenarioId">
                  {{ scenario.name }}
                </mat-option>
              </mat-select>
              <mat-error *ngIf="form.get('retirementScenarioId')?.hasError('required')">Retirement scenario is required</mat-error>
            </mat-form-field>

            <div class="contribution-form__row">
              <mat-form-field appearance="outline" class="contribution-form__field">
                <mat-label>Amount</mat-label>
                <input matInput type="number" formControlName="amount" placeholder="1000">
                <span matTextPrefix>$&nbsp;</span>
                <mat-error *ngIf="form.get('amount')?.hasError('required')">Amount is required</mat-error>
                <mat-error *ngIf="form.get('amount')?.hasError('min')">Amount must be greater than 0</mat-error>
              </mat-form-field>

              <mat-form-field appearance="outline" class="contribution-form__field">
                <mat-label>Contribution Date</mat-label>
                <input matInput [matDatepicker]="picker" formControlName="contributionDate">
                <mat-datepicker-toggle matIconSuffix [for]="picker"></mat-datepicker-toggle>
                <mat-datepicker #picker></mat-datepicker>
                <mat-error *ngIf="form.get('contributionDate')?.hasError('required')">Contribution date is required</mat-error>
              </mat-form-field>
            </div>

            <mat-form-field appearance="outline" class="contribution-form__field">
              <mat-label>Account Name</mat-label>
              <input matInput formControlName="accountName" placeholder="e.g., 401(k), IRA">
              <mat-error *ngIf="form.get('accountName')?.hasError('required')">Account name is required</mat-error>
            </mat-form-field>

            <div class="contribution-form__checkbox">
              <mat-checkbox formControlName="isEmployerMatch">
                Employer Match
              </mat-checkbox>
            </div>

            <mat-form-field appearance="outline" class="contribution-form__field">
              <mat-label>Notes</mat-label>
              <textarea matInput formControlName="notes" rows="3" placeholder="Additional notes about this contribution"></textarea>
            </mat-form-field>
          </form>
        </mat-card-content>
        <mat-card-actions class="contribution-form__actions">
          <button mat-button (click)="cancel()">Cancel</button>
          <button mat-raised-button color="primary" (click)="save()" [disabled]="!form.valid">
            {{ isEditMode ? 'Update' : 'Create' }}
          </button>
        </mat-card-actions>
      </mat-card>
    </div>
  `,
  styles: [`
    .contribution-form {
      padding: 2rem;
      max-width: 800px;
      margin: 0 auto;
    }

    .contribution-form__card {
      margin-bottom: 2rem;
    }

    .contribution-form__form {
      display: flex;
      flex-direction: column;
      gap: 1rem;
      padding: 1rem 0;
    }

    .contribution-form__row {
      display: grid;
      grid-template-columns: repeat(auto-fit, minmax(250px, 1fr));
      gap: 1rem;
    }

    .contribution-form__field {
      width: 100%;
    }

    .contribution-form__checkbox {
      padding: 0.5rem 0;
    }

    .contribution-form__actions {
      display: flex;
      justify-content: flex-end;
      gap: 1rem;
      padding: 1rem;
    }
  `]
})
export class ContributionForm implements OnInit {
  private fb = inject(FormBuilder);
  private contributionService = inject(ContributionService);
  private scenarioService = inject(RetirementScenarioService);
  private route = inject(ActivatedRoute);
  private router = inject(Router);

  form!: FormGroup;
  isEditMode = false;
  contributionId?: string;
  scenarios$ = this.scenarioService.scenarios$;

  ngOnInit(): void {
    this.scenarioService.loadScenarios().subscribe();
    this.initForm();

    const id = this.route.snapshot.paramMap.get('id');
    if (id && id !== 'new') {
      this.isEditMode = true;
      this.contributionId = id;
      this.loadContribution(id);
    }
  }

  private initForm(): void {
    this.form = this.fb.group({
      retirementScenarioId: ['', Validators.required],
      amount: [0, [Validators.required, Validators.min(0.01)]],
      contributionDate: [new Date(), Validators.required],
      accountName: ['', Validators.required],
      isEmployerMatch: [false],
      notes: ['']
    });
  }

  private loadContribution(id: string): void {
    this.contributionService.getContribution(id).subscribe(contribution => {
      this.form.patchValue({
        retirementScenarioId: contribution.retirementScenarioId,
        amount: contribution.amount,
        contributionDate: new Date(contribution.contributionDate),
        accountName: contribution.accountName,
        isEmployerMatch: contribution.isEmployerMatch,
        notes: contribution.notes
      });
    });
  }

  save(): void {
    if (this.form.valid) {
      const formValue = this.form.value;
      const contributionDate = formValue.contributionDate instanceof Date
        ? formValue.contributionDate.toISOString()
        : formValue.contributionDate;

      if (this.isEditMode && this.contributionId) {
        const update: UpdateContribution = {
          contributionId: this.contributionId,
          amount: formValue.amount,
          contributionDate: contributionDate,
          accountName: formValue.accountName,
          isEmployerMatch: formValue.isEmployerMatch,
          notes: formValue.notes
        };
        this.contributionService.updateContribution(update).subscribe(() => {
          this.router.navigate(['/contributions']);
        });
      } else {
        const create: CreateContribution = {
          retirementScenarioId: formValue.retirementScenarioId,
          amount: formValue.amount,
          contributionDate: contributionDate,
          accountName: formValue.accountName,
          isEmployerMatch: formValue.isEmployerMatch,
          notes: formValue.notes
        };
        this.contributionService.createContribution(create).subscribe(() => {
          this.router.navigate(['/contributions']);
        });
      }
    }
  }

  cancel(): void {
    this.router.navigate(['/contributions']);
  }
}
