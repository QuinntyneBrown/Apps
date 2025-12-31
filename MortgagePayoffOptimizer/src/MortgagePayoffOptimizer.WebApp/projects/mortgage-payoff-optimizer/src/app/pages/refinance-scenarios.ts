import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialogModule, MatDialog } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatCardModule } from '@angular/material/card';
import { RefinanceScenarioService, MortgageService } from '../services';
import { RefinanceScenario, CreateRefinanceScenario, UpdateRefinanceScenario, Mortgage } from '../models';

@Component({
  selector: 'app-refinance-scenario-dialog',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatButtonModule
  ],
  template: `
    <h2 mat-dialog-title>{{ data ? 'Edit Refinance Scenario' : 'Add Refinance Scenario' }}</h2>
    <mat-dialog-content>
      <form [formGroup]="form" class="scenario-form">
        <mat-form-field class="scenario-form__field">
          <mat-label>Mortgage</mat-label>
          <mat-select formControlName="mortgageId" required>
            <mat-option *ngFor="let mortgage of mortgages$ | async" [value]="mortgage.mortgageId">
              {{ mortgage.propertyAddress }} - {{ mortgage.lender }}
            </mat-option>
          </mat-select>
        </mat-form-field>

        <mat-form-field class="scenario-form__field">
          <mat-label>Name</mat-label>
          <input matInput formControlName="name" required>
        </mat-form-field>

        <mat-form-field class="scenario-form__field">
          <mat-label>New Interest Rate (%)</mat-label>
          <input matInput type="number" formControlName="newInterestRate" required>
        </mat-form-field>

        <mat-form-field class="scenario-form__field">
          <mat-label>New Loan Term (Years)</mat-label>
          <input matInput type="number" formControlName="newLoanTermYears" required>
        </mat-form-field>

        <mat-form-field class="scenario-form__field">
          <mat-label>Refinancing Costs</mat-label>
          <input matInput type="number" formControlName="refinancingCosts" required>
        </mat-form-field>

        <mat-form-field class="scenario-form__field">
          <mat-label>New Monthly Payment</mat-label>
          <input matInput type="number" formControlName="newMonthlyPayment" required>
        </mat-form-field>

        <mat-form-field class="scenario-form__field">
          <mat-label>Monthly Savings</mat-label>
          <input matInput type="number" formControlName="monthlySavings" required>
        </mat-form-field>

        <mat-form-field class="scenario-form__field">
          <mat-label>Break-Even (Months)</mat-label>
          <input matInput type="number" formControlName="breakEvenMonths" required>
        </mat-form-field>

        <mat-form-field class="scenario-form__field">
          <mat-label>Total Savings</mat-label>
          <input matInput type="number" formControlName="totalSavings" required>
        </mat-form-field>
      </form>
    </mat-dialog-content>
    <mat-dialog-actions align="end">
      <button mat-button mat-dialog-close>Cancel</button>
      <button mat-raised-button color="primary" (click)="save()" [disabled]="!form.valid">
        Save
      </button>
    </mat-dialog-actions>
  `,
  styles: [`
    .scenario-form {
      display: flex;
      flex-direction: column;
      gap: 1rem;
      min-width: 500px;
    }

    .scenario-form__field {
      width: 100%;
    }
  `]
})
export class RefinanceScenarioDialog {
  private fb = inject(FormBuilder);
  private dialogRef = inject(MatDialog);
  private mortgageService = inject(MortgageService);

  data: RefinanceScenario | null = null;
  form: FormGroup;
  mortgages$ = this.mortgageService.mortgages$;

  constructor() {
    this.mortgageService.getMortgages().subscribe();

    this.form = this.fb.group({
      mortgageId: ['', Validators.required],
      name: ['', Validators.required],
      newInterestRate: [0, [Validators.required, Validators.min(0)]],
      newLoanTermYears: [0, [Validators.required, Validators.min(1)]],
      refinancingCosts: [0, [Validators.required, Validators.min(0)]],
      newMonthlyPayment: [0, [Validators.required, Validators.min(0)]],
      monthlySavings: [0, Validators.required],
      breakEvenMonths: [0, [Validators.required, Validators.min(0)]],
      totalSavings: [0, Validators.required]
    });

    if (this.data) {
      this.form.patchValue(this.data);
    }
  }

  save(): void {
    if (this.form.valid) {
      // Dialog will close with the form value
    }
  }
}

@Component({
  selector: 'app-refinance-scenarios',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatCardModule,
    MatDialogModule
  ],
  template: `
    <div class="scenarios">
      <div class="scenarios__header">
        <h1 class="scenarios__title">Refinance Scenarios</h1>
        <button mat-raised-button color="primary" (click)="openDialog()" class="scenarios__add-btn">
          <mat-icon>add</mat-icon>
          Add Scenario
        </button>
      </div>

      <mat-card class="scenarios__card">
        <table mat-table [dataSource]="scenarios$ | async" class="scenarios__table">
          <ng-container matColumnDef="name">
            <th mat-header-cell *matHeaderCellDef>Name</th>
            <td mat-cell *matCellDef="let scenario">{{ scenario.name }}</td>
          </ng-container>

          <ng-container matColumnDef="mortgageId">
            <th mat-header-cell *matHeaderCellDef>Mortgage</th>
            <td mat-cell *matCellDef="let scenario">{{ getMortgageAddress(scenario.mortgageId) }}</td>
          </ng-container>

          <ng-container matColumnDef="newInterestRate">
            <th mat-header-cell *matHeaderCellDef>New Rate</th>
            <td mat-cell *matCellDef="let scenario">{{ scenario.newInterestRate }}%</td>
          </ng-container>

          <ng-container matColumnDef="newMonthlyPayment">
            <th mat-header-cell *matHeaderCellDef>New Payment</th>
            <td mat-cell *matCellDef="let scenario">{{ scenario.newMonthlyPayment | currency }}</td>
          </ng-container>

          <ng-container matColumnDef="monthlySavings">
            <th mat-header-cell *matHeaderCellDef>Monthly Savings</th>
            <td mat-cell *matCellDef="let scenario">
              <span [class.scenarios__savings--positive]="scenario.monthlySavings > 0"
                    [class.scenarios__savings--negative]="scenario.monthlySavings < 0">
                {{ scenario.monthlySavings | currency }}
              </span>
            </td>
          </ng-container>

          <ng-container matColumnDef="breakEvenMonths">
            <th mat-header-cell *matHeaderCellDef>Break-Even</th>
            <td mat-cell *matCellDef="let scenario">{{ scenario.breakEvenMonths }} months</td>
          </ng-container>

          <ng-container matColumnDef="totalSavings">
            <th mat-header-cell *matHeaderCellDef>Total Savings</th>
            <td mat-cell *matCellDef="let scenario">
              <span [class.scenarios__savings--positive]="scenario.totalSavings > 0"
                    [class.scenarios__savings--negative]="scenario.totalSavings < 0">
                {{ scenario.totalSavings | currency }}
              </span>
            </td>
          </ng-container>

          <ng-container matColumnDef="actions">
            <th mat-header-cell *matHeaderCellDef>Actions</th>
            <td mat-cell *matCellDef="let scenario">
              <button mat-icon-button (click)="openDialog(scenario)" class="scenarios__action-btn">
                <mat-icon>edit</mat-icon>
              </button>
              <button mat-icon-button color="warn" (click)="delete(scenario.refinanceScenarioId)" class="scenarios__action-btn">
                <mat-icon>delete</mat-icon>
              </button>
            </td>
          </ng-container>

          <tr mat-header-row *matHeaderRowDef="displayedColumns"></tr>
          <tr mat-row *matRowDef="let row; columns: displayedColumns;"></tr>
        </table>
      </mat-card>
    </div>
  `,
  styles: [`
    .scenarios {
      padding: 2rem;
    }

    .scenarios__header {
      display: flex;
      justify-content: space-between;
      align-items: center;
      margin-bottom: 2rem;
    }

    .scenarios__title {
      margin: 0;
      font-size: 2rem;
      font-weight: 400;
    }

    .scenarios__add-btn {
      display: flex;
      align-items: center;
      gap: 0.5rem;
    }

    .scenarios__card {
      overflow-x: auto;
    }

    .scenarios__table {
      width: 100%;
    }

    .scenarios__savings--positive {
      color: #4caf50;
      font-weight: 500;
    }

    .scenarios__savings--negative {
      color: #f44336;
      font-weight: 500;
    }

    .scenarios__action-btn {
      margin-right: 0.5rem;
    }
  `]
})
export class RefinanceScenarios implements OnInit {
  private refinanceScenarioService = inject(RefinanceScenarioService);
  private mortgageService = inject(MortgageService);
  private dialog = inject(MatDialog);

  scenarios$ = this.refinanceScenarioService.refinanceScenarios$;
  mortgages$ = this.mortgageService.mortgages$;
  displayedColumns = ['name', 'mortgageId', 'newInterestRate', 'newMonthlyPayment', 'monthlySavings', 'breakEvenMonths', 'totalSavings', 'actions'];

  private mortgagesMap = new Map<string, Mortgage>();

  ngOnInit(): void {
    this.refinanceScenarioService.getRefinanceScenarios().subscribe();
    this.mortgageService.getMortgages().subscribe(mortgages => {
      mortgages.forEach(m => this.mortgagesMap.set(m.mortgageId, m));
    });
  }

  openDialog(scenario?: RefinanceScenario): void {
    const dialogRef = this.dialog.open(RefinanceScenarioDialog, {
      width: '600px',
      data: scenario
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        if (scenario) {
          const updateScenario: UpdateRefinanceScenario = {
            refinanceScenarioId: scenario.refinanceScenarioId,
            ...result
          };
          this.refinanceScenarioService.updateRefinanceScenario(updateScenario).subscribe();
        } else {
          const createScenario: CreateRefinanceScenario = result;
          this.refinanceScenarioService.createRefinanceScenario(createScenario).subscribe();
        }
      }
    });
  }

  delete(id: string): void {
    if (confirm('Are you sure you want to delete this refinance scenario?')) {
      this.refinanceScenarioService.deleteRefinanceScenario(id).subscribe();
    }
  }

  getMortgageAddress(mortgageId: string): string {
    const mortgage = this.mortgagesMap.get(mortgageId);
    return mortgage ? mortgage.propertyAddress : 'Unknown';
  }
}
