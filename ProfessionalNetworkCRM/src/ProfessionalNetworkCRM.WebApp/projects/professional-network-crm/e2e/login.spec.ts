import { test, expect } from '@playwright/test';

test.describe('Login Flow', () => {
  test.beforeEach(async ({ page }) => {
    // Mock the authentication endpoint
    await page.route('**/api/auth/login', async (route) => {
      // Create a mock JWT token
      const header = btoa(JSON.stringify({ alg: 'HS256', typ: 'JWT' }));
      const payload = btoa(JSON.stringify({ 
        sub: 'test-user-id',
        name: 'Test User',
        exp: Math.floor(Date.now() / 1000) + 3600
      }));
      const signature = 'mock-signature';
      const token = `${header}.${payload}.${signature}`;
      
      await route.fulfill({
        status: 200,
        contentType: 'application/json',
        body: JSON.stringify({
          token: token,
          expiresAt: new Date(Date.now() + 3600000).toISOString(),
          user: {
            userId: 'test-user-id',
            userName: 'Test User',
            email: 'test@example.com',
            roles: []
          }
        })
      });
    });

    // Mock other required endpoints
    await page.route('**/api/contacts', async (route) => {
      await route.fulfill({
        status: 200,
        contentType: 'application/json',
        body: JSON.stringify([])
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

  test('should display login form', async ({ page }) => {
    await page.goto('/login');
    
    await expect(page.locator('mat-card-title')).toContainText('Professional Network CRM');
    await expect(page.locator('input[formcontrolname="username"]')).toBeVisible();
    await expect(page.locator('input[formcontrolname="password"]')).toBeVisible();
    await expect(page.locator('button[type="submit"]')).toBeVisible();
  });

  test('should login successfully with valid credentials', async ({ page }) => {
    await page.goto('/login');
    
    await page.fill('input[formcontrolname="username"]', 'testuser');
    await page.fill('input[formcontrolname="password"]', 'password123');
    await page.click('button[type="submit"]');
    
    // Wait for navigation
    await page.waitForURL('/');
    
    // Should redirect to dashboard
    await expect(page).toHaveURL('/');
    await expect(page.locator('h1')).toContainText('Dashboard');
  });

  test('should show validation errors for empty fields', async ({ page }) => {
    await page.goto('/login');
    
    // Try to submit empty form
    await page.click('input[formcontrolname="username"]');
    await page.click('input[formcontrolname="password"]');
    await page.click('mat-card-title'); // Click outside to trigger validation
    
    // Should show validation errors
    await expect(page.locator('mat-error')).toHaveCount(2);
  });

  test('should toggle password visibility', async ({ page }) => {
    await page.goto('/login');
    
    const passwordInput = page.locator('input[formcontrolname="password"]');
    
    // Initially should be password type
    await expect(passwordInput).toHaveAttribute('type', 'password');
    
    // Click visibility toggle
    await page.click('button[matsuffix]');
    
    // Should change to text type
    await expect(passwordInput).toHaveAttribute('type', 'text');
  });
});
