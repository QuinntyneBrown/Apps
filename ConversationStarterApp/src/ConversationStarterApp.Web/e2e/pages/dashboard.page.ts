import { type Locator, type Page } from '@playwright/test';

export class DashboardPage {
  readonly page: Page;
  readonly heroSection: Locator;
  readonly heroPromptText: Locator;
  readonly heroLabel: Locator;
  readonly heroBadges: Locator;
  readonly heroUseButton: Locator;
  readonly heroSkipButton: Locator;
  readonly statCards: Locator;
  readonly sectionHeader: Locator;
  readonly conversationCards: Locator;

  constructor(page: Page) {
    this.page = page;
    this.heroSection = page.locator('lib-daily-prompt-hero');
    this.heroPromptText = page.locator('.daily-prompt-hero__text');
    this.heroLabel = page.locator('.daily-prompt-hero__label');
    this.heroBadges = page.locator('.daily-prompt-hero__badge');
    this.heroUseButton = page.locator('.daily-prompt-hero__btn--accent');
    this.heroSkipButton = page.locator('.daily-prompt-hero__btn--ghost');
    this.statCards = page.locator('lib-stat-card');
    this.sectionHeader = page.locator('lib-section-header');
    this.conversationCards = page.locator('lib-conversation-card');
  }

  async goto(): Promise<void> {
    await this.page.goto('/dashboard');
  }

  async getStatValues(): Promise<string[]> {
    const values = this.page.locator('.stat-card__value');
    return values.allTextContents();
  }

  async getStatLabels(): Promise<string[]> {
    const labels = this.page.locator('.stat-card__label');
    return labels.allTextContents();
  }

  async clickUsePrompt(): Promise<void> {
    await this.heroUseButton.click();
  }

  async clickSkipPrompt(): Promise<void> {
    await this.heroSkipButton.click();
  }

  async getSectionHeaderTitle(): Promise<string> {
    return (await this.sectionHeader.locator('.section-header__title').textContent()) ?? '';
  }

  async getConversationCount(): Promise<number> {
    return this.conversationCards.count();
  }
}
