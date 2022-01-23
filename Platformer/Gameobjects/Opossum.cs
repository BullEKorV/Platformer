public class Opossum : Enemy
{
    GameObject lastTouched;
    public Opossum(Vector2 pos) : base()
    {
        speed = 5 * scale;
        mass = 1; // Controll gravity
        id = "opossum";

        animation = Animation.allAnimations["opossum"];

        // Gives a starting random frame
        Random rnd = new Random();
        currentFrame = rnd.Next(0, animation.frames.Count * Animation.framesPerFrame);

        // Define opossum hitbox
        Vector2 hitboxSize = new Vector2(26 * scale, 14 * scale);
        rect = new Rectangle(pos.X * 16 * scale, pos.Y * 16 * scale, hitboxSize.X, hitboxSize.Y);

        // Give texture an offset to match with hitbox
        xOffsetDiff = 3 * scale;
        xOffsetBase = -5 * scale;
        yOffset = 14 * scale;
        int xOffset = -5 * scale + (lookingRight ? -xOffsetDiff : xOffsetDiff);
        textureOffset = new Vector2(xOffset, yOffset);
    }
    public override void Update()
    {
        base.Update();

        if (IsTouchingMarker()) lookingRight = !lookingRight;

        velocity.X += lookingRight == true ? speed * Program.timeScale : -speed * Program.timeScale;
    }
    private bool IsTouchingMarker()
    {
        List<Tile> markers = new List<Tile>(gameObjects.FindAll(x => x is Tile).Cast<Tile>());

        foreach (Tile marker in markers)
        {
            if (marker != lastTouched && Raylib.CheckCollisionRecs(rect, marker.rect))
            {
                if (rect.x <= marker.rect.x + 5 && rect.x + rect.width >= marker.rect.x + marker.rect.width - 5)
                {
                    lastTouched = marker;
                    return true;
                }
            }
        }
        return false;
    }
}
