public class Animation
{
    public static Dictionary<string, Animation> allAnimations = new Dictionary<string, Animation>();
    public List<Texture2D> frames = new List<Texture2D>();
    int currentFrame = 0;
    int totalFrames;
    int framesPerFrame = 4;
    public Animation(string name)
    {
        string[] fileArray = Directory.GetFiles(@"animations\" + name);

        for (var i = 0; i < fileArray.Length; i++)
        {
            // Console.WriteLine(fileArray[i]);
            frames.Add(Raylib.LoadTexture(fileArray[i]));
        }
        totalFrames = frames.Count * framesPerFrame;

        allAnimations.Add(name, this);
    }
    public static void GetSubDirectories()
    {
        string temp = @"animations\";

        string[] subdirectoryEntries = Directory.GetDirectories(temp);
        foreach (string subdirectory in subdirectoryEntries)
        {
            Console.WriteLine("Directory found");
            LoadSubDirs(subdirectory);
        }
    }
    private static void LoadSubDirs(string dir)
    {
        Console.WriteLine(dir);

        string[] subdirectoryEntries = Directory.GetDirectories(dir);

        foreach (string subdirectory in subdirectoryEntries)

        {
            LoadSubDirs(subdirectory);
        }
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