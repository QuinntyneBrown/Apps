import { Page } from '@playwright/test';

export const mockRecipients = [
  {
    recipientId: 'recipient-1',
    userId: 'user-1',
    name: 'John Doe',
    relationship: 'Friend',
    createdAt: '2025-01-01T00:00:00Z'
  },
  {
    recipientId: 'recipient-2',
    userId: 'user-1',
    name: 'Jane Smith',
    relationship: 'Family',
    createdAt: '2025-01-02T00:00:00Z'
  }
];

export const mockGiftIdeas = [
  {
    giftIdeaId: 'gift-1',
    userId: 'user-1',
    recipientId: 'recipient-1',
    name: 'New iPhone',
    occasion: 'Birthday',
    estimatedPrice: 999,
    isPurchased: false,
    createdAt: '2025-01-01T00:00:00Z',
    recipient: mockRecipients[0]
  },
  {
    giftIdeaId: 'gift-2',
    userId: 'user-1',
    recipientId: 'recipient-2',
    name: 'Book Collection',
    occasion: 'Christmas',
    estimatedPrice: 50,
    isPurchased: true,
    createdAt: '2025-01-02T00:00:00Z',
    recipient: mockRecipients[1]
  },
  {
    giftIdeaId: 'gift-3',
    userId: 'user-1',
    recipientId: 'recipient-1',
    name: 'Watch',
    occasion: 'Anniversary',
    estimatedPrice: 250,
    isPurchased: false,
    createdAt: '2025-01-03T00:00:00Z',
    recipient: mockRecipients[0]
  }
];

export const mockPurchases = [
  {
    purchaseId: 'purchase-1',
    giftIdeaId: 'gift-2',
    purchaseDate: '2025-01-15T00:00:00Z',
    actualPrice: 45,
    store: 'Amazon',
    createdAt: '2025-01-15T00:00:00Z'
  }
];

export async function setupMockAPI(page: Page) {
  await page.route('**/api/recipients**', async (route) => {
    const method = route.request().method();
    const url = route.request().url();

    if (method === 'GET') {
      if (url.match(/\/api\/recipients\/[^/]+$/)) {
        const recipientId = url.split('/').pop();
        const recipient = mockRecipients.find(r => r.recipientId === recipientId);
        await route.fulfill({
          status: recipient ? 200 : 404,
          contentType: 'application/json',
          body: JSON.stringify(recipient || { error: 'Not found' })
        });
      } else {
        await route.fulfill({
          status: 200,
          contentType: 'application/json',
          body: JSON.stringify(mockRecipients)
        });
      }
    } else if (method === 'POST') {
      const body = route.request().postDataJSON();
      const newRecipient = {
        ...body,
        recipientId: 'recipient-new-' + Date.now(),
        userId: 'user-1',
        createdAt: new Date().toISOString()
      };
      await route.fulfill({
        status: 201,
        contentType: 'application/json',
        body: JSON.stringify(newRecipient)
      });
    } else if (method === 'PUT') {
      const body = route.request().postDataJSON();
      await route.fulfill({
        status: 200,
        contentType: 'application/json',
        body: JSON.stringify({ ...mockRecipients[0], ...body })
      });
    } else if (method === 'DELETE') {
      await route.fulfill({
        status: 204,
        body: ''
      });
    }
  });

  await page.route('**/api/giftideas**', async (route) => {
    const method = route.request().method();
    const url = route.request().url();

    if (method === 'GET') {
      if (url.match(/\/api\/giftideas\/[^/]+$/)) {
        const giftIdeaId = url.split('/').pop();
        const giftIdea = mockGiftIdeas.find(g => g.giftIdeaId === giftIdeaId);
        await route.fulfill({
          status: giftIdea ? 200 : 404,
          contentType: 'application/json',
          body: JSON.stringify(giftIdea || { error: 'Not found' })
        });
      } else {
        const urlObj = new URL(url);
        const recipientId = urlObj.searchParams.get('recipientId');
        let filteredIdeas = mockGiftIdeas;
        if (recipientId) {
          filteredIdeas = mockGiftIdeas.filter(g => g.recipientId === recipientId);
        }
        await route.fulfill({
          status: 200,
          contentType: 'application/json',
          body: JSON.stringify(filteredIdeas)
        });
      }
    } else if (method === 'POST') {
      const urlPath = new URL(url).pathname;
      if (urlPath.includes('/purchase')) {
        await route.fulfill({
          status: 200,
          contentType: 'application/json',
          body: JSON.stringify({ ...mockGiftIdeas[0], isPurchased: true })
        });
      } else {
        const body = route.request().postDataJSON();
        const newGiftIdea = {
          ...body,
          giftIdeaId: 'gift-new-' + Date.now(),
          userId: 'user-1',
          isPurchased: false,
          createdAt: new Date().toISOString()
        };
        await route.fulfill({
          status: 201,
          contentType: 'application/json',
          body: JSON.stringify(newGiftIdea)
        });
      }
    } else if (method === 'PUT') {
      const body = route.request().postDataJSON();
      await route.fulfill({
        status: 200,
        contentType: 'application/json',
        body: JSON.stringify({ ...mockGiftIdeas[0], ...body })
      });
    } else if (method === 'DELETE') {
      await route.fulfill({
        status: 204,
        body: ''
      });
    }
  });

  await page.route('**/api/purchases**', async (route) => {
    const method = route.request().method();
    const url = route.request().url();

    if (method === 'GET') {
      if (url.match(/\/api\/purchases\/[^/]+$/)) {
        const purchaseId = url.split('/').pop();
        const purchase = mockPurchases.find(p => p.purchaseId === purchaseId);
        await route.fulfill({
          status: purchase ? 200 : 404,
          contentType: 'application/json',
          body: JSON.stringify(purchase || { error: 'Not found' })
        });
      } else {
        await route.fulfill({
          status: 200,
          contentType: 'application/json',
          body: JSON.stringify(mockPurchases)
        });
      }
    } else if (method === 'POST') {
      const body = route.request().postDataJSON();
      const newPurchase = {
        ...body,
        purchaseId: 'purchase-new-' + Date.now(),
        createdAt: new Date().toISOString()
      };
      await route.fulfill({
        status: 201,
        contentType: 'application/json',
        body: JSON.stringify(newPurchase)
      });
    } else if (method === 'DELETE') {
      await route.fulfill({
        status: 204,
        body: ''
      });
    }
  });
}
