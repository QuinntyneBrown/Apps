import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { Observable } from 'rxjs';
import { Beneficiary } from '../../models';
import { BeneficiaryService } from '../../services';

@Component({
  selector: 'app-beneficiary-list',
  standalone: true,
  imports: [
    CommonModule,
    RouterLink,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatCardModule
  ],
  templateUrl: './beneficiary-list.html',
  styleUrl: './beneficiary-list.scss'
})
export class BeneficiaryList implements OnInit {
  beneficiaries$: Observable<Beneficiary[]>;
  displayedColumns: string[] = ['name', 'dateOfBirth', 'age', 'yearsUntilCollege', 'relationship', 'isPrimary'];

  constructor(private beneficiaryService: BeneficiaryService) {
    this.beneficiaries$ = this.beneficiaryService.beneficiaries$;
  }

  ngOnInit(): void {
    this.beneficiaryService.getBeneficiaries().subscribe();
  }

  deleteBeneficiary(id: string): void {
    if (confirm('Are you sure you want to delete this beneficiary?')) {
      this.beneficiaryService.deleteBeneficiary(id).subscribe();
    }
  }
}
