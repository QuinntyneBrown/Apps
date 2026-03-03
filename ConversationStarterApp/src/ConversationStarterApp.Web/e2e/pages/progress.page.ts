import { type Locator, type Page } from '@playwright/test';

export class ProgressPage {
  readonly page: Page;
  readonly pageHeader: Locator;
  readonly pageHeaderTitle: Locator;
  readonly streakBanner: Locator;
  readonly streakValue: Locator;
  readonly streakSubtitle: Locator;
  readonly streakDays: Locator;
  readonly statCards: Locator;
  readonly sectionHeader: Locator;
  readonly insightCards: Locator;
  readonly insightTitles: Locator;
  readonly insightDescriptions: Locator;

  constructor(page: Page) {
    this.page = page;
    this.pageHeader = page.locator('lib-page-header');
    this.pageHeaderTitle = page.locator('.page-header__title');
    this.streakBanner = page.locator('lib-streak-banner');
    this.streakValue = page.locator('.streak-banner__value');
    this.streakSubtitle = page.locator('.streak-banner__subtitle');
    this.streakDays = page.locator('.streak-banner__day');
    this.statCards = page.locator('lib-stat-card');
    this.sectionHeader = page.locator('lib-section-header');
    this.insightCards = page.locator('lib-insight-card');
    this.insightTitles = page.locator('.insight-card__title');
    this.insightDescriptions = page.locator('.insight-card__desc');
  }

  async goto(): Promise<void> {
    await this.page.goto('/progress');
  }

  async getHeaderTitle(): Promise<string> {
    return (await this.pageHeaderTitle.textContent()) ?? '';
  }

  async getStreakCount(): Promise<string> {
    return (await this.streakValue.textContent()) ?? '';
  }

  async getStreakSubtitle(): Promise<string> {
    return (await this.streakSubtitle.textContent()) ?? '';
  }

  async getStreakDayCount(): Promise<number> {
    return this.streakDays.count();
  }

  async getCompletedDayCount(): Promise<number> {
    return this.page.locator('.streak-banner__day--completed').count();
  }

  async getStatValues(): Promise<string[]> {
    return this.page.locator('.stat-card__value').allTextContents();
  }

  async getStatLabels(): Promise<string[]> {
    return this.page.locator('.stat-card__label').allTextContents();
  }

  async getInsightCount(): Promise<number> {
    return this.insightCards.count();
  }

  async getInsightTitles(): Promise<string[]> {
    return this.insightTitles.allTextContents();
  }

  async getInsightDescriptions(): Promise<string[]> {
    return this.insightDescriptions.allTextContents();
  }
}
