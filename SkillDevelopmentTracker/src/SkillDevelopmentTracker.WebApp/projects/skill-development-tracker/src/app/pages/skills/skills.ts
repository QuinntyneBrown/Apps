import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatChipsModule } from '@angular/material/chips';
import { SkillService } from '../../services';
import { ProficiencyLevel } from '../../models';

@Component({
  selector: 'app-skills',
  standalone: true,
  imports: [CommonModule, MatTableModule, MatButtonModule, MatIconModule, MatCardModule, MatChipsModule],
  templateUrl: './skills.html',
  styleUrl: './skills.scss'
})
export class Skills implements OnInit {
  private _skillService = inject(SkillService);

  skills$ = this._skillService.skills$;
  displayedColumns: string[] = ['name', 'category', 'proficiencyLevel', 'targetLevel', 'hoursSpent', 'actions'];

  ngOnInit(): void {
    this._skillService.getSkills().subscribe();
  }

  getProficiencyLabel(level: ProficiencyLevel): string {
    return ProficiencyLevel[level];
  }

  deleteSkill(id: string): void {
    if (confirm('Are you sure you want to delete this skill?')) {
      this._skillService.deleteSkill(id).subscribe();
    }
  }
}
