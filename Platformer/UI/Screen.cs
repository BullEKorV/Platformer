public class Screen
{
    public string name { get; set; }
    public Layout layout { get; set; }
    public Screen(string name, Layout layout)
    {
        this.name = name;
        this.layout = layout;
        this.layout.area = new Rectangle(0, 0, Raylib.GetScreenWidth(), Raylib.GetScreenHeight());
    }
    public void Update()
    {
        if (layout.layouts != null) layout.Update();
    }
    public void Draw()
    {
        // Write screen name
        int fontSize = Raylib.GetScreenHeight() / 7;
        Raylib.DrawText(name, Raylib.GetScreenWidth() / 2 - Raylib.MeasureText(name, fontSize) / 2, Raylib.GetScreenHeight() / 18, fontSize, Color.WHITE);

        if (layout.layouts != null) layout.Draw();
    }
    public static List<Screen> LoadScreensFromJSON()
    {
        // Fetch screens and deserialize them
        string root = @"UI\screens\";
        string[] filePaths = Directory.GetFiles(root);

        List<Screen> screens = new List<Screen>();

        foreach (string file in filePaths)
        {
            string response = File.ReadAllText(file);
            screens.Add(JsonSerializer.Deserialize<Screen>(response));
        }

        return screens;
    }
}