public class Opussom : Enemy
{
    public Opussom() : base()
    {
        animation = Animation.allAnimations["opossum"];

        // Define player hitbox
        Vector2 hitboxSize = new Vector2(130, 90);
        rect = new Rectangle(200, 200, hitboxSize.X, hitboxSize.Y);
        // Match texture cord with hitbox

        textureOffset = new Vector2(-10, 50);
    }
    public override void Update()
    {
        base.Update();
    }
}
