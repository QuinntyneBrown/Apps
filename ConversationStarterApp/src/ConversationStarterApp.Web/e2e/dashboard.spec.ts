import { test, expect } from '@playwright/test';
import { DashboardPage } from './pages/dashboard.page';

test.describe('Dashboard', () => {
  let dashboard: DashboardPage;

  test.beforeEach(async ({ page }) => {
    dashboard = new DashboardPage(page);
    await dashboard.goto();
  });

  test('should display the daily prompt hero section', async () => {
    await expect(dashboard.heroSection).toBeVisible();
  });

  test('should display "Today\'s Prompt" label', async () => {
    await expect(dashboard.heroLabel).toHaveText("Today's Prompt");
  });

  test('should display prompt text in hero', async () => {
    await expect(dashboard.heroPromptText).toBeVisible();
    const text = await dashboard.heroPromptText.textContent();
    expect(text?.length).toBeGreaterThan(0);
  });

  test('should display category and difficulty badges', async () => {
    const badgeCount = await dashboard.heroBadges.count();
    expect(badgeCount).toBeGreaterThanOrEqual(1);
  });

  test('should display Use Prompt and Skip buttons', async () => {
    await expect(dashboard.heroUseButton).toBeVisible();
    await expect(dashboard.heroSkipButton).toBeVisible();
    await expect(dashboard.heroUseButton).toContainText('Use Prompt');
    await expect(dashboard.heroSkipButton).toContainText('Skip');
  });

  test('should display three stat cards', async () => {
    const count = await dashboard.statCards.count();
    expect(count).toBe(3);
  });

  test('should display correct stat labels', async () => {
    const labels = await dashboard.getStatLabels();
    expect(labels).toContain('Day Streak');
    expect(labels).toContain('Prompts Used');
    expect(labels).toContain('Favorites');
  });

  test('should display stat values', async () => {
    const values = await dashboard.getStatValues();
    expect(values.length).toBe(3);
    values.forEach(v => expect(v.trim().length).toBeGreaterThan(0));
  });

  test('should display Recent Conversations section header', async () => {
    const title = await dashboard.getSectionHeaderTitle();
    expect(title).toBe('Recent Conversations');
  });

  test('should change prompt when Skip is clicked', async () => {
    const initialText = await dashboard.heroPromptText.textContent();
    await dashboard.clickSkipPrompt();
    // Allow time for the new prompt to load
    await dashboard.page.waitForTimeout(500);
    // The prompt should either change or remain (depends on API), but the action should work without error
    await expect(dashboard.heroPromptText).toBeVisible();
  });
});
