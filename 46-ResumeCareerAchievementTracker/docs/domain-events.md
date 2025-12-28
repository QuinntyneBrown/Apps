# Domain Events - Resume & Career Achievement Tracker

## Overview
This document defines the domain events tracked in the Resume & Career Achievement Tracker application. These events capture significant business occurrences related to career milestones, skill development, resume management, achievement documentation, and professional growth.

## Events

### AchievementEvents

#### AchievementLogged
- **Description**: Professional accomplishment has been recorded
- **Triggered When**: User documents significant career achievement
- **Key Data**: Achievement ID, title, description, date, category, impact metrics, company, role, skills demonstrated, quantifiable results
- **Consumers**: Achievement library, resume content generator, interview prep, skills tracker, accomplishment portfolio

#### AchievementCategorized
- **Description**: Achievement has been assigned to category
- **Triggered When**: User tags achievement with category or competency area
- **Key Data**: Achievement ID, category, subcategory, skills involved, industry relevance, career level
- **Consumers**: Category organizer, skills mapper, resume section assigner, achievement filter

#### AchievementQuantified
- **Description**: Measurable impact of achievement has been documented
- **Triggered When**: User adds metrics to accomplishment (revenue, savings, efficiency, etc.)
- **Key Data**: Achievement ID, metric type, metric value, baseline comparison, percentage improvement, business impact
- **Consumers**: Impact calculator, resume bullet strengthener, interview talking points, value demonstration

#### RecognitionReceived
- **Description**: Professional recognition or award has been earned
- **Triggered When**: User logs award, certification, or formal recognition
- **Key Data**: Recognition ID, recognition type, awarding organization, date received, significance level, achievement linked
- **Consumers**: Awards section, resume highlights, LinkedIn sync, credibility builder

### SkillEvents

#### SkillAdded
- **Description**: New professional skill has been added to profile
- **Triggered When**: User adds skill to their skillset
- **Key Data**: Skill ID, skill name, skill category, proficiency level, years of experience, last used date, endorsements count
- **Consumers**: Skills inventory, resume skills section, gap analyzer, job matching, development planner

#### SkillProficiencyUpdated
- **Description**: Competency level in skill has been modified
- **Triggered When**: User updates proficiency as skills improve
- **Key Data**: Skill ID, previous proficiency, new proficiency, update date, evidence of improvement, training completed
- **Consumers**: Skills dashboard, resume updater, growth tracker, development progress

#### SkillEndorsementReceived
- **Description**: Colleague or manager has validated skill
- **Triggered When**: External validation of skill competency received
- **Key Data**: Skill ID, endorsed by, endorsement date, endorser role, endorser relationship, endorsement strength
- **Consumers**: Credibility builder, LinkedIn sync, skills ranking, resume strengthener

#### SkillGapIdentified
- **Description**: Missing or weak skill for career goal has been detected
- **Triggered When**: Job matching or career planning reveals skill deficiency
- **Key Data**: Skill ID, current proficiency, required proficiency, career goal relevance, urgency level, development resources
- **Consumers**: Development planner, training recommender, learning path creator, career goal aligner

#### SkillDevelopmentGoalSet
- **Description**: Target for skill improvement has been established
- **Triggered When**: User commits to developing specific skill
- **Key Data**: Goal ID, skill ID, target proficiency, target date, development method, milestones, progress tracking
- **Consumers**: Goal tracker, learning plan, progress monitor, achievement anticipator

### ResumeEvents

#### ResumeVersionCreated
- **Description**: New version of resume has been generated
- **Triggered When**: User creates or updates resume version
- **Key Data**: Resume ID, version number, creation date, target role, target company, tailoring approach, format, length
- **Consumers**: Resume library, version manager, job application associator, A/B testing tracker

#### ResumeTailored
- **Description**: Resume has been customized for specific opportunity
- **Triggered When**: User modifies resume to match job description
- **Key Data**: Resume ID, job description, keywords matched, achievements highlighted, skills emphasized, customization date
- **Consumers**: Job application tracker, keyword optimizer, relevance scorer, application success correlator

#### ResumeReviewed
- **Description**: Resume has been evaluated for quality
- **Triggered When**: User or system analyzes resume effectiveness
- **Key Data**: Resume ID, review date, reviewer, score, feedback points, strengths identified, improvements suggested, ATS compatibility
- **Consumers**: Quality improver, optimization recommender, ATS checker, competitive analyzer

#### ResumeExported
- **Description**: Resume has been generated in specific format
- **Triggered When**: User exports resume for submission
- **Key Data**: Export ID, resume ID, format (PDF/Word/HTML), export date, target use, file generated, styling template
- **Consumers**: File generator, application submitter, format tracker, submission logger

#### KeywordOptimized
- **Description**: Resume has been enhanced for keyword matching
- **Triggered When**: User optimizes resume for ATS or job keywords
- **Key Data**: Resume ID, target keywords, keywords added, keyword density, match score, optimization date
- **Consumers**: ATS compatibility, keyword tracker, match scorer, application success predictor

### PositionEvents

#### PositionAdded
- **Description**: Work experience entry has been added to career history
- **Triggered When**: User logs new job or role in work history
- **Key Data**: Position ID, job title, company, start date, end date, employment type, location, reporting structure, current flag
- **Consumers**: Work history, resume experience section, timeline builder, tenure calculator

#### ResponsibilityDocumented
- **Description**: Job duty or responsibility has been recorded
- **Triggered When**: User details what they did in a role
- **Key Data**: Responsibility ID, position ID, description, key activities, scope, team size, budget managed
- **Consumers**: Resume bullets, job description library, interview prep, experience articulator

#### PositionTransition
- **Description**: Job change or promotion has occurred
- **Triggered When**: User moves to new role or company
- **Key Data**: Previous position ID, new position ID, transition date, transition type (promotion/lateral/company change), reason, salary change
- **Consumers**: Career timeline, growth tracker, transition analyzer, salary history, momentum indicator

### EducationEvents

#### EducationAdded
- **Description**: Educational credential has been added to profile
- **Triggered When**: User logs degree, certification, or training program
- **Key Data**: Education ID, institution, degree/certification, field of study, start date, completion date, GPA, honors, relevant coursework
- **Consumers**: Education section, credential validator, qualification checker, academic background

#### CertificationEarned
- **Description**: Professional certification has been achieved
- **Triggered When**: User completes certification program
- **Key Data**: Certification ID, certification name, issuing organization, earn date, expiration date, certification number, renewal required
- **Consumers**: Certifications section, credential library, expiration reminder, skills validator

#### CertificationExpiring
- **Description**: Professional certification approaching expiration
- **Triggered When**: Certification expiration within warning window
- **Key Data**: Certification ID, expiration date, days until expiration, renewal process, continuing education needed
- **Consumers**: Renewal reminder, continuing education planner, credential maintenance, resume accuracy

### ProjectEvents

#### ProjectCompleted
- **Description**: Significant professional project has been finished
- **Triggered When**: User documents completed project
- **Key Data**: Project ID, project name, description, role, duration, team size, technologies used, outcomes achieved, company
- **Consumers**: Projects portfolio, resume projects section, skills demonstration, achievement source

#### ProjectImpactQuantified
- **Description**: Measurable results of project have been documented
- **Triggered When**: User adds metrics showing project success
- **Key Data**: Project ID, impact metrics, ROI, efficiency gains, revenue impact, user adoption, business value
- **Consumers**: Impact showcase, resume strengthener, interview examples, value proposition

### CareerGoalEvents

#### CareerGoalSet
- **Description**: Professional objective or target role has been defined
- **Triggered When**: User establishes career aspiration
- **Key Data**: Goal ID, target role, target company type, target industry, target timeline, required skills, development plan
- **Consumers**: Goal tracker, gap analyzer, development planner, job matcher, motivation system

#### GoalMilestoneReached
- **Description**: Progress step toward career goal has been achieved
- **Triggered When**: User completes milestone on path to goal
- **Key Data**: Goal ID, milestone description, achievement date, skills gained, experience acquired, next milestone
- **Consumers**: Progress tracker, motivation system, goal path updater, achievement celebration

#### CareerPathExplored
- **Description**: Potential career direction has been researched
- **Triggered When**: User investigates career option
- **Key Data**: Path ID, target role, industry, required skills, typical trajectory, salary range, research date, interest level
- **Consumers**: Path explorer, comparison tool, decision support, goal setter

### NetworkingEvents

#### ProfessionalReferenceAdded
- **Description**: Reference contact has been added to profile
- **Triggered When**: User documents professional reference
- **Key Data**: Reference ID, name, title, company, relationship, contact info, years known, reference strength, permission to contact
- **Consumers**: Reference list, reference check preparation, relationship manager, credibility builder

#### MentorshipLogged
- **Description**: Mentoring relationship or session has been documented
- **Triggered When**: User records mentoring activity
- **Key Data**: Mentorship ID, mentor/mentee, date, topics discussed, advice received, action items, relationship type
- **Consumers**: Mentorship tracker, development support, relationship manager, guidance library

### InterviewPrepEvents

#### InterviewStoryPrepared
- **Description**: STAR method story has been crafted for interviews
- **Triggered When**: User creates behavioral interview response
- **Key Data**: Story ID, situation, task, action, result, skills demonstrated, question types addressed, achievement linked
- **Consumers**: Interview prep, story bank, practice tool, answer refiner

#### AccomplishmentHighlighted
- **Description**: Key achievement has been marked for interview emphasis
- **Triggered When**: User identifies top accomplishments to discuss
- **Key Data**: Achievement ID, highlight priority, target roles, talking points, quantified impact, practice frequency
- **Consumers**: Interview preparation, elevator pitch, value proposition, confidence builder

### DocumentationEvents

#### PerformanceReviewRecorded
- **Description**: Performance evaluation has been documented
- **Triggered When**: User logs annual or periodic review results
- **Key Data**: Review ID, review date, reviewer, rating, strengths noted, development areas, accomplishments recognized, raise/promotion
- **Consumers**: Performance history, achievement validator, growth tracker, compensation history

#### RecommendationReceived
- **Description**: Professional recommendation or testimonial has been obtained
- **Triggered When**: Colleague or manager provides written recommendation
- **Key Data**: Recommendation ID, author, relationship, date, recommendation text, platform (LinkedIn/email/letter), strength rating
- **Consumers**: Recommendation library, credibility builder, LinkedIn sync, reference material

### ExportSyncEvents

#### LinkedInSynced
- **Description**: Profile has been synchronized with LinkedIn
- **Triggered When**: User exports or updates LinkedIn profile from tracker
- **Key Data**: Sync ID, sync date, sections updated, changes made, skills added, experiences updated
- **Consumers**: LinkedIn updater, profile consistency, visibility maintainer, network currency

#### PortfolioGenerated
- **Description**: Professional portfolio has been created
- **Triggered When**: User generates comprehensive career portfolio
- **Key Data**: Portfolio ID, generation date, content included, format, target audience, completeness score
- **Consumers**: Portfolio presenter, job application supporter, personal brand builder
