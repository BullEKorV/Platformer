public class GameObject
{
    public static List<GameObject> gameObjects = new List<GameObject>();
    protected Rectangle rect;
    protected Texture2D texture;
    public GameObject()
    {
        gameObjects.Add(this);
    }
    public void Draw()
    {
        // BAD ???
        // Raylib.DrawRectangle((int)rect.x, (int)rect.y, (int)rect.width, (int)rect.height, Color.BROWN);
        Raylib.DrawRectangleRec(rect, Color.GOLD);
        Raylib.DrawTextureEx(texture, new Vector2(rect.x, rect.y), 0, rect.width / texture.width, Color.WHITE);
    }
}
