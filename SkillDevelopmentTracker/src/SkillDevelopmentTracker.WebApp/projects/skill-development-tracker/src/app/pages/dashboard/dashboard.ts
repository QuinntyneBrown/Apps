import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { RouterLink } from '@angular/router';
import { SkillService, CourseService, CertificationService, LearningPathService } from '../../services';
import { map } from 'rxjs/operators';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, MatCardModule, MatIconModule, MatButtonModule, RouterLink],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.scss'
})
export class Dashboard implements OnInit {
  private _skillService = inject(SkillService);
  private _courseService = inject(CourseService);
  private _certificationService = inject(CertificationService);
  private _learningPathService = inject(LearningPathService);

  stats$ = this._skillService.skills$.pipe(
    map(skills => ({
      totalSkills: skills.length,
      totalHours: skills.reduce((sum, skill) => sum + skill.hoursSpent, 0),
      averageProficiency: skills.length > 0
        ? skills.reduce((sum, skill) => sum + skill.proficiencyLevel, 0) / skills.length
        : 0
    }))
  );

  courseStats$ = this._courseService.courses$.pipe(
    map(courses => ({
      totalCourses: courses.length,
      completedCourses: courses.filter(c => c.isCompleted).length,
      inProgressCourses: courses.filter(c => !c.isCompleted).length
    }))
  );

  certificationStats$ = this._certificationService.certifications$.pipe(
    map(certifications => ({
      totalCertifications: certifications.length,
      activeCertifications: certifications.filter(c => c.isActive).length
    }))
  );

  learningPathStats$ = this._learningPathService.learningPaths$.pipe(
    map(paths => ({
      totalPaths: paths.length,
      completedPaths: paths.filter(p => p.isCompleted).length
    }))
  );

  ngOnInit(): void {
    this._skillService.getSkills().subscribe();
    this._courseService.getCourses().subscribe();
    this._certificationService.getCertifications().subscribe();
    this._learningPathService.getLearningPaths().subscribe();
  }
}
