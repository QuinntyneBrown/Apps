# Domain Events - Daily Journaling App

## Overview
This application supports daily journaling practices for personal reflection, emotional processing, and life documentation. Domain events capture journal entries, mood tracking, writing patterns, and the evolution of self-awareness over time.

## Events

### EntryEvents

#### JournalEntryCreated
- **Description**: A new journal entry has been written
- **Triggered When**: User creates and saves a journal entry
- **Key Data**: Entry ID, user ID, entry date, content, word count, writing duration, mood at writing, timestamp
- **Consumers**: Entry archive, search indexing, writing analytics, mood tracker

#### EntryUpdated
- **Description**: An existing journal entry has been edited
- **Triggered When**: User modifies a previous entry
- **Key Data**: Entry ID, updated fields, edit timestamp, version history, edit reason
- **Consumers**: Version control, audit log, edit pattern analyzer

#### EntryDeleted
- **Description**: A journal entry has been removed
- **Triggered When**: User deletes an entry
- **Key Data**: Entry ID, deletion timestamp, entry age, soft/hard delete flag, timestamp
- **Consumers**: Trash management, recovery service, deletion analytics

#### EntryTagged
- **Description**: Tags or categories have been applied to an entry
- **Triggered When**: User adds descriptive tags to entry
- **Key Data**: Entry ID, tag IDs, tag names, category assignments, timestamp
- **Consumers**: Tag organization, entry categorization, search enhancement

#### EntryFavorited
- **Description**: An entry has been marked as significant or favorite
- **Triggered When**: User highlights important entry
- **Key Data**: Entry ID, favorite timestamp, importance reason, timestamp
- **Consumers**: Favorites collection, memory highlights, easy access system

### MoodEvents

#### MoodLogged
- **Description**: User's emotional state has been recorded
- **Triggered When**: User logs their current mood
- **Key Data**: Mood ID, entry ID, mood type, intensity level, emotional descriptors, contributing factors, timestamp
- **Consumers**: Mood tracker, emotional pattern analyzer, mental health insights

#### MoodTrendDetected
- **Description**: A pattern in mood has been identified
- **Triggered When**: Analysis reveals consistent mood trajectory
- **Key Data**: Trend ID, trend direction, duration, severity, potential triggers, timestamp
- **Consumers**: Mental health monitoring, intervention alerts, wellness insights

#### EmotionalBreakthroughRecorded
- **Description**: A significant emotional insight has been documented
- **Triggered When**: User records meaningful emotional realization
- **Key Data**: Breakthrough ID, entry ID, insight description, what changed, impact level, timestamp
- **Consumers**: Growth tracker, therapy integration, wisdom archive

### PromptEvents

#### PromptAnswered
- **Description**: User has responded to a journaling prompt
- **Triggered When**: Entry is written in response to prompt
- **Key Data**: Entry ID, prompt ID, prompt text, response quality, timestamp
- **Consumers**: Prompt effectiveness tracker, recommendation engine, writing assistance

#### CustomPromptCreated
- **Description**: User has created their own journaling prompt
- **Triggered When**: User adds personal prompt to library
- **Key Data**: Prompt ID, creator ID, prompt text, category, intended use, timestamp
- **Consumers**: Personal prompt library, prompt sharing (if enabled), customization tracker

#### PromptStreakAchieved
- **Description**: User has consistently responded to daily prompts
- **Triggered When**: Consecutive days of prompt responses reach milestone
- **Key Data**: Streak ID, streak length, prompt variety used, timestamp
- **Consumers**: Gamification system, habit reinforcement, achievement badges

### WritingPatternEvents

#### DailyStreakAchieved
- **Description**: User has journaled consistently for consecutive days
- **Triggered When**: Daily journaling streak reaches milestone
- **Key Data**: Streak ID, streak length, average entry length, consistency score, timestamp
- **Consumers**: Motivation system, habit tracking, achievement celebration

#### StreakBroken
- **Description**: A journaling streak has ended
- **Triggered When**: User misses a day of journaling
- **Key Data**: Streak ID, final streak length, break date, days missed, timestamp
- **Consumers**: Re-engagement system, pattern analysis, motivation recovery

#### WritingTimePreferenceDetected
- **Description**: User's preferred journaling time has been identified
- **Triggered When**: Pattern analysis reveals consistent writing time
- **Key Data**: User ID, preferred time window, consistency percentage, timestamp
- **Consumers**: Reminder optimization, personalized scheduling, habit insights

#### LongFormEntryWritten
- **Description**: User has written an exceptionally long or detailed entry
- **Triggered When**: Entry exceeds typical length threshold
- **Key Data**: Entry ID, word count, writing duration, depth indicators, timestamp
- **Consumers**: Engagement analytics, processing intensity tracker, therapeutic value estimator

### ReflectionEvents

#### WeeklyReviewCompleted
- **Description**: User has reviewed their past week of entries
- **Triggered When**: User engages with weekly reflection feature
- **Key Data**: Review ID, week covered, entries reviewed, insights gained, patterns noticed, timestamp
- **Consumers**: Meta-reflection archive, pattern highlighter, growth documentation

#### MonthlyInsightGenerated
- **Description**: Monthly summary and insights have been created
- **Triggered When**: End of month triggers automatic analysis
- **Key Data**: Insight ID, month covered, key themes, mood summary, notable entries, timestamp
- **Consumers**: Progress visualization, long-term pattern detection, annual review preparation

#### PastEntryRevisited
- **Description**: User has re-read an old journal entry
- **Triggered When**: User opens entry from the past
- **Key Data**: Entry ID, original date, revisit date, time elapsed, user reaction, timestamp
- **Consumers**: Nostalgia tracker, growth comparison, memory refresh

### PrivacyEvents

#### EntryPrivacySet
- **Description**: Privacy level has been set for an entry
- **Triggered When**: User configures entry visibility or encryption
- **Key Data**: Entry ID, privacy level, encryption status, access restrictions, timestamp
- **Consumers**: Security service, privacy enforcement, access control

#### JournalLocked
- **Description**: Journal has been secured with password/biometric
- **Triggered When**: User enables journal lock
- **Key Data**: User ID, lock type, lock timestamp
- **Consumers**: Authentication system, security monitoring, privacy protection

#### JournalUnlocked
- **Description**: User has accessed their locked journal
- **Triggered When**: Successful authentication to view entries
- **Key Data**: Unlock ID, authentication method, unlock timestamp, device
- **Consumers**: Access log, security monitoring, usage tracking

### MediaEvents

#### PhotoAttached
- **Description**: An image has been added to a journal entry
- **Triggered When**: User includes photo in entry
- **Key Data**: Photo ID, entry ID, file reference, caption, timestamp
- **Consumers**: Media library, visual memory enhancement, backup service

#### VoiceNoteRecorded
- **Description**: An audio recording has been added to entry
- **Triggered When**: User records voice journal entry
- **Key Data**: Audio ID, entry ID, duration, transcription status, timestamp
- **Consumers**: Audio archive, transcription service, multimodal journaling

### ExportEvents

#### JournalExported
- **Description**: User has exported their journal data
- **Triggered When**: User requests export of entries
- **Key Data**: Export ID, date range, format (PDF/text/JSON), entry count, timestamp
- **Consumers**: Export service, data portability, backup creation

#### EntrySharedExternally
- **Description**: A journal entry has been shared outside the app
- **Triggered When**: User shares entry via external service
- **Key Data**: Entry ID, share method, recipient type, timestamp
- **Consumers**: Sharing analytics, privacy monitoring, external integration
