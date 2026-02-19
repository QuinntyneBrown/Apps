import { Component, inject } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { Header, EventDialog, EventDialogData, EventDialogResult } from './components';
import { EventsService, FamilyMembersService } from './services';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, Header],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class App {
  private dialog = inject(MatDialog);
  private eventsService = inject(EventsService);
  private membersService = inject(FamilyMembersService);

  onAddEventClick(): void {
    this.membersService.getFamilyMembers().subscribe(members => {
      const dialogRef = this.dialog.open(EventDialog, {
        width: '600px',
        data: {
          members,
          familyId: members[0]?.familyId || '',
          creatorId: members[0]?.memberId || ''
        } as EventDialogData
      });

      dialogRef.afterClosed().subscribe((result: EventDialogResult) => {
        if (result?.action === 'create' && result.data) {
          this.eventsService.createEvent(result.data).subscribe();
        }
      });
    });
  }
}
