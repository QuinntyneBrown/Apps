import { test, expect } from '@playwright/test';

test.describe('Navigation', () => {
  test.beforeEach(async ({ page }) => {
    await page.route('**/api/**', async route => {
      await route.fulfill({
        status: 200,
        contentType: 'application/json',
        body: JSON.stringify([])
      });
    });
  });

  test('should display app title in toolbar', async ({ page }) => {
    await page.goto('/');
    await expect(page.locator('mat-toolbar')).toContainText('Anniversary Birthday Reminder');
  });

  test('should display sidenav with navigation items', async ({ page }) => {
    await page.goto('/');
    await expect(page.locator('mat-sidenav')).toBeVisible();
    await expect(page.getByRole('link', { name: /Dashboard/i })).toBeVisible();
    await expect(page.getByRole('link', { name: /Dates/i })).toBeVisible();
    await expect(page.getByRole('link', { name: /Reminders/i })).toBeVisible();
    await expect(page.getByRole('link', { name: /Celebrations/i })).toBeVisible();
  });

  test('should navigate to Dashboard', async ({ page }) => {
    await page.goto('/dates');
    await page.click('a[href="/"]');
    await expect(page).toHaveURL('/');
  });

  test('should navigate to Dates', async ({ page }) => {
    await page.goto('/');
    await page.click('a[href="/dates"]');
    await expect(page).toHaveURL('/dates');
  });

  test('should navigate to Reminders', async ({ page }) => {
    await page.goto('/');
    await page.click('a[href="/reminders"]');
    await expect(page).toHaveURL('/reminders');
  });

  test('should navigate to Celebrations', async ({ page }) => {
    await page.goto('/');
    await page.click('a[href="/celebrations"]');
    await expect(page).toHaveURL('/celebrations');
  });

  test('should display logo in sidenav', async ({ page }) => {
    await page.goto('/');
    await expect(page.locator('.app-shell__logo')).toContainText('Celebrations');
  });

  test('should redirect unknown routes to dashboard', async ({ page }) => {
    await page.goto('/unknown-route');
    await expect(page).toHaveURL('/');
  });
});
