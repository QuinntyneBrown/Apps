# Backend Requirements - Tax Reporting

## API Endpoints
- GET /api/tax/report/{year} - Generate tax report
- GET /api/tax/gains/{year} - Get capital gains
- POST /api/tax/cost-basis-method - Set cost basis method
- GET /api/tax/export/{format} - Export to tax software

## Business Logic
- Calculate capital gains using FIFO/LIFO/HIFO
- Short-term vs long-term classification (1-year holding period)
- Form 8949 data generation
- Income tracking (staking, mining, airdrops)
- Cost basis tracking per tax lot

## Events
TaxableEventRecorded, CapitalGainCalculated, TaxReportGenerated, CostBasisMethodChanged

## Tax Calculations
- IRS compliant algorithms
- Historical price lookup for FMV calculations
