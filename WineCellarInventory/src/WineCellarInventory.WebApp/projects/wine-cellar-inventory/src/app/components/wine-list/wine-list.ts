import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { Wine } from '../../models';

@Component({
  selector: 'app-wine-list',
  standalone: true,
  imports: [CommonModule, MatTableModule, MatButtonModule, MatIconModule],
  templateUrl: './wine-list.html',
  styleUrl: './wine-list.scss'
})
export class WineList {
  @Input() wines: Wine[] = [];
  @Output() editWine = new EventEmitter<Wine>();
  @Output() deleteWine = new EventEmitter<Wine>();
  @Output() addTastingNote = new EventEmitter<Wine>();
  @Output() addDrinkingWindow = new EventEmitter<Wine>();

  displayedColumns: string[] = ['name', 'wineType', 'region', 'vintage', 'producer', 'bottleCount', 'purchasePrice', 'actions'];

  onEdit(wine: Wine): void {
    this.editWine.emit(wine);
  }

  onDelete(wine: Wine): void {
    this.deleteWine.emit(wine);
  }

  onAddTastingNote(wine: Wine): void {
    this.addTastingNote.emit(wine);
  }

  onAddDrinkingWindow(wine: Wine): void {
    this.addDrinkingWindow.emit(wine);
  }
}
