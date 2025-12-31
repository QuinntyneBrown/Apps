import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatTableModule } from '@angular/material/table';
import { MatChipsModule } from '@angular/material/chips';
import { EmployeesService } from '../../services';
import { EmployeeDialog } from '../../components';

@Component({
  selector: 'app-employees',
  imports: [CommonModule, MatButtonModule, MatIconModule, MatDialogModule, MatTableModule, MatChipsModule],
  templateUrl: './employees.html',
  styleUrl: './employees.scss',
})
export class Employees implements OnInit {
  private readonly _employeesService = inject(EmployeesService);
  private readonly _dialog = inject(MatDialog);

  displayedColumns = ['firstName', 'lastName', 'email', 'department', 'position', 'isActive', 'actions'];

  employees$ = this._employeesService.employees$;

  ngOnInit(): void {
    this._employeesService.getAll().subscribe();
  }

  openDialog(employee?: any): void {
    const dialogRef = this._dialog.open(EmployeeDialog, {
      width: '600px',
      data: { employee },
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        if (employee) {
          this._employeesService.update(employee.employeeId, result).subscribe();
        } else {
          this._employeesService.create(result).subscribe();
        }
      }
    });
  }

  deleteEmployee(id: string): void {
    if (confirm('Are you sure you want to delete this employee?')) {
      this._employeesService.delete(id).subscribe();
    }
  }
}
