# Requirements - Hydration Tracker

## Overview
A health-focused application that helps users track water intake, set hydration goals, receive reminders, and maintain healthy drinking habits.

## Features

### Feature 1: Water Intake Logging
- FR1.1: Log water intake with amount, beverage type, and timestamp
- FR1.2: Support quick-add presets for common amounts (8oz, 16oz, etc.)
- FR1.3: Modify or delete intake entries with audit trail
- FR1.4: Track different beverage types (water, tea, coffee, etc.)
- FR1.5: Calculate daily total intake automatically

### Feature 2: Hydration Goals
- FR2.1: Set daily hydration goals (manual or auto-calculated based on weight/activity)
- FR2.2: Track progress toward daily goal with visual indicators
- FR2.3: Detect goal achievement and trigger celebrations
- FR2.4: Track consecutive days streak of meeting goals
- FR2.5: Award badges for streak milestones (7, 30, 100 days)

### Feature 3: Smart Reminders
- FR3.1: Schedule customizable hydration reminders throughout the day
- FR3.2: Send notifications at scheduled times
- FR3.3: Track reminder effectiveness (action rate)
- FR3.4: Allow users to snooze reminders
- FR3.5: Auto-optimize reminder times based on user behavior

### Feature 4: Health Insights
- FR4.1: Display hydration history and trends
- FR4.2: Show correlation between hydration and user-reported wellness
- FR4.3: Generate weekly/monthly hydration reports
- FR4.4: Provide personalized recommendations for improvement

## Core Entities
- User, IntakeEntry, DailyGoal, Reminder, Streak, Badge, HealthInsight
