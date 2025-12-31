import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { RegistryItemService } from '../../services';
import { RegistryItemCard } from '../../components/registry-item-card/registry-item-card';
import { RegistryItemDialog } from '../../components/registry-item-dialog/registry-item-dialog';
import { ContributionDialog } from '../../components/contribution-dialog/contribution-dialog';
import { RegistryItem } from '../../models';
import { Observable } from 'rxjs';
import { ContributionService } from '../../services';

@Component({
  selector: 'app-registry-items',
  standalone: true,
  imports: [
    CommonModule,
    MatButtonModule,
    MatIconModule,
    MatProgressSpinnerModule,
    MatSnackBarModule,
    RegistryItemCard
  ],
  templateUrl: './registry-items.html',
  styleUrl: './registry-items.scss'
})
export class RegistryItems implements OnInit {
  private registryItemService = inject(RegistryItemService);
  private contributionService = inject(ContributionService);
  private dialog = inject(MatDialog);
  private snackBar = inject(MatSnackBar);
  private route = inject(ActivatedRoute);

  registryItems$!: Observable<RegistryItem[]>;
  loading$!: Observable<boolean>;

  private registryId?: string;

  ngOnInit(): void {
    this.registryItems$ = this.registryItemService.registryItems$;
    this.loading$ = this.registryItemService.loading$;

    this.route.queryParams.subscribe(params => {
      this.registryId = params['registryId'];
      this.loadRegistryItems();
    });
  }

  loadRegistryItems(): void {
    this.registryItemService.getRegistryItems(this.registryId).subscribe();
  }

  openCreateDialog(): void {
    if (!this.registryId) {
      this.snackBar.open('Please select a registry first', 'Close', { duration: 3000 });
      return;
    }

    const dialogRef = this.dialog.open(RegistryItemDialog, {
      width: '600px',
      data: { registryId: this.registryId }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.registryItemService.createRegistryItem(result).subscribe({
          next: () => {
            this.snackBar.open('Item added successfully', 'Close', { duration: 3000 });
          },
          error: () => {
            this.snackBar.open('Failed to add item', 'Close', { duration: 3000 });
          }
        });
      }
    });
  }

  onEditItem(item: RegistryItem): void {
    const dialogRef = this.dialog.open(RegistryItemDialog, {
      width: '600px',
      data: { item, registryId: item.registryId }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.registryItemService.updateRegistryItem(item.registryItemId, result).subscribe({
          next: () => {
            this.snackBar.open('Item updated successfully', 'Close', { duration: 3000 });
          },
          error: () => {
            this.snackBar.open('Failed to update item', 'Close', { duration: 3000 });
          }
        });
      }
    });
  }

  onDeleteItem(item: RegistryItem): void {
    if (confirm(`Are you sure you want to delete ${item.name}?`)) {
      this.registryItemService.deleteRegistryItem(item.registryItemId).subscribe({
        next: () => {
          this.snackBar.open('Item deleted successfully', 'Close', { duration: 3000 });
        },
        error: () => {
          this.snackBar.open('Failed to delete item', 'Close', { duration: 3000 });
        }
      });
    }
  }

  onContribute(item: RegistryItem): void {
    const dialogRef = this.dialog.open(ContributionDialog, {
      width: '600px',
      data: { item }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.contributionService.createContribution(result).subscribe({
          next: () => {
            this.snackBar.open('Thank you for your contribution!', 'Close', { duration: 3000 });
            this.loadRegistryItems();
          },
          error: () => {
            this.snackBar.open('Failed to record contribution', 'Close', { duration: 3000 });
          }
        });
      }
    });
  }
}
