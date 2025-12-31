import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatChipsModule } from '@angular/material/chips';
import { CertificationService } from '../../services';

@Component({
  selector: 'app-certifications',
  standalone: true,
  imports: [CommonModule, MatTableModule, MatButtonModule, MatIconModule, MatCardModule, MatChipsModule],
  templateUrl: './certifications.html',
  styleUrl: './certifications.scss'
})
export class Certifications implements OnInit {
  private _certificationService = inject(CertificationService);

  certifications$ = this._certificationService.certifications$;
  displayedColumns: string[] = ['name', 'issuingOrganization', 'issueDate', 'expirationDate', 'status', 'actions'];

  ngOnInit(): void {
    this._certificationService.getCertifications().subscribe();
  }

  deleteCertification(id: string): void {
    if (confirm('Are you sure you want to delete this certification?')) {
      this._certificationService.deleteCertification(id).subscribe();
    }
  }

  formatDate(date?: string): string {
    if (!date) return '-';
    return new Date(date).toLocaleDateString();
  }
}
