# Tagging - Backend Requirements

## Overview
Manages photo tagging including people, locations, events, and custom tags.

## Domain Models
- **PersonTag**: Links photos to people with face coordinates
- **LocationTag**: Associates GPS coordinates and location names
- **EventTag**: Categorizes photos by events (birthday, wedding, etc.)
- **CustomTag**: User-defined keyword tags

## Commands
- TagPersonCommand
- TagLocationCommand
- TagEventCommand
- AddCustomTagCommand
- RemoveTagCommand

## Queries
- GetPhotosByPersonQuery
- GetPhotosByLocationQuery
- GetPhotosByEventQuery
- GetPhotosByTagQuery
- SearchTagsQuery

## API Endpoints
- POST /api/photos/{photoId}/tags/person
- POST /api/photos/{photoId}/tags/location
- POST /api/photos/{photoId}/tags/event
- POST /api/photos/{photoId}/tags/custom
- DELETE /api/photos/{photoId}/tags/{tagId}
- GET /api/tags/search
