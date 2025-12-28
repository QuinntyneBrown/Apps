# Domain Events - Family Tree Builder

## Overview
This application enables users to build and maintain their family genealogy, capturing relationships, stories, photos, and historical records. Domain events track the growth of the family tree, preservation of family history, and collaborative family research.

## Events

### PersonEvents

#### PersonAdded
- **Description**: A new family member has been added to the family tree
- **Triggered When**: User creates a record for a family member
- **Key Data**: Person ID, full name, birth date, birth place, current status (living/deceased), generation level, added by user ID, timestamp
- **Consumers**: Tree visualization service, relationship calculator, genealogy analytics

#### PersonDetailsUpdated
- **Description**: Information about a family member has been updated
- **Triggered When**: User modifies biographical information
- **Key Data**: Person ID, updated fields, previous values, new values, source of information, update timestamp
- **Consumers**: Change history tracker, data quality service, family notifications

#### PersonMerged
- **Description**: Two duplicate person records have been merged
- **Triggered When**: User identifies and combines duplicate entries
- **Key Data**: Primary person ID, merged person ID, merged data, conflict resolutions, merge timestamp
- **Consumers**: Data integrity service, relationship recalculation, audit log

#### DeathRecorded
- **Description**: A family member's passing has been documented
- **Triggered When**: User records the death of a family member
- **Key Data**: Person ID, death date, death place, cause (if known), burial information, obituary, timestamp
- **Consumers**: Tree status update, memorial service, living status tracker, age calculator

### RelationshipEvents

#### RelationshipEstablished
- **Description**: A family relationship has been defined between two people
- **Triggered When**: User connects two family members with a relationship
- **Key Data**: Relationship ID, person 1 ID, person 2 ID, relationship type (parent/child/spouse/sibling), start date, confidence level, timestamp
- **Consumers**: Relationship graph builder, tree visualization, relationship calculator

#### MarriageRecorded
- **Description**: A marriage between family members has been documented
- **Triggered When**: User adds marriage information
- **Key Data**: Marriage ID, spouse 1 ID, spouse 2 ID, marriage date, marriage place, marriage status (current/divorced/widowed), certificate info, timestamp
- **Consumers**: Family relationship tracker, surname analyzer, descendant calculator

#### DivorceRecorded
- **Description**: A divorce or separation has been documented
- **Triggered When**: User records the end of a marriage
- **Key Data**: Divorce ID, marriage ID, divorce date, divorce place, custody information (if applicable), timestamp
- **Consumers**: Relationship status tracker, family structure analyzer

#### AdoptionRecorded
- **Description**: An adoption has been documented in the family tree
- **Triggered When**: User records adoption details
- **Key Data**: Adoption ID, adopted person ID, adoptive parent IDs, biological parent IDs (if known), adoption date, adoption place, timestamp
- **Consumers**: Biological vs. legal relationship tracker, family dynamics analyzer

### MediaEvents

#### PhotoUploaded
- **Description**: A photo of a family member or event has been added
- **Triggered When**: User uploads a family photo
- **Key Data**: Photo ID, file URL, associated person IDs, date taken, location, description, uploader ID, upload timestamp
- **Consumers**: Media library, facial recognition service, timeline generator, memory preservation

#### PhotoTagged
- **Description**: People have been identified in a family photo
- **Triggered When**: User tags family members in a photo
- **Key Data**: Tag ID, photo ID, person ID, tag coordinates, confidence level, tagger ID, timestamp
- **Consumers**: Person photo gallery, facial recognition training, memory association

#### DocumentUploaded
- **Description**: A historical document has been added to the family archive
- **Triggered When**: User uploads certificates, records, or other documents
- **Key Data**: Document ID, document type (birth cert/marriage cert/census/military), associated person IDs, document date, source, file URL, timestamp
- **Consumers**: Source verification system, evidence tracker, research library

### StoryEvents

#### FamilyStoryAdded
- **Description**: A family story or anecdote has been documented
- **Triggered When**: User records a family memory, story, or historical account
- **Key Data**: Story ID, title, narrative content, associated person IDs, time period, storyteller, story date, creation timestamp
- **Consumers**: Family history archive, memory preservation, story search index

#### StoryCommented
- **Description**: A family member has added comments or corrections to a story
- **Triggered When**: User comments on or adds to an existing story
- **Key Data**: Comment ID, story ID, commenter ID, comment text, additional details, timestamp
- **Consumers**: Collaborative history building, story enrichment, family engagement

#### OralHistoryRecorded
- **Description**: An audio or video oral history has been captured
- **Triggered When**: User uploads interview or recorded memories
- **Key Data**: Recording ID, person interviewed, interviewer, recording date, duration, transcript, key topics, timestamp
- **Consumers**: Multimedia archive, searchable transcript index, precious memories preservation

### ResearchEvents

#### ResearchHintGenerated
- **Description**: System has identified a potential research lead
- **Triggered When**: Automated matching finds possible records or connections
- **Key Data**: Hint ID, person ID, hint type, source database, confidence score, hint details, generation timestamp
- **Consumers**: Research task list, user notification service, genealogy partner integration

#### HintAccepted
- **Description**: User has accepted and incorporated a research hint
- **Triggered When**: User verifies and adds suggested information to tree
- **Key Data**: Hint ID, person ID, accepted data, verification notes, acceptance timestamp
- **Consumers**: Tree update service, research quality metrics, source citation tracker

#### HintRejected
- **Description**: User has rejected a research hint as incorrect
- **Triggered When**: User determines a hint doesn't match their family
- **Key Data**: Hint ID, person ID, rejection reason, conflicting information, timestamp
- **Consumers**: Machine learning refinement, hint quality improvement, false positive tracker

#### DNAMatchDiscovered
- **Description**: A DNA match with another family tree has been identified
- **Triggered When**: DNA testing reveals a genetic relative
- **Key Data**: Match ID, person ID, matched person ID, relationship estimate, shared DNA percentage, match confidence, timestamp
- **Consumers**: Relationship verification, potential connection explorer, collaborative research initiator

### CollaborationEvents

#### TreeSharedWithFamily
- **Description**: Family tree has been shared with other family members
- **Triggered When**: User grants access to relatives
- **Key Data**: Share ID, recipient IDs, access level (view/edit/admin), shared branches, invitation message, timestamp
- **Consumers**: Access control service, collaboration notification, family engagement tracker

#### CollaborativeEditMade
- **Description**: A family member has contributed to the shared tree
- **Triggered When**: Collaborator adds or modifies information
- **Key Data**: Edit ID, editor ID, edited entity (person/relationship/media), changes made, edit timestamp
- **Consumers**: Change notification service, contribution tracking, merge conflict detector

#### TreeMerged
- **Description**: Two family trees have been merged into one
- **Triggered When**: User combines their tree with another researcher's tree
- **Key Data**: Merge ID, source tree ID, target tree ID, common ancestors, new persons added, conflicts resolved, timestamp
- **Consumers**: Data integration service, relationship recalculation, family network expansion
