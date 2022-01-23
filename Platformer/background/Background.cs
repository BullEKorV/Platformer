public class Background
{
    static List<Element> backs = new List<Element>();
    static List<Element> middles = new List<Element>();
    public static void Setup()
    {
        for (var i = -1; i < 1; i++)
        {
            backs.Add(new Element(new Vector2(Raylib.GetScreenWidth() * i, 0), Raylib.LoadTexture(@"background\back.png")));
        }
        for (var i = -1; i < 4; i++)
        {
            middles.Add(new Element(new Vector2(Raylib.LoadTexture(@"background\middle.png").width * 5f * i, Raylib.GetScreenHeight() * 0.4f), Raylib.LoadTexture(@"background\middle.png")));
        }
    }
    public static void Update()
    {
        for (var i = 0; i < backs.Count; i++)
        {
            backs[i].pos.X -= ((Player)GameObject.gameObjects.Find(x => x is Player)).GetVelocity().X / 300 * Program.timeScale;
            if (backs[i].pos.X > Raylib.GetScreenWidth()) backs[i].pos.X -= Raylib.GetScreenWidth() * 2;
            else if (backs[i].pos.X < -Raylib.GetScreenWidth()) backs[i].pos.X += Raylib.GetScreenWidth() * 2;
        }

        // for (var i = 0; i < middles.Count; i++)
        // {
        //     float lowest = middles.Min(x => x.pos.X);
        //     float highest = middles.Max(x => x.pos.X);

        //     middles[i].pos.X -= ((Player)GameObject.gameObjects.Find(x => x is Player)).GetVelocity().X / 200 * Program.timeScale;
        //     if (middles[i].pos.X >= middles[i].texture.width * 5f * middles.Count)
        //     {
        //         middles[i].pos.X -= lowest - middles[i].texture.width * 4f;
        //         Console.WriteLine("Right");
        //     }

        //     else if (middles[i].pos.X < -middles[i].texture.width * 5f)
        //     {
        //         middles[i].pos.X = highest + middles[i].texture.width * 4f;
        //         Console.WriteLine("Left");
        //     }
        // }
    }
    public static void Draw()
    {
        foreach (Element back in backs)
        {
            DrawTexture(back.texture, back.pos, 1, 1);
        }
        foreach (Element middle in middles)
        {
            DrawTexture(middle.texture, middle.pos, 2f, 0.5f);
        }
    }
    private static void DrawTexture(Texture2D texture, Vector2 position, float amountOfHeight, float amountOfWidth)
    {
        // Draw texture with parameters and stuff (idk 100%, it works)
        Vector2 size = new Vector2(texture.width, texture.height);

        size.Y = Raylib.GetScreenHeight() * amountOfHeight;
        size.X = Raylib.GetScreenWidth() * amountOfWidth;

        Rectangle sourceRec = new Rectangle(0, 0, texture.width, texture.height);

        Rectangle destRec = new Rectangle(position.X, position.Y, size.X, size.Y);

        Raylib.DrawTexturePro(texture, sourceRec, destRec, new Vector2(0, 0), 0, Color.WHITE);
    }
}