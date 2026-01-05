import { test, expect } from '@playwright/test';

test.describe('Dashboard Display', () => {
  test.beforeEach(async ({ page }) => {
    // Set up authentication in localStorage
    await page.addInitScript(() => {
      const header = btoa(JSON.stringify({ alg: 'HS256', typ: 'JWT' }));
      const payload = btoa(JSON.stringify({ 
        sub: 'test-user-id',
        name: 'Test User',
        exp: Math.floor(Date.now() / 1000) + 3600
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
          { contactId: '2', fullName: 'Jane Smith', email: 'jane@example.com', isPriority: true },
          { contactId: '3', fullName: 'Bob Johnson', email: 'bob@example.com', isPriority: true }
        ])
      });
    });

    await page.route('**/api/follow-ups', async (route) => {
      await route.fulfill({
        status: 200,
        contentType: 'application/json',
        body: JSON.stringify([
          { followUpId: '1', contactId: '1', notes: 'Follow up call', isCompleted: false, dueDate: new Date().toISOString() },
          { followUpId: '2', contactId: '2', notes: 'Send email', isCompleted: false, dueDate: new Date().toISOString() },
          { followUpId: '3', contactId: '3', notes: 'Completed task', isCompleted: true, dueDate: new Date().toISOString() }
        ])
      });
    });

    await page.route('**/api/interactions', async (route) => {
      await route.fulfill({
        status: 200,
        contentType: 'application/json',
        body: JSON.stringify([
          { interactionId: '1', contactId: '1', notes: 'Had a meeting', date: new Date().toISOString() },
          { interactionId: '2', contactId: '2', notes: 'Phone call', date: new Date().toISOString() },
          { interactionId: '3', contactId: '3', notes: 'Email exchange', date: new Date().toISOString() }
        ])
      });
    });

    await page.goto('/');
  });

  test('should display dashboard title', async ({ page }) => {
    await expect(page.locator('h1')).toContainText('Dashboard');
  });

  test('should display contacts card with count', async ({ page }) => {
    const contactsCard = page.locator('mat-card:has-text("Contacts")');
    await expect(contactsCard).toBeVisible();
    await expect(contactsCard.locator('.dashboard__card-count')).toContainText('3');
  });

  test('should display follow-ups card with pending count', async ({ page }) => {
    const followUpsCard = page.locator('mat-card:has-text("Follow-ups")');
    await expect(followUpsCard).toBeVisible();
    await expect(followUpsCard.locator('.dashboard__card-count')).toContainText('2');
  });

  test('should display interactions card with count', async ({ page }) => {
    const interactionsCard = page.locator('mat-card:has-text("Interactions")');
    await expect(interactionsCard).toBeVisible();
    await expect(interactionsCard.locator('.dashboard__card-count')).toContainText('3');
  });

  test('should display priority contacts card with count', async ({ page }) => {
    const priorityCard = page.locator('mat-card:has-text("Priority Contacts")');
    await expect(priorityCard).toBeVisible();
    await expect(priorityCard.locator('.dashboard__card-count')).toContainText('2');
  });

  test('should have working navigation buttons', async ({ page }) => {
    // Test "View All" button on contacts card
    await page.click('mat-card:has-text("Contacts") button:has-text("View All")');
    await expect(page).toHaveURL('/contacts');
    
    // Navigate back to dashboard
    await page.goto('/');
    
    // Test "Add Contact" button
    await page.click('mat-card:has-text("Contacts") button:has-text("Add Contact")');
    await expect(page).toHaveURL('/contacts/new');
  });

  test('should display all dashboard cards', async ({ page }) => {
    const cards = page.locator('mat-card');
    await expect(cards).toHaveCount(4);
  });
});
