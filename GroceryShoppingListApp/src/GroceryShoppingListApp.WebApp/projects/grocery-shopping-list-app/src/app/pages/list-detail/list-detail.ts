import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { Observable, BehaviorSubject, switchMap, combineLatest, map, catchError, of, startWith, tap } from 'rxjs';
import { GroceryList, GroceryItem } from '../../models';
import { GroceryListService, GroceryItemService } from '../../services';
import { ItemCard } from '../../components/item-card';
import { CreateItemDialog, CreateItemDialogData } from '../../components/create-item-dialog';

interface ListDetailViewModel {
  list: GroceryList | null;
  items: GroceryItem[];
  loading: boolean;
  error: string | null;
  completedCount: number;
}

@Component({
  selector: 'app-list-detail',
  standalone: true,
  imports: [
    CommonModule,
    RouterModule,
    MatButtonModule,
    MatIconModule,
    MatDialogModule,
    MatProgressSpinnerModule,
    MatSnackBarModule,
    ItemCard
  ],
  templateUrl: './list-detail.html',
  styleUrl: './list-detail.scss'
})
export class ListDetail implements OnInit {
  private _route = inject(ActivatedRoute);
  private _router = inject(Router);
  private _groceryListService = inject(GroceryListService);
  private _groceryItemService = inject(GroceryItemService);
  private _dialog = inject(MatDialog);
  private _snackBar = inject(MatSnackBar);

  private _refreshSubject = new BehaviorSubject<void>(undefined);
  private _listId: string = '';

  viewModel$!: Observable<ListDetailViewModel>;

  ngOnInit(): void {
    this._listId = this._route.snapshot.paramMap.get('id') || '';

    this.viewModel$ = this._refreshSubject.pipe(
      switchMap(() => combineLatest([
        this._groceryListService.getById(this._listId),
        this._groceryItemService.getAll(this._listId)
      ]).pipe(
        map(([list, items]) => ({
          list,
          items,
          loading: false,
          error: null,
          completedCount: items.filter(i => i.isChecked).length
        })),
        startWith({ list: null, items: [], loading: true, error: null, completedCount: 0 }),
        catchError(err => of({ list: null, items: [], loading: false, error: 'Failed to load list details', completedCount: 0 }))
      ))
    );
  }

  openAddItemDialog(): void {
    const dialogRef = this._dialog.open(CreateItemDialog, {
      width: '400px',
      data: { groceryListId: this._listId } as CreateItemDialogData
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this._groceryItemService.create(result).subscribe({
          next: () => {
            this._snackBar.open('Item added successfully', 'Close', { duration: 3000 });
            this._refreshSubject.next();
          },
          error: () => {
            this._snackBar.open('Failed to add item', 'Close', { duration: 3000 });
          }
        });
      }
    });
  }

  onToggleItem(item: GroceryItem): void {
    this._groceryItemService.toggleChecked(item).subscribe({
      next: () => this._refreshSubject.next(),
      error: () => {
        this._snackBar.open('Failed to update item', 'Close', { duration: 3000 });
      }
    });
  }

  onDeleteItem(item: GroceryItem): void {
    this._groceryItemService.delete(item.groceryItemId).subscribe({
      next: () => {
        this._snackBar.open('Item deleted successfully', 'Close', { duration: 3000 });
        this._refreshSubject.next();
      },
      error: () => {
        this._snackBar.open('Failed to delete item', 'Close', { duration: 3000 });
      }
    });
  }

  goBack(): void {
    this._router.navigate(['/']);
  }
}
