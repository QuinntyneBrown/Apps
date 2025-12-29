import { test, expect } from '@playwright/test';

test.describe('Reminders', () => {
  const mockSettings = {
    oneWeekBefore: true,
    threeDaysBefore: true,
    oneDayBefore: true,
    channels: ['Email', 'Push']
  };

  test.beforeEach(async ({ page }) => {
    await page.route('**/api/reminders/settings', async route => {
      if (route.request().method() === 'GET') {
        await route.fulfill({
          status: 200,
          contentType: 'application/json',
          body: JSON.stringify(mockSettings)
        });
      } else if (route.request().method() === 'PUT') {
        await route.fulfill({
          status: 200,
          contentType: 'application/json',
          body: JSON.stringify(route.request().postDataJSON())
        });
      }
    });

    await page.route('**/api/dates/upcoming', async route => {
      await route.fulfill({
        status: 200,
        contentType: 'application/json',
        body: JSON.stringify([])
      });
    });
  });

  test('should display reminders page title', async ({ page }) => {
    await page.goto('/reminders');
    await expect(page.locator('h1.reminders__title')).toContainText('Reminder Settings');
  });

  test('should display timing settings', async ({ page }) => {
    await page.goto('/reminders');
    await expect(page.getByText('Notification Timing')).toBeVisible();
    await expect(page.getByText('1 week before')).toBeVisible();
    await expect(page.getByText('3 days before')).toBeVisible();
    await expect(page.getByText('1 day before')).toBeVisible();
  });

  test('should display channel settings', async ({ page }) => {
    await page.goto('/reminders');
    await expect(page.getByText('Notification Channels')).toBeVisible();
    await expect(page.getByText('Email')).toBeVisible();
    await expect(page.getByText('Push Notifications')).toBeVisible();
    await expect(page.getByText('SMS')).toBeVisible();
  });

  test('should have Save Settings button', async ({ page }) => {
    await page.goto('/reminders');
    await expect(page.getByRole('button', { name: /Save Settings/i })).toBeVisible();
  });

  test('should toggle timing checkbox', async ({ page }) => {
    await page.goto('/reminders');
    const checkbox = page.locator('mat-checkbox').filter({ hasText: '1 week before' });
    await checkbox.click();
    await expect(checkbox).not.toHaveClass(/mat-mdc-checkbox-checked/);
  });
});
