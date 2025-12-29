# Page Management - Frontend Requirements

## Components

### Page Editor
**Route**: `/pages/{id}/edit`

**Features**:
- Rich text WYSIWYG editor (TipTap/Quill)
- Markdown mode toggle
- Live preview pane
- Auto-save (every 30 seconds)
- Edit summary field
- Insert tools: links, images, code blocks, tables

### Page View
**Route**: `/pages/{id}`

**Features**:
- Rendered page content
- Table of contents (auto-generated)
- Edit/Delete/Rename buttons
- Page metadata (author, created, last modified)
- Backlinks section
- Related pages

### Page List
**Route**: `/pages`

**Features**:
- Searchable, filterable page list
- Sort by: title, date, views
- Filter by: namespace, category
- Bulk operations
