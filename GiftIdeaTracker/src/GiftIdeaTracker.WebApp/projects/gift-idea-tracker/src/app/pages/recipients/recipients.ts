import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialog } from '@angular/material/dialog';
import { BehaviorSubject, switchMap, map, startWith } from 'rxjs';
import { RecipientsService, GiftIdeasService } from '../../services';
import { RecipientCard, RecipientDialog, RecipientDialogResult } from '../../components';
import { Recipient, CreateRecipientRequest, UpdateRecipientRequest } from '../../models';

@Component({
  selector: 'app-recipients',
  standalone: true,
  imports: [
    CommonModule,
    MatButtonModule,
    MatIconModule,
    RecipientCard
  ],
  templateUrl: './recipients.html',
  styleUrl: './recipients.scss'
})
export class Recipients {
  private readonly recipientsService = inject(RecipientsService);
  private readonly giftIdeasService = inject(GiftIdeasService);
  private readonly dialog = inject(MatDialog);
  private readonly router = inject(Router);

  private refresh$ = new BehaviorSubject<void>(undefined);

  viewModel$ = this.refresh$.pipe(
    switchMap(() => this.recipientsService.getRecipients()),
    switchMap(recipients =>
      this.giftIdeasService.getGiftIdeas().pipe(
        map(giftIdeas => {
          const ideasCountByRecipient = new Map<string, number>();
          giftIdeas.forEach(idea => {
            if (idea.recipientId) {
              const count = ideasCountByRecipient.get(idea.recipientId) || 0;
              ideasCountByRecipient.set(idea.recipientId, count + 1);
            }
          });

          return {
            recipients,
            ideasCountByRecipient
          };
        })
      )
    ),
    startWith({
      recipients: [] as Recipient[],
      ideasCountByRecipient: new Map<string, number>()
    })
  );

  onAddRecipient(): void {
    const dialogRef = this.dialog.open(RecipientDialog, {
      width: '500px',
      data: {}
    });

    dialogRef.afterClosed().subscribe((result: RecipientDialogResult | undefined) => {
      if (result?.action === 'create') {
        this.recipientsService.createRecipient(result.data as CreateRecipientRequest).subscribe(() => {
          this.refresh$.next();
        });
      }
    });
  }

  onViewIdeas(recipient: Recipient): void {
    this.router.navigate(['/gift-ideas'], { queryParams: { recipientId: recipient.recipientId } });
  }

  onEditRecipient(recipient: Recipient): void {
    const dialogRef = this.dialog.open(RecipientDialog, {
      width: '500px',
      data: { recipient }
    });

    dialogRef.afterClosed().subscribe((result: RecipientDialogResult | undefined) => {
      if (result?.action === 'update') {
        this.recipientsService.updateRecipient(result.data as UpdateRecipientRequest).subscribe(() => {
          this.refresh$.next();
        });
      }
    });
  }

  onDeleteRecipient(recipient: Recipient): void {
    this.recipientsService.deleteRecipient(recipient.recipientId).subscribe(() => {
      this.refresh$.next();
    });
  }

  getIdeasCount(recipientId: string, ideasCountByRecipient: Map<string, number>): number {
    return ideasCountByRecipient.get(recipientId) || 0;
  }
}
