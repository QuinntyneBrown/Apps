import { test, expect } from '@playwright/test';

test.describe('Login Page', () => {
  test.beforeEach(async ({ page }) => {
    // Mock the auth API
    await page.route('**/api/auth/login', async (route) => {
      const request = route.request();
      const postData = request.postDataJSON();

      if (postData.username === 'testuser' && postData.password === 'password123') {
        await route.fulfill({
          status: 200,
          contentType: 'application/json',
          body: JSON.stringify({
            token: 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwidXNlcl9pZCI6InRlc3QtdXNlci1pZCIsImV4cCI6OTk5OTk5OTk5OX0.test',
            expiresAt: new Date(Date.now() + 86400000).toISOString(),
            user: {
              userId: 'test-user-id',
              userName: 'testuser',
              email: 'test@example.com',
              roles: ['Analyst']
            }
          })
        });
      } else {
        await route.fulfill({
          status: 401,
          contentType: 'application/json',
          body: JSON.stringify({ message: 'Invalid credentials' })
        });
      }
    });

    await page.goto('/login');
  });

  test('should display login form', async ({ page }) => {
    await expect(page.getByText('Market Intelligence Tool')).toBeVisible();
    await expect(page.getByLabel('Username')).toBeVisible();
    await expect(page.getByLabel('Password')).toBeVisible();
    await expect(page.getByRole('button', { name: 'Sign In' })).toBeVisible();
  });

  test('should show validation errors for empty form', async ({ page }) => {
    await page.getByRole('button', { name: 'Sign In' }).click();

    await expect(page.getByText('Username is required')).toBeVisible();
    await expect(page.getByText('Password is required')).toBeVisible();
  });

  test('should show error for invalid credentials', async ({ page }) => {
    await page.getByLabel('Username').fill('wronguser');
    await page.getByLabel('Password').fill('wrongpass');
    await page.getByRole('button', { name: 'Sign In' }).click();

    await expect(page.getByText('Invalid credentials')).toBeVisible();
  });

  test('should login successfully with valid credentials', async ({ page }) => {
    await page.getByLabel('Username').fill('testuser');
    await page.getByLabel('Password').fill('password123');
    await page.getByRole('button', { name: 'Sign In' }).click();

    // Should redirect to dashboard
    await expect(page).toHaveURL('/');
    await expect(page.getByText('Dashboard')).toBeVisible();
  });

  test('should toggle password visibility', async ({ page }) => {
    const passwordInput = page.getByLabel('Password');
    await passwordInput.fill('password123');

    // Initially password is hidden
    await expect(passwordInput).toHaveAttribute('type', 'password');

    // Click visibility toggle
    await page.getByRole('button', { name: /visibility/i }).click();

    // Password should now be visible
    await expect(passwordInput).toHaveAttribute('type', 'text');
  });
});
