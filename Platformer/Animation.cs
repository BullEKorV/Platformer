public class Animation
{
    public static Dictionary<string, Animation> allAnimations = new Dictionary<string, Animation>();
    public List<Texture2D> frames = new List<Texture2D>();
    int currentFrame = 0;
    int totalFrames;
    int framesPerFrame = 4;
    public Animation(string name)
    {
        string[] fileArray = Directory.GetFiles(@"textures\" + name);

        for (var i = 0; i < fileArray.Length; i++)
        {
            // Console.WriteLine(fileArray[i]);
            frames.Add(Raylib.LoadTexture(fileArray[i]));
        }
        totalFrames = frames.Count * framesPerFrame;

        allAnimations.Add(name, this);
    }
    public Texture2D GetCurrentFrame()
    {
        return frames[currentFrame / framesPerFrame];
    }
    public void Update()
    {
        currentFrame++;
        if (currentFrame == totalFrames * framesPerFrame)
        {
            currentFrame = 0;
        }
    }
}