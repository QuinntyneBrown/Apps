import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatDialog } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSelectModule } from '@angular/material/select';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { ItemService, ValueEstimateService } from '../../services';
import { ValueEstimateCard } from '../../components/value-estimate-card/value-estimate-card';
import { ValueEstimateDialog, ValueEstimateDialogData } from '../../components/value-estimate-dialog/value-estimate-dialog';
import { ValueEstimate, CreateValueEstimateCommand, UpdateValueEstimateCommand, Item } from '../../models';

@Component({
  selector: 'app-value-estimates',
  standalone: true,
  imports: [
    CommonModule,
    MatButtonModule,
    MatIconModule,
    MatProgressSpinnerModule,
    MatSelectModule,
    MatFormFieldModule,
    MatSnackBarModule,
    ValueEstimateCard
  ],
  templateUrl: './value-estimates.html',
  styleUrls: ['./value-estimates.scss']
})
export class ValueEstimates implements OnInit {
  private itemService = inject(ItemService);
  private valueEstimateService = inject(ValueEstimateService);
  private dialog = inject(MatDialog);
  private snackBar = inject(MatSnackBar);

  valueEstimates$ = this.valueEstimateService.valueEstimates$;
  items$ = this.itemService.items$;
  loading$ = this.valueEstimateService.loading$;

  selectedItemId: string | null = null;
  items: Item[] = [];

  readonly userId = '00000000-0000-0000-0000-000000000001'; // Demo user ID

  ngOnInit(): void {
    this.itemService.getItems(this.userId).subscribe(items => {
      this.items = items;
    });
    this.loadValueEstimates();
  }

  loadValueEstimates(): void {
    this.valueEstimateService.getValueEstimates(
      this.selectedItemId ?? undefined
    ).subscribe();
  }

  onItemFilterChange(itemId: string | null): void {
    this.selectedItemId = itemId;
    this.loadValueEstimates();
  }

  onAddValueEstimate(): void {
    if (this.items.length === 0) {
      this.snackBar.open('Please add items first before creating value estimates', 'Close', { duration: 3000 });
      return;
    }

    const itemId = this.selectedItemId || this.items[0]?.itemId;
    if (!itemId) {
      this.snackBar.open('No item selected', 'Close', { duration: 3000 });
      return;
    }

    const dialogRef = this.dialog.open(ValueEstimateDialog, {
      width: '500px',
      data: { itemId } as ValueEstimateDialogData
    });

    dialogRef.afterClosed().subscribe((command: CreateValueEstimateCommand | undefined) => {
      if (command) {
        this.valueEstimateService.createValueEstimate(command).subscribe({
          next: () => {
            this.snackBar.open('Value estimate created successfully', 'Close', { duration: 3000 });
          },
          error: (error) => {
            console.error('Error creating value estimate:', error);
            this.snackBar.open('Error creating value estimate', 'Close', { duration: 3000 });
          }
        });
      }
    });
  }

  onEditValueEstimate(estimate: ValueEstimate): void {
    const dialogRef = this.dialog.open(ValueEstimateDialog, {
      width: '500px',
      data: { estimate, itemId: estimate.itemId } as ValueEstimateDialogData
    });

    dialogRef.afterClosed().subscribe((command: UpdateValueEstimateCommand | undefined) => {
      if (command) {
        this.valueEstimateService.updateValueEstimate(estimate.valueEstimateId, command).subscribe({
          next: () => {
            this.snackBar.open('Value estimate updated successfully', 'Close', { duration: 3000 });
          },
          error: (error) => {
            console.error('Error updating value estimate:', error);
            this.snackBar.open('Error updating value estimate', 'Close', { duration: 3000 });
          }
        });
      }
    });
  }

  onDeleteValueEstimate(estimate: ValueEstimate): void {
    if (confirm('Are you sure you want to delete this value estimate?')) {
      this.valueEstimateService.deleteValueEstimate(estimate.valueEstimateId).subscribe({
        next: () => {
          this.snackBar.open('Value estimate deleted successfully', 'Close', { duration: 3000 });
        },
        error: (error) => {
          console.error('Error deleting value estimate:', error);
          this.snackBar.open('Error deleting value estimate', 'Close', { duration: 3000 });
        }
      });
    }
  }

  getItemName(itemId: string): string {
    const item = this.items.find(i => i.itemId === itemId);
    return item?.name || 'Unknown Item';
  }
}
