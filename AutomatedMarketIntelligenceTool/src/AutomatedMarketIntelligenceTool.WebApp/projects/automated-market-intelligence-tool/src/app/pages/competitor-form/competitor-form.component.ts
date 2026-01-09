import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import { CompetitorsService } from '../../services';
import { MarketPosition, MarketPositionLabels } from '../../models';

@Component({
  selector: 'app-competitor-form',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatButtonModule
  ],
  templateUrl: './competitor-form.component.html',
  styleUrl: './competitor-form.component.scss'
})
export class CompetitorForm implements OnInit {
  private readonly fb = inject(FormBuilder);
  private readonly router = inject(Router);
  private readonly route = inject(ActivatedRoute);
  private readonly competitorsService = inject(CompetitorsService);

  isEditMode = false;
  competitorId: string | null = null;

  marketPositions = Object.entries(MarketPositionLabels).map(([key, value]) => ({
    value: Number(key) as MarketPosition,
    label: value
  }));

  form = this.fb.group({
    name: ['', [Validators.required]],
    industry: [''],
    website: [''],
    description: [''],
    employeeCount: [null as number | null],
    annualRevenue: [null as number | null],
    marketPosition: [MarketPosition.Follower],
    strengths: [''],
    weaknesses: ['']
  });

  ngOnInit(): void {
    this.competitorId = this.route.snapshot.params['id'];
    this.isEditMode = !!this.competitorId && this.competitorId !== 'new';

    if (this.isEditMode && this.competitorId) {
      this.competitorsService.getCompetitorById(this.competitorId).subscribe(competitor => {
        this.form.patchValue(competitor);
      });
    }
  }

  onSubmit(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    const formValue = this.form.value;

    if (this.isEditMode && this.competitorId) {
      this.competitorsService.updateCompetitor({
        competitorId: this.competitorId,
        ...formValue
      } as any).subscribe(() => {
        this.router.navigate(['/competitors']);
      });
    } else {
      this.competitorsService.createCompetitor(formValue as any).subscribe(() => {
        this.router.navigate(['/competitors']);
      });
    }
  }

  onCancel(): void {
    this.router.navigate(['/competitors']);
  }
}
