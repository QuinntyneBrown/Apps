import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { InsightsService } from '../../services';
import { InsightCategory, InsightCategoryLabels, InsightImpact, InsightImpactLabels } from '../../models';

@Component({
  selector: 'app-insight-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, MatCardModule, MatFormFieldModule, MatInputModule, MatSelectModule, MatButtonModule, MatCheckboxModule],
  templateUrl: './insight-form.component.html',
  styleUrl: './insight-form.component.scss'
})
export class InsightForm implements OnInit {
  private readonly fb = inject(FormBuilder);
  private readonly router = inject(Router);
  private readonly route = inject(ActivatedRoute);
  private readonly insightsService = inject(InsightsService);

  isEditMode = false;
  insightId: string | null = null;
  categories = Object.entries(InsightCategoryLabels).map(([k, v]) => ({ value: Number(k), label: v }));
  impacts = Object.entries(InsightImpactLabels).map(([k, v]) => ({ value: Number(k), label: v }));

  form = this.fb.group({
    title: ['', [Validators.required]],
    description: ['', [Validators.required]],
    category: [InsightCategory.General],
    impact: [InsightImpact.Medium],
    source: [''],
    sourceUrl: [''],
    tagsInput: [''],
    isActionable: [false]
  });

  ngOnInit(): void {
    this.insightId = this.route.snapshot.params['id'];
    this.isEditMode = !!this.insightId && this.insightId !== 'new';
    if (this.isEditMode && this.insightId) {
      this.insightsService.getInsightById(this.insightId).subscribe(insight => {
        this.form.patchValue({ ...insight, tagsInput: insight.tags.join(', ') });
      });
    }
  }

  onSubmit(): void {
    if (this.form.invalid) { this.form.markAllAsTouched(); return; }
    const { tagsInput, ...formValue } = this.form.value;
    const tags = tagsInput ? tagsInput.split(',').map((t: string) => t.trim()).filter((t: string) => t) : [];
    if (this.isEditMode && this.insightId) {
      this.insightsService.updateInsight({ insightId: this.insightId, ...formValue, tags } as any).subscribe(() => this.router.navigate(['/insights']));
    } else {
      this.insightsService.createInsight({ ...formValue, tags } as any).subscribe(() => this.router.navigate(['/insights']));
    }
  }

  onCancel(): void { this.router.navigate(['/insights']); }
}
