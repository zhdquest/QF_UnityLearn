using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base_Transform : MonoBehaviour
{
    Transform theTransform;
    void _Position()
    {
        //世界空间中的变换位置,以世界的(0,0,0)为原点（世界坐标）
        Vector3 pos =theTransform.position;
        //相对于父变换的变换位置（本地坐标）
        Vector3 localPos = theTransform.localPosition;
    }

    void _Euler()
    {
        //世界欧拉角
        //以欧拉角表示的旋转（以度为单位）
        //仅使用该变量读取角度和将角度设置为绝对值。不要增大角度，因为当角度超过 360 度时，操作将失败。
        Vector3 eu = theTransform.eulerAngles;
        //本地欧拉角
        //以欧拉角表示的相对于父变换旋转的旋转（以度为单位）
        Vector3 localEu = theTransform.localEulerAngles;
    }

    void _Rotation()
    {
        //一个四元数，用于存储变换在世界空间中的旋转。
        //可以使用 rotation 来旋转游戏对象或提供当前旋转，不要编辑或修改 rotation。
        //Transform.rotation 小于 180 度。
        Quaternion ro = theTransform.rotation;

        //旋转游戏对象。通常以欧拉角而不是四元数提供旋转。
        //可以选择在世界轴或本地轴中指定旋转。
        theTransform.Rotate(Vector3.up, 10f);
    }

    void _Direction()
    {
        //在移动游戏对象的同时，还考虑其旋转，游戏对象在旋转时轴向也会随之改变。
        //Vector3的forward(0,0,1)right(1,0,0)up(0,1,0)

        //世界空间中变换的蓝轴(Z轴）方向
        Vector3 f =theTransform.forward;
        //世界空间中变换的绿轴(Y轴)
        Vector3 u = theTransform.up;
        //世界空间中变换的红轴(X轴)
        Vector3 r = theTransform.right;
    }

    void _Change()
    {
        //在每个轴向上移动相应距离，可设置相对坐标系
        theTransform.Translate(new Vector3(1,2,3),Space.World);

        //相对于父对象的变换缩放。
        theTransform.localScale = new Vector3(2, 2, 2);

        //绕某个点和某个轴旋转
        transform.RotateAround(Vector3.zero, Vector3.up, 10f);

        //旋转变换，使向前矢量指向 target 的当前位置。
        Transform target = null;
        theTransform.LookAt(target);
    }

    void _Parent()
    {
        //返回父对象的transform组件，没有时为null；
        Transform parent = theTransform.parent;
        //返回根对象的transform组件没有时为自己
        Transform root = theTransform.root;
        //设置父物体
        Transform Parent=null;
        theTransform.SetParent(Parent);
    }

    void _Child()
    {
        //子对象个数
        int co = theTransform.childCount;

        //按索引返回子对象
        //如果没有子项，或者 index 参数的值大于子项数，则会生成错误。
        Transform child = theTransform.GetChild(0);
    }

    //可以找到非激活的对象
    void _Find()
    {
        //如果找到子项，则返回该子对象；否则返回 null。
        // 如果name包含“/”字符，它将像访问路径名称那样访问层级视图中的变换。
        Transform child = theTransform.Find("name");
    }
}
