public class UI // IS CALCULATED FROM TOP LEFT
{
    public static List<Screen> allScreens = new List<Screen>();
    public static Screen currentScreen;
    public UI()
    {
        allScreens = Screen.LoadScreensFromJSON();
        currentScreen = allScreens.Find(x => x.name == "Main Menu");
    }
    public static void Update()
    {

    }
    public static void Draw()
    {
        Player player = (Player)GameObject.gameObjects.Find(x => x is Player);
        for (var i = 0; i < player.hp; i++)
        {
            Raylib.DrawRectangle(i * 80, 0, 20, 20, Color.RED);
        }
        string scoreText = "Score: " + player.score.ToString();
        int margin = Raylib.GetScreenHeight() / 54;
        int fontSize = Raylib.GetScreenHeight() / 21;
        Raylib.DrawText(scoreText, Raylib.GetScreenWidth() - Raylib.MeasureText(scoreText, fontSize) - margin, margin, fontSize, Color.BLACK);

        // Make background darker
        if (currentScreen.name == "Pause") Raylib.DrawRectangle(0, 0, Raylib.GetScreenWidth(), Raylib.GetScreenHeight(), new Color(0, 0, 0, 175));
        else if (currentScreen.name != "") Raylib.ClearBackground(Color.SKYBLUE);

        // Raylib.DrawLine(0, Raylib.GetScreenHeight() / 2, Raylib.GetScreenWidth(), Raylib.GetScreenHeight() / 2, Color.BLUE); // center line
        // Raylib.DrawLine(Raylib.GetScreenWidth() / 2, 0, Raylib.GetScreenWidth() / 2, Raylib.GetScreenHeight(), Color.BLUE); // center line

    }
}