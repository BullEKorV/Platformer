public class Player : Entity
{
    public Player()
    {
        hp = 100;
        rect = new Rectangle(50, 50, 50, 50);

        texture = Animation.allAnimations[@"player\idle"].frames[0];
    }
}