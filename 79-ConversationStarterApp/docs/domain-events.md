# Domain Events - Conversation Starter App

## Overview
This application provides conversation prompts and questions to deepen relationships and spark meaningful discussions. Domain events track prompt usage, user favorites, conversation quality feedback, and the creation of custom prompts.

## Events

### PromptEvents

#### PromptGenerated
- **Description**: A conversation starter prompt has been generated for the user
- **Triggered When**: User requests a new conversation prompt based on filters or randomization
- **Key Data**: Prompt ID, category, difficulty level, context (romantic/family/friends/professional), prompt text, generation timestamp
- **Consumers**: Usage analytics, recommendation engine, prompt history tracker

#### PromptViewed
- **Description**: User has viewed a conversation starter prompt
- **Triggered When**: Prompt is displayed to the user
- **Key Data**: Prompt ID, user ID, view timestamp, context of viewing, device type
- **Consumers**: Engagement analytics, popular prompt tracker, view-to-use conversion metrics

#### PromptSkipped
- **Description**: User has skipped a prompt without using it
- **Triggered When**: User dismisses or requests another prompt
- **Key Data**: Prompt ID, skip reason (if provided), time spent viewing, user preferences, timestamp
- **Consumers**: Recommendation engine, prompt quality assessment, user preference learning

#### PromptFavorited
- **Description**: User has marked a prompt as a favorite
- **Triggered When**: User saves a prompt to their favorites list
- **Key Data**: Prompt ID, user ID, category, favorited timestamp
- **Consumers**: User profile service, personalized recommendations, favorite collections manager

#### CustomPromptCreated
- **Description**: User has created their own custom conversation starter
- **Triggered When**: User adds a personal prompt to the system
- **Key Data**: Prompt ID, creator ID, prompt text, category, privacy setting, creation timestamp
- **Consumers**: Personal prompt library, community sharing service (if public), quality review system

### ConversationEvents

#### ConversationStarted
- **Description**: User has initiated a conversation using a prompt
- **Triggered When**: User marks a prompt as "used" or starts a conversation
- **Key Data**: Conversation ID, prompt ID, participant count, context, start timestamp
- **Consumers**: Usage analytics, prompt effectiveness tracker, conversation duration estimator

#### ConversationCompleted
- **Description**: A conversation session has been marked as finished
- **Triggered When**: User indicates the conversation has concluded
- **Key Data**: Conversation ID, prompt ID, duration, satisfaction rating, depth score, timestamp
- **Consumers**: Prompt effectiveness analytics, quality metrics, conversation insights

#### ConversationRated
- **Description**: User has rated the quality of a conversation from a prompt
- **Triggered When**: User provides feedback on how well the prompt worked
- **Key Data**: Conversation ID, prompt ID, quality rating, depth rating, engagement rating, detailed feedback, timestamp
- **Consumers**: Prompt ranking system, quality analytics, recommendation improvements

#### ConversationNotesAdded
- **Description**: User has added notes or insights from the conversation
- **Triggered When**: User documents key takeaways or memorable moments
- **Key Data**: Notes ID, conversation ID, prompt ID, notes content, tags, timestamp
- **Consumers**: Personal insights library, memory system, relationship depth tracker

### CategoryEvents

#### CategoryPreferenceSet
- **Description**: User has set preferences for conversation starter categories
- **Triggered When**: User indicates preferred topics or contexts
- **Key Data**: User ID, category preferences, weighted priorities, excluded categories, timestamp
- **Consumers**: Prompt filtering service, recommendation engine, personalization system

#### NewCategoryAdded
- **Description**: A new category of conversation starters has been added
- **Triggered When**: System administrator or curator adds a new topic category
- **Key Data**: Category ID, category name, description, parent category, difficulty range, timestamp
- **Consumers**: Categorization system, prompt organization service, UI category browser

#### CategoryExplored
- **Description**: User has browsed or explored a specific category
- **Triggered When**: User views prompts within a particular category
- **Key Data**: Category ID, user ID, prompts viewed, duration, engagement level, timestamp
- **Consumers**: Category popularity analytics, exploration pattern tracker, recommendation tuning

### CollectionEvents

#### CollectionCreated
- **Description**: User has created a custom collection of prompts
- **Triggered When**: User organizes prompts into a themed collection
- **Key Data**: Collection ID, collection name, theme, privacy setting, creator ID, timestamp
- **Consumers**: Collection manager, sharing service, organization system

#### PromptAddedToCollection
- **Description**: A prompt has been added to a user's collection
- **Triggered When**: User adds a prompt to one of their collections
- **Key Data**: Collection ID, prompt ID, add reason, order/position, timestamp
- **Consumers**: Collection organization, prompt cross-reference, usage pattern tracker

#### CollectionShared
- **Description**: User has shared a prompt collection with others
- **Triggered When**: User makes a collection public or shares with specific people
- **Key Data**: Collection ID, share method, recipient IDs or public flag, access permissions, timestamp
- **Consumers**: Sharing service, social features, viral growth tracker

### UsagePatternEvents

#### UsageStreakAchieved
- **Description**: User has maintained consistent use of conversation starters
- **Triggered When**: User hits milestone for consecutive days of usage
- **Key Data**: User ID, streak length, streak type, achievement date, consistency metrics, timestamp
- **Consumers**: Gamification system, motivation service, engagement rewards

#### DifficultyLevelProgressed
- **Description**: User has progressed to more challenging conversation prompts
- **Triggered When**: System detects user is ready for deeper or more complex prompts
- **Key Data**: User ID, previous difficulty level, new difficulty level, progression trigger, timestamp
- **Consumers**: Recommendation engine, user growth tracker, challenge system

#### ContextSwitchDetected
- **Description**: User has changed conversation context (e.g., from romantic to family)
- **Triggered When**: User switches between different relationship contexts
- **Key Data**: User ID, previous context, new context, switch reason, timestamp
- **Consumers**: Context-aware recommendations, usage pattern analytics, versatility tracker

### SocialEvents

#### PromptSharedWithPartner
- **Description**: User has shared a specific prompt with a conversation partner
- **Triggered When**: User sends a prompt to someone else
- **Key Data**: Prompt ID, sender ID, recipient ID, share channel, message included, timestamp
- **Consumers**: Sharing analytics, social engagement tracker, viral growth monitoring

#### CommunityPromptVoted
- **Description**: User has voted on a community-submitted prompt
- **Triggered When**: User rates or votes on prompts created by other users
- **Key Data**: Prompt ID, voter ID, vote type, vote weight, timestamp
- **Consumers**: Community curation system, prompt quality ranking, moderation service
