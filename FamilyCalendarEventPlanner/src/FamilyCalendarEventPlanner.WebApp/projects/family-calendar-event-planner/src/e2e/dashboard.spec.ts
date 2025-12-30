import { test, expect, setupMockAPI, mockMembers, mockEvents } from './fixtures';

test.describe('Dashboard Page', () => {
  test.beforeEach(async ({ page }) => {
    await setupMockAPI(page);
    await page.goto('/');
  });

  test('should display dashboard with header', async ({ page }) => {
    await expect(page.locator('app-header')).toBeVisible();
    await expect(page.locator('text=Family Calendar')).toBeVisible();
  });

  test('should display family members in sidebar', async ({ page }) => {
    for (const member of mockMembers) {
      await expect(page.locator(`text=${member.name}`)).toBeVisible();
    }
  });

  test('should display mini calendar', async ({ page }) => {
    await expect(page.locator('app-mini-calendar')).toBeVisible();
  });

  test('should display stats cards', async ({ page }) => {
    await expect(page.locator('text=Events This Week')).toBeVisible();
    await expect(page.locator('text=Conflicts Detected')).toBeVisible();
    await expect(page.locator('text=Pending RSVPs')).toBeVisible();
  });

  test('should display today\'s date', async ({ page }) => {
    const today = new Date();
    const options: Intl.DateTimeFormatOptions = { weekday: 'long', year: 'numeric', month: 'long', day: 'numeric' };
    const dateText = today.toLocaleDateString('en-US', options);
    await expect(page.locator('.dashboard__current-date')).toContainText(dateText.split(',')[0]);
  });

  test('should have Add Event button', async ({ page }) => {
    await expect(page.locator('button:has-text("Add Event")')).toBeVisible();
  });

  test('should navigate to Calendar page', async ({ page }) => {
    await page.click('a:has-text("Calendar")');
    await expect(page).toHaveURL('/calendar');
  });

  test('should navigate to Family Members page', async ({ page }) => {
    await page.click('a:has-text("Family Members")');
    await expect(page).toHaveURL('/family-members');
  });

  test('should open add event dialog when clicking Add Event', async ({ page }) => {
    await page.click('button:has-text("Add Event")');
    await expect(page.locator('text=Create New Event')).toBeVisible();
  });

  test('should display events for today when there are events', async ({ page }) => {
    await expect(page.locator('.dashboard__events-card')).toBeVisible();
  });
});
