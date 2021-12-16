public class Opossum : Enemy
{
    public Opossum(Vector2 pos) : base()
    {
        animation = Animation.allAnimations["opossum"];

        // Gives a starting random frame
        Random rnd = new Random();
        currentFrame = rnd.Next(0, animation.frames.Count * Animation.framesPerFrame);

        // Define player hitbox
        Vector2 hitboxSize = new Vector2(26 * scale, 14 * scale);
        rect = new Rectangle(pos.X, pos.Y, hitboxSize.X, hitboxSize.Y);

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
    }
}
