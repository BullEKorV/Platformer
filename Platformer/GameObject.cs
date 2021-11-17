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
        Raylib.DrawTextureRec(texture, rect, new Vector2(rect.x, rect.y), Color.WHITE);
    }
}
