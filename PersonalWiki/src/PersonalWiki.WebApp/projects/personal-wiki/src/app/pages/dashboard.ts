import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { WikiPageService, WikiCategoryService, PageRevisionService, PageLinkService } from '../services';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [
    CommonModule,
    RouterLink,
    MatCardModule,
    MatIconModule,
    MatButtonModule
  ],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.scss'
})
export class Dashboard implements OnInit {
  private readonly wikiPageService = inject(WikiPageService);
  private readonly wikiCategoryService = inject(WikiCategoryService);
  private readonly pageRevisionService = inject(PageRevisionService);
  private readonly pageLinkService = inject(PageLinkService);

  wikiPages$ = this.wikiPageService.wikiPages$;
  wikiCategories$ = this.wikiCategoryService.wikiCategories$;
  pageRevisions$ = this.pageRevisionService.pageRevisions$;
  pageLinks$ = this.pageLinkService.pageLinks$;

  ngOnInit(): void {
    this.wikiPageService.getAll().subscribe();
    this.wikiCategoryService.getAll().subscribe();
    this.pageRevisionService.getAll().subscribe();
    this.pageLinkService.getAll().subscribe();
  }
}
