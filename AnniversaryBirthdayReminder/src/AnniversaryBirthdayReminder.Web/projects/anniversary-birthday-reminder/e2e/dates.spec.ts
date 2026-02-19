import { test, expect } from '@playwright/test';

test.describe('Date Management', () => {
  const mockDates = [
    {
      dateId: '1',
      userId: 'user1',
      personName: 'John Doe',
      dateType: 'Birthday',
      dateValue: new Date(new Date().setDate(new Date().getDate() + 10)).toISOString(),
      recurrencePattern: 'Annual',
      relationship: 'Friend',
      notes: 'Remember to buy cake',
      isActive: true,
      createdAt: new Date().toISOString()
    }
  ];

  test.beforeEach(async ({ page }) => {
    await page.route('**/api/dates', async route => {
      if (route.request().method() === 'GET') {
        await route.fulfill({
          status: 200,
          contentType: 'application/json',
          body: JSON.stringify(mockDates)
        });
      } else if (route.request().method() === 'POST') {
        const body = route.request().postDataJSON();
        await route.fulfill({
          status: 201,
          contentType: 'application/json',
          body: JSON.stringify({
            ...body,
            dateId: '2',
            userId: 'user1',
            createdAt: new Date().toISOString()
          })
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

  test('should display date list page', async ({ page }) => {
    await page.goto('/dates');
    await expect(page.locator('h1.date-list__title')).toContainText('Important Dates');
  });

  test('should display dates in table', async ({ page }) => {
    await page.goto('/dates');
    await expect(page.locator('table')).toBeVisible();
    await expect(page.getByText('John Doe')).toBeVisible();
  });

  test('should have Add Date button', async ({ page }) => {
    await page.goto('/dates');
    await expect(page.getByRole('link', { name: /Add Date/i })).toBeVisible();
  });

  test('should navigate to add date form', async ({ page }) => {
    await page.goto('/dates');
    await page.click('a[href="/dates/new"]');
    await expect(page).toHaveURL('/dates/new');
  });

  test('should display add date form', async ({ page }) => {
    await page.goto('/dates/new');
    await expect(page.locator('mat-card-title')).toContainText('Add New Date');
    await expect(page.locator('input[formControlName="personName"]')).toBeVisible();
  });

  test('should show validation error for empty person name', async ({ page }) => {
    await page.goto('/dates/new');
    await page.click('input[formControlName="personName"]');
    await page.click('input[formControlName="relationship"]');
    await expect(page.locator('mat-error')).toContainText('Person name is required');
  });

  test('should show empty state when no dates', async ({ page }) => {
    await page.route('**/api/dates', async route => {
      await route.fulfill({
        status: 200,
        contentType: 'application/json',
        body: JSON.stringify([])
      });
    });

    await page.goto('/dates');
    await expect(page.locator('.date-list__empty')).toBeVisible();
    await expect(page.locator('.date-list__empty h2')).toContainText('No dates added yet');
  });
});
