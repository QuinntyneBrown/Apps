import { test, expect } from '@playwright/test';
import { NavShellPage } from './pages/nav-shell.page';

test.describe('Navigation', () => {
  let navShell: NavShellPage;

  test.beforeEach(async ({ page }) => {
    navShell = new NavShellPage(page);
    await page.goto('/');
  });

  test('should redirect root to dashboard', async ({ page }) => {
    await expect(page).toHaveURL(/\/dashboard/);
  });

  test('should display the brand name', async () => {
    await expect(navShell.brandName).toHaveText('ConvoStarter');
  });

  test('should display mobile tab bar', async ({ page }) => {
    await page.setViewportSize({ width: 390, height: 844 });
    await expect(navShell.tabbar).toBeVisible();
  });

  test('should display sidebar on desktop', async ({ page }) => {
    await page.setViewportSize({ width: 1440, height: 900 });
    await expect(navShell.sidebar).toBeVisible();
  });

  test('should hide sidebar on mobile', async ({ page }) => {
    await page.setViewportSize({ width: 390, height: 844 });
    await expect(navShell.sidebar).toBeHidden();
  });

  test('should navigate to prompts via tab bar', async ({ page }) => {
    await page.setViewportSize({ width: 390, height: 844 });
    await navShell.navigateViaTabPrompts();
    await expect(page).toHaveURL(/\/prompts/);
  });

  test('should navigate to collections via tab bar', async ({ page }) => {
    await page.setViewportSize({ width: 390, height: 844 });
    await navShell.navigateViaTabCollections();
    await expect(page).toHaveURL(/\/collections/);
  });

  test('should navigate to progress via tab bar', async ({ page }) => {
    await page.setViewportSize({ width: 390, height: 844 });
    await navShell.navigateViaTabProgress();
    await expect(page).toHaveURL(/\/progress/);
  });

  test('should navigate to dashboard via tab bar', async ({ page }) => {
    await page.setViewportSize({ width: 390, height: 844 });
    await navShell.navigateViaTabPrompts();
    await navShell.navigateViaTabHome();
    await expect(page).toHaveURL(/\/dashboard/);
  });

  test('should navigate via sidebar links on desktop', async ({ page }) => {
    await page.setViewportSize({ width: 1440, height: 900 });
    await navShell.navigateToPrompts();
    await expect(page).toHaveURL(/\/prompts/);
    await navShell.navigateToCollections();
    await expect(page).toHaveURL(/\/collections/);
    await navShell.navigateToProgress();
    await expect(page).toHaveURL(/\/progress/);
    await navShell.navigateToDashboard();
    await expect(page).toHaveURL(/\/dashboard/);
  });

  test('should highlight active tab on mobile', async ({ page }) => {
    await page.setViewportSize({ width: 390, height: 844 });
    const homeTab = navShell.tabHome;
    await expect(homeTab).toHaveClass(/nav-shell__tab--active/);
  });

  test('should highlight active sidebar link on desktop', async ({ page }) => {
    await page.setViewportSize({ width: 1440, height: 900 });
    await navShell.navigateToPrompts();
    const promptsLink = navShell.sidebarPromptsLink;
    await expect(promptsLink).toHaveClass(/nav-shell__link--active/);
  });

  test('should show all four tabs in mobile tab bar', async ({ page }) => {
    await page.setViewportSize({ width: 390, height: 844 });
    await expect(navShell.tabHome).toBeVisible();
    await expect(navShell.tabPrompts).toBeVisible();
    await expect(navShell.tabCollections).toBeVisible();
    await expect(navShell.tabProgress).toBeVisible();
  });

  test('should show all five sidebar links on desktop', async ({ page }) => {
    await page.setViewportSize({ width: 1440, height: 900 });
    await expect(navShell.sidebarDashboardLink).toBeVisible();
    await expect(navShell.sidebarPromptsLink).toBeVisible();
    await expect(navShell.sidebarCollectionsLink).toBeVisible();
    await expect(navShell.sidebarProgressLink).toBeVisible();
    await expect(navShell.sidebarSettingsLink).toBeVisible();
  });
});
