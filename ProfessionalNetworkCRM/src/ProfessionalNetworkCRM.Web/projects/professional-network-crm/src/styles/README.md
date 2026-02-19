# Design Tokens - Spacing System

This document describes the design token system used in the Professional Network CRM application.

## Overview

The application uses an **8px base unit system** for consistent spacing across all components. All spacing values are multiples of 4px or 8px to maintain a harmonious visual rhythm.

## Spacing Scale

| Token | Value | Pixels | Usage |
|-------|-------|--------|-------|
| `$spacing-0` | 0 | 0px | No spacing |
| `$spacing-1` | 0.25rem | 4px | Minimal spacing |
| `$spacing-2` | 0.5rem | 8px | Extra small |
| `$spacing-3` | 0.75rem | 12px | Small |
| `$spacing-4` | 1rem | 16px | Standard |
| `$spacing-5` | 1.25rem | 20px | Medium-small |
| `$spacing-6` | 1.5rem | 24px | Medium |
| `$spacing-7` | 1.75rem | 28px | Medium-large |
| `$spacing-8` | 2rem | 32px | Large |
| `$spacing-10` | 2.5rem | 40px | Extra large |
| `$spacing-12` | 3rem | 48px | XXL |
| `$spacing-16` | 4rem | 64px | XXXL |

## Semantic Spacing Tokens

These tokens provide meaningful names for specific use cases:

### Page Layout
- `$space-page-padding: 2rem (32px)` - Main page container padding
- `$space-section-gap: 2rem (32px)` - Gap between major sections
- `$space-header-margin: 2rem (32px)` - Margin below page headers

### Cards & Components
- `$space-card-padding: 1.5rem (24px)` - Card internal padding
- `$space-card-gap: 1.5rem (24px)` - Gap between cards

### Elements
- `$space-element-gap: 1rem (16px)` - Gap between related elements
- `$space-element-gap-sm: 0.5rem (8px)` - Small gap for tightly related elements
- `$space-inline-gap: 0.5rem (8px)` - Gap for inline elements

### Forms
- `$space-form-field-gap: 1rem (16px)` - Gap between form fields

### Buttons & Actions
- `$space-button-gap: 1rem (16px)` - Gap between buttons
- `$space-section-margin: 1.5rem (24px)` - Margin between sections

### Lists
- `$space-item-spacing: 0.5rem (8px)` - Spacing between list items

## Sidebar
- `$sidebar-width: 250px` - Fixed sidebar width
- `$sidebar-padding: 1rem (16px)` - Sidebar padding
- `$sidebar-item-gap: 0.25rem (4px)` - Gap between sidebar items

## Border Radius
- `$border-radius-sm: 4px` - Small radius for buttons, inputs
- `$border-radius-md: 8px` - Medium radius for cards
- `$border-radius-lg: 12px` - Large radius for modals
- `$border-radius-full: 9999px` - Full rounded (pills, avatars)

## Container Max Widths
- `$container-sm: 640px` - Small container (mobile)
- `$container-md: 768px` - Medium container (forms)
- `$container-lg: 1024px` - Large container (content)
- `$container-xl: 1280px` - Extra large container (wide layouts)

## Grid Gaps
- `$grid-gap-sm: 1rem (16px)` - Small grid gap
- `$grid-gap-md: 1.5rem (24px)` - Medium grid gap
- `$grid-gap-lg: 2rem (32px)` - Large grid gap

## Usage Examples

### In SCSS Files

```scss
@use '../../styles/tokens' as *;

.my-component {
  padding: $space-page-padding;
  margin-bottom: $space-header-margin;
  
  &__card {
    padding: $space-card-padding;
    border-radius: $border-radius-md;
    gap: $space-element-gap;
  }
}
```

### Using Utility Classes

For quick spacing adjustments in templates:

```html
<div class="mb-8">Content with bottom margin</div>
<div class="p-4 gap-4">Content with padding and gap</div>
```

Available utility classes:
- Margin: `.m-{size}`, `.mt-{size}`, `.mb-{size}`, `.ml-{size}`, `.mr-{size}`
- Padding: `.p-{size}`, `.pt-{size}`, `.pb-{size}`
- Gap: `.gap-{size}`
- Sizes: 0, 2, 4, 6, 8

## Best Practices

1. **Always use tokens**: Never use arbitrary spacing values. Always reference design tokens.

2. **Semantic over specific**: Prefer semantic tokens (`$space-page-padding`) over raw values (`$spacing-8`).

3. **Consistent patterns**: 
   - Page containers: `$space-page-padding`
   - Headers: `$space-header-margin` for bottom margin
   - Cards: `$space-card-padding` for internal padding
   - Forms: `$space-form-field-gap` between fields
   - Buttons: `$space-button-gap` between buttons

4. **Responsive spacing**: Consider reducing spacing on mobile devices if needed.

5. **Component spacing**: Let parent containers handle spacing between components rather than adding margins to components themselves.

## Migration Guide

When updating existing components:

1. Import tokens at the top: `@use '../../styles/tokens' as *;`
2. Replace hardcoded values with appropriate tokens
3. Use semantic tokens when available
4. Test visual appearance after changes

## Related Files

- `src/styles/_tokens.scss` - Token definitions
- `src/styles/_utilities.scss` - Utility classes
- `src/styles.scss` - Global imports
