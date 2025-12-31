import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatSelectModule } from '@angular/material/select';
import { NoteService, MeetingService } from '../services';
import { CreateNoteDto, UpdateNoteDto } from '../models';

@Component({
  selector: 'app-note-form',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatCheckboxModule,
    MatSelectModule
  ],
  templateUrl: './note-form.html',
  styleUrl: './note-form.scss'
})
export class NoteForm implements OnInit {
  private fb = inject(FormBuilder);
  private noteService = inject(NoteService);
  private meetingService = inject(MeetingService);
  private route = inject(ActivatedRoute);
  private router = inject(Router);

  noteForm!: FormGroup;
  isEditMode = false;
  noteId?: string;
  meetings$ = this.meetingService.meetings$;

  ngOnInit(): void {
    this.noteForm = this.fb.group({
      meetingId: ['', Validators.required],
      content: ['', Validators.required],
      category: [''],
      isImportant: [false]
    });

    this.meetingService.getMeetings().subscribe();

    this.route.params.subscribe(params => {
      if (params['id'] && params['id'] !== 'new') {
        this.isEditMode = true;
        this.noteId = params['id'];
        this.loadNote(this.noteId);
      }
    });
  }

  loadNote(id: string): void {
    this.noteService.getNoteById(id).subscribe(note => {
      this.noteForm.patchValue({
        meetingId: note.meetingId,
        content: note.content,
        category: note.category,
        isImportant: note.isImportant
      });
    });
  }

  onSubmit(): void {
    if (this.noteForm.valid) {
      const formValue = this.noteForm.value;

      if (this.isEditMode && this.noteId) {
        const dto: UpdateNoteDto = {
          noteId: this.noteId,
          content: formValue.content,
          category: formValue.category || undefined,
          isImportant: formValue.isImportant
        };
        this.noteService.updateNote(dto).subscribe(() => {
          this.router.navigate(['/notes']);
        });
      } else {
        const dto: CreateNoteDto = {
          userId: '00000000-0000-0000-0000-000000000000', // Replace with actual user ID
          meetingId: formValue.meetingId,
          content: formValue.content,
          category: formValue.category || undefined,
          isImportant: formValue.isImportant
        };
        this.noteService.createNote(dto).subscribe(() => {
          this.router.navigate(['/notes']);
        });
      }
    }
  }

  cancel(): void {
    this.router.navigate(['/notes']);
  }
}
