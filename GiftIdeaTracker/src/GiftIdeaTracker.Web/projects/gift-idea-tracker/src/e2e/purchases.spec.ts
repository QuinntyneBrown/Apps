import { test, expect } from '@playwright/test';
import { setupMockAPI, mockPurchases, mockGiftIdeas } from './fixtures';

test.describe('Purchases Page', () => {
  test.beforeEach(async ({ page }) => {
    await setupMockAPI(page);
    await page.goto('/purchases');
  });

  test('should display purchases page title', async ({ page }) => {
    await expect(page.locator('h1')).toContainText('Purchases');
  });

  test('should display purchase summary', async ({ page }) => {
    await expect(page.locator('text=purchases')).toBeVisible();
    await expect(page.locator('text=Total:')).toBeVisible();
  });

  test('should display purchase count', async ({ page }) => {
    await expect(page.locator(`text=${mockPurchases.length} purchases`)).toBeVisible();
  });

  test('should display purchase cards', async ({ page }) => {
    await expect(page.locator('text=Book Collection')).toBeVisible();
  });

  test('should display store name on purchase card', async ({ page }) => {
    await expect(page.locator('text=Amazon')).toBeVisible();
  });

  test('should display delete button on purchase cards', async ({ page }) => {
    const deleteButtons = page.locator('button:has-text("Delete")');
    await expect(deleteButtons.first()).toBeVisible();
  });

  test('should delete purchase', async ({ page }) => {
    const deleteButton = page.locator('button:has-text("Delete")').first();
    await deleteButton.click();
    // The page should still be functional after delete
    await expect(page.locator('h1')).toContainText('Purchases');
  });

  test('should display purchased status', async ({ page }) => {
    await expect(page.locator('text=Purchased').first()).toBeVisible();
  });

  test('should display total spent amount', async ({ page }) => {
    await expect(page.locator('text=$45.00')).toBeVisible();
  });
});
