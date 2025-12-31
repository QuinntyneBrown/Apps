import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { RetirementScenarioService } from '../services';
import { CreateRetirementScenario, UpdateRetirementScenario } from '../models';

@Component({
  selector: 'app-retirement-scenario-form',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatCheckboxModule
  ],
  template: `
    <div class="retirement-scenario-form">
      <mat-card class="retirement-scenario-form__card">
        <mat-card-header>
          <mat-card-title>{{ isEditMode ? 'Edit Retirement Scenario' : 'New Retirement Scenario' }}</mat-card-title>
        </mat-card-header>
        <mat-card-content>
          <form [formGroup]="form" class="retirement-scenario-form__form">
            <div class="retirement-scenario-form__section">
              <h3 class="retirement-scenario-form__section-title">Basic Information</h3>
              <mat-form-field appearance="outline" class="retirement-scenario-form__field">
                <mat-label>Name</mat-label>
                <input matInput formControlName="name" placeholder="e.g., Conservative Plan">
                <mat-error *ngIf="form.get('name')?.hasError('required')">Name is required</mat-error>
              </mat-form-field>

              <mat-form-field appearance="outline" class="retirement-scenario-form__field">
                <mat-label>Notes</mat-label>
                <textarea matInput formControlName="notes" rows="3" placeholder="Additional notes about this scenario"></textarea>
              </mat-form-field>
            </div>

            <div class="retirement-scenario-form__section">
              <h3 class="retirement-scenario-form__section-title">Age Information</h3>
              <div class="retirement-scenario-form__row">
                <mat-form-field appearance="outline" class="retirement-scenario-form__field">
                  <mat-label>Current Age</mat-label>
                  <input matInput type="number" formControlName="currentAge" placeholder="35">
                  <mat-error *ngIf="form.get('currentAge')?.hasError('required')">Current age is required</mat-error>
                  <mat-error *ngIf="form.get('currentAge')?.hasError('min')">Must be at least 0</mat-error>
                </mat-form-field>

                <mat-form-field appearance="outline" class="retirement-scenario-form__field">
                  <mat-label>Retirement Age</mat-label>
                  <input matInput type="number" formControlName="retirementAge" placeholder="65">
                  <mat-error *ngIf="form.get('retirementAge')?.hasError('required')">Retirement age is required</mat-error>
                  <mat-error *ngIf="form.get('retirementAge')?.hasError('min')">Must be at least 0</mat-error>
                </mat-form-field>

                <mat-form-field appearance="outline" class="retirement-scenario-form__field">
                  <mat-label>Life Expectancy Age</mat-label>
                  <input matInput type="number" formControlName="lifeExpectancyAge" placeholder="90">
                  <mat-error *ngIf="form.get('lifeExpectancyAge')?.hasError('required')">Life expectancy is required</mat-error>
                  <mat-error *ngIf="form.get('lifeExpectancyAge')?.hasError('min')">Must be at least 0</mat-error>
                </mat-form-field>
              </div>
            </div>

            <div class="retirement-scenario-form__section">
              <h3 class="retirement-scenario-form__section-title">Financial Information</h3>
              <div class="retirement-scenario-form__row">
                <mat-form-field appearance="outline" class="retirement-scenario-form__field">
                  <mat-label>Current Savings</mat-label>
                  <input matInput type="number" formControlName="currentSavings" placeholder="50000">
                  <span matTextPrefix>$&nbsp;</span>
                  <mat-error *ngIf="form.get('currentSavings')?.hasError('required')">Current savings is required</mat-error>
                  <mat-error *ngIf="form.get('currentSavings')?.hasError('min')">Must be at least 0</mat-error>
                </mat-form-field>

                <mat-form-field appearance="outline" class="retirement-scenario-form__field">
                  <mat-label>Annual Contribution</mat-label>
                  <input matInput type="number" formControlName="annualContribution" placeholder="10000">
                  <span matTextPrefix>$&nbsp;</span>
                  <mat-error *ngIf="form.get('annualContribution')?.hasError('required')">Annual contribution is required</mat-error>
                  <mat-error *ngIf="form.get('annualContribution')?.hasError('min')">Must be at least 0</mat-error>
                </mat-form-field>
              </div>
            </div>

            <div class="retirement-scenario-form__section">
              <h3 class="retirement-scenario-form__section-title">Rates</h3>
              <div class="retirement-scenario-form__row">
                <mat-form-field appearance="outline" class="retirement-scenario-form__field">
                  <mat-label>Expected Return Rate</mat-label>
                  <input matInput type="number" formControlName="expectedReturnRate" step="0.01" placeholder="0.07">
                  <mat-hint>Enter as decimal (e.g., 0.07 for 7%)</mat-hint>
                  <mat-error *ngIf="form.get('expectedReturnRate')?.hasError('required')">Expected return rate is required</mat-error>
                  <mat-error *ngIf="form.get('expectedReturnRate')?.hasError('min')">Must be at least 0</mat-error>
                </mat-form-field>

                <mat-form-field appearance="outline" class="retirement-scenario-form__field">
                  <mat-label>Inflation Rate</mat-label>
                  <input matInput type="number" formControlName="inflationRate" step="0.01" placeholder="0.03">
                  <mat-hint>Enter as decimal (e.g., 0.03 for 3%)</mat-hint>
                  <mat-error *ngIf="form.get('inflationRate')?.hasError('required')">Inflation rate is required</mat-error>
                  <mat-error *ngIf="form.get('inflationRate')?.hasError('min')">Must be at least 0</mat-error>
                </mat-form-field>
              </div>
            </div>

            <div class="retirement-scenario-form__section">
              <h3 class="retirement-scenario-form__section-title">Retirement Projections</h3>
              <div class="retirement-scenario-form__row">
                <mat-form-field appearance="outline" class="retirement-scenario-form__field">
                  <mat-label>Projected Annual Income</mat-label>
                  <input matInput type="number" formControlName="projectedAnnualIncome" placeholder="40000">
                  <span matTextPrefix>$&nbsp;</span>
                  <mat-error *ngIf="form.get('projectedAnnualIncome')?.hasError('required')">Projected annual income is required</mat-error>
                  <mat-error *ngIf="form.get('projectedAnnualIncome')?.hasError('min')">Must be at least 0</mat-error>
                </mat-form-field>

                <mat-form-field appearance="outline" class="retirement-scenario-form__field">
                  <mat-label>Projected Annual Expenses</mat-label>
                  <input matInput type="number" formControlName="projectedAnnualExpenses" placeholder="60000">
                  <span matTextPrefix>$&nbsp;</span>
                  <mat-error *ngIf="form.get('projectedAnnualExpenses')?.hasError('required')">Projected annual expenses is required</mat-error>
                  <mat-error *ngIf="form.get('projectedAnnualExpenses')?.hasError('min')">Must be at least 0</mat-error>
                </mat-form-field>
              </div>
            </div>
          </form>
        </mat-card-content>
        <mat-card-actions class="retirement-scenario-form__actions">
          <button mat-button (click)="cancel()">Cancel</button>
          <button mat-raised-button color="primary" (click)="save()" [disabled]="!form.valid">
            {{ isEditMode ? 'Update' : 'Create' }}
          </button>
        </mat-card-actions>
      </mat-card>
    </div>
  `,
  styles: [`
    .retirement-scenario-form {
      padding: 2rem;
      max-width: 1000px;
      margin: 0 auto;
    }

    .retirement-scenario-form__card {
      margin-bottom: 2rem;
    }

    .retirement-scenario-form__form {
      display: flex;
      flex-direction: column;
      gap: 1.5rem;
      padding: 1rem 0;
    }

    .retirement-scenario-form__section {
      display: flex;
      flex-direction: column;
      gap: 1rem;
    }

    .retirement-scenario-form__section-title {
      margin: 0;
      font-size: 1.1rem;
      font-weight: 500;
      color: #333;
    }

    .retirement-scenario-form__row {
      display: grid;
      grid-template-columns: repeat(auto-fit, minmax(250px, 1fr));
      gap: 1rem;
    }

    .retirement-scenario-form__field {
      width: 100%;
    }

    .retirement-scenario-form__actions {
      display: flex;
      justify-content: flex-end;
      gap: 1rem;
      padding: 1rem;
    }
  `]
})
export class RetirementScenarioForm implements OnInit {
  private fb = inject(FormBuilder);
  private scenarioService = inject(RetirementScenarioService);
  private route = inject(ActivatedRoute);
  private router = inject(Router);

  form!: FormGroup;
  isEditMode = false;
  scenarioId?: string;

  ngOnInit(): void {
    this.initForm();

    const id = this.route.snapshot.paramMap.get('id');
    if (id && id !== 'new') {
      this.isEditMode = true;
      this.scenarioId = id;
      this.loadScenario(id);
    }
  }

  private initForm(): void {
    this.form = this.fb.group({
      name: ['', Validators.required],
      currentAge: [0, [Validators.required, Validators.min(0)]],
      retirementAge: [65, [Validators.required, Validators.min(0)]],
      lifeExpectancyAge: [90, [Validators.required, Validators.min(0)]],
      currentSavings: [0, [Validators.required, Validators.min(0)]],
      annualContribution: [0, [Validators.required, Validators.min(0)]],
      expectedReturnRate: [0.07, [Validators.required, Validators.min(0)]],
      inflationRate: [0.03, [Validators.required, Validators.min(0)]],
      projectedAnnualIncome: [0, [Validators.required, Validators.min(0)]],
      projectedAnnualExpenses: [0, [Validators.required, Validators.min(0)]],
      notes: ['']
    });
  }

  private loadScenario(id: string): void {
    this.scenarioService.getScenario(id).subscribe(scenario => {
      this.form.patchValue({
        name: scenario.name,
        currentAge: scenario.currentAge,
        retirementAge: scenario.retirementAge,
        lifeExpectancyAge: scenario.lifeExpectancyAge,
        currentSavings: scenario.currentSavings,
        annualContribution: scenario.annualContribution,
        expectedReturnRate: scenario.expectedReturnRate,
        inflationRate: scenario.inflationRate,
        projectedAnnualIncome: scenario.projectedAnnualIncome,
        projectedAnnualExpenses: scenario.projectedAnnualExpenses,
        notes: scenario.notes
      });
    });
  }

  save(): void {
    if (this.form.valid) {
      const formValue = this.form.value;

      if (this.isEditMode && this.scenarioId) {
        const update: UpdateRetirementScenario = {
          retirementScenarioId: this.scenarioId,
          ...formValue
        };
        this.scenarioService.updateScenario(update).subscribe(() => {
          this.router.navigate(['/retirement-scenarios']);
        });
      } else {
        const create: CreateRetirementScenario = formValue;
        this.scenarioService.createScenario(create).subscribe(() => {
          this.router.navigate(['/retirement-scenarios']);
        });
      }
    }
  }

  cancel(): void {
    this.router.navigate(['/retirement-scenarios']);
  }
}
