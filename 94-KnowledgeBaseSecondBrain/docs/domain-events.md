# Domain Events - Knowledge Base & Second Brain

## Overview
This application serves as an external cognitive system for capturing, organizing, and connecting knowledge. Domain events track note creation, idea linking, knowledge synthesis, and the evolution of personal understanding across domains.

## Events

### NoteEvents

#### NoteCreated
- **Description**: A new note has been added to the knowledge base
- **Triggered When**: User captures information, idea, or thought
- **Key Data**: Note ID, user ID, title, content, source, note type, creation timestamp
- **Consumers**: Knowledge graph, search indexer, organization system, backup service

#### NoteUpdated
- **Description**: An existing note has been modified
- **Triggered When**: User edits or expands a note
- **Key Data**: Note ID, updated fields, version history, update reason, timestamp
- **Consumers**: Version control, change tracker, related notes notifier

#### NoteDeleted
- **Description**: A note has been removed from the system
- **Triggered When**: User deletes a note
- **Key Data**: Note ID, deletion timestamp, soft/permanent delete, archive option, timestamp
- **Consumers**: Trash management, link cleanup, backup system

#### NoteMerged
- **Description**: Multiple notes have been combined into one
- **Triggered When**: User consolidates related notes
- **Key Data**: Merged note ID, source note IDs, merge strategy, timestamp
- **Consumers**: Link updating, knowledge consolidation, deduplication

#### NoteSplit
- **Description**: A note has been divided into multiple notes
- **Triggered When**: User breaks complex note into separate concepts
- **Key Data**: Original note ID, new note IDs, split criteria, timestamp
- **Consumers**: Link preservation, organization refinement, knowledge atomization

### LinkingEvents

#### NotesLinked
- **Description**: A connection between two notes has been established
- **Triggered When**: User creates relationship between notes
- **Key Data**: Link ID, source note ID, target note ID, link type, relationship description, timestamp
- **Consumers**: Knowledge graph builder, bidirectional linking, connection strength tracker

#### BacklinkCreated
- **Description**: A note has been referenced from another note
- **Triggered When**: Note is mentioned or linked in different note
- **Key Data**: Backlink ID, linking note ID, linked note ID, context, timestamp
- **Consumers**: Backlink visualizer, note popularity tracker, connection mapper

#### LinkClusterIdentified
- **Description**: A group of highly interconnected notes has been detected
- **Triggered When**: Analysis reveals dense connection pattern
- **Key Data**: Cluster ID, note IDs in cluster, cluster theme, connection strength, timestamp
- **Consumers**: Topic identifier, knowledge area highlighter, organization suggester

#### OrphanNoteDetected
- **Description**: A note with no connections to other notes has been identified
- **Triggered When**: Note exists without links
- **Key Data**: Note ID, isolation duration, potential connections, timestamp
- **Consumers**: Integration suggester, review prompt, organization health

### OrganizationEvents

#### TagAdded
- **Description**: A tag has been applied to a note
- **Triggered When**: User categorizes note with tag
- **Key Data**: Tag ID, note ID, tag name, tag hierarchy, timestamp
- **Consumers**: Tag index, categorization system, search enhancement

#### FolderCreated
- **Description**: A new organizational folder has been established
- **Triggered When**: User creates structure for notes
- **Key Data**: Folder ID, folder name, parent folder, description, timestamp
- **Consumers**: Hierarchy manager, navigation system, organization framework

#### NoteMoved
- **Description**: A note has been relocated to different folder
- **Triggered When**: User reorganizes note location
- **Key Data**: Note ID, previous folder, new folder, move reason, timestamp
- **Consumers**: Organization tracker, navigation updates, structure evolution

#### CollectionCreated
- **Description**: A curated set of notes has been grouped
- **Triggered When**: User creates thematic collection
- **Key Data**: Collection ID, collection name, note IDs included, purpose, timestamp
- **Consumers**: Collection manager, knowledge curation, quick access

### SearchEvents

#### NoteSearched
- **Description**: User has searched the knowledge base
- **Triggered When**: Search query is executed
- **Key Data**: Search ID, query text, filters applied, results count, timestamp
- **Consumers**: Search optimization, content discovery, usage patterns

#### RelatedNotesFound
- **Description**: System has identified notes related to current note
- **Triggered When**: Automatic suggestion system finds connections
- **Key Data**: Suggestion ID, source note ID, related note IDs, relevance scores, timestamp
- **Consumers**: Discovery engine, knowledge connection, serendipity facilitator

#### DeadLinkDetected
- **Description**: A link to non-existent note has been found
- **Triggered When**: Link references deleted or missing note
- **Key Data**: Dead link ID, source note ID, missing target, timestamp
- **Consumers**: Link maintenance, cleanup system, integrity checker

### KnowledgeSynthesisEvents

#### InsightGenerated
- **Description**: A new understanding has emerged from connecting ideas
- **Triggered When**: User synthesizes information into new insight
- **Key Data**: Insight ID, source note IDs, insight content, novelty level, timestamp
- **Consumers**: Insight tracker, value measurement, knowledge creation monitor

#### ConceptDefined
- **Description**: A core concept or idea has been formally defined
- **Triggered When**: User creates definitive note for a concept
- **Key Data**: Concept ID, concept name, definition, related concepts, timestamp
- **Consumers**: Concept map, glossary, foundation knowledge

#### ThemeEmergence
- **Description**: A recurring theme across notes has been identified
- **Triggered When**: Pattern recognition reveals consistent topic
- **Key Data**: Theme ID, theme name, note IDs contributing, strength, timestamp
- **Consumers**: Theme tracker, knowledge organization, meta-analysis

#### KnowledgeGapIdentified
- **Description**: An area lacking knowledge has been recognized
- **Triggered When**: User notices missing information or understanding
- **Key Data**: Gap ID, gap description, related existing knowledge, priority, timestamp
- **Consumers**: Learning queue, research topics, knowledge roadmap

### CaptureEvents

#### QuickCaptureCreated
- **Description**: A rapid note has been created for later processing
- **Triggered When**: User quickly saves information
- **Key Data**: Capture ID, raw content, source, capture method, timestamp
- **Consumers**: Inbox processor, processing queue, later refinement

#### WebClipSaved
- **Description**: Content from web has been saved to knowledge base
- **Triggered When**: User clips article or webpage
- **Key Data**: Clip ID, source URL, content, annotations, timestamp
- **Consumers**: Web content archive, source tracker, reading list

#### HighlightCaptured
- **Description**: A highlighted passage has been saved
- **Triggered When**: User captures important excerpt
- **Key Data**: Highlight ID, source, highlighted text, context, personal notes, timestamp
- **Consumers**: Highlight collection, quote library, reference system

#### VoiceNoteTranscribed
- **Description**: Audio note has been converted to text
- **Triggered When**: Voice capture is processed
- **Key Data**: Transcription ID, audio file, transcribed text, accuracy, timestamp
- **Consumers**: Note creation, multi-modal capture, accessibility

### ReviewEvents

#### NoteReviewed
- **Description**: User has revisited and reviewed a note
- **Triggered When**: User re-reads or processes note
- **Key Data**: Review ID, note ID, review type, updates made, timestamp
- **Consumers**: Spaced repetition, knowledge retention, note evolution

#### InboxProcessed
- **Description**: Captured items have been organized and filed
- **Triggered When**: User processes quick capture inbox
- **Key Data**: Processing session ID, items processed, outcomes, timestamp
- **Consumers**: Inbox zero tracking, processing efficiency, organization flow

#### StaleNoteIdentified
- **Description**: A note hasn't been accessed in significant time
- **Triggered When**: Note exceeds inactivity threshold
- **Key Data**: Note ID, last accessed, inactivity duration, relevance question, timestamp
- **Consumers**: Review prompt, archive consideration, knowledge pruning

### VersioningEvents

#### NoteVersionCreated
- **Description**: A snapshot of note state has been saved
- **Triggered When**: Significant edit or scheduled versioning
- **Key Data**: Version ID, note ID, version number, content snapshot, timestamp
- **Consumers**: Version history, rollback capability, evolution tracking

#### NoteReverted
- **Description**: Note has been restored to previous version
- **Triggered When**: User undoes changes to note
- **Key Data**: Note ID, current version, reverted to version, revert reason, timestamp
- **Consumers**: Change management, error recovery, version control

### PublishingEvents

#### NotePublishedExternally
- **Description**: A note has been shared publicly or exported
- **Triggered When**: User publishes note outside knowledge base
- **Key Data**: Publish ID, note ID, publication platform, visibility, timestamp
- **Consumers**: Publishing tracker, external sync, share management

#### KnowledgeGraphExported
- **Description**: Knowledge base structure has been exported
- **Triggered When**: User exports graph visualization or data
- **Key Data**: Export ID, format, scope, destination, timestamp
- **Consumers**: Backup system, portability, external analysis

### MetricsEvents

#### KnowledgeGrowthMeasured
- **Description**: Expansion of knowledge base has been quantified
- **Triggered When**: Periodic analysis of knowledge accumulation
- **Key Data**: Metric ID, time period, notes added, connections created, growth rate, timestamp
- **Consumers**: Progress tracking, engagement monitoring, value demonstration

#### NoteQualityAssessed
- **Description**: Quality metrics for notes have been calculated
- **Triggered When**: Analysis evaluates note depth and usefulness
- **Key Data**: Quality ID, note ID, completeness score, link density, access frequency, timestamp
- **Consumers**: Quality improvement, prioritization, review scheduling

#### ConnectionDensityCalculated
- **Description**: Interconnectedness of knowledge base measured
- **Triggered When**: Graph analysis quantifies linking
- **Key Data**: Density ID, total notes, total links, density score, timestamp
- **Consumers**: Network health, knowledge integration, connection encouragement
