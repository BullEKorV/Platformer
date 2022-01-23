
public class Tile : GameObject
{
    public static Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();
    public bool hasCollision = true;
    public Tile(Vector2 gridPos, string name)
    {
        texture = textures[name];

        id = name;

        if (id == "marker") hasCollision = false;

        Vector2 worldPos = gridPos * 16 * scale;

        // Define tile hitbox
        Vector2 hitboxSize = new Vector2(16 * scale, 16 * scale);
        rect = new Rectangle(worldPos.X, worldPos.Y, hitboxSize.X, hitboxSize.Y);
    }
    public static void LoadTexturesFromDirectory()
    {
        string root = @"tiles\";

        string[] tiles = Directory.GetFiles(root);
        foreach (string tile in tiles)
        {
            textures.Add(Path.GetFileNameWithoutExtension(tile), Raylib.LoadTexture(tile));
        }
    }
}
