import { test, expect } from '@playwright/test';

test.describe('Sidebar Navigation', () => {
  test.beforeEach(async ({ page, context }) => {
    // Set up authentication token
    await context.addCookies([
      {
        name: 'auth_token',
        value: 'mock-jwt-token',
        domain: 'localhost',
        path: '/'
      }
    ]);

    // Mock API endpoints
    await page.route('**/api/contacts', async (route) => {
      await route.fulfill({
        status: 200,
        contentType: 'application/json',
        body: JSON.stringify([
          { contactId: '1', name: 'John Doe', email: 'john@example.com', isPriority: false },
          { contactId: '2', name: 'Jane Smith', email: 'jane@example.com', isPriority: true }
        ])
      });
    });

    await page.route('**/api/follow-ups', async (route) => {
      await route.fulfill({
        status: 200,
        contentType: 'application/json',
        body: JSON.stringify([
          { followUpId: '1', contactId: '1', notes: 'Follow up call', isCompleted: false },
          { followUpId: '2', contactId: '2', notes: 'Send email', isCompleted: false }
        ])
      });
    });

    await page.route('**/api/interactions', async (route) => {
      await route.fulfill({
        status: 200,
        contentType: 'application/json',
        body: JSON.stringify([
          { interactionId: '1', contactId: '1', notes: 'Had a meeting', date: new Date().toISOString() }
        ])
      });
    });

    await page.goto('/');
  });

  test('should display sidebar with all menu items', async ({ page }) => {
    await expect(page.locator('app-sidebar')).toBeVisible();
    
    // Check all menu items are present
    await expect(page.locator('text=Dashboard')).toBeVisible();
    await expect(page.locator('text=Contacts')).toBeVisible();
    await expect(page.locator('text=Follow-Ups')).toBeVisible();
    await expect(page.locator('text=Events')).toBeVisible();
    await expect(page.locator('text=Opportunities')).toBeVisible();
    await expect(page.locator('text=Goals')).toBeVisible();
    await expect(page.locator('text=Analytics')).toBeVisible();
  });

  test('should show counts for Contacts and Follow-Ups', async ({ page }) => {
    // Wait for data to load
    await page.waitForTimeout(1000);
    
    const sidebar = page.locator('app-sidebar');
    
    // Check if counts are displayed
    await expect(sidebar.locator('.sidebar__count').first()).toBeVisible();
  });

  test('should highlight active menu item', async ({ page }) => {
    // Dashboard should be active initially
    await expect(page.locator('.sidebar__item--active').first()).toContainText('Dashboard');
  });

  test('should navigate to Contacts page', async ({ page }) => {
    await page.click('text=Contacts');
    
    await expect(page).toHaveURL('/contacts');
    await expect(page.locator('.sidebar__item--active')).toContainText('Contacts');
  });

  test('should navigate to Follow-Ups page', async ({ page }) => {
    await page.click('text=Follow-Ups');
    
    await expect(page).toHaveURL('/follow-ups');
    await expect(page.locator('.sidebar__item--active')).toContainText('Follow-Ups');
  });

  test('should navigate to Interactions page', async ({ page }) => {
    await page.click('text=Interactions');
    
    await expect(page).toHaveURL('/interactions');
    await expect(page.locator('.sidebar__item--active')).toContainText('Interactions');
  });

  test('should navigate back to Dashboard', async ({ page }) => {
    // Navigate to Contacts first
    await page.click('text=Contacts');
    await expect(page).toHaveURL('/contacts');
    
    // Navigate back to Dashboard
    await page.click('text=Dashboard');
    await expect(page).toHaveURL('/');
    await expect(page.locator('.sidebar__item--active').first()).toContainText('Dashboard');
  });
});
