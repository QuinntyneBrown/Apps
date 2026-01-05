import { test, expect } from '@playwright/test';

test.describe('Sidebar Navigation', () => {
  test.beforeEach(async ({ page }) => {
    // Set up authentication in localStorage
    await page.addInitScript(() => {
      // Create a mock JWT token that won't expire soon
      const header = btoa(JSON.stringify({ alg: 'HS256', typ: 'JWT' }));
      const payload = btoa(JSON.stringify({ 
        sub: 'test-user-id',
        name: 'Test User',
        exp: Math.floor(Date.now() / 1000) + 3600 // Expires in 1 hour
      }));
      const signature = 'mock-signature';
      const token = `${header}.${payload}.${signature}`;
      
      localStorage.setItem('auth_token', token);
      localStorage.setItem('user_info', JSON.stringify({
        userId: 'test-user-id',
        userName: 'Test User',
        email: 'test@example.com',
        roles: []
      }));
    });

    // Mock API endpoints
    await page.route('**/api/contacts', async (route) => {
      await route.fulfill({
        status: 200,
        contentType: 'application/json',
        body: JSON.stringify([
          { contactId: '1', fullName: 'John Doe', email: 'john@example.com', isPriority: false },
          { contactId: '2', fullName: 'Jane Smith', email: 'jane@example.com', isPriority: true }
        ])
      });
    });

    await page.route('**/api/followups', async (route) => {
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
    
    // Check all menu items are present using more specific locators
    await expect(page.locator('app-sidebar .sidebar__label', { hasText: 'Dashboard' })).toBeVisible();
    await expect(page.locator('app-sidebar .sidebar__label', { hasText: 'Contacts' })).toBeVisible();
    await expect(page.locator('app-sidebar .sidebar__label', { hasText: 'Follow-Ups' })).toBeVisible();
    await expect(page.locator('app-sidebar .sidebar__label', { hasText: 'Events' })).toBeVisible();
    await expect(page.locator('app-sidebar .sidebar__label', { hasText: 'Opportunities' })).toBeVisible();
    await expect(page.locator('app-sidebar .sidebar__label', { hasText: 'Goals' })).toBeVisible();
    await expect(page.locator('app-sidebar .sidebar__label', { hasText: 'Analytics' })).toBeVisible();
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
