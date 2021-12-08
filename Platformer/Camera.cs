public class Camera
{
    public static Vector2 viewPos = new Vector2();
    static Vector2 position = new Vector2();
    static Vector2 velocity = new Vector2();
    public static void Update()
    {
        Player player = (Player)GameObject.gameObjects.Find(x => (x is Player));
        Vector2 targetVelocity = player.GetVelocity();
        Vector2 velocityMultiplier = new Vector2(45 * Raylib.GetFrameTime(), 25 * Raylib.GetFrameTime());

        // velocityMultiplier = new Vector2(0, 0);
        Vector2 targetPos = new Vector2(player.rect.x + targetVelocity.X * velocityMultiplier.X, player.rect.y + targetVelocity.Y * velocityMultiplier.Y);

        // Follow player
        // Console.WriteLine(position.X + " : " + velocity.X + " : " + targetPos.X);


        // velocity = new Vector2(SmoothDamp(position.X, targetPos.X, ref velocity.X, 0.5f, 0.1f, Raylib.GetFrameTime()), SmoothDamp(position.Y, targetPos.Y, ref velocity.Y, 0.5f, 0.1f, Raylib.GetFrameTime()));

        float smoothness = 6.5f;
        velocity = (targetPos - position) * Raylib.GetFrameTime() * smoothness;

        // velocity.X = (targetPos.X - position.X) / 10 * Raylib.GetFrameTime();
        // velocity.Y = (targetPos.Y - position.Y) / 10 * Raylib.GetFrameTime();

        position += velocity;

        viewPos = new Vector2(-position.X + Raylib.GetScreenWidth() / 2 - player.rect.width / 2, position.Y - Raylib.GetScreenWidth() / 4);
    }
    static float SmoothDamp(float current, float target, ref float currentVelocity, float smoothTime, float maxSpeed, float deltaTime)
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
