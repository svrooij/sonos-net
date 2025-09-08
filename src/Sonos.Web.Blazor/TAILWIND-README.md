# Tailwind CSS Configuration for Sonos.Web.Blazor

This document explains how Tailwind CSS is configured in the Blazor WebAssembly project.

## ?? File Structure

```
src/Sonos.Web.Blazor/
??? package.json                 # npm dependencies and scripts
??? tailwind.config.js          # Tailwind configuration
??? Styles/
?   ??? app.css                  # Tailwind input file with custom styles
??? wwwroot/css/
?   ??? tailwind.css            # Generated Tailwind CSS (auto-generated)
??? watch-tailwind.bat          # Windows watch script
??? watch-tailwind.sh           # Linux/Mac watch script
??? .gitignore                  # Excludes node_modules and generated CSS
```

## ?? Quick Start

### Initial Setup (First Time Only)
1. Navigate to the Blazor project directory
2. Run `npm install` to install Tailwind and plugins
3. Build CSS: `npm run build-css-prod`

### Development Workflow

**Option 1: Automatic Build (Recommended)**
- Just build/publish your project - Tailwind CSS builds automatically via MSBuild

**Option 2: Watch Mode for Active Development**
```bash
# Windows
watch-tailwind.bat

# Linux/Mac  
./watch-tailwind.sh

# Or directly
npm run build-css
```

## ?? Configuration Details

### 1. **package.json Scripts**
- `build-css`: Watch mode - rebuilds CSS on file changes
- `build-css-prod`: Production build - minified CSS

### 2. **tailwind.config.js Features**
- **Content Scanning**: Monitors `.razor`, `.html`, `.cshtml`, and `.cs` files
- **Custom Colors**: `sonos-primary` (#667eea) and `sonos-secondary` (#764ba2)  
- **Extended Animations**: `pulse-slow`, `spin-slow`
- **Plugins**: Forms and Typography support

### 3. **Custom Components in Styles/app.css**
```css
@layer components {
  .btn-primary     /* Sonos-themed buttons */
  .card           /* Modern card design */
  .nav-link       /* Navigation styling */
  .music-player   /* Music player container */
}

@layer utilities {
  .text-gradient  /* Gradient text effect */
  .glass-effect   /* Glass morphism */
}
```

### 4. **MSBuild Integration**
The project automatically:
- Installs npm packages before build
- Generates CSS during build/publish  
- Cleans generated CSS on clean

## ?? Usage Examples

### Basic Tailwind Classes
```razor
<div class="bg-gray-100 p-4 rounded-lg shadow-md">
    <h1 class="text-2xl font-bold text-sonos-primary">Title</h1>
    <p class="text-gray-600 mt-2">Content</p>
</div>
```

### Custom Components
```razor
<button class="btn-primary">Click Me</button>
<div class="card">
    <h2 class="text-gradient">Gradient Text</h2>
</div>
```

### Responsive Design
```razor
<div class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4">
    <!-- Responsive grid -->
</div>
```

## ?? Development Commands

| Command | Purpose |
|---------|---------|
| `npm install` | Install/update dependencies |
| `npm run build-css` | Watch mode for development |
| `npm run build-css-prod` | Production build |
| `dotnet build` | Builds project + auto-generates CSS |
| `dotnet publish` | Publishes with optimized CSS |

## ?? Included Plugins

- **@tailwindcss/forms**: Better form styling
- **@tailwindcss/typography**: Rich text styling with `.prose` classes

## ?? Production Optimization

- CSS is automatically minified during build
- Unused CSS is purged based on content scanning
- Build process is optimized for CI/CD pipelines

## ?? Troubleshooting

**CSS not updating?**
1. Check if `wwwroot/css/tailwind.css` exists
2. Run `npm run build-css-prod` manually
3. Clear browser cache
4. Verify content paths in `tailwind.config.js`

**Build failing?**
1. Ensure Node.js is installed
2. Run `npm install` in project directory
3. Check file paths in MSBuild targets

**Watch mode not working?**
- Use the provided batch/shell scripts
- Ensure npm packages are installed
- Check that input file `Styles/app.css` exists

## ?? CSS Loading Order

The CSS is loaded in this order in `index.html`:
1. `tailwind.css` (Tailwind utilities)
2. `bootstrap.min.css` (Bootstrap components)
3. `app.css` (Custom styles)
4. `music-player.css` (Component-specific styles)

This ensures Tailwind provides the base, with custom styles able to override as needed.