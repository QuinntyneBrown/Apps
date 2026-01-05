/// <reference types="vitest" />
import { defineConfig } from 'vite';
import angular from '@analogjs/vite-plugin-angular';
import path from 'path';

export default defineConfig({
  plugins: [
    angular({
      tsconfig: path.join(__dirname, 'projects/professional-network-crm/tsconfig.spec.json'),
    }),
  ],
  test: {
    globals: true,
    environment: 'jsdom',
    setupFiles: ['projects/professional-network-crm/src/test-setup.ts'],
    include: ['projects/**/*.spec.ts'],
    exclude: ['projects/**/e2e/**'],
    coverage: {
      provider: 'v8',
      reporter: ['text', 'json', 'html', 'lcov'],
      exclude: [
        'node_modules/',
        'projects/**/test-setup.ts',
        'projects/**/environments/**',
        'projects/**/main.ts',
        'projects/**/*.routes.ts',
        'projects/**/*.config.ts',
        'projects/**/index.ts',
        'projects/**/*.interface.ts',
        'projects/**/*.enum.ts',
        'projects/**/*.html',
        'projects/**/*.scss',
        '**/*.d.ts',
      ],
      thresholds: {
        lines: 80,
        functions: 80,
        branches: 80,
        statements: 80,
      },
    },
  },
});
