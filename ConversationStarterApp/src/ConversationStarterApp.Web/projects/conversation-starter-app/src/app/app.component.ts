import { Component } from '@angular/core';
import { Router, RouterOutlet, NavigationEnd } from '@angular/router';
import { filter } from 'rxjs/operators';
import { NavShellComponent } from '@lib/components';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, NavShellComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'conversation-starter-app';
  activeTab: 'home' | 'prompts' | 'collections' | 'progress' | 'settings' = 'home';

  private routeTabMap: Record<string, 'home' | 'prompts' | 'collections' | 'progress' | 'settings'> = {
    '/dashboard': 'home',
    '/prompts': 'prompts',
    '/collections': 'collections',
    '/progress': 'progress'
  };

  private tabRouteMap: Record<string, string> = {
    home: '/dashboard',
    prompts: '/prompts',
    collections: '/collections',
    progress: '/progress'
  };

  constructor(private router: Router) {
    this.router.events
      .pipe(filter((e): e is NavigationEnd => e instanceof NavigationEnd))
      .subscribe((e) => {
        const tab = this.routeTabMap[e.urlAfterRedirects];
        if (tab) {
          this.activeTab = tab;
        }
      });
  }

  onTabChange(tab: string): void {
    const route = this.tabRouteMap[tab];
    if (route) {
      this.router.navigate([route]);
    }
  }
}
