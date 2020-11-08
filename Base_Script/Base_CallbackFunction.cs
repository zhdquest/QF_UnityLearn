using System;
using System.Collections.Generic;
using System.Text;

namespace UnityCode_LearnStage.Base
{
    class Base_CallbackFunction
    {
        //在游戏启动之前调用
        //将初始化操作放在Awake中，用Awake代替构造函数进行初始化
        //必要时，可通过ScriptExecutionOrder调整脚本之间的执行顺序（有依赖关系）
        //也可将依赖执行的脚本的初始化放在start中
        void Awake() { }

        //在对象变为启用和激活状态时调用
        //只有当组件中有该函数，组件前面才会有是否可用的开关；
        void OnEnable() { }

        //在Awake和OnEnable之后调用，若脚本状态为不可用则不调用
        //找对象、找组件等操作放在Start中
        //使用 Awake 在脚本之间设置引用，并使用 Start 来回传递任何信息
        void Start() { }


        //每帧调用一次
        void Update() { }

        //在Update之后，每帧调用一次；安排脚本的执行顺序；
        //可用于设置跟随效果
        void LateUpdate() { }

        //具有物理系统的频率,每个固定帧率帧调用该函数
        //调用之间的默认时间为 0.02 秒（50 次调用/秒）
        //固定时间执行、联网时、物理运动等情况常用
        void FixedUpdate() { }


        //当脚本组件被设置为不可用时调用一次
        void OnDisable() { }

        //当脚本组件或所在游戏对象被销毁时调用一次
        void OnDistroy() { }


        //触发、碰撞、鼠标、UI拖拽.......
        //其他回调函数见API
    }
}
