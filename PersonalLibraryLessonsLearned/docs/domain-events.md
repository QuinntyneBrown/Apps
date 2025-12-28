# Domain Events - Personal Library of Lessons Learned

## Overview
This application enables users to capture, organize, and revisit lessons learned from life experiences. Domain events track lesson documentation, wisdom accumulation, insight application, and the evolution of personal understanding.

## Events

### LessonCaptureEvents

#### LessonRecorded
- **Description**: A new lesson learned has been documented
- **Triggered When**: User records an insight or learning from experience
- **Key Data**: Lesson ID, user ID, lesson content, source experience, category, learning date, timestamp
- **Consumers**: Lesson library, search indexing, wisdom tracker, categorization system

#### ExperienceDocumented
- **Description**: The full context of a learning experience has been captured
- **Triggered When**: User provides detailed background for a lesson
- **Key Data**: Experience ID, lesson ID, situation description, what happened, emotions felt, people involved, timestamp
- **Consumers**: Context archive, pattern identifier, storytelling system

#### MistakeAcknowledged
- **Description**: User has documented a mistake or failure
- **Triggered When**: User records what went wrong and why
- **Key Data**: Mistake ID, description, impact, root cause analysis, regrets, timestamp
- **Consumers**: Failure analytics, growth opportunity tracker, humility archive

#### InsightGained
- **Description**: A deeper understanding or realization has emerged
- **Triggered When**: User documents an "aha moment" or breakthrough understanding
- **Key Data**: Insight ID, insight content, what prompted it, previous understanding vs. new, significance level, timestamp
- **Consumers**: Insight collection, wisdom development, mental model evolution

### CategoryEvents

#### LessonCategorized
- **Description**: A lesson has been assigned to categories or themes
- **Triggered When**: User tags lesson with relevant categories
- **Key Data**: Lesson ID, category IDs, tags, primary theme, timestamp
- **Consumers**: Organization system, discovery enhancement, pattern detection

#### ThemeIdentified
- **Description**: A recurring pattern across multiple lessons has been recognized
- **Triggered When**: Analysis or user identifies common thread
- **Key Data**: Theme ID, theme name, related lesson IDs, frequency, significance, timestamp
- **Consumers**: Pattern highlighter, wisdom synthesis, self-awareness enhancement

#### CustomCategoryCreated
- **Description**: User has created a new category for organizing lessons
- **Triggered When**: Existing categories don't fit user's learning taxonomy
- **Key Data**: Category ID, category name, description, parent category, timestamp
- **Consumers**: Personalized organization, taxonomy builder, categorization system

### ApplicationEvents

#### LessonApplied
- **Description**: User has used a past lesson in a current situation
- **Triggered When**: User consciously applies learned wisdom
- **Key Data**: Application ID, lesson ID, situation, how applied, outcome, effectiveness, timestamp
- **Consumers**: Application tracker, lesson utility metrics, wisdom activation

#### LessonSharedWithOthers
- **Description**: User has shared a lesson with someone else
- **Triggered When**: User teaches or advises based on their lesson
- **Key Data**: Share ID, lesson ID, recipient type, context, reception, timestamp
- **Consumers**: Teaching moments, wisdom transmission, social impact

#### PrincipleDerived
- **Description**: A guiding principle has been extracted from lessons
- **Triggered When**: User distills lessons into actionable principle
- **Key Data**: Principle ID, principle statement, supporting lessons, application scope, timestamp
- **Consumers**: Principle library, decision framework, life philosophy builder

#### LessonRevisited
- **Description**: User has reviewed a past lesson
- **Triggered When**: User re-reads or reflects on previous learning
- **Key Data**: Revisit ID, lesson ID, time since original, current perspective, evolved understanding, timestamp
- **Consumers**: Spaced repetition, wisdom retention, perspective evolution tracker

### ReflectionEvents

#### PeriodicReviewCompleted
- **Description**: User has reviewed lessons from a time period
- **Triggered When**: User conducts weekly, monthly, or annual lesson review
- **Key Data**: Review ID, period covered, lessons reviewed, meta-insights, patterns noticed, timestamp
- **Consumers**: Meta-learning system, pattern highlighter, wisdom integration

#### GrowthMomentIdentified
- **Description**: Evidence of personal growth has been recognized
- **Triggered When**: Comparing past and present reveals development
- **Key Data**: Growth ID, area of growth, evidence, before/after comparison, timestamp
- **Consumers**: Growth tracker, encouragement system, progress visualization

#### BlindSpotRevealed
- **Description**: An area of unawareness has been discovered
- **Triggered When**: Reflection reveals something previously unseen
- **Key Data**: Blind spot ID, what was missed, impact, new awareness, timestamp
- **Consumers**: Self-awareness development, learning opportunity identifier

### ConnectionEvents

#### LessonsLinked
- **Description**: Related lessons have been connected
- **Triggered When**: User identifies relationship between separate learnings
- **Key Data**: Link ID, lesson IDs connected, relationship type, connection insight, timestamp
- **Consumers**: Knowledge graph builder, wisdom synthesis, deeper understanding

#### CounterLessonIdentified
- **Description**: A lesson that contradicts or nuances a previous one has been noted
- **Triggered When**: New learning challenges old understanding
- **Key Data**: Contradiction ID, original lesson ID, new lesson ID, resolution approach, timestamp
- **Consumers**: Nuance development, wisdom refinement, paradox tracker

#### LessonClusterFormed
- **Description**: Multiple related lessons have been grouped together
- **Triggered When**: User creates collection around theme or experience
- **Key Data**: Cluster ID, cluster name, lesson IDs, unifying theme, timestamp
- **Consumers**: Collection manager, comprehensive understanding, themed wisdom

### WisdomEvents

#### WisdomQuoteCreated
- **Description**: User has distilled lesson into memorable quote or maxim
- **Triggered When**: User creates pithy expression of learning
- **Key Data**: Quote ID, lesson ID, quote text, context, timestamp
- **Consumers**: Quote library, wisdom sharing, inspiration collection

#### LifePhilosophyUpdated
- **Description**: User's overarching life philosophy has evolved based on lessons
- **Triggered When**: Accumulated lessons change fundamental beliefs
- **Key Data**: Philosophy ID, updated beliefs, supporting lessons, what changed, timestamp
- **Consumers**: Philosophy tracker, worldview evolution, core belief system

#### MentorAdviceRecorded
- **Description**: Wisdom received from a mentor has been documented
- **Triggered When**: User records advice from trusted guide
- **Key Data**: Advice ID, mentor name, advice content, context, application notes, timestamp
- **Consumers**: Mentor wisdom library, guidance archive, influence tracker

### ValidationEvents

#### LessonConfirmed
- **Description**: A lesson has been validated through repeated experience
- **Triggered When**: Multiple instances confirm the learning
- **Key Data**: Validation ID, lesson ID, confirming instances, confidence level, timestamp
- **Consumers**: Lesson reliability, wisdom strength rating, principle formation

#### LessonQuestioned
- **Description**: User has begun to doubt or reconsider a previous lesson
- **Triggered When**: New experience contradicts old learning
- **Key Data**: Question ID, lesson ID, contradicting experience, uncertainty level, timestamp
- **Consumers**: Lesson refinement, critical thinking, wisdom evolution

#### LessonRevised
- **Description**: User has updated or refined a previous lesson
- **Triggered When**: Greater understanding leads to modified conclusion
- **Key Data**: Revision ID, lesson ID, original version, revised version, reason for change, timestamp
- **Consumers**: Version control, wisdom refinement, learning evolution

### SearchEvents

#### LessonSearched
- **Description**: User has searched their lesson library
- **Triggered When**: User looks for specific lesson or topic
- **Key Data**: Search ID, search query, results returned, lesson accessed, timestamp
- **Consumers**: Search optimization, content discovery, usage patterns

#### RelevantLessonSuggested
- **Description**: System has recommended a relevant past lesson
- **Triggered When**: Current situation matches past learning
- **Key Data**: Suggestion ID, lesson ID, relevance trigger, context match, timestamp
- **Consumers**: Proactive wisdom delivery, lesson utility, just-in-time learning

### SharingEvents

#### LessonMadePublic
- **Description**: User has chosen to share a lesson publicly
- **Triggered When**: User publishes lesson for others to learn from
- **Key Data**: Share ID, lesson ID, visibility level, platform, timestamp
- **Consumers**: Public sharing service, community wisdom, social impact

#### LessonBookCreated
- **Description**: User has compiled lessons into a personal book or guide
- **Triggered When**: User creates structured compilation of learnings
- **Key Data**: Book ID, title, included lessons, organization approach, timestamp
- **Consumers**: Compilation system, legacy creation, wisdom packaging
