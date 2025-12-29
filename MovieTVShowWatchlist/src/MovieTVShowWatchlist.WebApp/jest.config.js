module.exports = {
  preset: 'jest-preset-angular',
  setupFilesAfterEnv: ['<rootDir>/setup-jest.ts'],
  testPathIgnorePatterns: ['/node_modules/', '/e2e/'],
  moduleNameMapper: {
    '^@app/(.*)$': '<rootDir>/projects/movie-tv-show-watchlist/src/app/$1'
  },
  collectCoverage: true,
  coverageDirectory: 'coverage',
  coverageReporters: ['html', 'text', 'text-summary', 'lcov'],
  coveragePathIgnorePatterns: [
    '/node_modules/',
    '/e2e/',
    '.html$',
    'index.ts$'
  ],
  coverageThreshold: {
    global: {
      branches: 25,
      functions: 70,
      lines: 75,
      statements: 75
    }
  },
  testMatch: ['<rootDir>/projects/**/*.spec.ts'],
  moduleFileExtensions: ['ts', 'html', 'js', 'json', 'mjs'],
  transformIgnorePatterns: [
    'node_modules/(?!.*\\.mjs$)'
  ]
};
