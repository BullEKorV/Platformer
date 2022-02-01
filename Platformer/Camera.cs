public class Camera
{
    public static Vector2 viewPos;
    static Vector2 position;
    public static Vector2 velocity;
    public static void Update()
    {
        // Get targetpos for camera to move towards with player pos and player velocity. Camera tries to look in front of player
        Player player = (Player)GameObject.gameObjects.Find(x => (x is Player));
        Vector2 targetVelocity = player.GetVelocity();
        Vector2 velocityMultiplier = new Vector2(0.06f * GameObject.scale, 0.02f * GameObject.scale);
        Vector2 targetPos = new Vector2(player.rect.x + player.rect.width / 2 + targetVelocity.X * velocityMultiplier.X, player.rect.y + targetVelocity.Y * velocityMultiplier.Y);

        // Follow player
        float maxSpeed = 200 * GameObject.scale;
        position = new Vector2(SmoothDamp(position.X, targetPos.X, ref velocity.X, 0.03f * GameObject.scale, maxSpeed, Raylib.GetFrameTime()), SmoothDamp(position.Y, targetPos.Y, ref velocity.Y, 0.04f * GameObject.scale, maxSpeed, Raylib.GetFrameTime()));

        viewPos = new Vector2(-position.X + Raylib.GetScreenWidth() / 2, position.Y + player.crouchStuff.X * 1.75f - Raylib.GetScreenHeight() / 2);

        viewPos = new Vector2((float)Math.Floor(viewPos.X), (float)Math.Floor(viewPos.Y)); // Makes viewpos whole integer to elliminate visual glitch
    }
    public static void MoveToPlayer()
    {
        Player player = (Player)GameObject.gameObjects.Find(x => (x is Player));
        position = new Vector2(player.rect.x + player.rect.width / 2, player.rect.y);
        velocity = new Vector2();
    }
    public static void MoveToCords(Vector2 cords)
    {
        velocity = new Vector2(0, 0);
        position = cords;

        viewPos = new Vector2(-position.X + Raylib.GetScreenWidth() / 2, position.Y - Raylib.GetScreenHeight() / 2);
    }
    static float SmoothDamp(float current, float target, ref float currentVelocity, float smoothTime, float maxSpeed, float deltaTime) // Stolen from Unity ;()
    {
        // Based on Game Programming Gems 4 Chapter 1.10
        smoothTime = Math.Max(0.0001F, smoothTime);
        float omega = 2F / smoothTime;

        float x = omega * deltaTime;
        float exp = 1F / (1F + x + 0.48F * x * x + 0.235F * x * x * x);
        float change = current - target;
        float originalTo = target;

        // Clamp maximum speed
        float maxChange = maxSpeed * smoothTime;
        change = Math.Clamp(change, -maxChange, maxChange);
        target = current - change;

        float temp = (currentVelocity + omega * change) * deltaTime;
        currentVelocity = (currentVelocity - omega * temp) * exp;
        float output = target + (change + temp) * exp;

        // Prevent overshooting
        if (originalTo - current > 0.0F == output > originalTo)
        {
            output = originalTo;
            currentVelocity = (output - originalTo) / deltaTime;
        }

        return output;
    }
}
