# Backend Requirements - Portfolio Management

## API Endpoints
- GET /api/portfolio/value - Get total portfolio value
- GET /api/portfolio/allocation - Get allocation breakdown
- GET /api/portfolio/performance - Get performance metrics
- POST /api/portfolio/rebalance - Execute rebalancing

## Business Logic
- Calculate total value across all wallets
- Allocation percentages by coin
- Performance calculations (ROI, returns)
- Rebalancing suggestions based on target allocation
- Diversification scoring

## Events
PortfolioValueCalculated, AllocationAnalyzed, PortfolioRebalanced

## Real-Time Price Integration
- CoinGecko/CoinMarketCap API
- Price cache with 60-second refresh
