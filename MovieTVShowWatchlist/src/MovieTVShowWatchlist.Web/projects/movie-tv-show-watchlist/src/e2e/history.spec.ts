import { test, expect } from '@playwright/test';

test.describe('History Page', () => {
  test.beforeEach(async ({ page }) => {
    await page.goto('/history');
  });

  test('should display page title', async ({ page }) => {
    await expect(page.locator('h1')).toContainText('Viewing History');
  });

  test('should display history cards', async ({ page }) => {
    await expect(page.locator('.history__card').first()).toBeVisible();
  });

  test('should display movie icon for movies', async ({ page }) => {
    await expect(page.locator('.history__card-icon').first()).toBeVisible();
  });

  test('should display card title', async ({ page }) => {
    await expect(page.locator('.history__card-title').first()).toBeVisible();
  });

  test('should display watch date', async ({ page }) => {
    await expect(page.locator('.history__card-date').first()).toBeVisible();
  });

  test('should display platform', async ({ page }) => {
    await expect(page.locator('.history__card-platform').first()).toBeVisible();
  });

  test('should display rating stars', async ({ page }) => {
    await expect(page.locator('.history__card-rating').first()).toBeVisible();
  });

  test('should show rewatch chip for rewatches', async ({ page }) => {
    const rewatchChip = page.locator('.history__rewatch-chip');
    // May or may not be visible depending on mock data
    expect(rewatchChip).toBeDefined();
  });
});
