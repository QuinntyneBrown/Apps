# Domain Events - Personal Wiki

## Overview
This application enables users to create and maintain a personal wiki for organizing knowledge, documenting processes, and building a comprehensive reference system. Domain events capture page creation, content evolution, linking, and the growth of personal knowledge organization.

## Events

### PageEvents

#### PageCreated
- **Description**: A new wiki page has been created
- **Triggered When**: User creates a new page in the wiki
- **Key Data**: Page ID, user ID, page title, initial content, namespace, creation timestamp
- **Consumers**: Wiki index, search indexer, recent changes tracker, namespace organizer

#### PageUpdated
- **Description**: A wiki page has been edited
- **Triggered When**: User modifies page content
- **Key Data**: Page ID, editor ID, content changes, edit summary, version number, timestamp
- **Consumers**: Version history, change tracker, recent changes feed, watchers notification

#### PageDeleted
- **Description**: A wiki page has been removed
- **Triggered When**: User deletes a page
- **Key Data**: Page ID, deletion timestamp, deletion reason, soft/hard delete, last content snapshot, timestamp
- **Consumers**: Deletion log, link cleanup, page recovery system

#### PageRestored
- **Description**: A deleted wiki page has been recovered
- **Triggered When**: User restores page from deletion
- **Key Data**: Page ID, restoration timestamp, restored version, restoration reason, timestamp
- **Consumers**: Page index rebuilding, link restoration, recent changes

#### PageRenamed
- **Description**: A wiki page title has been changed
- **Triggered When**: User renames a page
- **Key Data**: Page ID, old title, new title, rename reason, redirect created, timestamp
- **Consumers**: Link updating, search index, redirect management, references updating

### LinkingEvents

#### InternalLinkCreated
- **Description**: A link to another wiki page has been added
- **Triggered When**: User creates hyperlink between pages
- **Key Data**: Link ID, source page ID, target page ID, link text, link context, timestamp
- **Consumers**: Link graph builder, backlink tracker, navigation enhancement, orphan page detection

#### ExternalLinkAdded
- **Description**: A link to external resource has been included
- **Triggered When**: User adds URL to outside content
- **Key Data**: Link ID, page ID, external URL, link description, timestamp
- **Consumers**: External references tracker, link validation, citation management

#### BrokenLinkDetected
- **Description**: A link to non-existent page has been identified
- **Triggered When**: Link points to deleted or non-existent page
- **Key Data**: Broken link ID, source page ID, target title, link context, timestamp
- **Consumers**: Link maintenance, page creation suggestions, quality monitoring

#### BacklinkUpdated
- **Description**: Backlink count or list for a page has changed
- **Triggered When**: New page links to existing page
- **Key Data**: Page ID, new backlink source, total backlinks, timestamp
- **Consumers**: Page popularity tracker, navigation enhancement, content importance

### VersioningEvents

#### VersionCreated
- **Description**: A new version of a page has been saved
- **Triggered When**: Page edit is committed
- **Key Data**: Version ID, page ID, version number, author, changes made, timestamp
- **Consumers**: Version history, diff generator, rollback capability, author contributions

#### PageReverted
- **Description**: A page has been rolled back to a previous version
- **Triggered When**: User restores earlier page version
- **Key Data**: Page ID, current version, reverted to version, revert reason, timestamp
- **Consumers**: Change log, version history, edit conflict prevention

#### VersionCompared
- **Description**: Two versions of a page have been compared
- **Triggered When**: User views diff between versions
- **Key Data**: Comparison ID, page ID, version 1, version 2, differences highlighted, timestamp
- **Consumers**: Change visualization, edit review, content evolution tracking

### NamespaceEvents

#### NamespaceCreated
- **Description**: A new organizational namespace has been established
- **Triggered When**: User creates category or section for pages
- **Key Data**: Namespace ID, namespace name, description, parent namespace, timestamp
- **Consumers**: Namespace hierarchy, organization system, navigation structure

#### PageMovedToNamespace
- **Description**: A page has been relocated to different namespace
- **Triggered When**: User reorganizes page categorization
- **Key Data**: Page ID, previous namespace, new namespace, move reason, timestamp
- **Consumers**: Organization updates, navigation rebuilding, categorization tracker

#### NamespaceHierarchyUpdated
- **Description**: Namespace structure has been reorganized
- **Triggered When**: User changes parent-child namespace relationships
- **Key Data**: Namespace ID, previous structure, new structure, affected pages, timestamp
- **Consumers**: Navigation regeneration, hierarchy visualization, breadcrumb updates

### TemplateEvents

#### TemplateCreated
- **Description**: A reusable page template has been designed
- **Triggered When**: User creates template for consistent page structure
- **Key Data**: Template ID, template name, template content, usage guidelines, timestamp
- **Consumers**: Template library, page creation assistance, consistency enforcement

#### TemplateApplied
- **Description**: A template has been used to create or format a page
- **Triggered When**: User applies template to page
- **Key Data**: Page ID, template ID, application timestamp, customizations made, timestamp
- **Consumers**: Template usage tracking, standardization monitoring, template effectiveness

#### TemplateUpdated
- **Description**: A template has been modified
- **Triggered When**: User edits template structure
- **Key Data**: Template ID, changes made, version number, affected pages count, timestamp
- **Consumers**: Template versioning, bulk update consideration, dependent page notification

### CategoryEvents

#### CategoryCreated
- **Description**: A new category for organizing pages has been established
- **Triggered When**: User creates classification category
- **Key Data**: Category ID, category name, description, parent category, timestamp
- **Consumers**: Category system, faceted navigation, taxonomy building

#### PageCategorized
- **Description**: A page has been assigned to one or more categories
- **Triggered When**: User adds category tags to page
- **Key Data**: Page ID, category IDs, categorization timestamp
- **Consumers**: Category index, filtered browsing, topic organization

#### CategoryMerged
- **Description**: Two categories have been combined
- **Triggered When**: User consolidates duplicate or similar categories
- **Key Data**: Primary category ID, merged category ID, affected pages, timestamp
- **Consumers**: Category cleanup, page recategorization, taxonomy simplification

### SearchEvents

#### PageSearched
- **Description**: User has searched the wiki
- **Triggered When**: Search query is executed
- **Key Data**: Search ID, query text, filters applied, results count, timestamp
- **Consumers**: Search analytics, content discovery, search optimization

#### SearchIndexRebuilt
- **Description**: Wiki search index has been regenerated
- **Triggered When**: Periodic or manual index reconstruction
- **Key Data**: Index ID, pages indexed, index completion time, timestamp
- **Consumers**: Search performance, content discoverability, index optimization

#### PopularPageIdentified
- **Description**: Frequently accessed page has been recognized
- **Triggered When**: Page view threshold exceeded
- **Key Data**: Page ID, view count, time period, access pattern, timestamp
- **Consumers**: Content prioritization, navigation prominence, popular content highlighting

### AttachmentEvents

#### FileUploaded
- **Description**: A file has been attached to wiki
- **Triggered When**: User uploads image, document, or other file
- **Key Data**: File ID, file name, file type, size, uploader ID, timestamp
- **Consumers**: Media library, storage management, file indexing

#### FileAttachedToPage
- **Description**: An uploaded file has been embedded in a page
- **Triggered When**: User includes file in page content
- **Key Data**: Attachment ID, file ID, page ID, attachment type, caption, timestamp
- **Consumers**: File usage tracking, page media inventory, orphan file detection

#### UnusedFileDetected
- **Description**: An uploaded file is not referenced by any page
- **Triggered When**: File has no page attachments
- **Key Data**: File ID, upload date, file size, deletion consideration, timestamp
- **Consumers**: Storage optimization, cleanup suggestions, orphan file management

### StructureEvents

#### TableOfContentsGenerated
- **Description**: Page navigation TOC has been created
- **Triggered When**: Page headers generate automatic TOC
- **Key Data**: Page ID, TOC structure, heading levels, timestamp
- **Consumers**: Page navigation, structure visualization, reading assistance

#### SectionEdited
- **Description**: A specific section of a page has been modified
- **Triggered When**: User edits page section independently
- **Key Data**: Page ID, section ID, section title, edit content, timestamp
- **Consumers**: Granular version control, targeted notifications, edit conflict reduction

#### PageHierarchyVisualized
- **Description**: Parent-child relationship of pages has been displayed
- **Triggered When**: User views page relationship tree
- **Key Data**: Root page ID, hierarchy depth, child pages, timestamp
- **Consumers**: Navigation tool, content organization view, structure understanding

### CollaborationEvents

#### PageWatched
- **Description**: User has subscribed to page change notifications
- **Triggered When**: User enables watch on specific page
- **Key Data**: Watch ID, page ID, user ID, notification preferences, timestamp
- **Consumers**: Notification service, change alerts, collaboration awareness

#### DiscussionStarted
- **Description**: A discussion thread about page has been initiated
- **Triggered When**: User posts to page discussion/talk page
- **Key Data**: Discussion ID, page ID, initiator ID, discussion topic, timestamp
- **Consumers**: Collaboration system, issue tracking, community engagement

#### EditConflictDetected
- **Description**: Two users have edited same page simultaneously
- **Triggered When**: Concurrent edits create conflict
- **Key Data**: Conflict ID, page ID, user IDs, conflicting changes, timestamp
- **Consumers**: Conflict resolution, merge assistance, collaboration coordination

### MaintenanceEvents

#### OrphanPageDetected
- **Description**: A page with no incoming links has been identified
- **Triggered When**: Page has zero backlinks
- **Key Data**: Page ID, creation date, orphan duration, timestamp
- **Consumers**: Link building suggestions, content integration, quality improvement

#### DeadEndPageIdentified
- **Description**: A page with no outgoing links has been found
- **Triggered When**: Page contains no internal links
- **Key Data**: Page ID, page length, potential link targets, timestamp
- **Consumers**: Linking suggestions, wiki connectivity, navigation enhancement

#### StubPageFlagged
- **Description**: A very short page needing expansion has been marked
- **Triggered When**: Page below length threshold
- **Key Data**: Page ID, current length, stub category, expansion suggestions, timestamp
- **Consumers**: Content improvement queue, expansion prompts, quality tracking

### ExportEvents

#### WikiExported
- **Description**: Wiki content has been exported
- **Triggered When**: User exports wiki data
- **Key Data**: Export ID, export format, scope (full/partial), timestamp
- **Consumers**: Backup system, data portability, external publishing

#### PagePrinted
- **Description**: A wiki page has been formatted for printing
- **Triggered When**: User generates print-friendly version
- **Key Data**: Page ID, print format, timestamp
- **Consumers**: Print optimization, offline access, documentation creation
