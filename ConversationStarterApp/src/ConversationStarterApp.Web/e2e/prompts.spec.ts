import { test, expect } from '@playwright/test';
import { PromptsPage } from './pages/prompts.page';

test.describe('Prompts', () => {
  let prompts: PromptsPage;

  test.beforeEach(async ({ page }) => {
    prompts = new PromptsPage(page);
    await prompts.goto();
  });

  test('should display page header with "Prompts" title', async () => {
    await expect(prompts.pageHeader).toBeVisible();
    const title = await prompts.getHeaderTitle();
    expect(title).toBe('Prompts');
  });

  test('should display Random Prompt action button', async () => {
    await expect(prompts.pageHeaderAction).toBeVisible();
    await expect(prompts.pageHeaderAction).toHaveText('Random Prompt');
  });

  test('should display filter chips', async () => {
    const count = await prompts.filterChips.count();
    expect(count).toBeGreaterThanOrEqual(1);
  });

  test('should have "All" filter active by default', async () => {
    const active = await prompts.getActiveFilter();
    expect(active.trim()).toBe('All');
  });

  test('should switch active filter on click', async () => {
    await prompts.clickFilter('Deep');
    const active = await prompts.getActiveFilter();
    expect(active.trim()).toBe('Deep');
  });

  test('should display prompt cards', async () => {
    await expect(prompts.promptCards.first()).toBeVisible();
  });

  test('should display prompt text in cards', async () => {
    const texts = await prompts.getPromptTexts();
    expect(texts.length).toBeGreaterThan(0);
    texts.forEach(t => expect(t.trim().length).toBeGreaterThan(0));
  });

  test('should display badges on prompt cards', async () => {
    const badges = await prompts.getBadgeTexts(0);
    expect(badges.length).toBeGreaterThanOrEqual(1);
  });

  test('should display favorite buttons on prompt cards', async () => {
    const count = await prompts.favoriteButtons.count();
    expect(count).toBeGreaterThan(0);
  });

  test('should toggle favorite on click', async () => {
    const initialState = await prompts.isFavorited(0);
    await prompts.toggleFavorite(0);
    await prompts.page.waitForTimeout(300);
    const newState = await prompts.isFavorited(0);
    expect(newState).not.toBe(initialState);
  });
});
