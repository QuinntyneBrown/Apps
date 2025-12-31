import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { DigitalAccountsService, LegacyDocumentsService, LegacyInstructionsService, TrustedContactsService } from '../../services';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, RouterLink, MatCardModule, MatIconModule, MatButtonModule],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.scss'
})
export class Dashboard implements OnInit {
  stats = {
    accounts: 0,
    documents: 0,
    instructions: 0,
    contacts: 0
  };

  constructor(
    private accountsService: DigitalAccountsService,
    private documentsService: LegacyDocumentsService,
    private instructionsService: LegacyInstructionsService,
    private contactsService: TrustedContactsService
  ) {}

  ngOnInit(): void {
    this.loadStats();
  }

  loadStats(): void {
    this.accountsService.accounts$.subscribe(accounts => {
      this.stats.accounts = accounts.length;
    });

    this.documentsService.documents$.subscribe(documents => {
      this.stats.documents = documents.length;
    });

    this.instructionsService.instructions$.subscribe(instructions => {
      this.stats.instructions = instructions.length;
    });

    this.contactsService.contacts$.subscribe(contacts => {
      this.stats.contacts = contacts.length;
    });

    // Load all data
    this.accountsService.getAll().subscribe();
    this.documentsService.getAll().subscribe();
    this.instructionsService.getAll().subscribe();
    this.contactsService.getAll().subscribe();
  }
}
