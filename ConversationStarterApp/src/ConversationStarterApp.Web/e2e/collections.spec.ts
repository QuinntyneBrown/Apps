import { test, expect } from '@playwright/test';
import { CollectionsPage } from './pages/collections.page';

test.describe('Collections', () => {
  let collections: CollectionsPage;

  test.beforeEach(async ({ page }) => {
    collections = new CollectionsPage(page);
    await collections.goto();
  });

  test('should display page header with "Collections" title', async () => {
    await expect(collections.pageHeader).toBeVisible();
    const title = await collections.getHeaderTitle();
    expect(title).toBe('Collections');
  });

  test('should display New Collection action button', async () => {
    const visible = await collections.hasNewCollectionButton();
    expect(visible).toBe(true);
    await expect(collections.pageHeaderAction).toHaveText('New Collection');
  });

  test('should display collection cards', async () => {
    const count = await collections.getCollectionCount();
    expect(count).toBe(4);
  });

  test('should display correct collection titles', async () => {
    const titles = await collections.getCollectionTitles();
    expect(titles).toContain('Date Night Starters');
    expect(titles).toContain('Getting to Know You');
    expect(titles).toContain('Growth Together');
    expect(titles).toContain('Weekend Fun');
  });

  test('should display collection descriptions', async () => {
    const descriptions = await collections.getCollectionDescriptions();
    expect(descriptions.length).toBe(4);
    descriptions.forEach(d => expect(d.trim().length).toBeGreaterThan(0));
  });

  test('should display prompt counts on collection cards', async () => {
    const counts = await collections.getCollectionPromptCounts();
    expect(counts.length).toBe(4);
    counts.forEach(c => expect(c).toMatch(/\d+ prompts/));
  });

  test('should display collections with different colors', async () => {
    const color1 = await collections.getCollectionColor(0);
    const color2 = await collections.getCollectionColor(1);
    expect(color1).toBe('primary');
    expect(color2).toBe('accent');
  });

  test('should display all four collection color variants', async () => {
    const colors: string[] = [];
    for (let i = 0; i < 4; i++) {
      colors.push(await collections.getCollectionColor(i));
    }
    expect(colors).toContain('primary');
    expect(colors).toContain('accent');
    expect(colors).toContain('success');
    expect(colors).toContain('warning');
  });
});
