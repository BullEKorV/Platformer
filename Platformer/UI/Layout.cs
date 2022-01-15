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
        Raylib.DrawRectangleLinesEx(layout.area, 2, Color.BLACK);

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
        const int margin = 30;

        float height = Raylib.GetScreenHeight() / 9;
        float y = Math.Min(30, (area.height - height) / 2);
        float width = (area.width / buttons.Count) - margin - margin / buttons.Count;
        float x = (area.width / 2) - (width + margin + margin / buttons.Count) / 2 * buttons.Count + margin; ;

        for (int i = 0; i < buttons.Count; i++)
        {

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

            if (layouts[i].loadLevels) layouts[i].layouts = LoadLevelsToLayout();

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
    public static List<Layout> LoadLevelsToLayout()
    {
        // List<Button> tilesButtons = new List<Button>();

        string[] levelNames = Directory.GetFiles(@"levels\").OrderBy(i => i.Substring(4).Remove(0, 7)).ToArray();

        const int maxLevelsPerRow = 6;
        int amoutOfLayouts = (int)Math.Ceiling((double)levelNames.Length / maxLevelsPerRow);

        List<Layout> tilesLayouts = new List<Layout>();
        for (int i = 0; i < amoutOfLayouts; i++)
        {
            tilesLayouts.Add(new Layout(new List<Layout>(), new List<Button>(), true));
            tilesLayouts[i].buttons = new List<Button>();
            for (int y = 0; y < Math.Min(levelNames.Length - maxLevelsPerRow * i, 6); y++)
            {
                Console.WriteLine(Path.GetFileNameWithoutExtension(levelNames[y + i * maxLevelsPerRow]));
                tilesLayouts[i].buttons.Add(new Button(Path.GetFileNameWithoutExtension(levelNames[y + i * maxLevelsPerRow]), "LoadLevel", Path.GetFileNameWithoutExtension(levelNames[y + i * maxLevelsPerRow])));
            }
        }
        return tilesLayouts;
    }

}