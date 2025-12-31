import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatDialog } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSelectModule } from '@angular/material/select';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { ItemService } from '../../services';
import { ItemCard } from '../../components/item-card/item-card';
import { ItemDialog, ItemDialogData } from '../../components/item-dialog/item-dialog';
import { Item, Category, Room, CategoryLabels, RoomLabels, CreateItemCommand, UpdateItemCommand } from '../../models';

@Component({
  selector: 'app-items',
  standalone: true,
  imports: [
    CommonModule,
    MatButtonModule,
    MatIconModule,
    MatProgressSpinnerModule,
    MatSelectModule,
    MatFormFieldModule,
    MatSnackBarModule,
    ItemCard
  ],
  templateUrl: './items.html',
  styleUrls: ['./items.scss']
})
export class Items implements OnInit {
  private itemService = inject(ItemService);
  private dialog = inject(MatDialog);
  private snackBar = inject(MatSnackBar);

  items$ = this.itemService.items$;
  loading$ = this.itemService.loading$;

  selectedCategory: Category | null = null;
  selectedRoom: Room | null = null;

  categories = [
    { value: null, label: 'All Categories' },
    ...Object.keys(Category)
      .filter(key => !isNaN(Number(key)))
      .map(key => ({ value: Number(key) as Category, label: CategoryLabels[Number(key) as Category] }))
  ];

  rooms = [
    { value: null, label: 'All Rooms' },
    ...Object.keys(Room)
      .filter(key => !isNaN(Number(key)))
      .map(key => ({ value: Number(key) as Room, label: RoomLabels[Number(key) as Room] }))
  ];

  readonly userId = '00000000-0000-0000-0000-000000000001'; // Demo user ID

  ngOnInit(): void {
    this.loadItems();
  }

  loadItems(): void {
    this.itemService.getItems(
      this.userId,
      this.selectedCategory ?? undefined,
      this.selectedRoom ?? undefined
    ).subscribe();
  }

  onCategoryChange(category: Category | null): void {
    this.selectedCategory = category;
    this.loadItems();
  }

  onRoomChange(room: Room | null): void {
    this.selectedRoom = room;
    this.loadItems();
  }

  onAddItem(): void {
    const dialogRef = this.dialog.open(ItemDialog, {
      width: '600px',
      data: { userId: this.userId } as ItemDialogData
    });

    dialogRef.afterClosed().subscribe((command: CreateItemCommand | undefined) => {
      if (command) {
        this.itemService.createItem(command).subscribe({
          next: () => {
            this.snackBar.open('Item created successfully', 'Close', { duration: 3000 });
          },
          error: (error) => {
            console.error('Error creating item:', error);
            this.snackBar.open('Error creating item', 'Close', { duration: 3000 });
          }
        });
      }
    });
  }

  onEditItem(item: Item): void {
    const dialogRef = this.dialog.open(ItemDialog, {
      width: '600px',
      data: { item, userId: this.userId } as ItemDialogData
    });

    dialogRef.afterClosed().subscribe((command: UpdateItemCommand | undefined) => {
      if (command) {
        this.itemService.updateItem(item.itemId, command).subscribe({
          next: () => {
            this.snackBar.open('Item updated successfully', 'Close', { duration: 3000 });
          },
          error: (error) => {
            console.error('Error updating item:', error);
            this.snackBar.open('Error updating item', 'Close', { duration: 3000 });
          }
        });
      }
    });
  }

  onDeleteItem(item: Item): void {
    if (confirm(`Are you sure you want to delete "${item.name}"?`)) {
      this.itemService.deleteItem(item.itemId).subscribe({
        next: () => {
          this.snackBar.open('Item deleted successfully', 'Close', { duration: 3000 });
        },
        error: (error) => {
          console.error('Error deleting item:', error);
          this.snackBar.open('Error deleting item', 'Close', { duration: 3000 });
        }
      });
    }
  }

  onViewDetails(item: Item): void {
    // Navigate to item details or show more info
    console.log('View details for:', item);
  }
}
