import { test, expect } from '@playwright/test';

test.describe('Statistics Page', () => {
  test.beforeEach(async ({ page }) => {
    await page.goto('/statistics');
  });

  test('should display page title', async ({ page }) => {
    await expect(page.locator('h1')).toContainText('Your Viewing Statistics');
  });

  test('should display stat cards', async ({ page }) => {
    await expect(page.locator('app-stat-card').first()).toBeVisible();
  });

  test('should show movies watched stat', async ({ page }) => {
    await expect(page.getByText('Movies Watched')).toBeVisible();
  });

  test('should show shows watched stat', async ({ page }) => {
    await expect(page.getByText('Shows Watched')).toBeVisible();
  });

  test('should show hours watched stat', async ({ page }) => {
    await expect(page.getByText('Hours Watched')).toBeVisible();
  });

  test('should show average rating stat', async ({ page }) => {
    await expect(page.getByText('Avg Rating')).toBeVisible();
  });

  test('should show current streak stat', async ({ page }) => {
    await expect(page.getByText('Current Streak')).toBeVisible();
  });

  test('should show milestones stat', async ({ page }) => {
    await expect(page.getByText('Milestones')).toBeVisible();
  });

  test('should have period selector', async ({ page }) => {
    const periodSelect = page.locator('mat-select');
    await expect(periodSelect).toBeVisible();
  });

  test('should change period', async ({ page }) => {
    const periodSelect = page.locator('mat-select');
    await periodSelect.click();
    await page.locator('mat-option').filter({ hasText: /this month/i }).click();
  });

  test('should display bar chart', async ({ page }) => {
    await expect(page.locator('.statistics__bar-chart')).toBeVisible();
  });

  test('should display genre breakdown', async ({ page }) => {
    await expect(page.getByText('Genre Breakdown')).toBeVisible();
  });
});
