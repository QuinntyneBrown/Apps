# Networking - Frontend Requirements

## Pages/Views

### 1. Contacts Dashboard (`/networking`)
- Contact cards grid/list view
- Filter by relationship, company, tags
- Search contacts
- Quick actions (email, LinkedIn, request referral)
- Last contacted date tracking

### 2. Add Contact Form (`/networking/contacts/new`)
- Name, title, company
- Contact information (email, phone, LinkedIn)
- Relationship type
- Tags
- Notes

### 3. Contact Detail View (`/networking/contacts/:id`)
- Contact information
- Interaction history timeline
- Referrals requested/received
- Informational interviews conducted
- Notes and tags
- Quick actions

### 4. Referrals Dashboard (`/networking/referrals`)
- Pending referrals
- Received referrals
- Track referral to application
- Success rate analytics

### 5. Informational Interviews (`/networking/informational-interviews`)
- Scheduled interviews
- Completed interviews with notes
- Insights gathered
- Follow-up tracking

## Components

### ContactCard
- Name and title
- Company
- Relationship badge
- Last contacted date
- Quick actions (email, LinkedIn, call)

### ReferralTracker
- Referral status
- Contact who provided referral
- Linked job listing
- Application status
- Timeline

### NetworkingStats
- Total contacts
- Referrals requested vs. received
- Informational interviews conducted
- Network growth over time

## State Management

**networkingSlice:**
```typescript
{
  contacts: Contact[],
  referrals: Referral[],
  informationalInterviews: InformationalInterview[],
  filters: {
    relationship: string[],
    company: string[],
    tags: string[]
  },
  loading: boolean
}
```

## UI/UX Features

- Contact import from LinkedIn
- Reminder to follow up with contacts
- Referral request templates
- Networking activity calendar
- Contact relationship mapping
- Mobile-responsive design
