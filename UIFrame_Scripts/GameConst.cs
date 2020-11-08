using UnityEngine;

public static class GameConst
{
    public static int OPEN_ANI_PARAMETER;

    static GameConst()
    {
        OPEN_ANI_PARAMETER = Animator.StringToHash("Open");
    }
}
