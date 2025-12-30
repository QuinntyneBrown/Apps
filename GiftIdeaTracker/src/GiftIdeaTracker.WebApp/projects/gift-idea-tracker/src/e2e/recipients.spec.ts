import { test, expect } from '@playwright/test';
import { setupMockAPI, mockRecipients } from './fixtures';

test.describe('Recipients Page', () => {
  test.beforeEach(async ({ page }) => {
    await setupMockAPI(page);
    await page.goto('/recipients');
  });

  test('should display recipients page title', async ({ page }) => {
    await expect(page.locator('h1')).toContainText('Recipients');
  });

  test('should display add recipient button', async ({ page }) => {
    await expect(page.locator('button:has-text("Add Recipient")')).toBeVisible();
  });

  test('should display recipient cards', async ({ page }) => {
    for (const recipient of mockRecipients) {
      await expect(page.locator(`text=${recipient.name}`)).toBeVisible();
    }
  });

  test('should display recipient relationships', async ({ page }) => {
    await expect(page.locator('text=Friend')).toBeVisible();
    await expect(page.locator('text=Family')).toBeVisible();
  });

  test('should display view ideas button on recipient cards', async ({ page }) => {
    const viewButtons = page.locator('button:has-text("View Ideas")');
    await expect(viewButtons.first()).toBeVisible();
  });

  test('should display edit button on recipient cards', async ({ page }) => {
    const editButtons = page.locator('button:has-text("Edit")');
    await expect(editButtons.first()).toBeVisible();
  });

  test('should display delete button on recipient cards', async ({ page }) => {
    const deleteButtons = page.locator('button:has-text("Delete")');
    await expect(deleteButtons.first()).toBeVisible();
  });

  test('should open add recipient dialog', async ({ page }) => {
    await page.click('button:has-text("Add Recipient")');
    await expect(page.locator('text=Add Recipient').first()).toBeVisible();
    await expect(page.locator('input[formcontrolname="name"]')).toBeVisible();
  });

  test('should create new recipient', async ({ page }) => {
    await page.click('button:has-text("Add Recipient")');
    await page.fill('input[formcontrolname="name"]', 'New Person');
    await page.fill('input[formcontrolname="relationship"]', 'Colleague');
    await page.click('button:has-text("Add")');
    await expect(page.locator('mat-dialog-container')).not.toBeVisible();
  });

  test('should open edit recipient dialog', async ({ page }) => {
    await page.locator('button:has-text("Edit")').first().click();
    await expect(page.locator('text=Edit Recipient')).toBeVisible();
  });

  test('should navigate to gift ideas when clicking view ideas', async ({ page }) => {
    await page.locator('button:has-text("View Ideas")').first().click();
    await expect(page).toHaveURL(/\/gift-ideas\?recipientId=/);
  });

  test('should delete recipient', async ({ page }) => {
    const deleteButton = page.locator('button:has-text("Delete")').first();
    await deleteButton.click();
    // The page should still be functional after delete
    await expect(page.locator('h1')).toContainText('Recipients');
  });
});
