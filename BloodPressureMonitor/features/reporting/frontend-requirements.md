# Frontend Requirements - Reporting

## UI Components

### 1. Reports Library

**Display:**
- List of generated reports
- Filter by type (Weekly, Monthly, Doctor, Custom)
- Sort by date
- Each report card shows:
  - Report type icon
  - Report period
  - Generated date
  - Download/View button
  - Share button
  - Delete button

### 2. Generate Report Interface

**Form:**
- Report type selection:
  - Weekly Summary
  - Monthly Progress
  - Doctor Report (Comprehensive)
  - Custom Date Range
- Date range picker (if custom)
- Include options (checkboxes):
  - All readings (table format)
  - Charts and graphs
  - Medication details
  - Lifestyle factors
  - Alerts history
  - Personal notes
- Format selection:
  - PDF (recommended)
  - Excel (data export)
  - Print-friendly HTML
- "Generate Report" button

### 3. Report Preview

**Before Download:**
- Thumbnail preview of first page
- Page count
- File size
- Summary of contents
- "Download" and "Email" buttons
- "Share Link" (temporary, expires in 7 days)

### 4. Weekly Report Email

**HTML Email Template:**
- Header: "Your Weekly BP Summary"
- Summary stats cards:
  - Average BP (large, color-coded)
  - Readings this week
  - Goal achievement %
- Mini chart image (embedded)
- Quick insights (2-3 bullets)
- "View Full Report" CTA button
- Footer with disclaimer

### 5. Doctor Report (PDF Layout)

**Page 1 - Cover:**
- Report title: "Blood Pressure Report"
- Patient name
- Report period
- Generated date
- Practice/doctor name (if configured)

**Page 2 - Executive Summary:**
- Overall averages
- Trend summary
- Key findings
- Alerts summary

**Page 3+ - Detailed Data:**
- Complete readings table
- Charts (BP over time, by time of day)
- Medication list and adherence
- Lifestyle factors
- Statistical analysis

**Last Page:**
- Notes section (for doctor to write)
- Signature line
- Disclaimer

### 6. Report Sharing

**Share Options:**
- Email to doctor (enter email)
- Download to device
- Print
- Generate shareable link (password-protected)
- Export to Apple Health / Google Fit

## User Flows

### Flow 1: Generate Weekly Report
1. User clicks "Reports" in menu
2. Reports library loads
3. User clicks "Generate New Report"
4. Form appears
5. User selects "Weekly Summary"
6. Auto-selects last 7 days
7. User clicks "Generate"
8. Processing indicator appears
9. Report generates (2-3 seconds)
10. Preview appears
11. User clicks "Download"
12. PDF downloads
13. Report saved to library

### Flow 2: Prepare Doctor Report
1. User has doctor appointment tomorrow
2. User navigates to Reports
3. User clicks "Generate Doctor Report"
4. User selects date range (last 3 months)
5. User checks all include options
6. User clicks "Generate"
7. Report generates (10-15 seconds for large dataset)
8. Preview shows comprehensive report
9. User clicks "Email to Doctor"
10. Email form appears
11. User enters doctor's email
12. Optional message: "See attached BP report for tomorrow's appointment"
13. User sends
14. Confirmation: "Report sent successfully"

### Flow 3: Review Monthly Progress
1. User receives monthly report email (auto-generated)
2. Email shows summary stats
3. User clicks "View Full Report"
4. App opens to monthly report
5. User views improvement vs last month (-3/-2 mmHg)
6. User views achievements (30-day streak!)
7. User reads recommendations
8. User shares to social media ("I lowered my BP this month!")

## Responsive Design

- Mobile: Simplified report preview, full PDF view
- Tablet: Side-by-side preview and options
- Desktop: Full preview pane with metadata sidebar

## Accessibility

- PDF reports must be screen-reader accessible
- Alt text for all charts in HTML reports
- High contrast mode for PDF generation
- Large print option

## Performance

- Generate reports asynchronously (background job)
- Cache generated reports (30 days)
- Compress PDFs for faster download
- Lazy load report thumbnails in library

## Analytics Events

- `report_generated`
- `report_downloaded`
- `report_emailed`
- `report_shared`
- `report_previewed`
