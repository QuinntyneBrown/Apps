import { test, expect } from '@playwright/test';

test.describe('Dashboard', () => {
  test.beforeEach(async ({ page }) => {
    await page.route('**/api/dates/upcoming', async route => {
      await route.fulfill({
        status: 200,
        contentType: 'application/json',
        body: JSON.stringify([
          {
            dateId: '1',
            userId: 'user1',
            personName: 'John Doe',
            dateType: 'Birthday',
            dateValue: new Date(new Date().setDate(new Date().getDate() + 1)).toISOString(),
            recurrencePattern: 'Annual',
            relationship: 'Friend',
            notes: 'Birthday celebration',
            isActive: true,
            createdAt: new Date().toISOString()
          },
          {
            dateId: '2',
            userId: 'user1',
            personName: 'Jane Smith',
            dateType: 'Anniversary',
            dateValue: new Date(new Date().setDate(new Date().getDate() + 5)).toISOString(),
            recurrencePattern: 'Annual',
            relationship: 'Family',
            notes: '',
            isActive: true,
            createdAt: new Date().toISOString()
          }
        ])
      });
    });
  });

  test('should display dashboard title', async ({ page }) => {
    await page.goto('/');
    await expect(page.locator('h1.dashboard__title')).toContainText('Upcoming Celebrations');
  });

  test('should display upcoming celebrations', async ({ page }) => {
    await page.goto('/');
    await expect(page.locator('mat-card').first()).toBeVisible();
    await expect(page.locator('mat-card-title').first()).toContainText('John Doe');
  });

  test('should show Set Reminder button', async ({ page }) => {
    await page.goto('/');
    await expect(page.getByRole('link', { name: /Set Reminder/i }).first()).toBeVisible();
  });

  test('should show Plan Gift button', async ({ page }) => {
    await page.goto('/');
    await expect(page.getByRole('link', { name: /Plan Gift/i }).first()).toBeVisible();
  });

  test('should navigate to dates page via sidenav', async ({ page }) => {
    await page.goto('/');
    await page.click('a[href="/dates"]');
    await expect(page).toHaveURL('/dates');
  });

  test('should show empty state when no upcoming dates', async ({ page }) => {
    await page.route('**/api/dates/upcoming', async route => {
      await route.fulfill({
        status: 200,
        contentType: 'application/json',
        body: JSON.stringify([])
      });
    });

    await page.goto('/');
    await expect(page.locator('.dashboard__empty')).toBeVisible();
    await expect(page.locator('.dashboard__empty h2')).toContainText('No upcoming celebrations');
  });
});
