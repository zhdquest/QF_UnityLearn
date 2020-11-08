using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base_Other : MonoBehaviour
{
    //调式方法
   void _Debug()
    {
        //打印调试
        Debug.Log("");
        //在指定的起始点与结束点之间绘制一条直线
        //Debug.DrawLine((Vector3 start, Vector3 end, Color color = Color.white, float duration = 0.0f, bool depthTest = true);
        //在世界坐标中绘制一条从 start 到 start + dir 的直线。
        //Debug.DrawRay(Vector3 start, Vector3 dir, Color color = Color.white, float duration = 0.0f, bool depthTest = true);
    }

    //四元数
    void _Quaternion()
    {
        //是静态变量，表示空旋转
        //该四元数对应于“no rotation”- 对象与世界轴或父轴完全对齐。
        transform.rotation = Quaternion.identity;

        //将一个向量转化为这个方向所代表的四元数
        Quaternion q = Quaternion.LookRotation(Vector3.forward);

        //插值计算
        //Quaternion l=Quaternion.Lerp()；
    }

    void _Time()
    {
        //自游戏开始到此时所花费的时间，即这一帧开始的时间（只读）
        float t = Time.time;
        //当前帧和上一帧之间的时间，可用于实现匀速移动（不受帧率影响）
        float dt = Time.deltaTime;
        //固定帧率的时间间隔；受timeScale影响
        float ft = Time.fixedDeltaTime;
        //时间缩放，默认为1（不变）
        //当 timeScale 设置为 0 时，不会调用 FixedUpdate 函数。
        //如果减小了timeScale，建议也将Time.fixedDeltaTime减小相同的量。
        Time.timeScale = 1;
    }

    void _Mathf()
    {
        //求绝对值
        float a = Mathf.Abs(-1);
        //求插值
        //当第三个参数等于0和1时，返回0或1；（当值为0.5时返回两数的中间点）
        float l = Mathf.Lerp(1, 2, 0.5f);
        //在给定的最小浮点值和最大浮点值之间钳制给定值。如果在最小和最大范围内，则返回给定值。
        float c = Mathf.Clamp(1, 0, 2);
        //将值限制在 0 与 1 之间并返回值。如果值为负，则返回 0。如果值大于 1，则返回 1。
        float c01 = Mathf.Clamp01(0.3f);
    }

    void _Instantiate()
    {
        //克隆 original 对象并返回克隆对象。
        //克隆 GameObject 或 Component 时，也将克隆所有子对象和组件，它们的属性设置与原始对象相同。
        //默认情况下，新对象的父对象 将为 null，因此它与原始对象不“同级”。可以使用重载方法设置父对象
        //克隆时将传递 GameObject 的激活状态,如果原始对象处于非激活状态，则克隆对象也将创建为非激活状态。
        //Object.Instantiate(original);
    }
}
