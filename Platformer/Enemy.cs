public class Enemy : Entity
{
    protected int xOffsetBase;
    protected int xOffsetDiff;
    protected int yOffset;
    public Enemy()
    {
    }
    public override void Update()
    {
        base.Update();
    }
    protected void FlipXAxis()
    {
        int xOffset = xOffsetBase + (lookingRight ? -xOffsetDiff : xOffsetDiff); // invert xoffset to line up texture
        textureOffset = new Vector2(xOffset, yOffset);
    }
    public void Die()
    {
        isAlive = false;
    }
}