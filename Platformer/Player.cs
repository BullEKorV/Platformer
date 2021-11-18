public class Player : Entity
{

    public Player()
    {
        hp = 100;

        // texture = Animation.allAnimations[@"player\idle"].frames[0];

        rect = new Rectangle(200, 200, 330, 320);
    }
}