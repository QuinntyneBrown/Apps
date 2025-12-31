import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { WineService, TastingNoteService, DrinkingWindowService } from '../../services';
import { WineList, WineForm, TastingNoteForm, DrinkingWindowForm } from '../../components';
import { Wine } from '../../models';

@Component({
  selector: 'app-wines',
  standalone: true,
  imports: [CommonModule, MatCardModule, MatButtonModule, MatDialogModule, WineList],
  templateUrl: './wines.html',
  styleUrl: './wines.scss'
})
export class Wines {
  private _wineService = inject(WineService);
  private _tastingNoteService = inject(TastingNoteService);
  private _drinkingWindowService = inject(DrinkingWindowService);
  private _dialog = inject(MatDialog);

  wines$ = this._wineService.wines$;

  ngOnInit(): void {
    this._wineService.getWines().subscribe();
  }

  onAddWine(): void {
    const dialogRef = this._dialog.open(WineForm, {
      width: '600px',
      data: {}
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this._wineService.createWine(result).subscribe();
      }
    });
  }

  onEditWine(wine: Wine): void {
    const dialogRef = this._dialog.open(WineForm, {
      width: '600px',
      data: { wine }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this._wineService.updateWine(wine.wineId, result).subscribe();
      }
    });
  }

  onDeleteWine(wine: Wine): void {
    if (confirm(`Are you sure you want to delete ${wine.name}?`)) {
      this._wineService.deleteWine(wine.wineId).subscribe();
    }
  }

  onAddTastingNote(wine: Wine): void {
    const dialogRef = this._dialog.open(TastingNoteForm, {
      width: '600px',
      data: { wine }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this._tastingNoteService.createTastingNote(result).subscribe();
      }
    });
  }

  onAddDrinkingWindow(wine: Wine): void {
    const dialogRef = this._dialog.open(DrinkingWindowForm, {
      width: '600px',
      data: { wine }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this._drinkingWindowService.createDrinkingWindow(result).subscribe();
      }
    });
  }
}
