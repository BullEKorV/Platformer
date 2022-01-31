public class Layout
{
    public Rectangle area;
    public float ratio { get; set; } = 1;
    public List<Layout> layouts { get; set; } = new List<Layout>();
    public List<Button> buttons { get; set; } = new List<Button>();
    public bool isHorizontal { get; set; } = true;
    public bool loadTiles { get; set; } = false;
    public bool loadLevels { get; set; } = false;
    public Layout(List<Layout> layouts, List<Button> buttons, bool isHorizontal)
    {
        this.layouts = layouts;
        this.buttons = buttons;
        this.isHorizontal = isHorizontal;
    }
    public void Update()
    {
        // calculate layout and button areas
        layouts = CalculateLayoutArea(area, layouts, isHorizontal);

        //
        if (buttons != null)
        {
            foreach (Button button in buttons)
            {
                button.Update();
            }
        }
        foreach (Layout layout in layouts)
        {
            UpdateButtons(layout.buttons, layout.layouts);
        }
    }
    private void UpdateButtons(List<Button> buttons, List<Layout> layouts)
    {
        if (buttons != null)
        {
            foreach (Button button in buttons)
            {
                button.Update();
            }
        }
        if (layouts != null)
        {
            foreach (Layout layout in layouts)
            {
                UpdateButtons(layout.buttons, layout.layouts);
            }
        }
    }
    public void Draw()
    {
        foreach (Layout layout in layouts)
        {
            DrawLayouts(layout);
        }
    }
    private void DrawLayouts(Layout layout)
    {
        // Raylib.DrawRectangleLinesEx(layout.area, 2, Color.BLACK);

        // Draw each button in the layout
        if (layout.buttons != null)
        {
            foreach (Button button in layout.buttons)
            {
                button.Draw();
            }
        }

        // Draw each layout in the layout
        if (layout.layouts != null)
        {
            foreach (Layout layout1 in layout.layouts)
            {
                DrawLayouts(layout1);
            }
        }
    }
    private List<Button> CalculateButtonsRect(List<Button> buttons, Rectangle area, bool isHorizontal)
    {
        const int margin = 25;

        float height = Math.Min(Raylib.GetScreenHeight() / 9, area.height);
        float y = Math.Min(30, (area.height - height) / 2);
        float width = (area.width / buttons.Count) - margin - margin / buttons.Count;
        float x = (area.width / 2) - (width + margin + margin / buttons.Count) / 2 * buttons.Count + margin;

        for (int i = 0; i < buttons.Count; i++)
        {
            if (buttons[i].action == "SelectLevel" || buttons[i].action == "SelectTile") width = height;
            buttons[i].rect = new Rectangle(x + area.x, y + area.y, width, height);

            if (isHorizontal)
                x += width + margin;
            else y += height + margin;
        }
        return buttons;

    }
    private List<Layout> CalculateLayoutArea(Rectangle area, List<Layout> layouts, bool isHorizontal)
    {
        float width = area.width;
        float height = area.height;
        float x = area.x;
        float y = area.y;
        float total = layouts.Sum(item => item.ratio);

        for (int i = 0; i < layouts.Count; i++)
        {
            width = area.width;
            height = area.height;

            // Get height and width for layout
            if (isHorizontal)
                width = width / total * layouts[i].ratio;
            else
                height = height / total * layouts[i].ratio;

            layouts[i].area = new Rectangle(x, y, width, height);

            if (isHorizontal)
                x += width;
            else y += height;

            // Load levels if the time is right >:)
            if (layouts[i].loadLevels) layouts[i].layouts = LoadLevelsToLayout(area.height);

            // Load tiles if the time is right >:)
            if (layouts[i].loadTiles) layouts[i].layouts = LoadTilesToLayout(area.height);

            // Calculate all buttons for this layout if exist
            if (layouts[i].buttons != null)
                layouts[i].buttons = CalculateButtonsRect(layouts[i].buttons, layouts[i].area, layouts[i].isHorizontal);

            // Calculate all layouts for this layout if exist
            if (layouts[i].layouts != null)
            {
                layouts[i].layouts = CalculateLayoutArea(layouts[i].area, layouts[i].layouts, layouts[i].isHorizontal);
            }
        }
        return layouts;
    }
    public static List<Layout> LoadLevelsToLayout(float layoutHeight) // Make the buttons next to each other, also keep buttons same size // Probably rewrite a lil lol
    {
        string[] levelNames = Directory.GetFiles(@"levels\").OrderBy(i => i.Substring(4).Remove(0, 7)).ToArray();

        const int maxLevelsPerRow = 7;
        int amoutOfLayouts = (int)Math.Ceiling((double)levelNames.Length / maxLevelsPerRow);

        float margin = 25;
        float spaceNeeded = Math.Min(2 * ((Raylib.GetScreenHeight() / 9 * amoutOfLayouts) + 2 * (margin * (amoutOfLayouts + 1))), layoutHeight); // Dont really work... Fix pls

        List<Layout> tilesLayouts = new List<Layout>(3);
        for (var i = 0; i < 3; i++)
        {
            tilesLayouts.Add(new Layout(new List<Layout>(), null, false));
            // if (i == 1) tilesLayouts[i].ratio = spaceNeeded / ((layoutHeight - spaceNeeded) / 2);
            if (i == 1) tilesLayouts[i].ratio = spaceNeeded / layoutHeight;
            else tilesLayouts[i].ratio = ((layoutHeight - spaceNeeded) / 2) / layoutHeight;
        }

        Layout tileLayout = tilesLayouts[1]; // The tile that needs the levels buttons
        for (int i = 0; i < amoutOfLayouts; i++)
        {
            tileLayout.layouts.Add(new Layout(new List<Layout>(), new List<Button>(), true));
            tileLayout.layouts[i].buttons = new List<Button>();
            for (int y = 0; y < Math.Min(levelNames.Length - maxLevelsPerRow * i, maxLevelsPerRow); y++)
            {
                tileLayout.layouts[i].buttons.Add(new Button(Path.GetFileNameWithoutExtension(levelNames[y + i * maxLevelsPerRow]), "SelectLevel", Path.GetFileNameWithoutExtension(levelNames[y + i * maxLevelsPerRow])));
            }
        }
        return tilesLayouts;
    }
    public static List<Layout> LoadTilesToLayout(float layoutHeight) // Make the buttons next to each other, also keep buttons same size // Probably rewrite a lil lol
    {
        string[] tileNames = Directory.GetFiles(@"tiles\").OrderBy(i => i.Substring(4).Remove(0, 7)).ToArray();
        Console.WriteLine("UWU");
        const int maxTilesPerRow = 7;
        int amoutOfLayouts = (int)Math.Ceiling((double)tileNames.Length / maxTilesPerRow);

        float margin = Raylib.GetScreenHeight() / 28;
        float spaceNeeded = (Raylib.GetScreenHeight() / 9 * amoutOfLayouts) + margin * amoutOfLayouts * 2 * 2; // Dont really work... Fix pls

        List<Layout> tilesLayouts = new List<Layout>(3);
        for (var i = 0; i < 3; i++)
        {
            tilesLayouts.Add(new Layout(new List<Layout>(), null, false));
            if (i == 1) tilesLayouts[i].ratio = spaceNeeded / ((layoutHeight - spaceNeeded) / 2);
        }

        Layout tileLayout = tilesLayouts[1]; // The tile that needs the levels buttons
        for (int i = 0; i < amoutOfLayouts; i++)
        {
            tileLayout.layouts.Add(new Layout(new List<Layout>(), new List<Button>(), true));
            tileLayout.layouts[i].buttons = new List<Button>();
            for (int y = 0; y < Math.Min(tileNames.Length - maxTilesPerRow * i, maxTilesPerRow); y++)
            {
                if (Path.GetFileNameWithoutExtension(tileNames[y + i * maxTilesPerRow]) == "selector") continue;
                tileLayout.layouts[i].buttons.Add(new Button("", "SelectTile", Path.GetFileNameWithoutExtension(tileNames[y + i * maxTilesPerRow])));
                tileLayout.layouts[i].buttons.Last().texture = Tile.textures[Path.GetFileNameWithoutExtension(tileNames[y + i * maxTilesPerRow])];
            }
        }
        return tilesLayouts;
    }
}