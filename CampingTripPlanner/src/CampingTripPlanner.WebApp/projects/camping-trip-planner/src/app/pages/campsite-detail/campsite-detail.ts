import { Component, OnInit, OnDestroy } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { CampsiteService, ReviewService } from '../../services';
import { CampsiteType } from '../../models';

@Component({
  selector: 'app-campsite-detail',
  standalone: true,
  imports: [CommonModule, RouterLink, MatCardModule, MatButtonModule, MatIconModule, MatChipsModule],
  templateUrl: './campsite-detail.html',
  styleUrl: './campsite-detail.scss'
})
export class CampsiteDetail implements OnInit, OnDestroy {
  campsite$ = this.campsiteService.selectedCampsite$;
  reviews$ = this.reviewService.reviews$;
  CampsiteType = CampsiteType;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private campsiteService: CampsiteService,
    private reviewService: ReviewService
  ) {}

  ngOnInit(): void {
    const campsiteId = this.route.snapshot.paramMap.get('id');
    if (campsiteId) {
      this.loadCampsite(campsiteId);
    }
  }

  ngOnDestroy(): void {
    this.campsiteService.clearSelectedCampsite();
  }

  loadCampsite(campsiteId: string): void {
    this.campsiteService.getCampsiteById(campsiteId).subscribe();
    this.reviewService.getReviews(undefined, campsiteId).subscribe();
  }

  getCampsiteTypeName(type: CampsiteType): string {
    return CampsiteType[type];
  }

  goBack(): void {
    this.router.navigate(['/campsites']);
  }
}
