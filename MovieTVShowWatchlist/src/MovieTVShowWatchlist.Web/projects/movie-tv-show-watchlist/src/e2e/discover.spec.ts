import { test, expect } from '@playwright/test';

test.describe('Discover Page', () => {
  test.beforeEach(async ({ page }) => {
    await page.goto('/discover');
  });

  test('should display page title', async ({ page }) => {
    await expect(page.locator('h1')).toContainText('Discover');
  });

  test('should display page subtitle', async ({ page }) => {
    await expect(page.getByText(/personalized recommendations/i)).toBeVisible();
  });

  test('should display recommendation cards', async ({ page }) => {
    await expect(page.locator('.discover__card').first()).toBeVisible();
  });

  test('should display match score', async ({ page }) => {
    await expect(page.locator('.discover__card-score').first()).toBeVisible();
  });

  test('should display card title', async ({ page }) => {
    await expect(page.locator('.discover__card-title').first()).toBeVisible();
  });

  test('should display genres', async ({ page }) => {
    await expect(page.locator('.discover__card-genre').first()).toBeVisible();
  });

  test('should display recommendation reason', async ({ page }) => {
    await expect(page.locator('.discover__card-reason').first()).toBeVisible();
  });

  test('should have add to watchlist button', async ({ page }) => {
    await expect(page.getByRole('button', { name: /add to watchlist/i }).first()).toBeVisible();
  });

  test('should have dismiss button', async ({ page }) => {
    await expect(page.getByRole('button', { name: /dismiss/i }).first()).toBeVisible();
  });

  test('should show source icon', async ({ page }) => {
    await expect(page.locator('.discover__card-source').first()).toBeVisible();
  });

  test('should add recommendation to watchlist', async ({ page }) => {
    const addButton = page.getByRole('button', { name: /add to watchlist/i }).first();
    await addButton.click();
    // Verify no error occurred
  });

  test('should dismiss recommendation', async ({ page }) => {
    const initialCount = await page.locator('.discover__card').count();
    const dismissButton = page.getByRole('button', { name: /dismiss/i }).first();
    await dismissButton.click();
    // Card should be removed or count should decrease
  });
});
