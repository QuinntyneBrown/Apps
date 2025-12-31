import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatChipsModule } from '@angular/material/chips';
import { DigitalAccountsService } from '../../services';
import { DigitalAccount, AccountType } from '../../models';
import { DigitalAccountDialog } from '../../components';

@Component({
  selector: 'app-digital-accounts',
  standalone: true,
  imports: [CommonModule, MatCardModule, MatButtonModule, MatIconModule, MatDialogModule, MatChipsModule],
  templateUrl: './digital-accounts.html',
  styleUrl: './digital-accounts.scss'
})
export class DigitalAccounts implements OnInit {
  accounts$ = this.accountsService.accounts$;
  AccountType = AccountType;

  // Hardcoded userId for demo purposes
  private readonly userId = '00000000-0000-0000-0000-000000000001';

  constructor(
    private accountsService: DigitalAccountsService,
    private dialog: MatDialog
  ) {}

  ngOnInit(): void {
    this.loadAccounts();
  }

  loadAccounts(): void {
    this.accountsService.getAll().subscribe();
  }

  openCreateDialog(): void {
    const dialogRef = this.dialog.open(DigitalAccountDialog, {
      width: '600px',
      data: { userId: this.userId }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.accountsService.create({
          userId: this.userId,
          ...result
        }).subscribe();
      }
    });
  }

  openEditDialog(account: DigitalAccount): void {
    const dialogRef = this.dialog.open(DigitalAccountDialog, {
      width: '600px',
      data: { account, userId: this.userId }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.accountsService.update(account.digitalAccountId, {
          digitalAccountId: account.digitalAccountId,
          ...result
        }).subscribe();
      }
    });
  }

  deleteAccount(account: DigitalAccount): void {
    if (confirm(`Are you sure you want to delete ${account.accountName}?`)) {
      this.accountsService.delete(account.digitalAccountId).subscribe();
    }
  }

  getAccountTypeName(type: AccountType): string {
    return AccountType[type];
  }
}
