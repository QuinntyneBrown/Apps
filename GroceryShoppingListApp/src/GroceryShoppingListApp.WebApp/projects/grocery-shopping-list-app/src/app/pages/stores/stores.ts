import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatCardModule } from '@angular/material/card';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { Observable, BehaviorSubject, switchMap, map, catchError, of, startWith } from 'rxjs';
import { Store } from '../../models';
import { StoreService } from '../../services';

interface StoresViewModel {
  stores: Store[];
  loading: boolean;
  error: string | null;
}

@Component({
  selector: 'app-stores',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatButtonModule,
    MatIconModule,
    MatFormFieldModule,
    MatInputModule,
    MatCardModule,
    MatProgressSpinnerModule,
    MatSnackBarModule
  ],
  templateUrl: './stores.html',
  styleUrl: './stores.scss'
})
export class Stores implements OnInit {
  private _storeService = inject(StoreService);
  private _fb = inject(FormBuilder);
  private _snackBar = inject(MatSnackBar);

  private _refreshSubject = new BehaviorSubject<void>(undefined);

  form = this._fb.group({
    name: ['', [Validators.required, Validators.maxLength(255)]],
    address: ['']
  });

  viewModel$!: Observable<StoresViewModel>;

  ngOnInit(): void {
    this.viewModel$ = this._refreshSubject.pipe(
      switchMap(() => this._storeService.getAll().pipe(
        map(stores => ({ stores, loading: false, error: null })),
        startWith({ stores: [], loading: true, error: null }),
        catchError(err => of({ stores: [], loading: false, error: 'Failed to load stores' }))
      ))
    );
  }

  onSubmit(): void {
    if (this.form.valid) {
      this._storeService.create({
        userId: '00000000-0000-0000-0000-000000000000',
        name: this.form.value.name!,
        address: this.form.value.address || undefined
      }).subscribe({
        next: () => {
          this._snackBar.open('Store added successfully', 'Close', { duration: 3000 });
          this.form.reset();
          this._refreshSubject.next();
        },
        error: () => {
          this._snackBar.open('Failed to add store', 'Close', { duration: 3000 });
        }
      });
    }
  }

  onDelete(store: Store): void {
    this._storeService.delete(store.storeId).subscribe({
      next: () => {
        this._snackBar.open('Store deleted successfully', 'Close', { duration: 3000 });
        this._refreshSubject.next();
      },
      error: () => {
        this._snackBar.open('Failed to delete store', 'Close', { duration: 3000 });
      }
    });
  }
}
