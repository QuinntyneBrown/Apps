import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { ExperienceService } from '../../services';
import { Observable } from 'rxjs';
import { Experience } from '../../models';

@Component({
  selector: 'app-experience-list',
  standalone: true,
  imports: [
    CommonModule,
    RouterLink,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatChipsModule
  ],
  templateUrl: './experience-list.html',
  styleUrl: './experience-list.scss'
})
export class ExperienceList implements OnInit {
  experiences$!: Observable<Experience[]>;

  constructor(private experienceService: ExperienceService) {}

  ngOnInit(): void {
    this.experiences$ = this.experienceService.experiences$;
    this.loadExperiences();
  }

  loadExperiences(): void {
    this.experienceService.getAll().subscribe();
  }

  delete(id: string): void {
    if (confirm('Are you sure you want to delete this experience?')) {
      this.experienceService.delete(id).subscribe();
    }
  }
}
