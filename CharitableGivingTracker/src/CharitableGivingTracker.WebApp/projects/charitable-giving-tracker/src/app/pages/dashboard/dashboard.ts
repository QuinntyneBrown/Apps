import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { DonationService, OrganizationService, TaxReportService } from '../../services';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, RouterLink, MatCardModule, MatButtonModule, MatIconModule],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.scss'
})
export class Dashboard implements OnInit {
  donations$ = this.donationService.donations$;
  organizations$ = this.organizationService.organizations$;
  taxReports$ = this.taxReportService.taxReports$;

  totalDonations = 0;
  totalOrganizations = 0;
  currentYearDonations = 0;

  constructor(
    private donationService: DonationService,
    private organizationService: OrganizationService,
    private taxReportService: TaxReportService
  ) {}

  ngOnInit(): void {
    this.loadData();
  }

  loadData(): void {
    this.donationService.getAll().subscribe(donations => {
      this.totalDonations = donations.reduce((sum, d) => sum + d.amount, 0);
      const currentYear = new Date().getFullYear();
      this.currentYearDonations = donations
        .filter(d => new Date(d.donationDate).getFullYear() === currentYear)
        .reduce((sum, d) => sum + d.amount, 0);
    });

    this.organizationService.getAll().subscribe(orgs => {
      this.totalOrganizations = orgs.length;
    });

    this.taxReportService.getAll().subscribe();
  }
}
