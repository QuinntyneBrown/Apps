<div align="center">
  <img src="../assets/logo.svg" alt="Professional Network CRM Logo" width="120" height="120">
  <h1>Professional Network CRM</h1>
  <p><strong>Build, maintain, and leverage your professional network through systematic relationship management</strong></p>
</div>

---

## Overview

Professional Network CRM is a comprehensive networking and relationship management application that helps professionals systematically build and nurture their network. Track interactions, schedule follow-ups, analyze relationship strength, and identify opportunities arising from your professional connections.

## Key Features

### Contact Management
- Comprehensive contact profiles with professional details
- Relationship type classification (mentor, colleague, client, prospect, vendor)
- Multi-tag categorization and advanced search
- Connection source tracking (where and when you met)
- Mutual connection mapping

### Interaction Tracking
- Log all communication types (meetings, calls, emails, messages, events)
- Complete interaction history timeline per contact
- Sentiment and quality tracking
- Conversation topics and notes
- Frequency and pattern analysis

### Follow-Up Management
- Scheduled reminders with priority levels
- Automatic follow-up suggestions based on interaction patterns
- Track completed, pending, and overdue follow-ups
- Generate follow-up tasks from meeting notes
- Batch creation after networking events

### Relationship Intelligence
- Automated relationship strength scoring
- Strong tie and weak tie identification
- Dormant relationship detection and alerts
- Trend tracking over time
- Re-engagement strategy recommendations

### Networking Events
- Event attendance tracking (conferences, meetups, industry events)
- Contact-to-event association
- Post-event follow-up automation
- ROI calculation per event
- Personalized talking points generation

### Opportunity Tracking
- Business and career opportunity logging
- Opportunity attribution to specific contacts
- Introduction and referral tracking
- Value exchange monitoring (give-and-take balance)
- Gratitude and thank you note management

### Network Analytics
- Dashboard with networking metrics
- Relationship strength visualization
- Network health reports
- Networking goal tracking and quotas
- Activity pattern analysis

## Tech Stack

- **Database**: SQL Server
- **Backend**: ASP.NET Core Web API
- **Frontend**: Angular 21+
- **Authentication**: ASP.NET Core Identity
- **Testing**: Vitest, Playwright
- **UI Framework**: Angular Material

## Multi-Tenancy

This application supports multi-tenant architecture with complete data isolation:

- **TenantId** property on all aggregate entities
- **Automatic query filtering** by tenant
- **JWT/Header-based** tenant identification
- **Row-level security** for data isolation

## Getting Started

### Prerequisites
- .NET 8 SDK
- Node.js 18+
- SQL Server
- Angular CLI

### Installation

1. Clone the repository
   ```bash
   git clone <repository-url>
   cd ProfessionalNetworkCRM
   ```

2. Set up SQL Server database
   ```bash
   # Update connection string in src/ProfessionalNetworkCRM.Api/appsettings.json
   ```

3. Run database migrations
   ```bash
   cd src/ProfessionalNetworkCRM.Api
   dotnet ef database update
   ```

4. Install frontend dependencies
   ```bash
   cd src/ProfessionalNetworkCRM.WebApp
   npm install
   ```

5. Start the API
   ```bash
   cd src/ProfessionalNetworkCRM.Api
   dotnet run
   ```

6. Start the frontend
   ```bash
   cd src/ProfessionalNetworkCRM.WebApp
   npm start
   ```

7. Navigate to `http://localhost:4200`

## Documentation

- [Requirements](requirements.md) - Detailed feature requirements and user personas
- [Domain Events](domain-events.md) - Domain event specifications
- [Definition of Done](definition-of-done.md) - Quality standards

## Architecture

The application follows Clean Architecture principles with:

- **Core Layer**: Domain entities, value objects, and business logic
- **Infrastructure Layer**: Data access, external integrations
- **API Layer**: RESTful endpoints, authentication, authorization
- **Web Layer**: Angular SPA with Material Design

## Testing

```bash
# Run unit tests
cd src/ProfessionalNetworkCRM.WebApp
npm test

# Run tests with coverage
npm run test:coverage

# Run E2E tests
npm run e2e
```

## Contributing

Contributions are welcome! Please ensure all tests pass and follow the existing code style.

## License

This project is licensed under the MIT License.
