import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { ShoppingListService } from '../../services';
import { ShoppingList } from '../../models';

@Component({
  selector: 'app-shopping-lists',
  standalone: true,
  imports: [
    CommonModule,
    MatButtonModule,
    MatIconModule,
    MatCardModule,
    MatCheckboxModule,
    MatProgressSpinnerModule,
  ],
  templateUrl: './shopping-lists.html',
  styleUrls: ['./shopping-lists.scss'],
})
export class ShoppingLists implements OnInit {
  shoppingLists$ = this.shoppingListService.shoppingLists$;
  loading$ = this.shoppingListService.loading$;
  userId = '00000000-0000-0000-0000-000000000001';

  constructor(private shoppingListService: ShoppingListService) {}

  ngOnInit(): void {
    this.shoppingListService.getShoppingLists(this.userId).subscribe();
  }

  onDelete(shoppingListId: string): void {
    if (confirm('Are you sure you want to delete this shopping list?')) {
      this.shoppingListService.deleteShoppingList(shoppingListId).subscribe();
    }
  }

  toggleCompleted(shoppingList: ShoppingList): void {
    const request = {
      ...shoppingList,
      isCompleted: !shoppingList.isCompleted,
    };
    this.shoppingListService.updateShoppingList(request).subscribe();
  }
}
