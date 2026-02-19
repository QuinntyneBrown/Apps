import { Component, inject } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { Header, GiftIdeaDialog, GiftIdeaDialogResult } from './components';
import { GiftIdeasService, RecipientsService } from '@lib/api';
import { CreateGiftIdeaRequest } from '@lib/api';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, Header],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  private readonly dialog = inject(MatDialog);
  private readonly giftIdeasService = inject(GiftIdeasService);
  private readonly recipientsService = inject(RecipientsService);

  onAddGiftIdea(): void {
    this.recipientsService.getRecipients().subscribe(recipients => {
      const dialogRef = this.dialog.open(GiftIdeaDialog, {
        width: '500px',
        data: { recipients }
      });

      dialogRef.afterClosed().subscribe((result: GiftIdeaDialogResult | undefined) => {
        if (result?.action === 'create') {
          this.giftIdeasService.createGiftIdea(result.data as CreateGiftIdeaRequest).subscribe(() => {
            window.location.reload();
          });
        }
      });
    });
  }
}
