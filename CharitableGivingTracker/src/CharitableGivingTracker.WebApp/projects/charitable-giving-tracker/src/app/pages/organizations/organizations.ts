import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { OrganizationService } from '../../services';
import { OrganizationCard } from '../../components/organization-card';
import { OrganizationDialog, OrganizationDialogData } from '../../components/organization-dialog';
import { Organization } from '../../models';

@Component({
  selector: 'app-organizations',
  standalone: true,
  imports: [
    CommonModule,
    MatButtonModule,
    MatIconModule,
    MatDialogModule,
    MatSnackBarModule,
    OrganizationCard
  ],
  templateUrl: './organizations.html',
  styleUrl: './organizations.scss'
})
export class Organizations implements OnInit {
  organizations$ = this.organizationService.organizations$;

  constructor(
    private organizationService: OrganizationService,
    private dialog: MatDialog,
    private snackBar: MatSnackBar
  ) {}

  ngOnInit(): void {
    this.loadData();
  }

  loadData(): void {
    this.organizationService.getAll().subscribe();
  }

  openCreateDialog(): void {
    const dialogRef = this.dialog.open(OrganizationDialog, {
      width: '500px',
      data: {} as OrganizationDialogData
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.organizationService.create(result).subscribe({
          next: () => {
            this.snackBar.open('Organization created successfully', 'Close', { duration: 3000 });
          },
          error: (error) => {
            this.snackBar.open('Error creating organization', 'Close', { duration: 3000 });
            console.error(error);
          }
        });
      }
    });
  }

  openEditDialog(organization: Organization): void {
    const dialogRef = this.dialog.open(OrganizationDialog, {
      width: '500px',
      data: { organization } as OrganizationDialogData
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.organizationService.update(result.organizationId, result).subscribe({
          next: () => {
            this.snackBar.open('Organization updated successfully', 'Close', { duration: 3000 });
          },
          error: (error) => {
            this.snackBar.open('Error updating organization', 'Close', { duration: 3000 });
            console.error(error);
          }
        });
      }
    });
  }

  deleteOrganization(id: string): void {
    if (confirm('Are you sure you want to delete this organization?')) {
      this.organizationService.delete(id).subscribe({
        next: () => {
          this.snackBar.open('Organization deleted successfully', 'Close', { duration: 3000 });
        },
        error: (error) => {
          this.snackBar.open('Error deleting organization', 'Close', { duration: 3000 });
          console.error(error);
        }
      });
    }
  }
}
