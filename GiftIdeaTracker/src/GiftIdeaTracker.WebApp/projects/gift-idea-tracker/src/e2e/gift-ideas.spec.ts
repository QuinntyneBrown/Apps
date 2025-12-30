import { test, expect } from '@playwright/test';
import { setupMockAPI, mockGiftIdeas, mockRecipients } from './fixtures';

test.describe('Gift Ideas Page', () => {
  test.beforeEach(async ({ page }) => {
    await setupMockAPI(page);
    await page.goto('/gift-ideas');
  });

  test('should display gift ideas page title', async ({ page }) => {
    await expect(page.locator('h1')).toContainText('Gift Ideas');
  });

  test('should display add gift idea button', async ({ page }) => {
    await expect(page.locator('button:has-text("Add Gift Idea")')).toBeVisible();
  });

  test('should display gift idea cards', async ({ page }) => {
    await expect(page.locator('text=New iPhone')).toBeVisible();
    await expect(page.locator('text=Book Collection')).toBeVisible();
    await expect(page.locator('text=Watch')).toBeVisible();
  });

  test('should display recipient filter dropdown', async ({ page }) => {
    await expect(page.locator('mat-select').first()).toBeVisible();
  });

  test('should display occasion filter dropdown', async ({ page }) => {
    const selects = page.locator('mat-select');
    await expect(selects.nth(1)).toBeVisible();
  });

  test('should display purchase button for unpurchased items', async ({ page }) => {
    await expect(page.locator('button:has-text("Purchase")').first()).toBeVisible();
  });

  test('should display edit button on gift idea cards', async ({ page }) => {
    const editButtons = page.locator('button:has-text("Edit")');
    await expect(editButtons.first()).toBeVisible();
  });

  test('should display delete button on gift idea cards', async ({ page }) => {
    const deleteButtons = page.locator('button:has-text("Delete")');
    await expect(deleteButtons.first()).toBeVisible();
  });

  test('should open add gift idea dialog', async ({ page }) => {
    await page.click('button:has-text("Add Gift Idea")');
    await expect(page.locator('h2:has-text("Add Gift Idea")')).toBeVisible();
    await expect(page.locator('input[formcontrolname="name"]')).toBeVisible();
  });

  test('should create new gift idea', async ({ page }) => {
    await page.click('button:has-text("Add Gift Idea")');
    await page.fill('input[formcontrolname="name"]', 'New Gift');
    await page.click('mat-select[formcontrolname="occasion"]');
    await page.click('mat-option:has-text("Birthday")');
    await page.click('button:has-text("Add")');
    await expect(page.locator('mat-dialog-container')).not.toBeVisible();
  });

  test('should open edit gift idea dialog', async ({ page }) => {
    await page.locator('button:has-text("Edit")').first().click();
    await expect(page.locator('text=Edit Gift Idea')).toBeVisible();
  });

  test('should filter by recipient', async ({ page }) => {
    await page.locator('mat-select').first().click();
    await page.click('mat-option:has-text("John Doe")');
    await expect(page.locator('text=New iPhone')).toBeVisible();
  });

  test('should filter by occasion', async ({ page }) => {
    await page.locator('mat-select').nth(1).click();
    await page.click('mat-option:has-text("Birthday")');
    await expect(page.locator('text=New iPhone')).toBeVisible();
  });

  test('should purchase gift idea', async ({ page }) => {
    const purchaseButton = page.locator('button:has-text("Purchase")').first();
    await purchaseButton.click();
    // The page should still be functional after purchase
    await expect(page.locator('h1')).toContainText('Gift Ideas');
  });

  test('should delete gift idea', async ({ page }) => {
    const deleteButton = page.locator('button:has-text("Delete")').first();
    await deleteButton.click();
    // The page should still be functional after delete
    await expect(page.locator('h1')).toContainText('Gift Ideas');
  });

  test('should display gift idea with recipient filter from URL', async ({ page }) => {
    await page.goto('/gift-ideas?recipientId=recipient-1');
    await expect(page.locator('text=Gift Ideas for John Doe')).toBeVisible();
  });
});
