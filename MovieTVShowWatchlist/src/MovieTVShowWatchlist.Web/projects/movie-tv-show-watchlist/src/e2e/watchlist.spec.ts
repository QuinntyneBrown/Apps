import { test, expect } from '@playwright/test';

test.describe('Watchlist Page', () => {
  test.beforeEach(async ({ page }) => {
    await page.goto('/watchlist');
  });

  test('should display page title', async ({ page }) => {
    await expect(page.locator('h1')).toContainText('My Watchlist');
  });

  test('should display watchlist items', async ({ page }) => {
    await expect(page.locator('app-watchlist-card').first()).toBeVisible();
  });

  test('should have add content button', async ({ page }) => {
    await expect(page.getByRole('button', { name: /add content/i })).toBeVisible();
  });

  test('should have sort dropdown', async ({ page }) => {
    await expect(page.locator('mat-select')).toBeVisible();
  });

  test('should filter by content type', async ({ page }) => {
    const moviesCheckbox = page.getByLabel('Movies');
    await expect(moviesCheckbox).toBeVisible();
  });

  test('should filter by genre', async ({ page }) => {
    const actionCheckbox = page.getByLabel('Action');
    await expect(actionCheckbox).toBeVisible();
  });

  test('should have sidebar with filters', async ({ page }) => {
    await expect(page.locator('app-sidebar')).toBeVisible();
  });

  test('should display priority badges on cards', async ({ page }) => {
    await expect(page.locator('.watchlist-card__priority').first()).toBeVisible();
  });

  test('should have watch and remove buttons on cards', async ({ page }) => {
    const card = page.locator('app-watchlist-card').first();
    await expect(card.getByRole('button', { name: /watch|start/i })).toBeVisible();
    await expect(card.getByRole('button', { name: /remove/i })).toBeVisible();
  });

  test('should clear all filters', async ({ page }) => {
    const clearButton = page.getByRole('button', { name: /clear all filters/i });
    await expect(clearButton).toBeVisible();
    await clearButton.click();
  });

  test('should sort by different options', async ({ page }) => {
    const sortSelect = page.locator('mat-select');
    await sortSelect.click();
    await page.locator('mat-option').filter({ hasText: /title/i }).click();
  });
});
