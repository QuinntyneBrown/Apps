module.exports = {
  preset: 'jest-preset-angular',
  setupFilesAfterEnv: ['<rootDir>/setup-jest.ts'],
  testPathIgnorePatterns: [
    '<rootDir>/node_modules/',
    '<rootDir>/dist/',
    '<rootDir>/projects/gift-idea-tracker/src/e2e/'
  ],
  moduleNameMapper: {
    '^@app/(.*)$': '<rootDir>/projects/gift-idea-tracker/src/app/$1',
    '^@environments/(.*)$': '<rootDir>/projects/gift-idea-tracker/src/environments/$1'
  },
  collectCoverageFrom: [
    'projects/gift-idea-tracker/src/app/**/*.ts',
    '!projects/gift-idea-tracker/src/app/**/*.spec.ts',
    '!projects/gift-idea-tracker/src/app/**/index.ts',
    '!projects/gift-idea-tracker/src/e2e/**/*'
  ],
  coverageDirectory: 'coverage',
  coverageReporters: ['html', 'lcov', 'text-summary'],
  coverageThreshold: {
    global: {
      lines: 80,
      statements: 80,
      branches: 70,
      functions: 80
    }
  },
  testMatch: [
    '**/+(*.)+(spec).+(ts)'
  ],
  transform: {
    '^.+\\.(ts|mjs|js|html)$': [
      'jest-preset-angular',
      {
        tsconfig: '<rootDir>/projects/gift-idea-tracker/tsconfig.spec.json',
        stringifyContentPathRegex: '\\.html$'
      }
    ]
  },
  transformIgnorePatterns: ['node_modules/(?!.*\\.mjs$)']
};
