import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { DonationService, OrganizationService } from '../../services';
import { DonationCard } from '../../components/donation-card';
import { DonationDialog, DonationDialogData } from '../../components/donation-dialog';
import { Donation, Organization } from '../../models';

@Component({
  selector: 'app-donations',
  standalone: true,
  imports: [
    CommonModule,
    MatButtonModule,
    MatIconModule,
    MatDialogModule,
    MatSnackBarModule,
    DonationCard
  ],
  templateUrl: './donations.html',
  styleUrl: './donations.scss'
})
export class Donations implements OnInit {
  donations$ = this.donationService.donations$;
  organizations: Organization[] = [];

  constructor(
    private donationService: DonationService,
    private organizationService: OrganizationService,
    private dialog: MatDialog,
    private snackBar: MatSnackBar
  ) {}

  ngOnInit(): void {
    this.loadData();
  }

  loadData(): void {
    this.donationService.getAll().subscribe();
    this.organizationService.getAll().subscribe(orgs => {
      this.organizations = orgs;
    });
  }

  openCreateDialog(): void {
    const dialogRef = this.dialog.open(DonationDialog, {
      width: '500px',
      data: { organizations: this.organizations } as DonationDialogData
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.donationService.create(result).subscribe({
          next: () => {
            this.snackBar.open('Donation created successfully', 'Close', { duration: 3000 });
          },
          error: (error) => {
            this.snackBar.open('Error creating donation', 'Close', { duration: 3000 });
            console.error(error);
          }
        });
      }
    });
  }

  openEditDialog(donation: Donation): void {
    const dialogRef = this.dialog.open(DonationDialog, {
      width: '500px',
      data: { donation, organizations: this.organizations } as DonationDialogData
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.donationService.update(result.donationId, result).subscribe({
          next: () => {
            this.snackBar.open('Donation updated successfully', 'Close', { duration: 3000 });
          },
          error: (error) => {
            this.snackBar.open('Error updating donation', 'Close', { duration: 3000 });
            console.error(error);
          }
        });
      }
    });
  }

  deleteDonation(id: string): void {
    if (confirm('Are you sure you want to delete this donation?')) {
      this.donationService.delete(id).subscribe({
        next: () => {
          this.snackBar.open('Donation deleted successfully', 'Close', { duration: 3000 });
        },
        error: (error) => {
          this.snackBar.open('Error deleting donation', 'Close', { duration: 3000 });
          console.error(error);
        }
      });
    }
  }
}
