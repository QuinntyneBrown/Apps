import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Observable } from 'rxjs';
import { FavoriteService } from '@lib/api';
import { Favorite } from '@lib/api';
import {
  PageHeaderComponent,
  CollectionCardComponent
} from '@lib/components';

@Component({
  selector: 'app-favorites',
  standalone: true,
  imports: [
    CommonModule,
    PageHeaderComponent,
    CollectionCardComponent
  ],
  templateUrl: './favorites.html',
  styleUrl: './favorites.scss'
})
export class Favorites implements OnInit {
  favorites$: Observable<Favorite[]>;

  collections = [
    {
      title: 'Date Night Starters',
      description: 'Perfect prompts for romantic evenings together. Spark deeper connection and meaningful dialogue.',
      promptCount: '12 prompts',
      color: 'primary' as const
    },
    {
      title: 'Getting to Know You',
      description: 'Fun and lighthearted questions to discover new sides of each other. Great for new couples.',
      promptCount: '18 prompts',
      color: 'accent' as const
    },
    {
      title: 'Growth Together',
      description: 'Prompts focused on personal development and growing as a couple. Build stronger bonds.',
      promptCount: '15 prompts',
      color: 'success' as const
    },
    {
      title: 'Weekend Fun',
      description: 'Light, playful conversation starters perfect for lazy weekends and casual hangouts.',
      promptCount: '10 prompts',
      color: 'warning' as const
    }
  ];

  private readonly userId = '00000000-0000-0000-0000-000000000001';

  constructor(private favoriteService: FavoriteService) {
    this.favorites$ = this.favoriteService.favorites$;
  }

  ngOnInit(): void {
    this.favoriteService.getByUserId(this.userId).subscribe();
  }
}
