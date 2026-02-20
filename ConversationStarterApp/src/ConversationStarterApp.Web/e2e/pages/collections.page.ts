import { type Locator, type Page } from '@playwright/test';

export class CollectionsPage {
  readonly page: Page;
  readonly pageHeader: Locator;
  readonly pageHeaderTitle: Locator;
  readonly pageHeaderAction: Locator;
  readonly collectionCards: Locator;
  readonly collectionTitles: Locator;
  readonly collectionDescriptions: Locator;
  readonly collectionCounts: Locator;

  constructor(page: Page) {
    this.page = page;
    this.pageHeader = page.locator('lib-page-header');
    this.pageHeaderTitle = page.locator('.page-header__title');
    this.pageHeaderAction = page.locator('.page-header__action');
    this.collectionCards = page.locator('lib-collection-card');
    this.collectionTitles = page.locator('.collection-card__title');
    this.collectionDescriptions = page.locator('.collection-card__desc');
    this.collectionCounts = page.locator('.collection-card__count');
  }

  async goto(): Promise<void> {
    await this.page.goto('/collections');
  }

  async getHeaderTitle(): Promise<string> {
    return (await this.pageHeaderTitle.textContent()) ?? '';
  }

  async getCollectionCount(): Promise<number> {
    return this.collectionCards.count();
  }

  async getCollectionTitles(): Promise<string[]> {
    return this.collectionTitles.allTextContents();
  }

  async getCollectionDescriptions(): Promise<string[]> {
    return this.collectionDescriptions.allTextContents();
  }

  async getCollectionPromptCounts(): Promise<string[]> {
    return this.collectionCounts.allTextContents();
  }

  async hasNewCollectionButton(): Promise<boolean> {
    return this.pageHeaderAction.isVisible();
  }

  async getCollectionColor(index: number): Promise<string> {
    const card = this.collectionCards.nth(index).locator('.collection-card');
    const classes = await card.getAttribute('class');
    const match = classes?.match(/collection-card--(\w+)/);
    return match ? match[1] : '';
  }
}
