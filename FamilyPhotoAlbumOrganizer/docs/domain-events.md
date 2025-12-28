# Domain Events - Family Photo Album Organizer

## Overview
This document defines the domain events tracked in the Family Photo Album Organizer application. These events capture significant business occurrences related to photo management, album organization, sharing, tagging, and family memory preservation.

## Events

### PhotoEvents

#### PhotoUploaded
- **Description**: New photo has been added to the family collection
- **Triggered When**: User uploads photo from device or imports from external source
- **Key Data**: Photo ID, file name, upload date, file size, dimensions, format, EXIF data, uploaded by, source device
- **Consumers**: Photo library, thumbnail generator, metadata extractor, backup service, storage manager

#### PhotoEdited
- **Description**: Photo has been modified or enhanced
- **Triggered When**: User applies edits, filters, or adjustments to photo
- **Key Data**: Photo ID, edit type, edit timestamp, editor, original preserved, edit parameters applied
- **Consumers**: Photo versioning, edit history, rendering service, backup updater

#### PhotoDeleted
- **Description**: Photo has been removed from collection
- **Triggered When**: User deletes photo permanently or moves to trash
- **Key Data**: Photo ID, deletion date, deleted by, deletion reason, soft delete flag, albums affected
- **Consumers**: Storage reclaimer, album updater, trash manager, backup cleaner, audit log

#### PhotoRestored
- **Description**: Previously deleted photo has been recovered
- **Triggered When**: User restores photo from trash
- **Key Data**: Photo ID, restoration date, restored by, original deletion date, albums to restore to
- **Consumers**: Photo library, album re-adder, metadata restorer, backup service

### AlbumEvents

#### AlbumCreated
- **Description**: New photo album has been created
- **Triggered When**: User creates collection to organize photos
- **Key Data**: Album ID, album name, description, created by, creation date, privacy setting, theme/category
- **Consumers**: Album library, organization system, sharing manager, search indexer

#### PhotoAddedToAlbum
- **Description**: Photo has been placed into album
- **Triggered When**: User adds existing photo to album or uploads directly to album
- **Key Data**: Album ID, photo ID, added date, added by, photo order/sequence
- **Consumers**: Album view, photo count updater, album cover selector, chronological sorter

#### PhotoRemovedFromAlbum
- **Description**: Photo has been taken out of album
- **Triggered When**: User removes photo from album without deleting photo
- **Key Data**: Album ID, photo ID, removal date, removed by, photo still in other albums flag
- **Consumers**: Album updater, orphaned photo detector, album count adjuster

#### AlbumReorganized
- **Description**: Photos within album have been reordered
- **Triggered When**: User changes photo sequence or applies new sorting
- **Key Data**: Album ID, reorganization date, new sort order, sort criteria, reorganized by
- **Consumers**: Album view, display renderer, sort preference saver

#### AlbumShared
- **Description**: Album has been made accessible to others
- **Triggered When**: User shares album with family members or external recipients
- **Key Data**: Album ID, share date, shared by, recipients, permission level, access method, expiration date
- **Consumers**: Sharing service, notification sender, access control, collaboration manager

### TaggingEvents

#### PersonTagged
- **Description**: Person has been identified in photo
- **Triggered When**: User manually tags or AI recognizes face in photo
- **Key Data**: Photo ID, person ID, tag location coordinates, confidence level, tagged by, tag method, verification status
- **Consumers**: Face recognition training, person photo collection, search indexer, notification service

#### LocationTagged
- **Description**: Geographic location has been associated with photo
- **Triggered When**: User adds location or system extracts GPS from EXIF
- **Key Data**: Photo ID, location name, coordinates, location type, tagged date, source (manual/EXIF/AI)
- **Consumers**: Map view, location-based search, trip album generator, geo analytics

#### EventTagged
- **Description**: Photo has been categorized under specific event or occasion
- **Triggered When**: User assigns event label to photo
- **Key Data**: Photo ID, event name, event date, event type, tagged by, auto-suggest accepted
- **Consumers**: Event album generator, timeline view, search filter, memory collections

#### CustomTagAdded
- **Description**: Descriptive keyword tag has been applied to photo
- **Triggered When**: User adds custom label or category to photo
- **Key Data**: Photo ID, tag text, tag category, tagged date, tagged by
- **Consumers**: Search indexer, photo discovery, tag cloud, filtering system

### SharingEvents

#### ShareLinkGenerated
- **Description**: Shareable link has been created for photo or album
- **Triggered When**: User generates link for sharing content
- **Key Data**: Link ID, content type, content ID, link URL, created by, creation date, expiration date, password protected, view limit
- **Consumers**: Link manager, access control, analytics tracker, expiration monitor

#### ShareLinkAccessed
- **Description**: Shared content has been viewed via share link
- **Triggered When**: Recipient opens shared link
- **Key Data**: Link ID, access timestamp, viewer IP/location, viewer device, photos viewed, download count
- **Consumers**: Analytics service, access log, popular content tracker, security monitor

#### CollaboratorAdded
- **Description**: New person has been granted collaboration access to album
- **Triggered When**: User invites collaborator to contribute to album
- **Key Data**: Album ID, collaborator ID, invitation date, invited by, permission level, acceptance status
- **Consumers**: Collaboration manager, notification service, permission controller, activity tracker

#### CollaborativePhotoAdded
- **Description**: Collaborator has contributed photo to shared album
- **Triggered When**: Invited collaborator uploads photo to shared album
- **Key Data**: Album ID, photo ID, contributor ID, contribution date, moderation required
- **Consumers**: Album updater, owner notification, moderation queue, collaboration analytics

### OrganizationEvents

#### SmartAlbumCreated
- **Description**: Auto-generated album based on AI analysis has been created
- **Triggered When**: System identifies photo collection matching criteria (faces, locations, dates)
- **Key Data**: Album ID, album type, selection criteria, photo count, confidence score, creation date, accepted by user
- **Consumers**: Album library, user notification, AI training feedback, organization suggestions

#### DuplicatePhotosDetected
- **Description**: Identical or very similar photos have been identified
- **Triggered When**: System analyzes photos for duplicates
- **Key Data**: Photo group ID, photo IDs in group, similarity score, detection date, resolution suggestion
- **Consumers**: Duplicate manager, storage optimizer, deletion suggester, user notification

#### TimelineGenerated
- **Description**: Chronological photo timeline has been constructed
- **Triggered When**: System organizes photos by date for viewing
- **Key Data**: Timeline ID, date range, photo count, time gaps identified, milestone dates
- **Consumers**: Timeline viewer, memory lane feature, year-in-review generator

### MemoryEvents

#### MemoryCreated
- **Description**: Curated memory collection has been generated
- **Triggered When**: System creates "On This Day" or thematic memory collection
- **Key Data**: Memory ID, memory type, photos included, date significance, theme, creation date
- **Consumers**: Memory notifications, nostalgia feature, sharing suggester, engagement driver

#### MemoryShared
- **Description**: Generated memory has been shared with family
- **Triggered When**: User shares system-generated memory
- **Key Data**: Memory ID, shared with, share date, shared by, reactions received
- **Consumers**: Sharing analytics, engagement tracker, memory popularity, reaction collector

#### AnniversaryDetected
- **Description**: Significant photo anniversary has been identified
- **Triggered When**: System finds photos from 1, 5, 10+ years ago
- **Key Data**: Original photo date, anniversary type, years ago, photos from that time, notification sent
- **Consumers**: Anniversary notifier, memory generator, nostalgia content, celebration trigger

### BackupEvents

#### BackupCompleted
- **Description**: Photos have been successfully backed up to cloud storage
- **Triggered When**: Backup process finishes successfully
- **Key Data**: Backup ID, backup date, photo count, total size, backup destination, backup duration
- **Consumers**: Backup status dashboard, reliability monitor, storage usage tracker

#### BackupFailed
- **Description**: Photo backup attempt has encountered error
- **Triggered When**: Backup process fails or is interrupted
- **Key Data**: Backup ID, failure date, error type, photos affected, retry count, failure reason
- **Consumers**: Error handler, retry scheduler, user alert, support diagnostic

#### CloudSyncConflictDetected
- **Description**: Conflicting versions of photo found across devices
- **Triggered When**: Sync detects different versions of same photo
- **Key Data**: Photo ID, devices involved, modification dates, file differences, resolution options
- **Consumers**: Conflict resolver, user notification, version selector, sync pauser

### SearchEvents

#### PhotoSearchPerformed
- **Description**: User has searched for photos
- **Triggered When**: User executes search query in photo library
- **Key Data**: Search query, search filters, results count, search timestamp, user ID, search type
- **Consumers**: Search analytics, popular searches tracker, AI improvement, relevance tuner

#### VisualSimilaritySearchExecuted
- **Description**: User has searched for visually similar photos
- **Triggered When**: User initiates "find similar" feature
- **Key Data**: Source photo ID, similarity threshold, results found, search date, algorithm version
- **Consumers**: AI training, similarity algorithm, results ranker, feature usage tracker

### PrintEvents

#### PrintOrderCreated
- **Description**: Photos have been selected for physical printing
- **Triggered When**: User orders prints, photo books, or other physical products
- **Key Data**: Order ID, photos selected, product type, quantity, order date, delivery address, total cost
- **Consumers**: Print fulfillment service, order tracker, cost tracker, product catalog

#### PhotoBookGenerated
- **Description**: Custom photo book has been designed
- **Triggered When**: User or system creates photo book layout
- **Key Data**: Book ID, photos included, layout template, page count, creation date, customization details
- **Consumers**: Print service, preview generator, order system, saved projects
