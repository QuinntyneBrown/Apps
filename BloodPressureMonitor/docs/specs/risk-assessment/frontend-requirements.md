# Frontend Requirements - Risk Assessment

## UI Components

### 1. Risk Dashboard Card

**Location:** Main dashboard, prominent position

**Display:**
- Risk level badge (color-coded)
  - Low: Green
  - Moderate: Yellow
  - High: Orange
  - Very High: Red
- Risk score gauge (0-10)
- Brief summary: "Your cardiovascular risk is moderate"
- "Learn More" button
- Last assessed date

### 2. Cardiovascular Risk Detail

**Sections:**

**Risk Level Overview:**
- Large risk level badge
- Risk score (numeric and visual gauge)
- Trend arrow (increasing/decreasing/stable)
- Comparison to previous assessment

**Contributing Factors:**
- List of factors with impact levels:
  - Elevated BP (High impact) ⚠️
  - Uncontrolled for 60 days (Medium impact) ⚠️
  - Medication adherence 75% (Low impact) ℹ️
- Each factor expandable for explanation

**Risk Breakdown Chart:**
- Pie chart or stacked bar showing factor contributions
- Interactive (hover for details)

**Recommendations:**
- Prioritized action items:
  1. "Consult doctor about medication adjustment"
  2. "Increase exercise to 150 min/week"
  3. "Reduce sodium intake to <2300mg/day"
- Each with "Learn How" link

**What This Means:**
- Explanation in plain language
- "Moderate risk means your BP increases your chance of heart disease"
- Link to educational resources

### 3. Organ Damage Risk View

**Display:**
- Human body diagram with organs highlighted by risk level
- Clickable organs for details

**Organ Risk Cards:**
```
Heart (Moderate Risk) 🧡
- Risk of left ventricular hypertrophy
- BP elevated for 90 days
- Recommendation: ECG and cardiology consult
```

```
Kidneys (Moderate Risk) 🧡
- Risk of chronic kidney disease
- Recommendation: Kidney function tests
```

```
Eyes (Low Risk) 💚
- Low risk of retinopathy
- Continue monitoring
```

```
Brain (Low Risk) 💚
- Low stroke risk currently
- Maintain BP control to keep risk low
```

**Urgency Indicator:**
- Banner at top if urgent:
  - "Schedule appointment soon" (Orange)
  - "Schedule appointment urgent" (Red)
  - "Seek immediate care" (Red, flashing)
- Call-to-action button based on urgency

### 4. Risk History Timeline

**Display:**
- Timeline chart showing risk level over time
- Markers for:
  - Risk assessments
  - Medication changes
  - Lifestyle changes
- Interactive: click marker for details

**Trend Analysis:**
- "Your risk has decreased from High to Moderate over 3 months"
- Chart showing risk score trend line

### 5. Risk Calculator (Optional)

**Enhanced Assessment:**
- Input additional factors:
  - Age
  - Gender
  - Smoking status (current/former/never)
  - Diabetes (yes/no)
  - Family history of heart disease
- Calculate more accurate CVD risk
- Compare BP-only risk vs comprehensive risk

### 6. Educational Content

**Risk Education Sections:**
- What is cardiovascular risk?
- How BP affects your heart
- How BP affects your kidneys
- Understanding organ damage
- Steps to reduce risk
- When to see a doctor

**Videos and Infographics:**
- Embedded educational videos
- Downloadable infographics
- Links to trusted medical sources (AHA, Mayo Clinic)

### 7. Risk Alerts

**Alert Types:**

**Risk Increased:**
- "Your cardiovascular risk has increased to Moderate"
- Notification with CTA to view details

**Organ Risk Elevated:**
- "Elevated risk of kidney damage detected"
- "Schedule doctor appointment recommended"

**Positive Progress:**
- "Great news! Your risk has decreased to Low"
- Celebration animation

## User Flows

### Flow 1: Review Risk Assessment
1. User sees "Risk Assessment Available" notification
2. User taps notification
3. Risk dashboard loads
4. User sees "Moderate Risk" badge
5. User taps "Learn More"
6. Risk detail page opens
7. User views risk score (6.5/10)
8. User expands "Elevated BP" factor
9. Reads explanation
10. Reviews recommendations
11. User taps "Consult Doctor"
12. Doctor contact info or appointment scheduler opens

### Flow 2: Track Risk Improvement
1. User implements recommendations for 2 months
2. Monthly assessment runs
3. Risk level decreases to Low
4. Celebration notification appears
5. User opens risk dashboard
6. User sees improvement: High → Moderate → Low
7. User views risk history timeline
8. Chart shows downward trend
9. User feels motivated to maintain progress

### Flow 3: Address Organ Risk
1. User receives "Moderate kidney risk" alert
2. User taps alert
3. Organ damage risk view opens
4. Human body diagram shows kidneys highlighted orange
5. User taps on kidneys
6. Kidney risk card expands
7. Reads: "BP elevated for 95 days"
8. Recommendation: "Kidney function tests"
9. Urgency: "Schedule appointment soon"
10. User taps "Schedule Appointment"
11. Integration with calendar or opens contact

## Responsive Design

- Mobile: Simplified risk gauge, stacked cards
- Tablet: Side-by-side risk summary and details
- Desktop: Full dashboard with charts and body diagram

## Accessibility

- Risk levels conveyed through text, not just color
- Screen reader describes risk level and recommendations
- High contrast mode for risk indicators
- Clear, plain language explanations

## Medical Disclaimers

**Displayed Prominently:**
- "This assessment is not medical advice"
- "Based on BP data only; consult doctor for complete evaluation"
- "Additional risk factors may apply"
- "Always seek professional medical advice"

**Placement:**
- Bottom of risk detail page
- In smaller text on risk dashboard card
- In educational content

## State Management

- Cache latest risk assessment
- Update when new assessment completed
- Real-time updates via WebSocket
- Persist risk history

## Analytics Events

- `risk_assessed`
- `risk_viewed`
- `risk_factor_expanded`
- `risk_recommendation_clicked`
- `risk_calculator_used`
- `organ_risk_viewed`
- `appointment_scheduled_from_risk`
