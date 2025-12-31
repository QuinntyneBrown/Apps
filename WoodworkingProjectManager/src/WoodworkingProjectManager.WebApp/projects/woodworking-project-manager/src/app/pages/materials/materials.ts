import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MaterialService } from '../../services';
import { MaterialDialog } from '../../components/material-dialog/material-dialog';
import { Material } from '../../models';

@Component({
  selector: 'app-materials',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatCardModule,
    MatDialogModule
  ],
  templateUrl: './materials.html',
  styleUrl: './materials.scss'
})
export class Materials implements OnInit {
  private _materialService = inject(MaterialService);
  private _dialog = inject(MatDialog);

  materials$ = this._materialService.materials$;
  displayedColumns = ['name', 'description', 'quantity', 'unit', 'cost', 'supplier', 'actions'];

  ngOnInit(): void {
    this._materialService.getMaterials().subscribe();
  }

  openCreateDialog(): void {
    const dialogRef = this._dialog.open(MaterialDialog, {
      width: '600px'
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this._materialService.createMaterial(result).subscribe();
      }
    });
  }

  openEditDialog(material: Material): void {
    const dialogRef = this._dialog.open(MaterialDialog, {
      width: '600px',
      data: material
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this._materialService.updateMaterial(material.materialId, result).subscribe();
      }
    });
  }

  deleteMaterial(id: string): void {
    if (confirm('Are you sure you want to delete this material?')) {
      this._materialService.deleteMaterial(id).subscribe();
    }
  }
}
