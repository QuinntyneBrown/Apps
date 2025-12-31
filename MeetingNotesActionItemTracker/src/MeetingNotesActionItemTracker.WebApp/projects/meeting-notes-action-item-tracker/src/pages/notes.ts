import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterLink } from '@angular/router';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatChipsModule } from '@angular/material/chips';
import { NoteService } from '../services';
import { Note } from '../models';

@Component({
  selector: 'app-notes',
  standalone: true,
  imports: [CommonModule, RouterLink, MatTableModule, MatButtonModule, MatIconModule, MatCardModule, MatChipsModule],
  templateUrl: './notes.html',
  styleUrl: './notes.scss'
})
export class Notes implements OnInit {
  private noteService = inject(NoteService);
  private router = inject(Router);

  notes$ = this.noteService.notes$;
  displayedColumns: string[] = ['content', 'category', 'isImportant', 'createdAt', 'actions'];

  ngOnInit(): void {
    this.noteService.getNotes().subscribe();
  }

  editNote(note: Note): void {
    this.router.navigate(['/notes', note.noteId]);
  }

  deleteNote(note: Note): void {
    if (confirm(`Are you sure you want to delete this note?`)) {
      this.noteService.deleteNote(note.noteId).subscribe();
    }
  }

  formatDate(date: string): string {
    return new Date(date).toLocaleDateString();
  }
}
