import { test, expect } from '@playwright/test';
import { setupMockAPI } from './fixtures';

test.describe('Navigation', () => {
  test.beforeEach(async ({ page }) => {
    await setupMockAPI(page);
    await page.goto('/');
  });

  test('should display header on all pages', async ({ page }) => {
    await expect(page.locator('text=Gift Idea Tracker')).toBeVisible();

    await page.goto('/recipients');
    await expect(page.locator('text=Gift Idea Tracker')).toBeVisible();

    await page.goto('/gift-ideas');
    await expect(page.locator('text=Gift Idea Tracker')).toBeVisible();

    await page.goto('/purchases');
    await expect(page.locator('text=Gift Idea Tracker')).toBeVisible();
  });

  test('should navigate between pages using header links', async ({ page }) => {
    await page.click('a:has-text("Recipients")');
    await expect(page).toHaveURL('/recipients');
    await expect(page.locator('h1')).toContainText('Recipients');

    await page.click('a:has-text("Ideas")');
    await expect(page).toHaveURL('/gift-ideas');
    await expect(page.locator('h1')).toContainText('Gift Ideas');

    await page.click('a:has-text("Purchases")');
    await expect(page).toHaveURL('/purchases');
    await expect(page.locator('h1')).toContainText('Purchases');

    await page.click('a:has-text("Dashboard")');
    await expect(page).toHaveURL('/');
    await expect(page.locator('h1')).toContainText('Dashboard');
  });

  test('should redirect unknown routes to dashboard', async ({ page }) => {
    await page.goto('/unknown-route');
    await expect(page).toHaveURL('/');
    await expect(page.locator('h1')).toContainText('Dashboard');
  });

  test('should display add idea button in header on all pages', async ({ page }) => {
    await expect(page.locator('header button:has-text("Add Idea")')).toBeVisible();

    await page.goto('/recipients');
    await expect(page.locator('header button:has-text("Add Idea")')).toBeVisible();

    await page.goto('/gift-ideas');
    await expect(page.locator('header button:has-text("Add Idea")')).toBeVisible();

    await page.goto('/purchases');
    await expect(page.locator('header button:has-text("Add Idea")')).toBeVisible();
  });
});
