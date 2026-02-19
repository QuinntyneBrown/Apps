import { setupZoneTestEnv } from 'jest-preset-angular/setup-env/zone';
import { randomUUID } from 'crypto';

// Polyfill crypto.randomUUID for Jest environment
if (typeof globalThis.crypto === 'undefined') {
  (globalThis as any).crypto = { randomUUID };
} else if (typeof globalThis.crypto.randomUUID === 'undefined') {
  (globalThis.crypto as any).randomUUID = randomUUID;
}

setupZoneTestEnv();
