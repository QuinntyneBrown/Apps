import { test as base, Page } from '@playwright/test';

export const mockMembers = [
  {
    memberId: 'member1',
    familyId: 'family1',
    name: 'Jennifer Martinez',
    email: 'jennifer@example.com',
    color: '#ef4444',
    role: 'Admin'
  },
  {
    memberId: 'member2',
    familyId: 'family1',
    name: 'John Martinez',
    email: 'john@example.com',
    color: '#3b82f6',
    role: 'Admin'
  },
  {
    memberId: 'member3',
    familyId: 'family1',
    name: 'Emma Martinez',
    email: 'emma@example.com',
    color: '#10b981',
    role: 'Member'
  },
  {
    memberId: 'member4',
    familyId: 'family1',
    name: 'Liam Martinez',
    email: 'liam@example.com',
    color: '#f59e0b',
    role: 'Member'
  }
];

export const mockEvents = [
  {
    eventId: 'event1',
    familyId: 'family1',
    creatorId: 'member1',
    title: 'Soccer Practice',
    description: 'Weekly soccer practice',
    startTime: new Date().toISOString(),
    endTime: new Date(Date.now() + 3600000).toISOString(),
    location: 'Sports Complex',
    eventType: 'Sports',
    recurrencePattern: { frequency: 'Weekly', interval: 1, endDate: null, daysOfWeek: [] },
    status: 'Scheduled'
  },
  {
    eventId: 'event2',
    familyId: 'family1',
    creatorId: 'member2',
    title: 'Family Dinner',
    description: 'Monthly family dinner',
    startTime: new Date().toISOString(),
    endTime: new Date(Date.now() + 7200000).toISOString(),
    location: 'Home',
    eventType: 'FamilyDinner',
    recurrencePattern: { frequency: 'None', interval: 1, endDate: null, daysOfWeek: [] },
    status: 'Scheduled'
  },
  {
    eventId: 'event3',
    familyId: 'family1',
    creatorId: 'member3',
    title: 'School Day',
    description: 'Regular school day',
    startTime: new Date().toISOString(),
    endTime: new Date(Date.now() + 28800000).toISOString(),
    location: 'School',
    eventType: 'School',
    recurrencePattern: { frequency: 'Daily', interval: 1, endDate: null, daysOfWeek: [] },
    status: 'Scheduled'
  }
];

export const mockConflicts = [
  {
    conflictId: 'conflict1',
    conflictingEventIds: ['event1', 'event3'],
    affectedMemberIds: ['member3'],
    conflictSeverity: 'Medium',
    isResolved: false,
    resolvedAt: null
  }
];

export async function setupMockAPI(page: Page) {
  await page.route('**/api/familymembers**', async (route) => {
    await route.fulfill({
      status: 200,
      contentType: 'application/json',
      body: JSON.stringify(mockMembers)
    });
  });

  await page.route('**/api/events**', async (route) => {
    const method = route.request().method();
    if (method === 'GET') {
      await route.fulfill({
        status: 200,
        contentType: 'application/json',
        body: JSON.stringify(mockEvents)
      });
    } else if (method === 'POST') {
      const body = route.request().postDataJSON();
      const newEvent = {
        ...body,
        eventId: 'event-new-' + Date.now(),
        status: 'Scheduled'
      };
      await route.fulfill({
        status: 201,
        contentType: 'application/json',
        body: JSON.stringify(newEvent)
      });
    } else {
      await route.continue();
    }
  });

  await page.route('**/api/conflicts**', async (route) => {
    await route.fulfill({
      status: 200,
      contentType: 'application/json',
      body: JSON.stringify(mockConflicts)
    });
  });

  await page.route('**/api/attendees**', async (route) => {
    await route.fulfill({
      status: 200,
      contentType: 'application/json',
      body: JSON.stringify([])
    });
  });

  await page.route('**/api/availability**', async (route) => {
    await route.fulfill({
      status: 200,
      contentType: 'application/json',
      body: JSON.stringify([])
    });
  });

  await page.route('**/api/reminders**', async (route) => {
    await route.fulfill({
      status: 200,
      contentType: 'application/json',
      body: JSON.stringify([])
    });
  });
}

export const test = base.extend({});
export { expect } from '@playwright/test';
