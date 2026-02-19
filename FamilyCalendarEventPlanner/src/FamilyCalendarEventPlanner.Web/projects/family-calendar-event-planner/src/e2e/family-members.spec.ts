import { test, expect, setupMockAPI, mockMembers } from './fixtures';

test.describe('Family Members Page', () => {
  test.beforeEach(async ({ page }) => {
    await setupMockAPI(page);
    await page.goto('/family-members');
  });

  test('should display family members page with header', async ({ page }) => {
    await expect(page.locator('app-header')).toBeVisible();
  });

  test('should display page title', async ({ page }) => {
    await expect(page.locator('text=Family Members')).toBeVisible();
  });

  test('should display page description', async ({ page }) => {
    await expect(page.locator('text=Add family members, assign colors, and manage permissions')).toBeVisible();
  });

  test('should display stats cards', async ({ page }) => {
    await expect(page.locator('text=Active Members')).toBeVisible();
    await expect(page.locator('text=Admins')).toBeVisible();
    await expect(page.locator('text=Pending Invitations')).toBeVisible();
  });

  test('should display correct number of active members', async ({ page }) => {
    await expect(page.locator('.family-members__stat-number').first()).toContainText('4');
  });

  test('should display all family member cards', async ({ page }) => {
    for (const member of mockMembers) {
      await expect(page.locator(`app-member-card:has-text("${member.name}")`)).toBeVisible();
    }
  });

  test('should display member roles', async ({ page }) => {
    await expect(page.locator('text=Admin')).toBeVisible();
    await expect(page.locator('text=Member')).toBeVisible();
  });

  test('should display member emails', async ({ page }) => {
    for (const member of mockMembers) {
      await expect(page.locator(`text=${member.email}`)).toBeVisible();
    }
  });

  test('should have Edit button for each member', async ({ page }) => {
    const editButtons = page.locator('button:has-text("Edit")');
    expect(await editButtons.count()).toBe(mockMembers.length);
  });

  test('should have View Schedule button for each member', async ({ page }) => {
    const viewButtons = page.locator('button:has-text("View Schedule")');
    expect(await viewButtons.count()).toBe(mockMembers.length);
  });

  test('should display add member card', async ({ page }) => {
    await expect(page.locator('text=Add New Family Member')).toBeVisible();
  });

  test('should have Invite Member button', async ({ page }) => {
    await expect(page.locator('button:has-text("Invite Member")')).toBeVisible();
  });

  test('should navigate to Calendar page', async ({ page }) => {
    await page.click('a:has-text("Calendar")');
    await expect(page).toHaveURL('/calendar');
  });

  test('should navigate to Dashboard page', async ({ page }) => {
    await page.click('a:has-text("Dashboard")');
    await expect(page).toHaveURL('/');
  });
});
