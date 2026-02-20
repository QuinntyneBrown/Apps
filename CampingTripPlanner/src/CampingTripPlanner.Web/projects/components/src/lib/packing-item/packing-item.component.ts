import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'lib-packing-item',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './packing-item.component.html',
  styleUrls: ['./packing-item.component.scss']
})
export class PackingItemComponent {
  @Input({ required: true }) name!: string;
  @Input({ required: true }) weight!: string;
  @Input() checked: boolean = false;
  @Output() checkedChange = new EventEmitter<boolean>();

  toggle(): void {
    this.checked = !this.checked;
    this.checkedChange.emit(this.checked);
  }
}
