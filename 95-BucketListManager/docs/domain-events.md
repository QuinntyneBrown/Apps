# Domain Events - Bucket List Manager

## Overview
This application helps users create, track, and achieve their life goals and dream experiences. Domain events capture dream documentation, goal progress, experience completion, and the journey of living a fulfilling life.

## Events

### ItemEvents

#### BucketListItemAdded
- **Description**: A new life goal or experience has been added to the bucket list
- **Triggered When**: User documents something they want to do in their lifetime
- **Key Data**: Item ID, user ID, title, description, category, priority, inspiration source, creation timestamp
- **Consumers**: Bucket list manager, categorization system, priority queue, dream tracker

#### ItemDetailEnriched
- **Description**: Additional information has been added to a bucket list item
- **Triggered When**: User expands on their dream with more details
- **Key Data**: Item ID, additional details, why important, ideal timing, who to share with, timestamp
- **Consumers**: Item depth tracker, planning preparation, meaning documentation

#### ItemPriorityChanged
- **Description**: The importance or urgency of a bucket list item has been adjusted
- **Triggered When**: User reprioritizes their goals
- **Key Data**: Item ID, previous priority, new priority, change reason, timestamp
- **Consumers**: Priority management, focus direction, goal sequencing

#### ItemCompleted
- **Description**: A bucket list item has been successfully achieved
- **Triggered When**: User marks experience or goal as done
- **Key Data**: Item ID, completion date, actual experience, photos/memories, satisfaction rating, timestamp
- **Consumers**: Achievement tracker, celebration system, completion analytics, life satisfaction

#### ItemRemoved
- **Description**: A bucket list item has been deleted from the list
- **Triggered When**: User decides a goal is no longer relevant
- **Key Data**: Item ID, removal reason, time on list, evolved priorities, timestamp
- **Consumers**: Goal evolution tracker, priority shift analyzer, archive management

### CategoryEvents

#### CategoryCreated
- **Description**: A new category for organizing bucket list items has been established
- **Triggered When**: User creates custom category for their goals
- **Key Data**: Category ID, category name, description, icon, timestamp
- **Consumers**: Category management, organization system, filtering

#### ItemRecategorized
- **Description**: A bucket list item has been moved to different category
- **Triggered When**: User reclassifies an item
- **Key Data**: Item ID, previous category, new category, recategorization reason, timestamp
- **Consumers**: Organization updates, category analytics, goal classification

#### CategoryBalanceAnalyzed
- **Description**: Distribution of goals across life categories has been evaluated
- **Triggered When**: System analyzes life area balance
- **Key Data**: Analysis ID, category distribution, balance score, underrepresented areas, timestamp
- **Consumers**: Life balance insights, goal diversity, holistic living encouragement

### PlanningEvents

#### ItemMovedToPlanning
- **Description**: A bucket list item has transitioned to active planning
- **Triggered When**: User begins seriously planning how to achieve the goal
- **Key Data**: Item ID, planning start date, initial plan, resources needed, timestamp
- **Consumers**: Planning workflow, action item generation, resource assessment

#### ActionStepCreated
- **Description**: A specific action toward achieving a goal has been defined
- **Triggered When**: User breaks down bucket list item into actionable steps
- **Key Data**: Step ID, item ID, action description, timeline, dependencies, timestamp
- **Consumers**: Action tracker, progress monitoring, task management

#### TimeframeSet
- **Description**: A target date or timeframe has been established for an item
- **Triggered When**: User commits to timing for achieving goal
- **Key Data**: Item ID, target date, deadline type, flexibility, timestamp
- **Consumers**: Timeline management, deadline tracking, urgency assessment

#### BudgetEstimated
- **Description**: Financial requirements for a bucket list item have been calculated
- **Triggered When**: User estimates cost of experience or goal
- **Key Data**: Budget ID, item ID, estimated cost, cost breakdown, savings needed, timestamp
- **Consumers**: Financial planning, savings goals, affordability assessment

### ProgressEvents

#### ProgressUpdated
- **Description**: Advancement toward a bucket list item has been logged
- **Triggered When**: User records progress on achieving goal
- **Key Data**: Progress ID, item ID, progress percentage, milestone reached, notes, timestamp
- **Consumers**: Progress visualization, motivation system, momentum tracking

#### MilestoneAchieved
- **Description**: A significant step toward completing a bucket list item has been reached
- **Triggered When**: User hits an important milestone
- **Key Data**: Milestone ID, item ID, milestone description, celebration, timestamp
- **Consumers**: Achievement celebration, progress encouragement, completion prediction

#### ObstacleEncountered
- **Description**: A barrier to achieving a bucket list item has been identified
- **Triggered When**: User notes challenge or setback
- **Key Data**: Obstacle ID, item ID, obstacle description, severity, potential solutions, timestamp
- **Consumers**: Problem-solving system, support resources, persistence tracking

#### ObstacleOvercome
- **Description**: A barrier to a bucket list item has been resolved
- **Triggered When**: User successfully navigates challenge
- **Key Data**: Obstacle ID, item ID, resolution method, lessons learned, timestamp
- **Consumers**: Success patterns, resilience tracking, problem-solving library

### ExperienceEvents

#### ExperienceDocumented
- **Description**: The actual experience of completing a bucket list item has been captured
- **Triggered When**: User records their experience in detail
- **Key Data**: Experience ID, item ID, date(s), location, companions, detailed story, timestamp
- **Consumers**: Memory archive, storytelling system, life documentation

#### PhotosAdded
- **Description**: Images from completed bucket list experience have been uploaded
- **Triggered When**: User adds photos to commemorate achievement
- **Key Data**: Photo set ID, item ID, photo URLs, captions, timestamp
- **Consumers**: Visual memory bank, photo album, sharing system

#### ReflectionRecorded
- **Description**: User has reflected on the meaning and impact of a completed item
- **Triggered When**: User processes experience and its significance
- **Key Data**: Reflection ID, item ID, reflection content, personal growth, what changed, timestamp
- **Consumers**: Meaning-making system, growth documentation, life satisfaction

#### ExperienceRated
- **Description**: A completed bucket list item has been evaluated
- **Triggered When**: User rates how experience compared to expectations
- **Key Data**: Rating ID, item ID, satisfaction score, expectations vs reality, would recommend, timestamp
- **Consumers**: Satisfaction analytics, life quality metrics, future goal refinement

### InspirationEvents

#### InspiredByOthers
- **Description**: Someone else's experience has inspired a bucket list addition
- **Triggered When**: User adds item after seeing others' experiences
- **Key Data**: Item ID, inspiration source, what resonated, adaptation notes, timestamp
- **Consumers**: Inspiration tracker, social influence, goal sourcing

#### ItemSharedForInspiration
- **Description**: User has shared their bucket list item with others
- **Triggered When**: User posts or tells others about their goals
- **Key Data**: Share ID, item ID, share platform, audience, timestamp
- **Consumers**: Social sharing, accountability creation, inspiration spreading

#### CommunityGoalDiscovered
- **Description**: User has found a bucket list item from community suggestions
- **Triggered When**: User browses or discovers goals from community
- **Key Data**: Discovery ID, item ID, discovery source, adoption decision, timestamp
- **Consumers**: Community engagement, goal discovery, social features

### TimeBasedEvents

#### LifeSeasonDefined
- **Description**: User has defined a life phase for certain goals
- **Triggered When**: User categorizes items by life stage
- **Key Data**: Season ID, season name, timeframe, associated items, timestamp
- **Consumers**: Life stage planning, age-appropriate goals, timing optimization

#### AgeBasedGoalHighlighted
- **Description**: A bucket list item has time sensitivity due to age
- **Triggered When**: Goal is best achieved at certain life stage
- **Key Data**: Item ID, optimal age range, current age, urgency level, timestamp
- **Consumers**: Priority escalation, timing reminders, life stage awareness

#### AnniversaryReminder
- **Description**: Anniversary of bucket list item completion has been noted
- **Triggered When**: Annual reminder of past achievement
- **Key Data**: Item ID, years since completion, reflection prompt, timestamp
- **Consumers**: Memory celebration, gratitude practice, life review

### MotivationEvents

#### MotivationalQuoteAdded
- **Description**: Inspiring quote related to bucket list item has been saved
- **Triggered When**: User adds motivational content to goal
- **Key Data**: Quote ID, item ID, quote text, source, personal relevance, timestamp
- **Consumers**: Motivation system, inspiration library, encouragement delivery

#### WhyStatementCreated
- **Description**: User has articulated why a bucket list item matters to them
- **Triggered When**: User defines deep meaning behind goal
- **Key Data**: Why statement ID, item ID, motivation content, values connection, timestamp
- **Consumers**: Meaning tracker, motivation strength, persistence support

#### PeerSupportRequested
- **Description**: User has sought encouragement or accountability for a goal
- **Triggered When**: User involves others in achieving bucket list item
- **Key Data**: Support request ID, item ID, supporters involved, support type, timestamp
- **Consumers**: Accountability system, social support, achievement probability

### VisualizationEvents

#### VisionBoardCreated
- **Description**: Visual representation of bucket list has been created
- **Triggered When**: User compiles images of their goals
- **Key Data**: Vision board ID, items included, layout, images used, timestamp
- **Consumers**: Visualization system, motivation tool, dream clarity

#### FutureVisionArticulated
- **Description**: User has described their ideal future life
- **Triggered When**: User writes about life after achieving goals
- **Key Data**: Vision ID, timeframe, life description, key accomplishments, timestamp
- **Consumers**: Future self connection, motivation enhancement, life direction

### MetricsEvents

#### CompletionRateCalculated
- **Description**: Percentage of bucket list completed has been measured
- **Triggered When**: Periodic analysis of achievement progress
- **Key Data**: Metric ID, total items, completed items, completion rate, timestamp
- **Consumers**: Progress analytics, life satisfaction, achievement tracking

#### LifeFulfillmentScored
- **Description**: Overall fulfillment from bucket list pursuits has been assessed
- **Triggered When**: User reflects on life satisfaction from goals
- **Key Data**: Score ID, fulfillment rating, contributing factors, life quality assessment, timestamp
- **Consumers**: Life satisfaction tracking, purpose monitoring, wellbeing insights

#### TimeInvestedTracked
- **Description**: Time spent pursuing bucket list items has been logged
- **Triggered When**: User tracks time dedicated to goals
- **Key Data**: Time log ID, item ID, hours invested, time period, timestamp
- **Consumers**: Time allocation analysis, priority validation, life investment tracking
