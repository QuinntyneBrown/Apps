import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTableModule } from '@angular/material/table';
import { MatChipsModule } from '@angular/material/chips';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { TeamService } from '../../services';
import { TeamDialog } from '../../components';
import { CreateTeamRequest, UpdateTeamRequest } from '../../models';

@Component({
  selector: 'app-teams',
  standalone: true,
  imports: [
    CommonModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatTableModule,
    MatChipsModule,
    MatDialogModule
  ],
  templateUrl: './teams.html',
  styleUrl: './teams.scss'
})
export class Teams {
  private _teamService = inject(TeamService);
  private _dialog = inject(MatDialog);

  teams$ = this._teamService.teams$;
  displayedColumns = ['name', 'sport', 'league', 'city', 'isFavorite', 'gamesCount', 'actions'];

  constructor() {
    this._teamService.getTeams().subscribe();
  }

  openAddDialog(): void {
    const dialogRef = this._dialog.open(TeamDialog, {
      width: '500px',
      data: {}
    });

    dialogRef.afterClosed().subscribe((result: CreateTeamRequest) => {
      if (result) {
        this._teamService.createTeam(result).subscribe();
      }
    });
  }

  openEditDialog(team: any): void {
    const dialogRef = this._dialog.open(TeamDialog, {
      width: '500px',
      data: { team }
    });

    dialogRef.afterClosed().subscribe((result: UpdateTeamRequest) => {
      if (result) {
        this._teamService.updateTeam(result).subscribe();
      }
    });
  }

  deleteTeam(id: string): void {
    if (confirm('Are you sure you want to delete this team?')) {
      this._teamService.deleteTeam(id).subscribe();
    }
  }
}
