import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';

export interface InfoRow {
  label: string;
  value: string;
}

@Component({
  selector: 'app-info-table',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './info-table.html',
  styleUrl: './info-table.scss'
})
export class InfoTable {
  @Input({ required: true }) rows: InfoRow[] = [];
}
