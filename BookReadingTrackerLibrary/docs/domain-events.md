# Domain Events - Book Reading Tracker & Library

## Overview
This application tracks domain events related to personal book collections, reading activities, book reviews, and reading goals. These events support reading habit development, literary discovery, and personal library management.

## Events

### LibraryManagementEvents

#### BookAddedToLibrary
- **Description**: A new book has been added to the personal library
- **Triggered When**: User adds a book to their collection (physical, digital, or audiobook)
- **Key Data**: Book ID, title, author(s), ISBN, publication year, publisher, format, acquisition date, acquisition source, purchase price, shelf location
- **Consumers**: Library catalog, search indexer, collection statistics, budget tracker, recommendation engine

#### BookCategorized
- **Description**: A book has been assigned to genres, categories, or custom collections
- **Triggered When**: User organizes books into classification systems
- **Key Data**: Book ID, genres, categories, tags, custom collections, series information, priority level, categorization date
- **Consumers**: Library organizer, search filter, recommendation engine, reading queue manager, collection analytics

#### BookLent
- **Description**: A book from the collection has been lent to someone
- **Triggered When**: User loans a book to a friend, family member, or colleague
- **Key Data**: Loan ID, book ID, borrower name, loan date, expected return date, condition notes, loan history, reminder preference
- **Consumers**: Loan tracker, reminder service, collection availability, borrower history, overdue alert system

#### BookReturned
- **Description**: A loaned book has been returned to the library
- **Triggered When**: User marks a loaned book as returned
- **Key Data**: Loan ID, book ID, return date, condition on return, days borrowed, borrower feedback, renewal count
- **Consumers**: Library availability updater, loan history, collection condition tracker, borrower relationship manager

#### BookRemoved
- **Description**: A book has been removed from the library
- **Triggered When**: User sells, donates, or otherwise removes a book from collection
- **Key Data**: Book ID, removal date, removal reason (donated/sold/lost/damaged), recipient/destination, sale price, removal notes
- **Consumers**: Collection statistics, budget tracker, donation records, library analytics, space management

### ReadingActivityEvents

#### ReadingStarted
- **Description**: User has begun reading a book
- **Triggered When**: User marks a book as currently reading
- **Key Data**: Reading session ID, book ID, start date, starting page, reading goal date, reading context (leisure/study/book club), device/format
- **Consumers**: Currently reading tracker, progress monitor, reading streak calculator, time analytics, reading schedule

#### ReadingProgressUpdated
- **Description**: User's reading progress has been updated
- **Triggered When**: User logs current page number or percentage completed
- **Key Data**: Progress ID, book ID, current page, total pages, percentage complete, update timestamp, reading pace, pages read since last update
- **Consumers**: Progress visualizer, completion estimator, reading statistics, goal tracker, achievement system

#### ReadingCompleted
- **Description**: User has finished reading a book
- **Triggered When**: User marks book as read/completed
- **Key Data**: Completion ID, book ID, completion date, total reading time, start date, rating (optional), would reread flag, emotional impact
- **Consumers**: Reading history, statistics calculator, recommendation engine, achievement tracker, annual reading goal

#### ReadingAbandoned
- **Description**: User has stopped reading without completing
- **Triggered When**: User marks book as DNF (did not finish) or abandoned
- **Key Data**: Abandonment ID, book ID, abandon date, pages read, abandon reason, progress percentage, would retry later flag
- **Consumers**: Reading pattern analyzer, recommendation refinement, DNF statistics, future reading consideration

#### BookReread
- **Description**: User has read a book again
- **Triggered When**: User starts or completes a subsequent reading of a previously read book
- **Key Data**: Reread ID, book ID, original read date, reread date, reread count, reason for rereading, new insights gained, rating comparison
- **Consumers**: Favorite books tracker, reading pattern analyzer, recommendation engine, literary value assessor

### ReviewAndRatingEvents

#### BookRated
- **Description**: User has assigned a rating to a book
- **Triggered When**: User provides a star rating or numerical score
- **Key Data**: Rating ID, book ID, rating value, rating scale, rating date, reading completion status, private/public flag
- **Consumers**: Personal ratings tracker, recommendation engine, reading statistics, best books list, average rating calculator

#### BookReviewWritten
- **Description**: User has written a review of a book
- **Triggered When**: User creates detailed written feedback about a book
- **Key Data**: Review ID, book ID, review text, rating, review date, spoiler flag, themes discussed, target audience notes, would recommend
- **Consumers**: Review library, sharing platform, personal reflection archive, recommendation context, search indexer

#### BookHighlighted
- **Description**: User has highlighted or marked a favorite passage
- **Triggered When**: User saves a quote, passage, or favorite excerpt
- **Key Data**: Highlight ID, book ID, passage text, page number, highlight date, highlight category, personal note, emotional response
- **Consumers**: Quote library, search indexer, review helper, commonplace book, sharing service

#### BookNoteAdded
- **Description**: User has added personal notes or annotations to a book
- **Triggered When**: User creates a note about content, thoughts, or analysis
- **Key Data**: Note ID, book ID, note content, page reference, note type (analysis/question/connection), timestamp, privacy level
- **Consumers**: Note management, search indexer, study aid, review preparation, knowledge management

### ReadingGoalEvents

#### ReadingGoalSet
- **Description**: User has established a reading goal
- **Triggered When**: User creates a target for books to read in a timeframe
- **Key Data**: Goal ID, goal type, target number, timeframe (month/quarter/year), start date, end date, genre constraints, current progress
- **Consumers**: Goal tracker, progress monitor, reminder service, achievement system, reading pace calculator

#### ReadingChallengeJoined
- **Description**: User has joined a reading challenge or themed reading event
- **Triggered When**: User enrolls in a structured reading challenge
- **Key Data**: Challenge ID, challenge name, challenge rules, start date, end date, required books, progress tracking method, participants
- **Consumers**: Challenge tracker, community features, progress monitor, achievement unlocks, social sharing

#### ReadingMilestoneAchieved
- **Description**: User has reached a significant reading milestone
- **Triggered When**: User completes a goal or reaches notable achievement (50 books/year, 10,000 pages, etc.)
- **Key Data**: Milestone ID, milestone type, achievement date, metric achieved, historical context, celebration tier, sharing preference
- **Consumers**: Achievement system, notification service, statistics dashboard, social sharing, gamification engine

#### ReadingStreakUpdated
- **Description**: User's consecutive reading days streak has changed
- **Triggered When**: User reads for consecutive days or breaks a streak
- **Key Data**: Streak ID, current streak length, longest streak, last read date, streak status (active/broken), streak type (daily/weekly)
- **Consumers**: Habit tracker, motivation system, reminder service, achievement badges, reading consistency analyzer

### DiscoveryAndRecommendationEvents

#### BookWishlisted
- **Description**: A book has been added to the user's wishlist or to-read list
- **Triggered When**: User adds a book they want to read in the future
- **Key Data**: Wishlist ID, book ID, added date, priority level, source of recommendation, anticipated read date, acquisition plan, interest reason
- **Consumers**: Wishlist manager, purchase suggestion, library hold request, reading queue planner, gift suggestion

#### BookRecommendationReceived
- **Description**: User has received a book recommendation
- **Triggered When**: Friend, system, or book club suggests a book to read
- **Key Data**: Recommendation ID, book ID, recommender, recommendation reason, recommendation date, acceptance status, relationship to recommender
- **Consumers**: Social reading features, recommendation tracker, wishlist manager, reading queue, relationship manager

#### BookRecommendationGiven
- **Description**: User has recommended a book to someone else
- **Triggered When**: User shares a book suggestion with others
- **Key Data**: Recommendation ID, book ID, recipient, recommendation reason, sharing method, recommendation date, recipient response
- **Consumers**: Social features, influence tracker, reading community, relationship builder, recommendation history

#### ReadingListCreated
- **Description**: User has created a curated reading list
- **Triggered When**: User organizes books into a themed or purposeful list
- **Key Data**: List ID, list name, description, book IDs, list order, creation date, public/private flag, list theme/purpose
- **Consumers**: List manager, sharing platform, reading planner, collection organizer, discovery tool

### SocialReadingEvents

#### BookClubSessionScheduled
- **Description**: A book club meeting or discussion has been scheduled
- **Triggered When**: User plans a book discussion event
- **Key Data**: Session ID, book ID, scheduled date, participants, discussion topics, reading sections, location/platform, organizer
- **Consumers**: Calendar integration, reminder service, participant notification, discussion prep, attendance tracker

#### BookDiscussionParticipated
- **Description**: User has participated in a book discussion or book club
- **Triggered When**: User attends and contributes to book discussion
- **Key Data**: Discussion ID, book ID, session date, participants, discussion notes, insights shared, next book decision, participation level
- **Consumers**: Social reading tracker, discussion archive, book club history, engagement metrics, community builder
