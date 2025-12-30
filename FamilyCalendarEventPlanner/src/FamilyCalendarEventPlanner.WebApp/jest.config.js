module.exports = {
  preset: 'jest-preset-angular',
  setupFilesAfterEnv: ['<rootDir>/setup-jest.ts'],
  testPathIgnorePatterns: [
    '<rootDir>/node_modules/',
    '<rootDir>/dist/',
    '<rootDir>/projects/family-calendar-event-planner/src/e2e/'
  ],
  testMatch: ['**/*.spec.ts'],
  moduleNameMapper: {
    '^@app/(.*)$': '<rootDir>/projects/family-calendar-event-planner/src/app/$1',
    '^@environments/(.*)$': '<rootDir>/projects/family-calendar-event-planner/src/environments/$1'
  },
  coverageDirectory: 'coverage',
  collectCoverageFrom: [
    'projects/family-calendar-event-planner/src/app/**/*.ts',
    '!projects/family-calendar-event-planner/src/app/**/*.spec.ts',
    '!projects/family-calendar-event-planner/src/app/**/*.module.ts',
    '!projects/family-calendar-event-planner/src/app/**/index.ts'
  ],
  transform: {
    '^.+\\.(ts|js|html|svg)$': [
      'jest-preset-angular',
      {
        tsconfig: '<rootDir>/projects/family-calendar-event-planner/tsconfig.spec.json',
        stringifyContentPathRegex: '\\.(html|svg)$'
      }
    ]
  }
};
