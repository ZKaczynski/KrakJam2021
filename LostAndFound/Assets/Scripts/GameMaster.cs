using Utils;

public class GameMaster
{
    private static readonly GameMaster Instance = null;

    public static GameMaster GetInstance()
    {
        return Instance ?? new GameMaster();
    }
    
    public TransformUtils TransformUtils = new TransformUtils();
    
    private GameMaster()
    {
    }
}
