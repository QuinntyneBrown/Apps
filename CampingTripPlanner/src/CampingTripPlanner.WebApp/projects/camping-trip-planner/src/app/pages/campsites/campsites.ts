import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { CampsiteService } from '../../services';
import { CampsiteType } from '../../models';

@Component({
  selector: 'app-campsites',
  standalone: true,
  imports: [CommonModule, RouterLink, MatCardModule, MatButtonModule, MatIconModule, MatChipsModule],
  templateUrl: './campsites.html',
  styleUrl: './campsites.scss'
})
export class Campsites implements OnInit {
  campsites$ = this.campsiteService.campsites$;
  CampsiteType = CampsiteType;

  private userId = '00000000-0000-0000-0000-000000000001';

  constructor(private campsiteService: CampsiteService) {}

  ngOnInit(): void {
    this.loadCampsites();
  }

  loadCampsites(): void {
    this.campsiteService.getCampsites(this.userId).subscribe();
  }

  getCampsiteTypeName(type: CampsiteType): string {
    return CampsiteType[type];
  }
}
