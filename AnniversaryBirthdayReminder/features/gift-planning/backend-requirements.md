# Gift Planning - Backend Requirements

## Domain Model
### Gift Aggregate
- **GiftId**: Guid
- **DateId**: Guid
- **Description**: string
- **EstimatedPrice**: decimal
- **ActualPrice**: decimal nullable
- **PurchaseUrl**: string
- **Status**: Idea, Purchased, Delivered (enum)
- **PurchasedAt**: DateTime nullable

## Commands
- AddGiftIdeaCommand
- MarkGiftPurchasedCommand
- MarkGiftDeliveredCommand
