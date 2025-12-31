import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { Observable, BehaviorSubject, switchMap, tap, catchError, of, startWith } from 'rxjs';
import { GroceryList } from '../../models';
import { GroceryListService } from '../../services';
import { ListCard } from '../../components/list-card';
import { CreateListDialog } from '../../components/create-list-dialog';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [
    CommonModule,
    MatButtonModule,
    MatIconModule,
    MatDialogModule,
    MatProgressSpinnerModule,
    MatSnackBarModule,
    ListCard
  ],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.scss'
})
export class Dashboard implements OnInit {
  private _groceryListService = inject(GroceryListService);
  private _dialog = inject(MatDialog);
  private _snackBar = inject(MatSnackBar);

  private _refreshSubject = new BehaviorSubject<void>(undefined);

  viewModel$: Observable<{ lists: GroceryList[]; loading: boolean; error: string | null }> =
    this._refreshSubject.pipe(
      switchMap(() => this._groceryListService.getAll().pipe(
        switchMap(lists => of({ lists, loading: false, error: null })),
        startWith({ lists: [], loading: true, error: null }),
        catchError(err => of({ lists: [], loading: false, error: 'Failed to load lists' }))
      ))
    );

  ngOnInit(): void {
    this._refreshSubject.next();
  }

  openCreateDialog(): void {
    const dialogRef = this._dialog.open(CreateListDialog, {
      width: '400px'
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this._groceryListService.create({
          userId: '00000000-0000-0000-0000-000000000000',
          name: result.name
        }).subscribe({
          next: () => {
            this._snackBar.open('List created successfully', 'Close', { duration: 3000 });
            this._refreshSubject.next();
          },
          error: () => {
            this._snackBar.open('Failed to create list', 'Close', { duration: 3000 });
          }
        });
      }
    });
  }

  onDeleteList(list: GroceryList): void {
    this._groceryListService.delete(list.groceryListId).subscribe({
      next: () => {
        this._snackBar.open('List deleted successfully', 'Close', { duration: 3000 });
        this._refreshSubject.next();
      },
      error: () => {
        this._snackBar.open('Failed to delete list', 'Close', { duration: 3000 });
      }
    });
  }
}
