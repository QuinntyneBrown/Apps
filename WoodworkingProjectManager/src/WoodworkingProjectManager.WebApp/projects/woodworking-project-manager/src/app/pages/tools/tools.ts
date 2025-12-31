import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { ToolService } from '../../services';
import { ToolDialog } from '../../components/tool-dialog/tool-dialog';
import { Tool } from '../../models';

@Component({
  selector: 'app-tools',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatCardModule,
    MatDialogModule
  ],
  templateUrl: './tools.html',
  styleUrl: './tools.scss'
})
export class Tools implements OnInit {
  private _toolService = inject(ToolService);
  private _dialog = inject(MatDialog);

  tools$ = this._toolService.tools$;
  displayedColumns = ['name', 'brand', 'model', 'location', 'purchasePrice', 'purchaseDate', 'actions'];

  ngOnInit(): void {
    this._toolService.getTools().subscribe();
  }

  openCreateDialog(): void {
    const dialogRef = this._dialog.open(ToolDialog, {
      width: '600px'
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this._toolService.createTool(result).subscribe();
      }
    });
  }

  openEditDialog(tool: Tool): void {
    const dialogRef = this._dialog.open(ToolDialog, {
      width: '600px',
      data: tool
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this._toolService.updateTool(tool.toolId, result).subscribe();
      }
    });
  }

  deleteTool(id: string): void {
    if (confirm('Are you sure you want to delete this tool?')) {
      this._toolService.deleteTool(id).subscribe();
    }
  }
}
