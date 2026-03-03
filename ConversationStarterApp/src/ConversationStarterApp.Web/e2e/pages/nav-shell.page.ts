import { type Locator, type Page } from '@playwright/test';

export class NavShellPage {
  readonly page: Page;
  readonly sidebar: Locator;
  readonly tabbar: Locator;
  readonly sidebarDashboardLink: Locator;
  readonly sidebarPromptsLink: Locator;
  readonly sidebarCollectionsLink: Locator;
  readonly sidebarProgressLink: Locator;
  readonly sidebarSettingsLink: Locator;
  readonly tabHome: Locator;
  readonly tabPrompts: Locator;
  readonly tabCollections: Locator;
  readonly tabProgress: Locator;
  readonly brandName: Locator;
  readonly mainContent: Locator;

  constructor(page: Page) {
    this.page = page;
    this.sidebar = page.locator('.nav-shell__sidebar');
    this.tabbar = page.locator('.nav-shell__tabbar');
    this.mainContent = page.locator('.nav-shell__content');
    this.brandName = page.locator('.nav-shell__brand');

    this.sidebarDashboardLink = this.sidebar.locator('.nav-shell__link', { hasText: 'Dashboard' });
    this.sidebarPromptsLink = this.sidebar.locator('.nav-shell__link', { hasText: 'Prompts' });
    this.sidebarCollectionsLink = this.sidebar.locator('.nav-shell__link', { hasText: 'Collections' });
    this.sidebarProgressLink = this.sidebar.locator('.nav-shell__link', { hasText: 'Progress' });
    this.sidebarSettingsLink = this.sidebar.locator('.nav-shell__link', { hasText: 'Settings' });

    this.tabHome = this.tabbar.locator('.nav-shell__tab', { hasText: 'Home' });
    this.tabPrompts = this.tabbar.locator('.nav-shell__tab', { hasText: 'Prompts' });
    this.tabCollections = this.tabbar.locator('.nav-shell__tab', { hasText: 'Collections' });
    this.tabProgress = this.tabbar.locator('.nav-shell__tab', { hasText: 'Progress' });
  }

  async navigateToDashboard(): Promise<void> {
    await this.sidebarDashboardLink.click();
  }

  async navigateToPrompts(): Promise<void> {
    await this.sidebarPromptsLink.click();
  }

  async navigateToCollections(): Promise<void> {
    await this.sidebarCollectionsLink.click();
  }

  async navigateToProgress(): Promise<void> {
    await this.sidebarProgressLink.click();
  }

  async navigateViaTabHome(): Promise<void> {
    await this.tabHome.click();
  }

  async navigateViaTabPrompts(): Promise<void> {
    await this.tabPrompts.click();
  }

  async navigateViaTabCollections(): Promise<void> {
    await this.tabCollections.click();
  }

  async navigateViaTabProgress(): Promise<void> {
    await this.tabProgress.click();
  }

  async getActiveTab(): Promise<string> {
    const activeTab = this.tabbar.locator('.nav-shell__tab--active');
    return (await activeTab.textContent()) ?? '';
  }

  async getActiveSidebarLink(): Promise<string> {
    const activeLink = this.sidebar.locator('.nav-shell__link--active');
    return (await activeLink.textContent()) ?? '';
  }
}
