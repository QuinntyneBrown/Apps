import { Component, EventEmitter, Input, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatButtonModule } from '@angular/material/button';
import { MatDividerModule } from '@angular/material/divider';
import { FilterState, ContentType, Genre, Mood } from '../../models';

@Component({
  selector: 'app-sidebar',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    MatSidenavModule,
    MatCheckboxModule,
    MatButtonModule,
    MatDividerModule
  ],
  templateUrl: './sidebar.html',
  styleUrl: './sidebar.scss'
})
export class Sidebar {
  @Input() filter: FilterState = {
    contentTypes: ['movie', 'tvshow'],
    genres: [],
    moods: [],
    availableNow: false,
    comingSoon: false,
    unavailable: false
  };

  @Output() filterChange = new EventEmitter<Partial<FilterState>>();
  @Output() clearFilters = new EventEmitter<void>();

  contentTypes: { value: ContentType; label: string }[] = [
    { value: 'movie', label: 'Movies' },
    { value: 'tvshow', label: 'TV Shows' }
  ];

  genres: { value: Genre; label: string }[] = [
    { value: 'action', label: 'Action' },
    { value: 'drama', label: 'Drama' },
    { value: 'sci-fi', label: 'Sci-Fi' },
    { value: 'comedy', label: 'Comedy' },
    { value: 'thriller', label: 'Thriller' },
    { value: 'horror', label: 'Horror' }
  ];

  moods: { value: Mood; label: string }[] = [
    { value: 'action-packed', label: 'Action-packed' },
    { value: 'thought-provoking', label: 'Thought-provoking' },
    { value: 'relaxing', label: 'Relaxing' },
    { value: 'funny', label: 'Funny' }
  ];

  isContentTypeChecked(type: ContentType): boolean {
    return this.filter.contentTypes.includes(type);
  }

  isGenreChecked(genre: Genre): boolean {
    return this.filter.genres.includes(genre);
  }

  isMoodChecked(mood: Mood): boolean {
    return this.filter.moods.includes(mood);
  }

  toggleContentType(type: ContentType): void {
    const current = this.filter.contentTypes;
    const updated = current.includes(type)
      ? current.filter(t => t !== type)
      : [...current, type];
    this.filterChange.emit({ contentTypes: updated });
  }

  toggleGenre(genre: Genre): void {
    const current = this.filter.genres;
    const updated = current.includes(genre)
      ? current.filter(g => g !== genre)
      : [...current, genre];
    this.filterChange.emit({ genres: updated });
  }

  toggleMood(mood: Mood): void {
    const current = this.filter.moods;
    const updated = current.includes(mood)
      ? current.filter(m => m !== mood)
      : [...current, mood];
    this.filterChange.emit({ moods: updated });
  }

  onClearFilters(): void {
    this.clearFilters.emit();
  }
}
