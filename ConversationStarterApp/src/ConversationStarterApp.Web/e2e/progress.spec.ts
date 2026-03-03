import { test, expect } from '@playwright/test';
import { ProgressPage } from './pages/progress.page';

test.describe('Progress', () => {
  let progress: ProgressPage;

  test.beforeEach(async ({ page }) => {
    progress = new ProgressPage(page);
    await progress.goto();
  });

  test('should display page header with "Progress" title', async () => {
    await expect(progress.pageHeader).toBeVisible();
    const title = await progress.getHeaderTitle();
    expect(title).toBe('Progress');
  });

  test('should display streak banner', async () => {
    await expect(progress.streakBanner).toBeVisible();
  });

  test('should display streak count', async () => {
    const count = await progress.getStreakCount();
    expect(count).toContain('7 Day Streak!');
  });

  test('should display streak subtitle', async () => {
    const subtitle = await progress.getStreakSubtitle();
    expect(subtitle).toBe('Keep talking daily to maintain your streak');
  });

  test('should display 7 day indicators', async () => {
    const dayCount = await progress.getStreakDayCount();
    expect(dayCount).toBe(7);
  });

  test('should show completed days in streak', async () => {
    const completedCount = await progress.getCompletedDayCount();
    expect(completedCount).toBe(6);
  });

  test('should display three stat cards', async () => {
    const count = await progress.statCards.count();
    expect(count).toBe(3);
  });

  test('should display correct stat labels', async () => {
    const labels = await progress.getStatLabels();
    expect(labels).toContain('Total Prompts');
    expect(labels).toContain('Avg Rating');
    expect(labels).toContain('Total Time');
  });

  test('should display correct stat values', async () => {
    const values = await progress.getStatValues();
    expect(values).toContain('42');
    expect(values).toContain('4.2');
    expect(values).toContain('18h');
  });

  test('should display Insights section header', async () => {
    await expect(progress.sectionHeader).toBeVisible();
  });

  test('should display insight cards', async () => {
    const count = await progress.getInsightCount();
    expect(count).toBe(2);
  });

  test('should display correct insight titles', async () => {
    const titles = await progress.getInsightTitles();
    expect(titles).toContain('Conversation Quality Rising');
    expect(titles).toContain('Most Popular Category');
  });

  test('should display insight descriptions', async () => {
    const descriptions = await progress.getInsightDescriptions();
    expect(descriptions).toContain('Your average rating improved 15% this month');
    expect(descriptions).toContain('Romantic prompts are your favorite — 60% of sessions');
  });
});
