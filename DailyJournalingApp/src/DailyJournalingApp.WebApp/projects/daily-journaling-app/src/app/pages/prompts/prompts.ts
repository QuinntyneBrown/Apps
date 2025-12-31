import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { MatDialog } from '@angular/material/dialog';
import { Observable } from 'rxjs';
import { PromptService } from '../../services';
import { Prompt } from '../../models';
import { PromptDialog } from '../../components/prompt-dialog/prompt-dialog';

@Component({
  selector: 'app-prompts',
  standalone: true,
  imports: [
    CommonModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatChipsModule
  ],
  templateUrl: './prompts.html',
  styleUrl: './prompts.scss'
})
export class Prompts implements OnInit {
  private readonly promptService = inject(PromptService);
  private readonly dialog = inject(MatDialog);

  prompts$: Observable<Prompt[]>;
  userId = '00000000-0000-0000-0000-000000000001'; // Mock user ID

  constructor() {
    this.prompts$ = this.promptService.prompts$;
  }

  ngOnInit(): void {
    this.loadPrompts();
  }

  loadPrompts(): void {
    this.promptService.getAll().subscribe();
  }

  openCreateDialog(): void {
    const dialogRef = this.dialog.open(PromptDialog, {
      width: '500px',
      data: { userId: this.userId }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.promptService.create(result).subscribe();
      }
    });
  }

  onDelete(id: string, isSystemPrompt: boolean): void {
    if (isSystemPrompt) {
      alert('System prompts cannot be deleted');
      return;
    }

    if (confirm('Are you sure you want to delete this prompt?')) {
      this.promptService.delete(id).subscribe();
    }
  }
}
