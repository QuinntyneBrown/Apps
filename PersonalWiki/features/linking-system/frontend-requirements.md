# Linking System - Frontend Requirements

## Components

### Link Editor
**Component**: `<WikiLinkInserter />`

**Features**:
- [[WikiLink]] syntax support
- Page autocomplete dropdown
- Create new page from link
- External link formatting

### Backlinks Panel
**Component**: `<BacklinksPanel />`

**Features**:
- List of pages linking here
- Link context preview
- Navigate to source pages

### Link Graph Visualization
**Route**: `/graph`

**Features**:
- Interactive network graph (D3.js/Cytoscape)
- Node sizing by page importance
- Filter by namespace
- Click nodes to navigate
- Zoom/pan controls

### Broken Links Dashboard
**Route**: `/maintenance/broken-links`

**Features**:
- List all broken links
- Suggested fix actions
- Bulk link updates
- Create missing pages
