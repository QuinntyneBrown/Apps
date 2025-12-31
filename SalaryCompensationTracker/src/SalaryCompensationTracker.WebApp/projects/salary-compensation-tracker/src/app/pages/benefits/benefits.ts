import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTableModule } from '@angular/material/table';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { Observable } from 'rxjs';
import { BenefitService } from '../../services';
import { Benefit } from '../../models';
import { BenefitFormDialog } from '../../components/benefit-form-dialog/benefit-form-dialog';

@Component({
  selector: 'app-benefits',
  standalone: true,
  imports: [CommonModule, MatCardModule, MatButtonModule, MatIconModule, MatTableModule, MatDialogModule],
  templateUrl: './benefits.html',
  styleUrl: './benefits.scss'
})
export class Benefits implements OnInit {
  private _benefitService = inject(BenefitService);
  private _dialog = inject(MatDialog);

  benefits$!: Observable<Benefit[]>;
  displayedColumns: string[] = ['name', 'category', 'estimatedValue', 'employerContribution', 'employeeContribution', 'actions'];

  ngOnInit(): void {
    this._benefitService.getBenefits().subscribe();
    this.benefits$ = this._benefitService.benefits$;
  }

  openCreateDialog(): void {
    const dialogRef = this._dialog.open(BenefitFormDialog, {
      width: '600px',
      data: null
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this._benefitService.createBenefit(result).subscribe();
      }
    });
  }

  openEditDialog(benefit: Benefit): void {
    const dialogRef = this._dialog.open(BenefitFormDialog, {
      width: '600px',
      data: benefit
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this._benefitService.updateBenefit(benefit.benefitId, result).subscribe();
      }
    });
  }

  deleteBenefit(id: string): void {
    if (confirm('Are you sure you want to delete this benefit?')) {
      this._benefitService.deleteBenefit(id).subscribe();
    }
  }
}
