# Database ER Diagrams - How to View

I've created multiple ER diagram formats for the HotelBookingApp database. Choose the format that works best for you:

## 📊 Available Diagram Files

### 1. **ER_DIAGRAM.svg** ⭐ RECOMMENDED
**Visual SVG Diagram - Ready to View**

✅ **How to view:**
- Double-click the file to open in your browser
- Or right-click → Open With → Browser (Chrome, Firefox, Edge, etc.)
- Works on Windows, Mac, Linux
- High-quality, scalable vector graphics

**What it shows:**
- All 6 main entities (Users, Hotels, Rooms, RoomImages, Bookings, Reviews)
- All fields with data types
- Primary Keys (PK) in red
- Foreign Keys (FK) in green
- Relationships with cardinality (1:many)
- Legend and key relationships explanation

### 2. **DATABASE_DIAGRAM.mmd**
**Mermaid Diagram Format**

✅ **How to view:**
- Open in GitHub (automatic rendering)
- Use Mermaid Live Editor: https://mermaid.live/
- VS Code with Mermaid extension
- Notion, Obsidian, or other markdown editors

**How to use:**
1. Copy the entire content of `DATABASE_DIAGRAM.mmd`
2. Paste into https://mermaid.live/
3. The diagram will render automatically
4. Export as PNG or SVG

### 3. **DATABASE_DIAGRAM.puml**
**PlantUML Format**

✅ **How to view:**
- Online: http://www.plantuml.com/plantuml/uml/
- VS Code with PlantUML extension
- IntelliJ IDEA (built-in support)
- Command line: `java -jar plantuml.jar DATABASE_DIAGRAM.puml`

**How to use:**
1. Visit http://www.plantuml.com/plantuml/uml/
2. Copy content from `DATABASE_DIAGRAM.puml`
3. Paste in the text area
4. Click "Submit" to generate
5. Download PNG or SVG

### 4. **DATABASE_ER_DIAGRAM.md**
**Detailed ASCII Diagram + Documentation**

✅ **How to view:**
- Open in any text editor
- Best viewed in VS Code, Notepad++
- GitHub will render it nicely
- Contains ASCII art diagram + full documentation

**Contents:**
- ASCII ER Diagram
- Detailed entity descriptions
- Relationship explanations
- Database constraints
- Sample queries
- Migration history

### 5. **ER_DIAGRAM_VISUAL.md**
**Simplified Visual Diagram + Examples**

✅ **How to view:**
- Open in any markdown viewer
- VS Code, GitHub, or any text editor
- Contains simplified ASCII diagram

**Contents:**
- Simplified ASCII ER diagram
- Cardinality legend
- Table summaries
- Data flow examples
- C# LINQ query patterns

## 🎯 Quick Start Guide

### For Non-Technical Users:
**→ Open `ER_DIAGRAM.svg` in your browser**
This gives you a beautiful, interactive diagram you can zoom and pan.

### For Developers:
**→ Use `DATABASE_DIAGRAM.puml`** with PlantUML online editor
This format is best for documentation and can be easily modified.

### For GitHub/Markdown:
**→ Use `DATABASE_DIAGRAM.mmd`** for Mermaid
Renders automatically on GitHub and many markdown platforms.

### For Detailed Reference:
**→ Read `DATABASE_ER_DIAGRAM.md`**
Complete documentation with all entity details, constraints, and queries.

## 🖼️ What the Diagrams Show

All diagrams illustrate the same database structure:

### Entities (6 Main Tables):
1. **AspNetUsers** - User accounts with authentication
2. **Hotels** - Hotel information and location
3. **Rooms** - Room types with pricing and amenities
4. **RoomImages** - Image gallery for rooms
5. **Bookings** - Guest reservations
6. **Reviews** - Hotel ratings and reviews

### Relationships:
```
User → Bookings (1:many)
User → Reviews (1:many)
Hotel → Rooms (1:many)
Hotel → Reviews (1:many)
Room → RoomImages (1:many)
Room → Bookings (1:many)
```

## 🔧 Tools to Install (Optional)

### VS Code Extensions:
1. **Mermaid Preview** - View .mmd files
   ```
   ext install bierner.markdown-mermaid
   ```

2. **PlantUML** - View .puml files
   ```
   ext install jebbs.plantuml
   ```

3. **SVG Viewer** - View .svg files inline
   ```
   ext install cssho.vscode-svgviewer
   ```

### Online Tools (No Installation):
- Mermaid Live: https://mermaid.live/
- PlantUML Server: http://www.plantuml.com/plantuml/uml/
- SVG Viewer: Any modern web browser

## 📝 Diagram Legend

### Symbols Used:

| Symbol | Meaning |
|--------|---------|
| **PK** | Primary Key (Red) |
| **FK** | Foreign Key (Green) |
| **1** | One (Required) |
| **\*** | Many (Zero or More) |
| **||--||** | One-to-One relationship |
| **||--o{** | One-to-Many relationship |

### Data Types:
- **int** - Integer (4 bytes)
- **string** - Text/VARCHAR
- **decimal** - Decimal(18,2) for currency
- **datetime** - Date and time
- **bool** - Boolean (true/false)
- **enum** - Enumeration (predefined values)

## 💡 Tips

1. **Best for Presentations**: Use `ER_DIAGRAM.svg` - high quality and professional
2. **Best for Documentation**: Use `DATABASE_DIAGRAM.puml` - easy to maintain
3. **Best for GitHub**: Use `DATABASE_DIAGRAM.mmd` - automatic rendering
4. **Best for Learning**: Read `DATABASE_ER_DIAGRAM.md` - comprehensive details

## 🔄 Updating Diagrams

If the database schema changes:

1. Update the `.puml` file (easiest to edit)
2. Generate new diagrams from PlantUML
3. Update the SVG manually or use conversion tools
4. Update the markdown documentation

## 📚 Additional Resources

- **Entity Relationship Diagrams**: https://en.wikipedia.org/wiki/Entity%E2%80%93relationship_model
- **Mermaid Documentation**: https://mermaid.js.org/
- **PlantUML Guide**: https://plantuml.com/
- **EF Core Relationships**: https://learn.microsoft.com/en-us/ef/core/modeling/relationships

## ❓ Common Questions

**Q: Which file should I open first?**
A: Open `ER_DIAGRAM.svg` in your browser for the best visual experience.

**Q: Can I edit these diagrams?**
A: Yes! Edit `.puml` or `.mmd` files in a text editor, then regenerate.

**Q: How do I print the diagram?**
A: Open `ER_DIAGRAM.svg` in browser, then File → Print or Ctrl+P.

**Q: The SVG looks blurry**
A: SVG files are vectors - zoom in your browser and they stay sharp!

**Q: Can I use these in my documentation?**
A: Absolutely! All diagrams are part of the project documentation.

---

**Database**: SQLite (`hotelbooking.db`)
**ORM**: Entity Framework Core 8.0
**Tables**: 5 Core + ASP.NET Identity tables
**Migrations**: 2 applied (InitialCreate, AddProfilePicture)
