import { test, expect, setupMockAPI, mockMembers } from './fixtures';

test.describe('Event Dialog', () => {
  test.beforeEach(async ({ page }) => {
    await setupMockAPI(page);
    await page.goto('/');
    await page.click('button:has-text("Add Event")');
  });

  test('should open dialog with create title', async ({ page }) => {
    await expect(page.locator('text=Create New Event')).toBeVisible();
  });

  test('should have event title input', async ({ page }) => {
    await expect(page.locator('input[formControlName="title"]')).toBeVisible();
  });

  test('should have event type select', async ({ page }) => {
    await expect(page.locator('mat-select[formControlName="eventType"]')).toBeVisible();
  });

  test('should have start date picker', async ({ page }) => {
    await expect(page.locator('input[formControlName="startDate"]')).toBeVisible();
  });

  test('should have end date picker', async ({ page }) => {
    await expect(page.locator('input[formControlName="endDate"]')).toBeVisible();
  });

  test('should have start time input', async ({ page }) => {
    await expect(page.locator('input[formControlName="startTime"]')).toBeVisible();
  });

  test('should have end time input', async ({ page }) => {
    await expect(page.locator('input[formControlName="endTime"]')).toBeVisible();
  });

  test('should have location input', async ({ page }) => {
    await expect(page.locator('input[formControlName="location"]')).toBeVisible();
  });

  test('should have description textarea', async ({ page }) => {
    await expect(page.locator('textarea[formControlName="description"]')).toBeVisible();
  });

  test('should display attendees section', async ({ page }) => {
    await expect(page.locator('text=Attendees')).toBeVisible();
  });

  test('should display all family members as attendee options', async ({ page }) => {
    for (const member of mockMembers) {
      await expect(page.locator(`.event-dialog__attendee:has-text("${member.name}")`)).toBeVisible();
    }
  });

  test('should have recurring event checkbox', async ({ page }) => {
    await expect(page.locator('mat-checkbox:has-text("Recurring Event")')).toBeVisible();
  });

  test('should have Cancel button', async ({ page }) => {
    await expect(page.locator('button:has-text("Cancel")')).toBeVisible();
  });

  test('should have Create Event button', async ({ page }) => {
    await expect(page.locator('button:has-text("Create Event")')).toBeVisible();
  });

  test('should close dialog when clicking Cancel', async ({ page }) => {
    await page.click('button:has-text("Cancel")');
    await expect(page.locator('text=Create New Event')).not.toBeVisible();
  });

  test('should disable Create Event button when title is empty', async ({ page }) => {
    const createButton = page.locator('button:has-text("Create Event")');
    await expect(createButton).toBeDisabled();
  });

  test('should enable Create Event button when title is filled', async ({ page }) => {
    await page.fill('input[formControlName="title"]', 'Test Event');
    const createButton = page.locator('button:has-text("Create Event")');
    await expect(createButton).toBeEnabled();
  });

  test('should show recurring options when checkbox is checked', async ({ page }) => {
    await page.click('mat-checkbox:has-text("Recurring Event")');
    await expect(page.locator('mat-select[formControlName="recurrenceFrequency"]')).toBeVisible();
  });

  test('should toggle attendee selection', async ({ page }) => {
    const firstAttendee = page.locator('.event-dialog__attendee').first();
    await firstAttendee.click();
    await expect(firstAttendee).toHaveClass(/event-dialog__attendee--selected/);
    await firstAttendee.click();
    await expect(firstAttendee).not.toHaveClass(/event-dialog__attendee--selected/);
  });

  test('should submit form with valid data', async ({ page }) => {
    await page.fill('input[formControlName="title"]', 'Test Event');
    await page.fill('input[formControlName="location"]', 'Test Location');
    await page.fill('textarea[formControlName="description"]', 'Test Description');

    await page.click('button:has-text("Create Event")');
    await expect(page.locator('text=Create New Event')).not.toBeVisible();
  });
});
