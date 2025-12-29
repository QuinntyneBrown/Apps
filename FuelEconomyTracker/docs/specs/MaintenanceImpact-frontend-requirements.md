# Maintenance Impact - Frontend Requirements

## Pages

### Maintenance Tracker (`/vehicles/{id}/maintenance`)
- **Upcoming Maintenance**: Due soon based on MPG/mileage
- **Recent Services**: List with MPG impact badges
- **Impact Analysis**: Chart showing MPG before/after services
- **ROI Calculator**: Shows fuel savings from maintenance
- **Add Maintenance Button**

### Add Maintenance Modal
- Service type selector (Oil Change, Tire Rotation, Air Filter, etc.)
- Date and odometer reading
- Cost input
- Notes field
- Auto-calculate pre-service MPG
- Schedule next service reminder

### Maintenance Impact Report
- Service history timeline
- MPG trend line with maintenance markers
- Cost vs savings analysis
- Most impactful services ranking

## UI Components

### MaintenanceImpactCard
- Service name and date
- Cost display
- Before/After MPG comparison
- Impact percentage badge
- ROI indicator

### ServiceRecommendation
- Recommended service alert
- Reason (e.g., "MPG down 12%")
- Expected improvement
- Estimated cost
- Schedule service button
