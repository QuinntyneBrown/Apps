# Vehicle Comparison - Frontend Requirements

## Pages

### Vehicle Comparison (`/vehicles/compare`)
- **Vehicle Selector**: Checkboxes for owned vehicles
- **Comparison Table**: Side-by-side metrics
  - Average MPG
  - Monthly cost
  - Cost per mile
  - Total spent (YTD)
  - Best MPG achieved
- **Winner Badges**: Highlight best in each category
- **Usage Recommendations**: Which vehicle for which purpose

### EPA Comparison (`/vehicles/{id}/epa`)
- **EPA Ratings Display**: City/Highway/Combined
- **Actual Achievement**: Your real-world MPG
- **Variance Analysis**: % above/below EPA
- **Driving Mix**: Your city/highway split
- **Meets Expectations Indicator**

## UI Components

### ComparisonTable
- Responsive multi-column layout
- Highlight cells for winners
- Sort by any metric
- Visual indicators (bars, icons)

### EPAComparisonCard
- Gauge showing EPA vs actual
- Driving mix pie chart
- Factors affecting variance
- Share comparison image
