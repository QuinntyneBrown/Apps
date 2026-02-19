import { test, expect } from '@playwright/test';

test.describe('Gift Planning', () => {
  const mockGifts = [
    {
      giftId: '1',
      dateId: 'date1',
      description: 'Watch',
      estimatedPrice: 200,
      actualPrice: null,
      purchaseUrl: 'https://example.com',
      status: 'Idea',
      purchasedAt: null
    },
    {
      giftId: '2',
      dateId: 'date1',
      description: 'Book',
      estimatedPrice: 25,
      actualPrice: 20,
      purchaseUrl: '',
      status: 'Purchased',
      purchasedAt: new Date().toISOString()
    }
  ];

  const mockDate = {
    dateId: 'date1',
    userId: 'user1',
    personName: 'John Doe',
    dateType: 'Birthday',
    dateValue: new Date().toISOString(),
    recurrencePattern: 'Annual',
    relationship: 'Friend',
    notes: '',
    isActive: true,
    createdAt: new Date().toISOString()
  };

  test.beforeEach(async ({ page }) => {
    await page.route('**/api/dates/date1/gifts', async route => {
      await route.fulfill({
        status: 200,
        contentType: 'application/json',
        body: JSON.stringify(mockGifts)
      });
    });

    await page.route('**/api/dates/date1', async route => {
      await route.fulfill({
        status: 200,
        contentType: 'application/json',
        body: JSON.stringify(mockDate)
      });
    });

    await page.route('**/api/dates/upcoming', async route => {
      await route.fulfill({
        status: 200,
        contentType: 'application/json',
        body: JSON.stringify([])
      });
    });

    await page.route('**/api/gifts', async route => {
      await route.fulfill({
        status: 200,
        contentType: 'application/json',
        body: JSON.stringify(mockGifts)
      });
    });
  });

  test('should display gift list for a date', async ({ page }) => {
    await page.goto('/gifts/date1');
    await expect(page.locator('h1.gift-list__title')).toContainText('Gifts for John Doe');
  });

  test('should display budget tracker', async ({ page }) => {
    await page.goto('/gifts/date1');
    await expect(page.getByText('Budget Tracker')).toBeVisible();
    await expect(page.getByText('$225.00')).toBeVisible();
  });

  test('should display gift items', async ({ page }) => {
    await page.goto('/gifts/date1');
    await expect(page.getByText('Watch')).toBeVisible();
    await expect(page.getByText('Book')).toBeVisible();
  });

  test('should show gift status chips', async ({ page }) => {
    await page.goto('/gifts/date1');
    await expect(page.locator('mat-chip').filter({ hasText: 'Idea' })).toBeVisible();
    await expect(page.locator('mat-chip').filter({ hasText: 'Purchased' })).toBeVisible();
  });

  test('should have Add Gift Idea button', async ({ page }) => {
    await page.goto('/gifts/date1');
    await expect(page.getByRole('button', { name: /Add Gift Idea/i })).toBeVisible();
  });

  test('should show empty state when no gifts', async ({ page }) => {
    await page.route('**/api/dates/date1/gifts', async route => {
      await route.fulfill({
        status: 200,
        contentType: 'application/json',
        body: JSON.stringify([])
      });
    });

    await page.goto('/gifts/date1');
    await expect(page.locator('.gift-list__empty')).toBeVisible();
    await expect(page.locator('.gift-list__empty h2')).toContainText('No gift ideas yet');
  });
});
