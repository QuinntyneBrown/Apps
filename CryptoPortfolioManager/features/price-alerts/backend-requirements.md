# Backend Requirements - Price Alerts
## API: POST/GET/DELETE /api/alerts
## Models: PriceAlert (Id, CoinSymbol, AlertType, TargetPrice, Triggered)
## Logic: Price monitoring every 60s, threshold checking, notification delivery
## Events: PriceAlertTriggered, SignificantPriceMovement
