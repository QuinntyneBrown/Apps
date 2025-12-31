import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { DateIdeaService } from '../../services';
import { Category, BudgetRange } from '../../models';

@Component({
  selector: 'app-date-idea-form',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatButtonModule,
    MatIconModule
  ],
  templateUrl: './date-idea-form.html',
  styleUrl: './date-idea-form.scss'
})
export class DateIdeaForm implements OnInit {
  form!: FormGroup;
  isEditMode = false;
  dateIdeaId?: string;
  categories = Object.keys(Category).filter(k => isNaN(Number(k)));
  budgetRanges = Object.keys(BudgetRange).filter(k => isNaN(Number(k)));

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private dateIdeaService: DateIdeaService
  ) {}

  ngOnInit(): void {
    this.initializeForm();

    this.dateIdeaId = this.route.snapshot.paramMap.get('id') || undefined;
    this.isEditMode = !!this.dateIdeaId;

    if (this.isEditMode && this.dateIdeaId) {
      this.dateIdeaService.getById(this.dateIdeaId).subscribe(dateIdea => {
        if (dateIdea) {
          this.form.patchValue(dateIdea);
        }
      });
    }
  }

  initializeForm(): void {
    this.form = this.fb.group({
      title: ['', Validators.required],
      description: ['', Validators.required],
      category: [Category.Romantic, Validators.required],
      budgetRange: [BudgetRange.Medium, Validators.required],
      location: [''],
      durationMinutes: [null],
      season: [''],
      isFavorite: [false]
    });
  }

  onSubmit(): void {
    if (this.form.valid) {
      const formValue = this.form.value;

      if (this.isEditMode && this.dateIdeaId) {
        this.dateIdeaService.update(this.dateIdeaId, {
          dateIdeaId: this.dateIdeaId,
          ...formValue
        }).subscribe(() => {
          this.router.navigate(['/date-ideas', this.dateIdeaId]);
        });
      } else {
        this.dateIdeaService.create({
          userId: '00000000-0000-0000-0000-000000000000', // TODO: Get from auth service
          ...formValue
        }).subscribe(created => {
          this.router.navigate(['/date-ideas', created.dateIdeaId]);
        });
      }
    }
  }

  cancel(): void {
    if (this.isEditMode && this.dateIdeaId) {
      this.router.navigate(['/date-ideas', this.dateIdeaId]);
    } else {
      this.router.navigate(['/date-ideas']);
    }
  }
}
