import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatChipsModule } from '@angular/material/chips';
import { GroceryListService } from '../../services';

@Component({
  selector: 'app-grocery',
  standalone: true,
  imports: [
    CommonModule,
    RouterLink,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatCheckboxModule,
    MatChipsModule
  ],
  templateUrl: './grocery.html',
  styleUrl: './grocery.scss'
})
export class Grocery implements OnInit {
  private groceryListService = inject(GroceryListService);

  groceryLists$ = this.groceryListService.groceryLists$;

  ngOnInit(): void {
    this.groceryListService.getGroceryLists().subscribe();
  }
}
