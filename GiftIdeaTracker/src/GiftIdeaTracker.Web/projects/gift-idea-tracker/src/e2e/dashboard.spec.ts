import { test, expect } from '@playwright/test';
import { setupMockAPI, mockRecipients, mockGiftIdeas, mockPurchases } from './fixtures';

test.describe('Dashboard Page', () => {
  test.beforeEach(async ({ page }) => {
    await setupMockAPI(page);
    await page.goto('/');
  });

  test('should display dashboard title', async ({ page }) => {
    await expect(page.locator('h1')).toContainText('Dashboard');
  });

  test('should display statistics cards', async ({ page }) => {
    await expect(page.locator('text=Recipients')).toBeVisible();
    await expect(page.locator('text=Gift Ideas')).toBeVisible();
    await expect(page.locator('text=Purchased')).toBeVisible();
  });

  test('should display recipient count', async ({ page }) => {
    await expect(page.locator('text=' + mockRecipients.length)).toBeVisible();
  });

  test('should display gift ideas count', async ({ page }) => {
    await expect(page.locator('text=' + mockGiftIdeas.length)).toBeVisible();
  });

  test('should display recent gift ideas', async ({ page }) => {
    await expect(page.locator('text=Recent Gift Ideas')).toBeVisible();
    await expect(page.locator('text=New iPhone')).toBeVisible();
  });

  test('should display budget overview', async ({ page }) => {
    await expect(page.locator('text=Budget')).toBeVisible();
  });

  test('should navigate to recipients page', async ({ page }) => {
    await page.click('a:has-text("Recipients")');
    await expect(page).toHaveURL('/recipients');
  });

  test('should navigate to gift ideas page', async ({ page }) => {
    await page.click('a:has-text("Ideas")');
    await expect(page).toHaveURL('/gift-ideas');
  });

  test('should navigate to purchases page', async ({ page }) => {
    await page.click('a:has-text("Purchases")');
    await expect(page).toHaveURL('/purchases');
  });

  test('should open add gift idea dialog from header', async ({ page }) => {
    await page.click('button:has-text("Add Idea")');
    await expect(page.locator('text=Add Gift Idea')).toBeVisible();
  });

  test('should open add gift idea dialog from quick actions', async ({ page }) => {
    await page.locator('.dashboard__actions button:has-text("Add Gift Idea")').click();
    await expect(page.locator('text=Add Gift Idea')).toBeVisible();
  });

  test('should close add gift idea dialog on cancel', async ({ page }) => {
    await page.click('button:has-text("Add Idea")');
    await expect(page.locator('text=Add Gift Idea')).toBeVisible();
    await page.click('button:has-text("Cancel")');
    await expect(page.locator('mat-dialog-container')).not.toBeVisible();
  });
});
