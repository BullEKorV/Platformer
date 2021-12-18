public class Opossum : Enemy
{
    public Opossum(Vector2 pos) : base()
    {
        speed = 30;
        mass = 1; // Controll gravity

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
    }
}
