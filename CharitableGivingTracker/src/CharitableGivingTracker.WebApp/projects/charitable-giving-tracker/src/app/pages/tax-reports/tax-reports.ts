import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatTableModule } from '@angular/material/table';
import { TaxReportService } from '../../services';

@Component({
  selector: 'app-tax-reports',
  standalone: true,
  imports: [
    CommonModule,
    MatButtonModule,
    MatIconModule,
    MatCardModule,
    MatTableModule
  ],
  templateUrl: './tax-reports.html',
  styleUrl: './tax-reports.scss'
})
export class TaxReports implements OnInit {
  taxReports$ = this.taxReportService.taxReports$;
  displayedColumns: string[] = ['taxYear', 'totalCash', 'totalNonCash', 'totalDeductible', 'generatedDate'];

  constructor(private taxReportService: TaxReportService) {}

  ngOnInit(): void {
    this.loadData();
  }

  loadData(): void {
    this.taxReportService.getAll().subscribe();
  }
}
