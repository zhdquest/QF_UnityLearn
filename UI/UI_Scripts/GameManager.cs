using System.Collections.Generic;

public class GameManager
{
    #region Singleton
    
    //单例
    private static GameManager instance;

    //获取单例
    public static GameManager GetInstance()
    {
        if (instance == null)
        {
            instance = new GameManager();
        }

        return instance;
    }

    private GameManager()
    {
        levelsData = new List<int>();
    }
    #endregion

    //关卡数据
    public List<int> levelsData;

    //当前选择的关卡编号
    public int levelIndex = 0;
    //当前关卡的星星数量
    public int currentStars = 0;
}