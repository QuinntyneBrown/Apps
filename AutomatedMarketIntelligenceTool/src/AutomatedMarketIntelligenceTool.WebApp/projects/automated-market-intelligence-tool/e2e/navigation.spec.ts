import { test, expect, Page } from '@playwright/test';

async function mockAuthenticatedUser(page: Page) {
  // Set auth token in localStorage before navigation
  await page.addInitScript(() => {
    const mockToken = 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwidXNlcl9pZCI6InRlc3QtdXNlci1pZCIsImV4cCI6OTk5OTk5OTk5OX0.test';
    const mockUser = {
      userId: 'test-user-id',
      userName: 'testuser',
      email: 'test@example.com',
      roles: ['Analyst']
    };
    localStorage.setItem('auth_token', mockToken);
    localStorage.setItem('auth_user', JSON.stringify(mockUser));
  });

  // Mock all API endpoints
  await page.route('**/api/competitors', async (route) => {
    await route.fulfill({
      status: 200,
      contentType: 'application/json',
      body: JSON.stringify([
        { competitorId: '1', name: 'TechCorp', industry: 'Software', marketPosition: 0 },
        { competitorId: '2', name: 'InnovateSoft', industry: 'Software', marketPosition: 1 }
      ])
    });
  });

  await page.route('**/api/insights', async (route) => {
    await route.fulfill({
      status: 200,
      contentType: 'application/json',
      body: JSON.stringify([
        { insightId: '1', title: 'Market Opportunity', category: 1, impact: 0, isActionable: true, tags: [] }
      ])
    });
  });

  await page.route('**/api/alerts', async (route) => {
    await route.fulfill({
      status: 200,
      contentType: 'application/json',
      body: JSON.stringify([
        { alertId: '1', name: 'Price Alert', alertType: 0, isActive: true }
      ])
    });
  });
}

test.describe('Navigation', () => {
  test.beforeEach(async ({ page }) => {
    await mockAuthenticatedUser(page);
  });

  test('should display sidebar with navigation items', async ({ page }) => {
    await page.goto('/');

    // Check sidebar sections
    await expect(page.getByText('Market Intel')).toBeVisible();
    await expect(page.getByText('Intelligence Dashboard')).toBeVisible();

    // Check navigation items
    await expect(page.getByRole('link', { name: /Dashboard/i })).toBeVisible();
    await expect(page.getByRole('link', { name: /Competitors/i })).toBeVisible();
    await expect(page.getByRole('link', { name: /Insights/i })).toBeVisible();
    await expect(page.getByRole('link', { name: /Alerts/i })).toBeVisible();
  });

  test('should navigate to competitors page', async ({ page }) => {
    await page.goto('/');
    await page.getByRole('link', { name: /Competitors/i }).click();

    await expect(page).toHaveURL('/competitors');
    await expect(page.getByRole('heading', { name: 'Competitors' })).toBeVisible();
  });

  test('should navigate to insights page', async ({ page }) => {
    await page.goto('/');
    await page.getByRole('link', { name: /Insights/i }).click();

    await expect(page).toHaveURL('/insights');
    await expect(page.getByRole('heading', { name: 'Insights' })).toBeVisible();
  });

  test('should navigate to alerts page', async ({ page }) => {
    await page.goto('/');
    await page.getByRole('link', { name: /Alerts/i }).click();

    await expect(page).toHaveURL('/alerts');
    await expect(page.getByRole('heading', { name: 'Alerts' })).toBeVisible();
  });

  test('should highlight active navigation item', async ({ page }) => {
    await page.goto('/competitors');

    const competitorsLink = page.getByRole('link', { name: /Competitors/i });
    await expect(competitorsLink).toHaveClass(/sidebar__item--active/);
  });

  test('should display header with user menu', async ({ page }) => {
    await page.goto('/');

    await expect(page.getByText('Market Intelligence Tool')).toBeVisible();

    // Click user menu
    await page.locator('button:has(mat-icon:text("account_circle"))').click();

    await expect(page.getByText('testuser')).toBeVisible();
    await expect(page.getByText('test@example.com')).toBeVisible();
    await expect(page.getByRole('menuitem', { name: /Logout/i })).toBeVisible();
  });
});
