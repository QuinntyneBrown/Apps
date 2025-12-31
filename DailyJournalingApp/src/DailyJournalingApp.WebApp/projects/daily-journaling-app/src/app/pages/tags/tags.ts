import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { MatDialog } from '@angular/material/dialog';
import { Observable } from 'rxjs';
import { TagService } from '../../services';
import { Tag } from '../../models';
import { TagDialog } from '../../components/tag-dialog/tag-dialog';

@Component({
  selector: 'app-tags',
  standalone: true,
  imports: [
    CommonModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatChipsModule
  ],
  templateUrl: './tags.html',
  styleUrl: './tags.scss'
})
export class Tags implements OnInit {
  private readonly tagService = inject(TagService);
  private readonly dialog = inject(MatDialog);

  tags$: Observable<Tag[]>;
  userId = '00000000-0000-0000-0000-000000000001'; // Mock user ID

  constructor() {
    this.tags$ = this.tagService.tags$;
  }

  ngOnInit(): void {
    this.loadTags();
  }

  loadTags(): void {
    this.tagService.getAll(this.userId).subscribe();
  }

  openCreateDialog(): void {
    const dialogRef = this.dialog.open(TagDialog, {
      width: '500px',
      data: { userId: this.userId }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.tagService.create(result).subscribe();
      }
    });
  }

  onEdit(tag: Tag): void {
    const dialogRef = this.dialog.open(TagDialog, {
      width: '500px',
      data: { tag, userId: this.userId }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.tagService.update(tag.tagId, result).subscribe();
      }
    });
  }

  onDelete(id: string): void {
    if (confirm('Are you sure you want to delete this tag?')) {
      this.tagService.delete(id).subscribe();
    }
  }
}
