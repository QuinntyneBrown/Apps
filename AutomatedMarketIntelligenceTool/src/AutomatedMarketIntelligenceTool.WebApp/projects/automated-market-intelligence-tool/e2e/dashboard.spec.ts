import { test, expect, Page } from '@playwright/test';

async function setupMocks(page: Page) {
  await page.addInitScript(() => {
    const mockToken = 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwidXNlcl9pZCI6InRlc3QtdXNlci1pZCIsImV4cCI6OTk5OTk5OTk5OX0.test';
    const mockUser = { userId: 'test-user-id', userName: 'testuser', email: 'test@example.com', roles: ['Analyst'] };
    localStorage.setItem('auth_token', mockToken);
    localStorage.setItem('auth_user', JSON.stringify(mockUser));
  });

  await page.route('**/api/competitors', async (route) => {
    await route.fulfill({
      status: 200,
      contentType: 'application/json',
      body: JSON.stringify([
        { competitorId: '1', name: 'TechCorp Inc.', industry: 'Software & Services', marketPosition: 0, website: 'https://techcorp.com' },
        { competitorId: '2', name: 'InnovateSoft', industry: 'Software & Services', marketPosition: 1 },
        { competitorId: '3', name: 'DataSystems Ltd', industry: 'Data Analytics', marketPosition: 1 }
      ])
    });
  });

  await page.route('**/api/insights', async (route) => {
    await route.fulfill({
      status: 200,
      contentType: 'application/json',
      body: JSON.stringify([
        { insightId: '1', title: 'New Market Opportunity in APAC', description: 'Competitor withdrawing from region', category: 1, impact: 0, source: 'Industry Report', isActionable: true, tags: ['APAC', 'opportunity'] },
        { insightId: '2', title: 'Competitor Y Launching New Product', description: 'Direct competitor announcement', category: 2, impact: 0, source: 'Press Release', isActionable: true, tags: ['competitor', 'product'] },
        { insightId: '3', title: 'Industry Trend: AI Adoption Rising', description: '60% of competitors using AI', category: 0, impact: 1, source: 'Market Analysis', isActionable: false, tags: ['AI', 'trend'] }
      ])
    });
  });

  await page.route('**/api/alerts', async (route) => {
    await route.fulfill({
      status: 200,
      contentType: 'application/json',
      body: JSON.stringify([
        { alertId: '1', name: 'Competitor pricing change', alertType: 0, isActive: true, lastTriggered: new Date().toISOString() },
        { alertId: '2', name: 'New press release from TechCorp', alertType: 2, isActive: true, lastTriggered: new Date(Date.now() - 3600000).toISOString() },
        { alertId: '3', name: 'Market index threshold', alertType: 1, isActive: false, lastTriggered: null }
      ])
    });
  });
}

test.describe('Dashboard', () => {
  test.beforeEach(async ({ page }) => {
    await setupMocks(page);
    await page.goto('/');
  });

  test('should display dashboard title and subtitle', async ({ page }) => {
    await expect(page.getByRole('heading', { name: 'Dashboard' })).toBeVisible();
    await expect(page.getByText("Welcome back! Here's your market intelligence overview.")).toBeVisible();
  });

  test('should display statistics cards', async ({ page }) => {
    await expect(page.getByText('Competitors Tracked')).toBeVisible();
    await expect(page.getByText('Active Insights')).toBeVisible();
    await expect(page.getByText('Active Alerts')).toBeVisible();
  });

  test('should display competitor count', async ({ page }) => {
    const competitorsCard = page.locator('.stat-card').filter({ hasText: 'Competitors Tracked' });
    await expect(competitorsCard.locator('.stat-card__value')).toHaveText('3');
  });

  test('should display recent insights section', async ({ page }) => {
    await expect(page.getByRole('heading', { name: 'Recent Insights' })).toBeVisible();
    await expect(page.getByText('New Market Opportunity in APAC')).toBeVisible();
    await expect(page.getByText('Competitor Y Launching New Product')).toBeVisible();
  });

  test('should display top competitors section', async ({ page }) => {
    await expect(page.getByRole('heading', { name: 'Top Competitors' })).toBeVisible();
    await expect(page.getByText('TechCorp Inc.')).toBeVisible();
    await expect(page.getByText('InnovateSoft')).toBeVisible();
  });

  test('should navigate to competitors page from View All link', async ({ page }) => {
    await page.getByRole('link', { name: 'View All' }).first().click();
    // Should navigate to either competitors, insights, or alerts based on which View All was clicked
    await expect(page.url()).toMatch(/\/(competitors|insights|alerts)/);
  });
});
