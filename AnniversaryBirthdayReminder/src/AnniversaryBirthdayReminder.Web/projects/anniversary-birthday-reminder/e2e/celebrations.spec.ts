import { test, expect } from '@playwright/test';

test.describe('Celebrations', () => {
  const mockCelebrations = [
    {
      celebrationId: '1',
      dateId: 'date1',
      celebrationDate: new Date(2024, 2, 15).toISOString(),
      notes: 'Great birthday party!',
      photos: ['photo1.jpg', 'photo2.jpg'],
      attendees: ['John', 'Jane', 'Bob'],
      rating: 5,
      status: 'Completed'
    },
    {
      celebrationId: '2',
      dateId: 'date2',
      celebrationDate: new Date(2024, 1, 14).toISOString(),
      notes: 'Could not attend',
      photos: [],
      attendees: [],
      rating: 0,
      status: 'Skipped'
    }
  ];

  test.beforeEach(async ({ page }) => {
    await page.route('**/api/celebrations', async route => {
      await route.fulfill({
        status: 200,
        contentType: 'application/json',
        body: JSON.stringify(mockCelebrations)
      });
    });

    await page.route('**/api/celebrations/1', async route => {
      await route.fulfill({
        status: 200,
        contentType: 'application/json',
        body: JSON.stringify(mockCelebrations[0])
      });
    });

    await page.route('**/api/dates/upcoming', async route => {
      await route.fulfill({
        status: 200,
        contentType: 'application/json',
        body: JSON.stringify([])
      });
    });
  });

  test('should display celebration history title', async ({ page }) => {
    await page.goto('/celebrations');
    await expect(page.locator('h1.celebration-list__title')).toContainText('Celebration History');
  });

  test('should display celebration stats', async ({ page }) => {
    await page.goto('/celebrations');
    await expect(page.locator('.celebration-list__stat-value').first()).toContainText('1');
    await expect(page.locator('.celebration-list__stat-label').first()).toContainText('Celebrated');
  });

  test('should display celebration cards', async ({ page }) => {
    await page.goto('/celebrations');
    await expect(page.locator('mat-card.celebration-list__card')).toHaveCount(2);
  });

  test('should display rating stars for completed celebrations', async ({ page }) => {
    await page.goto('/celebrations');
    await expect(page.locator('.celebration-list__stars').first()).toContainText('★★★★★');
  });

  test('should display notes', async ({ page }) => {
    await page.goto('/celebrations');
    await expect(page.getByText('Great birthday party!')).toBeVisible();
  });

  test('should display photo count', async ({ page }) => {
    await page.goto('/celebrations');
    await expect(page.getByText('2 photos')).toBeVisible();
  });

  test('should display attendee count', async ({ page }) => {
    await page.goto('/celebrations');
    await expect(page.getByText('3 attendees')).toBeVisible();
  });

  test('should have View Details button', async ({ page }) => {
    await page.goto('/celebrations');
    await expect(page.getByRole('button', { name: /View Details/i }).first()).toBeVisible();
  });

  test('should show empty state when no celebrations', async ({ page }) => {
    await page.route('**/api/celebrations', async route => {
      await route.fulfill({
        status: 200,
        contentType: 'application/json',
        body: JSON.stringify([])
      });
    });

    await page.goto('/celebrations');
    await expect(page.locator('.celebration-list__empty')).toBeVisible();
    await expect(page.locator('.celebration-list__empty h2')).toContainText('No celebrations recorded yet');
  });
});
