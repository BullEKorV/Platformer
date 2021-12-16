public class Player : Entity
{
    int hp;
    int jumpForce;
    float highJumpTimer;
    bool highJumpActive;
    float invulnerabilityTimer;
    public Player() : base()
    {
        speed = 12 * scale;
        jumpForce = 96 * scale;
        hp = 3;

        animation = Animation.allAnimations["player-idle"];

        Vector2 hitboxSize = new Vector2(15 * scale, 21 * scale); // Define player hitbox
        rect = new Rectangle(24 * scale, 20 * scale, hitboxSize.X, hitboxSize.Y);

        textureOffset = new Vector2(-9 * scale, 11 * scale); // Give texture an offset to match with hitbox
    }
    public override void Update()
    {
        // Decrease character invisibility timer
        invulnerabilityTimer -= Raylib.GetFrameTime() * Program.timeScale;
        invulnerabilityTimer = Math.Max(0, invulnerabilityTimer);

        // Decrease high jump timer
        highJumpTimer -= Raylib.GetFrameTime() * Program.timeScale;
        highJumpTimer = Math.Max(0, highJumpTimer);

        // Calculate x velocity
        float thisFrameX = 0;
        if (Raylib.IsKeyDown(KeyboardKey.KEY_A) && Program.timeScale > 0)
        {
            lookingRight = false;
            thisFrameX -= speed;
        }
        if (Raylib.IsKeyDown(KeyboardKey.KEY_D) && Program.timeScale > 0)
        {
            lookingRight = true;
            thisFrameX += speed;
        }
        if (Raylib.IsKeyDown(KeyboardKey.KEY_LEFT_SHIFT))
        {
            thisFrameX *= 1.6f; // FIX SCALE
        }
        velocity.X += thisFrameX;

        // Jump logic
        if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE) && touchingGround)
        {
            highJumpTimer = 0.628f; // Perfect for hitting 3 blocks
            highJumpActive = true;
            velocity.Y = jumpForce * Program.timeScale;
            touchingGround = false;
        }
        // High jump logic
        if (Raylib.IsKeyReleased(KeyboardKey.KEY_SPACE) || highJumpTimer == 0)
            highJumpActive = false;
        if (Raylib.IsKeyDown(KeyboardKey.KEY_SPACE) && highJumpActive)
            velocity.Y += 460 * scale * highJumpTimer * Raylib.GetFrameTime() * Program.timeScale; // Perfect for hitting 3 blocks

        // Check collision with enemies
        foreach (GameObject gameObject in gameObjects)
        {
            if (gameObject is Enemy)
                CheckEnemyCollision((Enemy)gameObject);
        }

        // Character animations
        if (Math.Abs(velocity.X) > 4)
            animation = Animation.allAnimations["player-run"];
        else
            animation = Animation.allAnimations["player-idle"];
        if (velocity.Y > 0)
            animation = Animation.allAnimations["player-jump-up"];
        if (velocity.Y < -0)
            animation = Animation.allAnimations["player-jump-down"];

        base.Update(); // 
    }
    void CheckEnemyCollision(Enemy enemy)
    {
        int extraDepth = 13;

        Rectangle rectVelocity = new Rectangle(rect.x + velocity.X * Raylib.GetFrameTime(), rect.y + velocity.Y * Raylib.GetFrameTime(), rect.width, rect.height); // Maybe better??

        bool isOverlapping = Raylib.CheckCollisionRecs(rectVelocity, enemy.rect);

        if (isOverlapping)
        {
            if (rectVelocity.y >= enemy.rect.y + enemy.rect.height - extraDepth && velocity.Y < 0) // If you come from above enemy dies
            {
                velocity.Y = 60 * scale;
                enemy.Die();
            }
            else if (invulnerabilityTimer == 0) // Player take damage
            {
                DamageTaken();
            }
        }
    }
    public void ResetPos(Vector2 pos)
    {
        rect.x = pos.X * scale;
        rect.y = pos.Y * scale;
        velocity.Y = 0;
        velocity.X = 0;
    }
    public Vector2 GetVelocity()
    {
        return velocity;
    }
    public void DisableHighJump()
    {
        highJumpActive = false;
    }
    void DamageTaken()
    {
        hp--;
        invulnerabilityTimer = 3; // Make player invu
    }
}