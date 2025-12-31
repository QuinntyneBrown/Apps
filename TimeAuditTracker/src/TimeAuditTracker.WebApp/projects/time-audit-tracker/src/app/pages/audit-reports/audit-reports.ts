import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { AuditReportService } from '../../services';
import { AuditReportDialog } from '../../components/audit-report-dialog/audit-report-dialog';

@Component({
  selector: 'app-audit-reports',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatCardModule,
    MatDialogModule
  ],
  templateUrl: './audit-reports.html',
  styleUrl: './audit-reports.scss'
})
export class AuditReports implements OnInit {
  private _auditReportService = inject(AuditReportService);
  private _dialog = inject(MatDialog);

  auditReports$ = this._auditReportService.auditReports$;
  displayedColumns = ['title', 'startDate', 'endDate', 'periodDays', 'totalTrackedHours', 'productiveHours', 'productivityPercentage', 'createdAt', 'actions'];

  ngOnInit(): void {
    this._auditReportService.getAll().subscribe();
  }

  openDialog(report?: any): void {
    const dialogRef = this._dialog.open(AuditReportDialog, {
      width: '600px',
      data: {
        auditReport: report,
        userId: '00000000-0000-0000-0000-000000000000' // TODO: Get from auth service
      }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        if (report) {
          this._auditReportService.update(report.auditReportId, result).subscribe();
        } else {
          this._auditReportService.create(result).subscribe();
        }
      }
    });
  }

  deleteReport(id: string): void {
    if (confirm('Are you sure you want to delete this audit report?')) {
      this._auditReportService.delete(id).subscribe();
    }
  }

  getProductivityClass(percentage: number): string {
    if (percentage >= 70) return 'high';
    if (percentage >= 40) return 'medium';
    return 'low';
  }
}
