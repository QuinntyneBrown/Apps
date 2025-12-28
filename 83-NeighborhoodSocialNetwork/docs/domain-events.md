# Domain Events - Neighborhood Social Network

## Overview
This application connects neighbors and builds community through local events, recommendations, and mutual support. Domain events capture neighborhood interactions, local business reviews, community organizing, and the strengthening of local social bonds.

## Events

### NeighborEvents

#### NeighborProfileCreated
- **Description**: A new resident has joined the neighborhood network
- **Triggered When**: User creates profile and verifies neighborhood address
- **Key Data**: User ID, address (verified), household size, move-in date, interests, skills to share, timestamp
- **Consumers**: Welcome committee notification, neighbor matching service, directory updates

#### AddressVerified
- **Description**: User's residence in the neighborhood has been confirmed
- **Triggered When**: Address verification process completes successfully
- **Key Data**: User ID, verified address, verification method, verification timestamp
- **Consumers**: Access control, neighborhood boundary enforcement, resident directory

#### NeighborConnectionMade
- **Description**: Two neighbors have connected on the platform
- **Triggered When**: Neighbors accept connection request or meet at event
- **Key Data**: Connection ID, both user IDs, connection context, proximity (houses apart), timestamp
- **Consumers**: Social graph builder, recommendation engine, community strength metrics

#### NeighborMovedAway
- **Description**: A resident has moved out of the neighborhood
- **Triggered When**: User updates status to moved or account becomes inactive
- **Key Data**: User ID, move-out date, years in neighborhood, forwarding option, timestamp
- **Consumers**: Directory cleanup, historical records, alumni network

### CommunityEventEvents

#### NeighborhoodEventCreated
- **Description**: A local community event has been organized
- **Triggered When**: Resident creates event for neighbors
- **Key Data**: Event ID, organizer ID, event type, date/time, location, capacity, target audience, timestamp
- **Consumers**: Event calendar, neighbor notifications, RSVP system

#### EventRSVPReceived
- **Description**: A neighbor has RSVP'd to a community event
- **Triggered When**: Resident responds to event invitation
- **Key Data**: RSVP ID, event ID, user ID, response type, household size attending, timestamp
- **Consumers**: Attendance tracker, organizer notifications, capacity management

#### NeighborhoodPartyHosted
- **Description**: A social gathering for neighbors has taken place
- **Triggered When**: Event completes and host logs attendance
- **Key Data**: Event ID, actual attendance, highlights, new connections made, photos, timestamp
- **Consumers**: Community engagement metrics, social capital tracker, event success analyzer

#### BlockPartyApprovalRequested
- **Description**: Request for street closure or official block party submitted
- **Triggered When**: Organizer initiates formal neighborhood event
- **Key Data**: Request ID, event ID, organizer ID, street(s) affected, date requested, signatures collected, timestamp
- **Consumers**: Approval workflow, neighbor petition system, city liaison service

### RecommendationEvents

#### LocalBusinessReviewed
- **Description**: Neighbor has reviewed a local business or service
- **Triggered When**: User posts review of neighborhood business
- **Key Data**: Review ID, business ID, reviewer ID, rating, review content, service type, timestamp
- **Consumers**: Local business directory, recommendation engine, community favorites tracker

#### ServiceProviderRecommended
- **Description**: A service provider has been recommended to the neighborhood
- **Triggered When**: User shares positive experience with service provider
- **Key Data**: Recommendation ID, provider name, service category, recommender ID, endorsement strength, timestamp
- **Consumers**: Trusted provider list, neighbor referral network, local economy support

#### ServiceRequestPosted
- **Description**: Neighbor has requested a service or contractor recommendation
- **Triggered When**: User seeks help finding a service provider
- **Key Data**: Request ID, service type needed, urgency, budget range, specific requirements, timestamp
- **Consumers**: Request matching system, neighbor notification (filtered by relevance), response tracker

#### RecommendationVouched
- **Description**: Additional neighbor has endorsed a recommendation
- **Triggered When**: User confirms a previous recommendation based on own experience
- **Key Data**: Vouch ID, original recommendation ID, voucher ID, strength of endorsement, timestamp
- **Consumers**: Trust score calculator, recommendation ranking, community validation system

### SafetyEvents

#### SafetyConcernReported
- **Description**: A neighborhood safety issue has been reported
- **Triggered When**: Resident reports safety concern or suspicious activity
- **Key Data**: Report ID, reporter ID, concern type, location, severity, description, photo evidence, timestamp
- **Consumers**: Neighborhood watch coordinator, local authorities (if escalated), safety alert system

#### SafetyAlertIssued
- **Description**: Important safety information has been broadcast to neighborhood
- **Triggered When**: Validated safety concern requires neighbor awareness
- **Key Data**: Alert ID, alert type, affected area, recommended actions, source, timestamp
- **Consumers**: Priority notification service, safety log, community awareness tracker

#### NeighborhoodWatchPatrolLogged
- **Description**: Neighborhood watch activity has been recorded
- **Triggered When**: Watch member completes patrol or monitoring shift
- **Key Data**: Patrol ID, watch member ID, patrol area, duration, observations, timestamp
- **Consumers**: Watch coordination system, safety coverage tracker, incident correlation

#### LostPetReported
- **Description**: A pet has been reported lost in the neighborhood
- **Triggered When**: Resident reports missing pet
- **Key Data**: Report ID, pet details, last seen location/time, owner contact, photo, timestamp
- **Consumers**: Urgent neighborhood notification, sighting tracking system, reunion coordinator

### MutualAidEvents

#### HelpRequestPosted
- **Description**: Neighbor has requested assistance from the community
- **Triggered When**: Resident needs help and posts request
- **Key Data**: Request ID, requester ID, help type, urgency, timeframe needed, timestamp
- **Consumers**: Neighbor matching (by skills/availability), help network coordinator, response tracker

#### HelpOfferMade
- **Description**: Neighbor has volunteered to assist with a request
- **Triggered When**: User offers to help with posted need
- **Key Data**: Offer ID, request ID, volunteer ID, what they can provide, availability, timestamp
- **Consumers**: Request fulfillment matcher, volunteer coordination, community giving tracker

#### SkillShared
- **Description**: Neighbor has offered to teach or share a skill with community
- **Triggered When**: User posts skill-sharing opportunity
- **Key Data**: Skill ID, teacher ID, skill offered, format, availability, cost (if any), timestamp
- **Consumers**: Community education coordinator, skill exchange network, social capital builder

#### ItemShared
- **Description**: Neighbor has made an item available for borrowing/sharing
- **Triggered When**: User adds item to neighborhood sharing library
- **Key Data**: Item ID, owner ID, item description, borrowing terms, availability, timestamp
- **Consumers**: Tool library, sharing economy tracker, sustainability metrics

### CommunicationEvents

#### NeighborhoodAnnouncementPosted
- **Description**: Important information has been shared with neighborhood
- **Triggered When**: User posts community-wide announcement
- **Key Data**: Announcement ID, poster ID, category, content, target audience, priority, timestamp
- **Consumers**: Notification service (priority-based), announcement board, information archive

#### DiscussionThreadStarted
- **Description**: A neighborhood discussion topic has been initiated
- **Triggered When**: Resident starts conversation thread
- **Key Data**: Thread ID, initiator ID, topic, category, initial post, timestamp
- **Consumers**: Community forum, engagement tracker, trending topics analyzer

#### ConcernEscalated
- **Description**: A neighborhood issue has been elevated to leaders or authorities
- **Triggered When**: Issue requires official action or intervention
- **Key Data**: Escalation ID, original issue ID, escalation reason, authority notified, urgency, timestamp
- **Consumers**: Issue tracking system, authority liaison, resolution monitor

### PropertyEvents

#### YardSaleAnnounced
- **Description**: Neighbor has announced an upcoming yard/garage sale
- **Triggered When**: User posts about yard sale event
- **Key Data**: Sale ID, host ID, date/time, address, preview of items, timestamp
- **Consumers**: Yard sale map, neighbor notifications, community calendar

#### NewResidentWelcomed
- **Description**: Welcome has been extended to new neighborhood resident
- **Triggered When**: Existing neighbor welcomes newcomer
- **Key Data**: Welcome ID, welcomer ID, new resident ID, welcome message, offer of help, timestamp
- **Consumers**: Community integration tracker, relationship formation, welcoming culture metrics
