public class Cherry : Collectible
{
    public Cherry(Vector2 pos) : base()
    {
        animation = Animation.allAnimations["cherry"];
        id = "cherry";

        // Gives a starting random frame
        Random rnd = new Random();
        currentFrame = rnd.Next(0, animation.frames.Count * Animation.framesPerFrame);

        // Define gem hitbox
        Vector2 hitboxSize = new Vector2(14 * scale, 14 * scale);
        Vector2 distFromCorner = new Vector2((16 * scale - hitboxSize.X) / 2, (16 * scale - hitboxSize.Y) / 2);
        rect = new Rectangle(pos.X * 16 * scale + distFromCorner.X, pos.Y * 16 * scale + distFromCorner.Y, hitboxSize.X, hitboxSize.Y);

        // Give gem textureOffset
        textureOffset = new Vector2(-2 * scale, 3 * scale);
    }
    public override void OnCollision()
    {
        Player player = (Player)gameObjects.Find(x => x is (Player));
        player.hp++;
        isAlive = false;
    }
}