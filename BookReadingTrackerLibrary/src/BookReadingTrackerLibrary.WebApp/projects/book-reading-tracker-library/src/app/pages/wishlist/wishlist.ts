import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatChipsModule } from '@angular/material/chips';
import { WishlistService } from '../../services';

@Component({
  selector: 'app-wishlist',
  imports: [CommonModule, MatButtonModule, MatIconModule, MatCardModule, MatChipsModule],
  templateUrl: './wishlist.html',
  styleUrl: './wishlist.scss'
})
export class Wishlist implements OnInit {
  private readonly wishlistService = inject(WishlistService);

  wishlists$ = this.wishlistService.wishlists$;

  ngOnInit(): void {
    this.wishlistService.getWishlists(undefined, false).subscribe();
  }

  onDeleteWishlist(wishlist: any): void {
    if (confirm(`Are you sure you want to delete "${wishlist.title}" from your wishlist?`)) {
      this.wishlistService.deleteWishlist(wishlist.wishlistId).subscribe();
    }
  }

  onMarkAsAcquired(wishlist: any): void {
    this.wishlistService.updateWishlist(wishlist.wishlistId, { ...wishlist, isAcquired: true }).subscribe();
  }
}
