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
import { WithdrawalStrategyService, RetirementScenarioService } from '../services';
import { CreateWithdrawalStrategy, UpdateWithdrawalStrategy, WithdrawalStrategyType, WithdrawalStrategyTypeLabels } from '../models';

@Component({
  selector: 'app-withdrawal-strategy-form',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatCheckboxModule,
    MatSelectModule
  ],
  template: `
    <div class="withdrawal-strategy-form">
      <mat-card class="withdrawal-strategy-form__card">
        <mat-card-header>
          <mat-card-title>{{ isEditMode ? 'Edit Withdrawal Strategy' : 'New Withdrawal Strategy' }}</mat-card-title>
        </mat-card-header>
        <mat-card-content>
          <form [formGroup]="form" class="withdrawal-strategy-form__form">
            <mat-form-field appearance="outline" class="withdrawal-strategy-form__field">
              <mat-label>Retirement Scenario</mat-label>
              <mat-select formControlName="retirementScenarioId">
                <mat-option *ngFor="let scenario of (scenarios$ | async)" [value]="scenario.retirementScenarioId">
                  {{ scenario.name }}
                </mat-option>
              </mat-select>
              <mat-error *ngIf="form.get('retirementScenarioId')?.hasError('required')">Retirement scenario is required</mat-error>
            </mat-form-field>

            <mat-form-field appearance="outline" class="withdrawal-strategy-form__field">
              <mat-label>Strategy Name</mat-label>
              <input matInput formControlName="name" placeholder="e.g., 4% Rule">
              <mat-error *ngIf="form.get('name')?.hasError('required')">Strategy name is required</mat-error>
            </mat-form-field>

            <mat-form-field appearance="outline" class="withdrawal-strategy-form__field">
              <mat-label>Strategy Type</mat-label>
              <mat-select formControlName="strategyType">
                <mat-option *ngFor="let type of strategyTypes" [value]="type.value">
                  {{ type.label }}
                </mat-option>
              </mat-select>
              <mat-error *ngIf="form.get('strategyType')?.hasError('required')">Strategy type is required</mat-error>
            </mat-form-field>

            <div class="withdrawal-strategy-form__row">
              <mat-form-field appearance="outline" class="withdrawal-strategy-form__field">
                <mat-label>Withdrawal Rate</mat-label>
                <input matInput type="number" formControlName="withdrawalRate" step="0.01" placeholder="0.04">
                <mat-hint>Enter as decimal (e.g., 0.04 for 4%)</mat-hint>
                <mat-error *ngIf="form.get('withdrawalRate')?.hasError('required')">Withdrawal rate is required</mat-error>
                <mat-error *ngIf="form.get('withdrawalRate')?.hasError('min')">Must be at least 0</mat-error>
              </mat-form-field>

              <mat-form-field appearance="outline" class="withdrawal-strategy-form__field">
                <mat-label>Annual Withdrawal Amount</mat-label>
                <input matInput type="number" formControlName="annualWithdrawalAmount" placeholder="40000">
                <span matTextPrefix>$&nbsp;</span>
                <mat-error *ngIf="form.get('annualWithdrawalAmount')?.hasError('required')">Annual withdrawal amount is required</mat-error>
                <mat-error *ngIf="form.get('annualWithdrawalAmount')?.hasError('min')">Must be at least 0</mat-error>
              </mat-form-field>
            </div>

            <mat-form-field appearance="outline" class="withdrawal-strategy-form__field">
              <mat-label>Minimum Balance</mat-label>
              <input matInput type="number" formControlName="minimumBalance" placeholder="100000">
              <span matTextPrefix>$&nbsp;</span>
              <mat-hint>Optional: Minimum account balance to maintain</mat-hint>
              <mat-error *ngIf="form.get('minimumBalance')?.hasError('min')">Must be at least 0</mat-error>
            </mat-form-field>

            <div class="withdrawal-strategy-form__checkbox">
              <mat-checkbox formControlName="adjustForInflation">
                Adjust for Inflation
              </mat-checkbox>
            </div>

            <mat-form-field appearance="outline" class="withdrawal-strategy-form__field">
              <mat-label>Notes</mat-label>
              <textarea matInput formControlName="notes" rows="3" placeholder="Additional notes about this strategy"></textarea>
            </mat-form-field>
          </form>
        </mat-card-content>
        <mat-card-actions class="withdrawal-strategy-form__actions">
          <button mat-button (click)="cancel()">Cancel</button>
          <button mat-raised-button color="primary" (click)="save()" [disabled]="!form.valid">
            {{ isEditMode ? 'Update' : 'Create' }}
          </button>
        </mat-card-actions>
      </mat-card>
    </div>
  `,
  styles: [`
    .withdrawal-strategy-form {
      padding: 2rem;
      max-width: 800px;
      margin: 0 auto;
    }

    .withdrawal-strategy-form__card {
      margin-bottom: 2rem;
    }

    .withdrawal-strategy-form__form {
      display: flex;
      flex-direction: column;
      gap: 1rem;
      padding: 1rem 0;
    }

    .withdrawal-strategy-form__row {
      display: grid;
      grid-template-columns: repeat(auto-fit, minmax(250px, 1fr));
      gap: 1rem;
    }

    .withdrawal-strategy-form__field {
      width: 100%;
    }

    .withdrawal-strategy-form__checkbox {
      padding: 0.5rem 0;
    }

    .withdrawal-strategy-form__actions {
      display: flex;
      justify-content: flex-end;
      gap: 1rem;
      padding: 1rem;
    }
  `]
})
export class WithdrawalStrategyForm implements OnInit {
  private fb = inject(FormBuilder);
  private strategyService = inject(WithdrawalStrategyService);
  private scenarioService = inject(RetirementScenarioService);
  private route = inject(ActivatedRoute);
  private router = inject(Router);

  form!: FormGroup;
  isEditMode = false;
  strategyId?: string;
  scenarios$ = this.scenarioService.scenarios$;

  strategyTypes = Object.keys(WithdrawalStrategyType)
    .filter(key => !isNaN(Number(WithdrawalStrategyType[key as keyof typeof WithdrawalStrategyType])))
    .map(key => ({
      value: WithdrawalStrategyType[key as keyof typeof WithdrawalStrategyType],
      label: WithdrawalStrategyTypeLabels[WithdrawalStrategyType[key as keyof typeof WithdrawalStrategyType]]
    }));

  ngOnInit(): void {
    this.scenarioService.loadScenarios().subscribe();
    this.initForm();

    const id = this.route.snapshot.paramMap.get('id');
    if (id && id !== 'new') {
      this.isEditMode = true;
      this.strategyId = id;
      this.loadStrategy(id);
    }
  }

  private initForm(): void {
    this.form = this.fb.group({
      retirementScenarioId: ['', Validators.required],
      name: ['', Validators.required],
      strategyType: [WithdrawalStrategyType.PercentageBased, Validators.required],
      withdrawalRate: [0.04, [Validators.required, Validators.min(0)]],
      annualWithdrawalAmount: [0, [Validators.required, Validators.min(0)]],
      adjustForInflation: [true],
      minimumBalance: [null, Validators.min(0)],
      notes: ['']
    });
  }

  private loadStrategy(id: string): void {
    this.strategyService.getStrategy(id).subscribe(strategy => {
      this.form.patchValue({
        retirementScenarioId: strategy.retirementScenarioId,
        name: strategy.name,
        strategyType: strategy.strategyType,
        withdrawalRate: strategy.withdrawalRate,
        annualWithdrawalAmount: strategy.annualWithdrawalAmount,
        adjustForInflation: strategy.adjustForInflation,
        minimumBalance: strategy.minimumBalance,
        notes: strategy.notes
      });
    });
  }

  save(): void {
    if (this.form.valid) {
      const formValue = this.form.value;

      if (this.isEditMode && this.strategyId) {
        const update: UpdateWithdrawalStrategy = {
          withdrawalStrategyId: this.strategyId,
          name: formValue.name,
          strategyType: formValue.strategyType,
          withdrawalRate: formValue.withdrawalRate,
          annualWithdrawalAmount: formValue.annualWithdrawalAmount,
          adjustForInflation: formValue.adjustForInflation,
          minimumBalance: formValue.minimumBalance,
          notes: formValue.notes
        };
        this.strategyService.updateStrategy(update).subscribe(() => {
          this.router.navigate(['/withdrawal-strategies']);
        });
      } else {
        const create: CreateWithdrawalStrategy = {
          retirementScenarioId: formValue.retirementScenarioId,
          name: formValue.name,
          strategyType: formValue.strategyType,
          withdrawalRate: formValue.withdrawalRate,
          annualWithdrawalAmount: formValue.annualWithdrawalAmount,
          adjustForInflation: formValue.adjustForInflation,
          minimumBalance: formValue.minimumBalance,
          notes: formValue.notes
        };
        this.strategyService.createStrategy(create).subscribe(() => {
          this.router.navigate(['/withdrawal-strategies']);
        });
      }
    }
  }

  cancel(): void {
    this.router.navigate(['/withdrawal-strategies']);
  }
}
