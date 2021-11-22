public class Opussom : Enemy
{
    public Opussom() : base()
    {
        animation = Animation.allAnimations["opossum"];

        // Define player hitbox
        Vector2 hitboxSize = new Vector2(130, 90);
        rect = new Rectangle(200, 200, hitboxSize.X, hitboxSize.Y);
        // Match texture cord with hitbox

        xOffsetDiff = 15;
        xOffsetBase = -25;
        yOffset = 50;
        int xOffset = -25 + (lookingRight ? -xOffsetDiff : xOffsetDiff);

        textureOffset = new Vector2(xOffset, yOffset);
    }
    public override void Update()
    {
        base.Update();
    }
}
