import { test, expect } from '@playwright/test';

test.describe('Navigation', () => {
  test('should redirect to watchlist by default', async ({ page }) => {
    await page.goto('/');
    await expect(page).toHaveURL(/.*watchlist/);
  });

  test('should display header', async ({ page }) => {
    await page.goto('/');
    await expect(page.locator('app-header')).toBeVisible();
  });

  test('should display logo', async ({ page }) => {
    await page.goto('/');
    await expect(page.locator('.header__logo')).toContainText('WatchTrack');
  });

  test('should navigate to watchlist', async ({ page }) => {
    await page.goto('/');
    await page.getByRole('link', { name: /watchlist/i }).click();
    await expect(page).toHaveURL(/.*watchlist/);
  });

  test('should navigate to history', async ({ page }) => {
    await page.goto('/');
    await page.getByRole('link', { name: /history/i }).click();
    await expect(page).toHaveURL(/.*history/);
  });

  test('should navigate to statistics', async ({ page }) => {
    await page.goto('/');
    await page.getByRole('link', { name: /statistics/i }).click();
    await expect(page).toHaveURL(/.*statistics/);
  });

  test('should navigate to discover', async ({ page }) => {
    await page.goto('/');
    await page.getByRole('link', { name: /discover/i }).click();
    await expect(page).toHaveURL(/.*discover/);
  });

  test('should have all navigation links', async ({ page }) => {
    await page.goto('/');
    await expect(page.getByRole('link', { name: /watchlist/i })).toBeVisible();
    await expect(page.getByRole('link', { name: /history/i })).toBeVisible();
    await expect(page.getByRole('link', { name: /statistics/i })).toBeVisible();
    await expect(page.getByRole('link', { name: /discover/i })).toBeVisible();
  });

  test('should handle unknown routes', async ({ page }) => {
    await page.goto('/unknown-route');
    await expect(page).toHaveURL(/.*watchlist/);
  });
});
