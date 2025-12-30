import { test, expect, setupMockAPI, mockMembers, mockEvents } from './fixtures';

test.describe('Calendar Page', () => {
  test.beforeEach(async ({ page }) => {
    await setupMockAPI(page);
    await page.goto('/calendar');
  });

  test('should display calendar with header', async ({ page }) => {
    await expect(page.locator('app-header')).toBeVisible();
  });

  test('should display month navigation', async ({ page }) => {
    const currentMonth = new Date().toLocaleDateString('en-US', { month: 'long', year: 'numeric' });
    await expect(page.locator('.calendar__month-title')).toContainText(currentMonth);
  });

  test('should have previous and next month buttons', async ({ page }) => {
    await expect(page.locator('button:has(mat-icon:text("chevron_left"))')).toBeVisible();
    await expect(page.locator('button:has(mat-icon:text("chevron_right"))')).toBeVisible();
  });

  test('should navigate to previous month', async ({ page }) => {
    const currentDate = new Date();
    currentDate.setMonth(currentDate.getMonth() - 1);
    const prevMonth = currentDate.toLocaleDateString('en-US', { month: 'long', year: 'numeric' });

    await page.click('button:has(mat-icon:text("chevron_left"))');
    await expect(page.locator('.calendar__month-title')).toContainText(prevMonth);
  });

  test('should navigate to next month', async ({ page }) => {
    const currentDate = new Date();
    currentDate.setMonth(currentDate.getMonth() + 1);
    const nextMonth = currentDate.toLocaleDateString('en-US', { month: 'long', year: 'numeric' });

    await page.click('button:has(mat-icon:text("chevron_right"))');
    await expect(page.locator('.calendar__month-title')).toContainText(nextMonth);
  });

  test('should have Today button', async ({ page }) => {
    await expect(page.locator('button:has-text("Today")')).toBeVisible();
  });

  test('should display view toggle buttons', async ({ page }) => {
    await expect(page.locator('mat-button-toggle:has-text("Day")')).toBeVisible();
    await expect(page.locator('mat-button-toggle:has-text("Week")')).toBeVisible();
    await expect(page.locator('mat-button-toggle:has-text("Month")')).toBeVisible();
  });

  test('should display week day headers', async ({ page }) => {
    const weekDays = ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'];
    for (const day of weekDays) {
      await expect(page.locator(`.calendar__day-header:has-text("${day}")`)).toBeVisible();
    }
  });

  test('should display calendar grid', async ({ page }) => {
    await expect(page.locator('.calendar__grid')).toBeVisible();
    const cells = page.locator('.calendar__day-cell');
    expect(await cells.count()).toBeGreaterThan(28);
  });

  test('should display filter chips for family members', async ({ page }) => {
    for (const member of mockMembers) {
      await expect(page.locator(`mat-chip:has-text("${member.name}")`)).toBeVisible();
    }
  });

  test('should display legend', async ({ page }) => {
    await expect(page.locator('.calendar__legend')).toBeVisible();
    await expect(page.locator('text=Legend:')).toBeVisible();
  });

  test('should highlight today', async ({ page }) => {
    await expect(page.locator('.calendar__day-cell--today')).toBeVisible();
  });

  test('should have Add Event button', async ({ page }) => {
    await expect(page.locator('button:has-text("Add Event")')).toBeVisible();
  });

  test('should open add event dialog when clicking Add Event', async ({ page }) => {
    await page.click('button:has-text("Add Event")');
    await expect(page.locator('text=Create New Event')).toBeVisible();
  });
});
