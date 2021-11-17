
public class GameObject
{
    public static List<GameObject> gameObjects = new List<GameObject>();
    Rectangle rect;
    Texture2D texture;
    public GameObject()
    {
        gameObjects.Add(this);
    }
    public void Draw()
    {
        // Raylib.DrawTextureRec()
    }
}
