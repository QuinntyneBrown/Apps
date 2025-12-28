# Domain Events - Letter to Future Self

## Overview
This application enables users to write letters to their future selves, schedule delivery, and reflect on personal growth over time. Domain events capture letter creation, scheduled delivery, reading experiences, and the journey of self-discovery across time.

## Events

### LetterEvents

#### LetterCreated
- **Description**: A new letter to future self has been written
- **Triggered When**: User composes and saves a letter
- **Key Data**: Letter ID, author ID, letter content, current mood/state, life circumstances, scheduled delivery date, timestamp
- **Consumers**: Letter vault, delivery scheduler, encryption service, draft management

#### LetterScheduled
- **Description**: A letter has been scheduled for future delivery
- **Triggered When**: User sets delivery date and commits letter
- **Key Data**: Letter ID, scheduled delivery date, delivery method preference, reminder settings, timestamp
- **Consumers**: Delivery queue, reminder scheduler, time capsule service

#### LetterDelivered
- **Description**: A scheduled letter has been delivered to the user
- **Triggered When**: Delivery date arrives and letter is unlocked
- **Key Data**: Letter ID, delivery timestamp, time since written, delivery notification sent, timestamp
- **Consumers**: Notification service, reading tracker, reflection prompt system

#### LetterRead
- **Description**: User has opened and read their delivered letter
- **Triggered When**: User accesses delivered letter
- **Key Data**: Letter ID, read timestamp, time to first read after delivery, device used, timestamp
- **Consumers**: Reading analytics, engagement tracker, reflection flow initiator

#### LetterReflectionAdded
- **Description**: User has added reflection notes after reading their letter
- **Triggered When**: User documents thoughts after reading past letter
- **Key Data**: Reflection ID, letter ID, reflection content, insights gained, emotions felt, growth observed, timestamp
- **Consumers**: Growth tracker, insight aggregator, comparative analysis service

#### LetterRescheduled
- **Description**: Delivery date of an undelivered letter has been changed
- **Triggered When**: User updates when they want to receive the letter
- **Key Data**: Letter ID, original delivery date, new delivery date, reschedule reason, timestamp
- **Consumers**: Delivery queue updater, scheduler adjustment, change log

#### LetterDeleted
- **Description**: A letter has been permanently deleted before delivery
- **Triggered When**: User decides not to receive a scheduled letter
- **Key Data**: Letter ID, deletion timestamp, was_read flag, deletion reason, timestamp
- **Consumers**: Archive cleanup, analytics (understanding deletion patterns), audit log

### TimeCapsuletEvents

#### TimeCapsuleCreated
- **Description**: A collection of letters has been grouped as a time capsule
- **Triggered When**: User creates a themed set of letters for future delivery
- **Key Data**: Capsule ID, title, theme, included letter IDs, delivery date, timestamp
- **Consumers**: Capsule manager, batch delivery scheduler, thematic organization system

#### CapsuleItemAdded
- **Description**: Additional content has been added to a time capsule
- **Triggered When**: User adds photo, voice memo, or document to capsule
- **Key Data**: Item ID, capsule ID, item type, file reference, description, timestamp
- **Consumers**: Media storage, capsule compiler, delivery package builder

#### CapsuleSealed
- **Description**: A time capsule has been locked for future opening
- **Triggered When**: User finalizes capsule and prevents further edits
- **Key Data**: Capsule ID, seal timestamp, delivery date, item count, timestamp
- **Consumers**: Immutability enforcement, delivery scheduler, anticipation tracker

#### CapsuleOpened
- **Description**: A sealed time capsule has reached its opening date
- **Triggered When**: Capsule delivery date arrives
- **Key Data**: Capsule ID, open timestamp, time sealed, items contained, timestamp
- **Consumers**: Content delivery, comprehensive reflection initiator, nostalgia experience

### GoalTrackingEvents

#### FutureGoalDeclared
- **Description**: User has declared a goal in letter to future self
- **Triggered When**: Letter contains specific goal or aspiration
- **Key Data**: Goal ID, letter ID, goal description, target date, success criteria, timestamp
- **Consumers**: Goal extraction service, progress tracker, accountability system

#### GoalProgressEvaluated
- **Description**: User has assessed progress on goal from past letter
- **Triggered When**: Delivered letter prompts goal progress reflection
- **Key Data**: Evaluation ID, goal ID, letter ID, achievement level, obstacles faced, timestamp
- **Consumers**: Goal success analytics, pattern identifier, motivational insights

#### PredictionMade
- **Description**: User has made a prediction about their future
- **Triggered When**: Letter includes specific prediction or expectation
- **Key Data**: Prediction ID, letter ID, prediction content, confidence level, category, timestamp
- **Consumers**: Prediction tracker, accuracy analyzer, life change detector

#### PredictionVerified
- **Description**: A prediction has been evaluated against reality
- **Triggered When**: Delivered letter's predictions are compared to actual outcomes
- **Key Data**: Prediction ID, accuracy rating, actual outcome, variance notes, timestamp
- **Consumers**: Prediction accuracy metrics, self-knowledge insights, expectation analyzer

### PromptEvents

#### WritingPromptGenerated
- **Description**: A suggested prompt for letter writing has been provided
- **Triggered When**: User requests inspiration for what to write
- **Key Data**: Prompt ID, prompt text, category, difficulty, usage count, timestamp
- **Consumers**: Prompt library, recommendation engine, writer's block assistance

#### PromptUsed
- **Description**: User has written a letter based on a prompt
- **Triggered When**: Letter is created in response to specific prompt
- **Key Data**: Letter ID, prompt ID, prompt effectiveness rating, timestamp
- **Consumers**: Prompt success analytics, recommendation improvement, popular prompts tracker

### EmotionalEvents

#### MoodCaptured
- **Description**: User's current emotional state has been recorded with letter
- **Triggered When**: User logs mood while writing letter
- **Key Data**: Mood ID, letter ID, mood descriptor, intensity, contributing factors, timestamp
- **Consumers**: Emotional timeline, mood pattern analyzer, mental health insights

#### EmotionalContrastDetected
- **Description**: Significant difference between past and current emotional state identified
- **Triggered When**: User's current state differs notably from when letter was written
- **Key Data**: Contrast ID, letter ID, original mood, current mood, life changes between, timestamp
- **Consumers**: Growth visualization, life change detector, resilience metrics

### ReminderEvents

#### DeliveryReminderSent
- **Description**: Upcoming letter delivery reminder has been sent
- **Triggered When**: Delivery date is approaching
- **Key Data**: Reminder ID, letter ID, days until delivery, reminder type, timestamp
- **Consumers**: Notification service, anticipation builder, engagement maintainer

#### WriteReminderSent
- **Description**: Reminder to write a new letter has been sent
- **Triggered When**: User hasn't written in defined period or scheduled reminder
- **Key Data**: Reminder ID, user ID, last letter date, reminder reason, timestamp
- **Consumers**: Engagement system, habit formation, user retention

### ArchiveEvents

#### LetterArchived
- **Description**: A read letter has been moved to permanent archive
- **Triggered When**: User archives letter after reading and reflecting
- **Key Data**: Letter ID, archive date, archive category, tags, timestamp
- **Consumers**: Archive organization, search indexing, long-term storage

#### ArchiveSearched
- **Description**: User has searched their letter archive
- **Triggered When**: User looks for past letters or reflections
- **Key Data**: Search ID, user ID, search terms, results count, timestamp
- **Consumers**: Search optimization, content discovery, usage analytics

#### TimelineViewed
- **Description**: User has viewed their chronological letter timeline
- **Triggered When**: User explores their letters over time
- **Key Data**: View ID, user ID, date range viewed, interaction type, timestamp
- **Consumers**: Engagement metrics, feature usage, personal growth visualization
