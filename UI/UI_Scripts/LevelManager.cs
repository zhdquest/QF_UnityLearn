using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private void Start()
    {
        //Debug.Log(GameManager.GetInstance().levelsData.Count);
        LevelInit();
    }

    private void LevelInit()
    {
        for (int i = 0; i < GameManager.GetInstance().levelsData.Count; i++)
        {
            //Debug.Log(GameManager.GetInstance().levelsData.Count);
            //设置当前关卡的星星数量
            transform.GetChild(i).GetComponent<LevelController>().SetStar(
                GameManager.GetInstance().levelsData[i]);
            //解锁
            transform.GetChild(i).GetComponent<LevelController>().UnlockLevel(false);
        }
        Transform newLevel = transform.GetChild(GameManager.GetInstance().levelsData.Count);
        //一个对象隐式转换为bool
        //null --> false , NotNull --> true
        if (newLevel)
        {
            newLevel.GetComponent<LevelController>().UnlockLevel(true);
        }
    }
    
    /// <summary>
    /// 进入关卡
    /// </summary>
    public void EnterLevel()
    {
        //场景切换
        SceneManager.LoadScene("ScoreScene");
    }
}