# Domain Events - Professional Reading List

## Overview
This application captures domain events related to professional reading activities, including article discovery, reading progress, note-taking, and knowledge sharing. These events support continuous learning and professional development tracking.

## Events

### ReadingMaterialEvents

#### ArticleAdded
- **Description**: A new article has been added to the reading list
- **Triggered When**: User adds an article via URL, browser extension, or manual entry
- **Key Data**: Article ID, title, author, source URL, publication date, category/tags, added by, timestamp
- **Consumers**: Content categorizer, reading queue manager, recommendation engine, duplicate detector

#### BookAdded
- **Description**: A new book has been added to the professional reading list
- **Triggered When**: User adds a book to their reading list
- **Key Data**: Book ID, title, author(s), ISBN, publication year, publisher, page count, category, format (physical/digital)
- **Consumers**: Reading queue, recommendation service, library integration, progress tracker

#### ReadingMaterialCategorized
- **Description**: Reading material has been assigned to categories or tagged
- **Triggered When**: User assigns categories, topics, or custom tags to reading material
- **Key Data**: Material ID, category list, tags, assigned by, timestamp, priority level
- **Consumers**: Content organizer, search indexer, recommendation engine, analytics service

#### ReadingMaterialArchived
- **Description**: Reading material has been moved to archive
- **Triggered When**: User archives completed or no longer relevant reading material
- **Key Data**: Material ID, archived by, archive timestamp, archive reason, original category
- **Consumers**: Storage manager, analytics service, search indexer, cleanup scheduler

### ReadingProgressEvents

#### ReadingStarted
- **Description**: User has begun reading a specific article or book
- **Triggered When**: User opens and begins consuming the reading material
- **Key Data**: Material ID, reader, start timestamp, reading session ID, device/platform used
- **Consumers**: Progress tracker, time analytics, engagement metrics, reading streak calculator

#### ReadingProgressUpdated
- **Description**: User's reading progress has been updated
- **Triggered When**: User manually updates progress or system tracks reading advancement
- **Key Data**: Material ID, reader, progress percentage, pages/sections completed, update timestamp, estimated time remaining
- **Consumers**: Progress visualization, goal tracker, recommendation engine, completion predictor

#### ReadingCompleted
- **Description**: User has finished reading the material
- **Triggered When**: User marks material as read or reaches 100% progress
- **Key Data**: Material ID, reader, completion timestamp, total reading time, completion rating, would recommend flag
- **Consumers**: Achievement tracker, reading statistics, recommendation engine, knowledge base updater

#### ReadingAbandoned
- **Description**: User has decided to stop reading without completing
- **Triggered When**: User marks material as abandoned or removed from active reading list
- **Key Data**: Material ID, reader, abandon timestamp, progress at abandonment, abandon reason, would retry flag
- **Consumers**: Analytics service, recommendation refinement, reading pattern analyzer, content evaluator

### NotesAndHighlightsEvents

#### HighlightCreated
- **Description**: User has highlighted or marked important text passage
- **Triggered When**: User selects and saves a text highlight
- **Key Data**: Highlight ID, material ID, highlighted text, page/location, created by, timestamp, highlight color/category
- **Consumers**: Knowledge base, search indexer, quote library, review generator

#### NoteAdded
- **Description**: User has added a personal note to reading material
- **Triggered When**: User creates a note, annotation, or reflection on the content
- **Key Data**: Note ID, material ID, note content, page/section reference, author, timestamp, note type (summary/question/insight)
- **Consumers**: Knowledge management system, search indexer, review helper, learning tracker

#### KeyInsightCaptured
- **Description**: User has marked a key insight or takeaway from reading
- **Triggered When**: User explicitly identifies and saves an important learning or insight
- **Key Data**: Insight ID, material ID, insight text, category, practical application, captured by, timestamp
- **Consumers**: Personal knowledge base, career development tracker, sharing service, review generator

### SharingAndCollaborationEvents

#### ReadingRecommendationCreated
- **Description**: User has recommended reading material to colleagues
- **Triggered When**: User shares material with specific individuals or team
- **Key Data**: Recommendation ID, material ID, recommender, recipient list, recommendation reason, timestamp
- **Consumers**: Notification service, social features, recommendation tracker, influence metrics

#### ReadingListShared
- **Description**: User has shared a curated reading list publicly or with team
- **Triggered When**: User creates and shares a collection of related reading materials
- **Key Data**: List ID, creator, material IDs, list title, description, shared with, visibility level, share timestamp
- **Consumers**: Collaboration platform, notification service, discovery engine, team learning tracker

#### DiscussionStarted
- **Description**: A discussion thread has been initiated about reading material
- **Triggered When**: User or team member starts a conversation about the content
- **Key Data**: Discussion ID, material ID, initiator, topic, initial message, participants, timestamp
- **Consumers**: Collaboration platform, notification service, engagement tracker, knowledge sharing metrics

### LearningAndDevelopmentEvents

#### ReadingGoalSet
- **Description**: User has established a reading goal
- **Triggered When**: User sets targets for reading completion (articles per week, books per month, etc.)
- **Key Data**: Goal ID, goal type, target metric, timeframe, start date, end date, current progress
- **Consumers**: Goal tracker, progress monitor, reminder service, achievement system

#### ReadingMilestoneAchieved
- **Description**: User has reached a significant reading milestone
- **Triggered When**: User completes a goal or reaches a notable achievement (100 articles read, 12 books/year, etc.)
- **Key Data**: Milestone ID, milestone type, achievement date, associated goal, metrics achieved, celebration tier
- **Consumers**: Achievement system, notification service, gamification engine, career development tracker

#### SkillAreaDeveloped
- **Description**: Reading activity has contributed to skill development in a specific area
- **Triggered When**: System or user identifies skill improvement based on reading patterns
- **Key Data**: Skill ID, skill name, related materials, proficiency increase, assessment date, evidence sources
- **Consumers**: Professional development tracker, skill gap analyzer, career planning tool, learning path generator
