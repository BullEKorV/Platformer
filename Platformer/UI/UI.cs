public class UI // IS CALCULATED FROM TOP LEFT
{
    public static List<Screen> allScreens = new List<Screen>();
    public static Stack<Screen> history = new Stack<Screen>();
    public static Screen currentScreen;
    public static Dictionary<string, Texture2D> allTextures;
    public UI()
    {
        allScreens = Screen.LoadScreensFromJSON();
        currentScreen = allScreens.Find(x => x.name == "Main Menu");
        allTextures = LoadTextures();
    }
    public static void ChangeToScreen(string screen)
    {
        history.Push(currentScreen);
        currentScreen = UI.allScreens.Find(x => x.name == screen);
        if (screen == "Main Menu")
        {
            Createmode.EndCreatemode();
            history.Clear();
            LevelManager.ClearLevel();
        }
    }
    public static void ChangeToLastScreen()
    {
        if (history.Any()) currentScreen = history.Pop();
    }
    public static void Update()
    {

    }
    public static void Draw()
    {
        if ((currentScreen.name == "" || currentScreen.name == "Pause") && !Createmode.isActive) DrawPlayerStats();


        // Make background darker
        if (currentScreen.name == "Pause" || currentScreen.name == "Object Select" || currentScreen.name == "Pause Create") Raylib.DrawRectangle(0, 0, Raylib.GetScreenWidth(), Raylib.GetScreenHeight(), new Color(0, 0, 0, 175));
        else if (currentScreen.name != "") Raylib.ClearBackground(Color.SKYBLUE);



        Raylib.DrawLine(0, Raylib.GetScreenHeight() / 2, Raylib.GetScreenWidth(), Raylib.GetScreenHeight() / 2, Color.BLUE); // center line
        Raylib.DrawLine(Raylib.GetScreenWidth() / 2, 0, Raylib.GetScreenWidth() / 2, Raylib.GetScreenHeight(), Color.BLUE); // center line
    }
    private static void DrawPlayerStats()
    {
        Player player = (Player)GameObject.gameObjects.Find(x => x is Player);

        int fontSize = Raylib.GetScreenWidth() / 20;
        Raylib.DrawText(player.score.ToString(), Raylib.GetScreenWidth() - Raylib.MeasureText(player.score.ToString(), fontSize) - Raylib.GetScreenWidth() / 80, Raylib.GetScreenHeight() / 80, fontSize, Color.GOLD);
        Raylib.DrawLineEx(new Vector2(Raylib.GetScreenWidth() - Raylib.MeasureText(player.score.ToString(), fontSize) - Raylib.GetScreenWidth() / 80 - 10, Raylib.GetScreenHeight() / 80 + fontSize), new Vector2(Raylib.GetScreenWidth() - Raylib.GetScreenWidth() / 80 + 10, Raylib.GetScreenHeight() / 80 + fontSize), 10, Color.BLACK);

        int margin = 15;
        for (int i = 0; i < player.hp; i++)
        {
            Raylib.DrawTextureEx(allTextures["heart"], new Vector2(margin * 1.5f + i * allTextures["heart"].width * 5 + i * margin, Raylib.GetScreenHeight() / 80 * 1.5f), 0, 5, Color.WHITE);
        }
    }
    private static Dictionary<string, Texture2D> LoadTextures()
    {
        Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();

        string[] files = Directory.GetFiles(@"UI\textures\");
        foreach (string texture in files)
        {
            textures.Add(Path.GetFileNameWithoutExtension(texture), Raylib.LoadTexture(texture));
        }
        return textures;
    }
}