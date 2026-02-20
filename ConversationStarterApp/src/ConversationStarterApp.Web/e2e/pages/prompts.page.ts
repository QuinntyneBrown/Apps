import { type Locator, type Page } from '@playwright/test';

export class PromptsPage {
  readonly page: Page;
  readonly pageHeader: Locator;
  readonly pageHeaderTitle: Locator;
  readonly pageHeaderAction: Locator;
  readonly filterChips: Locator;
  readonly promptCards: Locator;
  readonly promptTexts: Locator;
  readonly favoriteButtons: Locator;

  constructor(page: Page) {
    this.page = page;
    this.pageHeader = page.locator('lib-page-header');
    this.pageHeaderTitle = page.locator('.page-header__title');
    this.pageHeaderAction = page.locator('.page-header__action');
    this.filterChips = page.locator('lib-filter-chip');
    this.promptCards = page.locator('lib-prompt-card');
    this.promptTexts = page.locator('.prompt-card__text');
    this.favoriteButtons = page.locator('.prompt-card__fav');
  }

  async goto(): Promise<void> {
    await this.page.goto('/prompts');
  }

  async getHeaderTitle(): Promise<string> {
    return (await this.pageHeaderTitle.textContent()) ?? '';
  }

  async clickRandomPrompt(): Promise<void> {
    await this.pageHeaderAction.click();
  }

  async getFilterLabels(): Promise<string[]> {
    return this.filterChips.locator('.filter-chip').allTextContents();
  }

  async clickFilter(label: string): Promise<void> {
    await this.filterChips.locator('.filter-chip', { hasText: label }).click();
  }

  async getActiveFilter(): Promise<string> {
    const active = this.page.locator('.filter-chip--active');
    return (await active.textContent()) ?? '';
  }

  async getPromptCount(): Promise<number> {
    return this.promptCards.count();
  }

  async getPromptTexts(): Promise<string[]> {
    return this.promptTexts.allTextContents();
  }

  async toggleFavorite(index: number): Promise<void> {
    await this.favoriteButtons.nth(index).click();
  }

  async isFavorited(index: number): Promise<boolean> {
    const fav = this.favoriteButtons.nth(index);
    return fav.evaluate(el => el.classList.contains('prompt-card__fav--active'));
  }

  async getBadgeTexts(index: number): Promise<string[]> {
    const card = this.promptCards.nth(index);
    return card.locator('.prompt-card__badge').allTextContents();
  }
}
