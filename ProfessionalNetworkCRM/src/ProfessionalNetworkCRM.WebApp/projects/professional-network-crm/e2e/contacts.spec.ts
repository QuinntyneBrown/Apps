import { test, expect } from '@playwright/test';

test.describe('Contacts Page', () => {
  test.beforeEach(async ({ page, context }) => {
    // Set up authentication
    await context.addCookies([
      {
        name: 'auth_token',
        value: 'mock-jwt-token',
        domain: 'localhost',
        path: '/'
      }
    ]);

    // Mock contacts API
    await page.route('**/api/contacts', async (route) => {
      await route.fulfill({
        status: 200,
        contentType: 'application/json',
        body: JSON.stringify([
          { 
            contactId: '1', 
            name: 'John Doe', 
            email: 'john@example.com', 
            company: 'Acme Corp',
            isPriority: false,
            contactType: 'Client'
          },
          { 
            contactId: '2', 
            name: 'Jane Smith', 
            email: 'jane@example.com', 
            company: 'Tech Inc',
            isPriority: true,
            contactType: 'Colleague'
          },
          { 
            contactId: '3', 
            name: 'Bob Johnson', 
            email: 'bob@example.com', 
            company: 'Startup LLC',
            isPriority: false,
            contactType: 'Mentor'
          }
        ])
      });
    });

    await page.route('**/api/follow-ups', async (route) => {
      await route.fulfill({
        status: 200,
        contentType: 'application/json',
        body: JSON.stringify([])
      });
    });

    await page.route('**/api/interactions', async (route) => {
      await route.fulfill({
        status: 200,
        contentType: 'application/json',
        body: JSON.stringify([])
      });
    });
  });

  test('should display contacts list page', async ({ page }) => {
    await page.goto('/contacts');
    
    await expect(page.locator('h1')).toContainText('Contacts');
  });

  test('should display contacts from API', async ({ page }) => {
    await page.goto('/contacts');
    
    // Wait for contacts to load
    await page.waitForTimeout(1000);
    
    // Check if contacts are displayed
    await expect(page.locator('text=John Doe')).toBeVisible();
    await expect(page.locator('text=Jane Smith')).toBeVisible();
    await expect(page.locator('text=Bob Johnson')).toBeVisible();
  });

  test('should navigate to contact form on add contact', async ({ page }) => {
    await page.goto('/contacts');
    
    // Click add contact button
    await page.click('button:has-text("Add Contact")');
    
    await expect(page).toHaveURL('/contacts/new');
  });

  test('should navigate to contact details', async ({ page }) => {
    await page.goto('/contacts');
    
    // Wait for contacts to load
    await page.waitForTimeout(1000);
    
    // Click on a contact (if there's a view button or link)
    const viewButton = page.locator('button:has-text("View")').first();
    if (await viewButton.isVisible()) {
      await viewButton.click();
      await expect(page.url()).toContain('/contacts/');
    }
  });

  test('should show sidebar on contacts page', async ({ page }) => {
    await page.goto('/contacts');
    
    await expect(page.locator('app-sidebar')).toBeVisible();
    await expect(page.locator('.sidebar__item--active')).toContainText('Contacts');
  });
});
