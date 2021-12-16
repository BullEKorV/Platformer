public class LevelManager
{
    public static List<Level> allLevels = new List<Level>();
    public static void LoadLevel(int level)
    {
        ClearLevel();
        Level lvl = GetLevelJson(level);

        // Reset player position to start position
        Player player = (Player)GameObject.gameObjects.Find(x => x is Player);
        player.ResetPos(new Vector2(lvl.startPos.x, lvl.startPos.y));
        Camera.MoveToPlayer(); // Move camera to player immideately

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
    }
    public static void ClearLevel()
    {
        GameObject.gameObjects.RemoveAll(x => (x is Tile));
        GameObject.gameObjects.RemoveAll(x => (x is Enemy));
    }
    static Level GetLevelJson(int lvl)
    {
        string[] levelsDir = Directory.GetFiles(@"levels\");

        string response = File.ReadAllText(levelsDir[lvl - 1]); // Level names start at 1, so therefore -1

        Level level = JsonSerializer.Deserialize<Level>(response);

        return level;
    }
}

public class Level
{
    public int level { get; set; }
    public StartPos startPos { get; set; }
    public List<JsonGameobject> tiles { get; set; }
    public List<JsonGameobject> enemies { get; set; }
}
public class JsonGameobject
{
    public string type { get; set; }
    public int x { get; set; }
    public int y { get; set; }
}
public class StartPos
{
    public int x { get; set; }
    public int y { get; set; }

}
