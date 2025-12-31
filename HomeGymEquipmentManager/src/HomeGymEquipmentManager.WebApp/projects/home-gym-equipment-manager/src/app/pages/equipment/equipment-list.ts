import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { EquipmentService } from '../../services';
import { Equipment, EquipmentType } from '../../models';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-equipment-list',
  standalone: true,
  imports: [CommonModule, MatTableModule, MatButtonModule, MatIconModule, MatCardModule],
  templateUrl: './equipment-list.html',
  styleUrl: './equipment-list.scss'
})
export class EquipmentList implements OnInit {
  equipmentList$: Observable<Equipment[]>;
  displayedColumns: string[] = ['name', 'equipmentType', 'brand', 'model', 'location', 'isActive', 'actions'];
  EquipmentType = EquipmentType;

  constructor(
    private equipmentService: EquipmentService,
    private router: Router
  ) {
    this.equipmentList$ = this.equipmentService.equipmentList$;
  }

  ngOnInit(): void {
    this.equipmentService.getAll().subscribe();
  }

  getEquipmentTypeName(type: EquipmentType): string {
    return EquipmentType[type];
  }

  onCreate(): void {
    this.router.navigate(['/equipment/create']);
  }

  onEdit(id: string): void {
    this.router.navigate(['/equipment/edit', id]);
  }

  onDelete(id: string): void {
    if (confirm('Are you sure you want to delete this equipment?')) {
      this.equipmentService.delete(id).subscribe();
    }
  }
}
