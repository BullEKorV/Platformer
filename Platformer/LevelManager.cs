using System.Text.Json;
public class LevelManager
{
    public static List<Level> allLevels = new List<Level>();
    public static int currentLevel = 1;

    public static void LoadLevel()
    {
        ClearLevel();
        Level lvl = GetLevelJson();

        // Load tiles and enemies to gameobjects
        foreach (JsonGameobject tile in lvl.tiles)
        {
            new Tile(new Vector2(tile.x, tile.y), tile.type);
        }
        foreach (JsonGameobject enemy in lvl.enemies)
        {
            if (enemy.type == "opossum")
                new Opossum(new Vector2(enemy.x, enemy.y));
        }

        currentLevel++;
    }
    static void ClearLevel()
    {
        GameObject.gameObjects.RemoveAll(x => (x is Tile));
        GameObject.gameObjects.RemoveAll(x => (x is Enemy));
    }
    static Level GetLevelJson()
    {
        string root = @"levels\";

        string[] levelsDir = Directory.GetFiles(root);

        string response = File.ReadAllText(levelsDir[currentLevel - 1]);

        Level level = JsonSerializer.Deserialize<Level>(response);

        return level;
    }
}

public class Level
{
    public int level { get; set; }
    public List<JsonGameobject> tiles { get; set; }
    public List<JsonGameobject> enemies { get; set; }
}
public class JsonGameobject
{
    public string type { get; set; }
    public int x { get; set; }
    public int y { get; set; }
}
