import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatChipsModule } from '@angular/material/chips';
import { WishlistsService } from '../../services';
import { WishlistFormDialog } from '../../components/wishlist-form-dialog/wishlist-form-dialog';
import { Wishlist } from '../../models';

@Component({
  selector: 'app-wishlists',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatCardModule,
    MatDialogModule,
    MatChipsModule
  ],
  templateUrl: './wishlists.html',
  styleUrl: './wishlists.scss'
})
export class Wishlists {
  private _wishlistsService = inject(WishlistsService);
  private _dialog = inject(MatDialog);

  wishlists$ = this._wishlistsService.wishlists$;
  displayedColumns: string[] = ['title', 'platform', 'genre', 'priority', 'acquired', 'actions'];

  ngOnInit() {
    this._wishlistsService.getAll().subscribe();
  }

  openAddDialog(): void {
    const dialogRef = this._dialog.open(WishlistFormDialog, {
      width: '600px',
      data: {}
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this._wishlistsService.create(result).subscribe();
      }
    });
  }

  openEditDialog(wishlist: Wishlist): void {
    const dialogRef = this._dialog.open(WishlistFormDialog, {
      width: '600px',
      data: { wishlist }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this._wishlistsService.update(wishlist.wishlistId, result).subscribe();
      }
    });
  }

  deleteWishlist(id: string): void {
    if (confirm('Are you sure you want to delete this wishlist item?')) {
      this._wishlistsService.delete(id).subscribe();
    }
  }
}
