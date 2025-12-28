# Domain Events - Marriage Enrichment Journal

## Overview
This application helps couples strengthen their relationship through gratitude practices, appreciation sharing, and reflective journaling. Domain events capture moments of gratitude, relationship insights, communication patterns, and the journey of marital growth.

## Events

### GratitudeEvents

#### GratitudeEntryCreated
- **Description**: One partner has recorded something they're grateful for about their spouse
- **Triggered When**: User creates a gratitude journal entry
- **Key Data**: Entry ID, author ID, spouse ID, gratitude content, category (action/quality/moment), privacy level, creation timestamp
- **Consumers**: Gratitude feed, partner notification service, relationship health tracker, positivity metrics

#### GratitudeSharedWithSpouse
- **Description**: A gratitude entry has been shared with the partner
- **Triggered When**: User chooses to reveal their gratitude to their spouse
- **Key Data**: Entry ID, share timestamp, delivery method, partner read status
- **Consumers**: Partner notification service, emotional connection tracker, appreciation delivery system

#### GratitudeAcknowledged
- **Description**: Spouse has read and acknowledged a gratitude entry
- **Triggered When**: Partner views and responds to shared gratitude
- **Key Data**: Entry ID, acknowledgment timestamp, response message, emotional reaction
- **Consumers**: Feedback loop tracker, appreciation cycle monitor, relationship satisfaction metrics

#### GratitudeStreakAchieved
- **Description**: User has maintained consistent gratitude practice
- **Triggered When**: Consecutive days of gratitude entries reach milestone
- **Key Data**: User ID, streak length, streak type, achievement date, consistency score, timestamp
- **Consumers**: Motivation system, habit reinforcement, achievement badges

### AppreciationEvents

#### AppreciationExpressed
- **Description**: One partner has expressed specific appreciation for their spouse
- **Triggered When**: User records what they appreciate about their partner
- **Key Data**: Appreciation ID, expresser ID, recipient ID, appreciation type (words/actions/qualities), specific instance, intensity level, timestamp
- **Consumers**: Love language analyzer, appreciation pattern tracker, relationship strengthening insights

#### AppreciationReciprocated
- **Description**: Partner has responded with their own appreciation
- **Triggered When**: Spouse replies with their own appreciation entry
- **Key Data**: Original appreciation ID, reciprocation ID, reciprocator ID, time to reciprocate, reciprocation content, timestamp
- **Consumers**: Reciprocity tracker, relationship balance monitor, positive interaction analyzer

#### LoveLanguageIdentified
- **Description**: System has identified a partner's primary love language
- **Triggered When**: Pattern analysis reveals preference in appreciation types
- **Key Data**: User ID, identified love language(s), confidence score, supporting evidence, timestamp
- **Consumers**: Personalized suggestion system, partner insight dashboard, relationship education content

### ReflectionEvents

#### ReflectionJournalCreated
- **Description**: Couple or individual has completed a reflection exercise
- **Triggered When**: User engages with guided reflection prompts
- **Key Data**: Reflection ID, author ID(s), prompt ID, reflection content, themes identified, privacy level, timestamp
- **Consumers**: Insight aggregator, growth tracking, relationship patterns analyzer

#### ConflictReflectionRecorded
- **Description**: Partner has reflected on a disagreement or conflict
- **Triggered When**: User journals about a conflict and their role in it
- **Key Data**: Reflection ID, author ID, conflict description, self-awareness insights, resolution ideas, timestamp
- **Consumers**: Conflict resolution tracker, personal growth monitor, communication improvement system

#### GrowthMomentCaptured
- **Description**: A moment of relationship growth has been documented
- **Triggered When**: User identifies a breakthrough or positive change
- **Key Data**: Moment ID, date, description, what changed, what prompted growth, partners involved, timestamp
- **Consumers**: Progress timeline, milestone tracker, encouragement system

#### WeeklyReviewCompleted
- **Description**: Couple has completed their weekly relationship review
- **Triggered When**: Partners finish guided weekly reflection together
- **Key Data**: Review ID, review date, highlights, challenges, goals for next week, satisfaction rating, timestamp
- **Consumers**: Relationship health dashboard, trend analyzer, goal setting system

### CommunicationEvents

#### PromptAnswered
- **Description**: User has responded to a relationship-building prompt
- **Triggered When**: Partner answers a guided question or prompt
- **Key Data**: Response ID, prompt ID, user ID, response content, share status, timestamp
- **Consumers**: Conversation starter system, depth of sharing tracker, prompt effectiveness analyzer

#### JointJournalEntryCreated
- **Description**: Couple has created a collaborative journal entry
- **Triggered When**: Both partners contribute to a shared entry
- **Key Data**: Entry ID, both partner IDs, combined content, creation process duration, agreement level, timestamp
- **Consumers**: Collaborative activity tracker, unity metrics, shared memory archive

#### CommunicationGoalSet
- **Description**: Couple has established a communication goal
- **Triggered When**: Partners define an area to improve in their communication
- **Key Data**: Goal ID, goal description, target behavior, success criteria, timeline, timestamp
- **Consumers**: Goal tracking system, reminder service, progress monitor

#### CommunicationWinRecorded
- **Description**: A successful communication moment has been celebrated
- **Triggered When**: User notes an instance of excellent communication
- **Key Data**: Win ID, what happened, skills used, outcome, celebration notes, timestamp
- **Consumers**: Success pattern analyzer, positive reinforcement system, skill development tracker

### CelebrationEvents

#### MilestoneCelebrated
- **Description**: A relationship milestone has been acknowledged
- **Triggered When**: Anniversary, achievement, or significant event is marked
- **Key Data**: Milestone ID, milestone type, date, significance, celebration notes, photos, timestamp
- **Consumers**: Memory archive, anniversary reminder system, relationship timeline

#### PositivePatternIdentified
- **Description**: System has detected a positive relationship pattern
- **Triggered When**: Analysis reveals consistent positive behaviors
- **Key Data**: Pattern ID, pattern type, frequency, examples, both partners' contribution, timestamp
- **Consumers**: Encouragement system, strength identification, success amplification

### MoodEvents

#### RelationshipMoodLogged
- **Description**: User has recorded their current feeling about the relationship
- **Triggered When**: Partner logs relationship satisfaction or mood
- **Key Data**: Mood ID, user ID, mood rating, contributing factors, notes, timestamp
- **Consumers**: Relationship health monitor, trend tracker, early warning system for issues

#### MoodTrendDetected
- **Description**: A trend in relationship satisfaction has been identified
- **Triggered When**: Pattern analysis reveals upward or downward mood trajectory
- **Key Data**: Trend ID, direction, duration, contributing factors, severity, timestamp
- **Consumers**: Proactive intervention system, counseling suggestion service, celebration trigger (if positive)

### PrivacyEvents

#### PrivateEntryCreated
- **Description**: User has created a private journal entry not shared with spouse
- **Triggered When**: Partner needs personal space to process thoughts
- **Key Data**: Entry ID, author ID, content type, privacy duration, timestamp
- **Consumers**: Private journal archive, personal processing space, optional share-later system

#### EntrySharedAfterDelay
- **Description**: A previously private entry has been shared with spouse
- **Triggered When**: User decides to share what was initially private
- **Key Data**: Entry ID, original creation date, share date, delay duration, share reason, timestamp
- **Consumers**: Vulnerability tracker, delayed sharing insights, trust building metrics
