using System;
using UnityEngine;
using FSM_Frame;

public class UseFSMDemo : MonoBehaviour
{

    private StateMachine roleMachine;
    private StateMachine controledMachine;

    private State idle;
    private State run;
    private State dizziness;//眩晕
    private State silence;//沉默

    private bool isRun;//是否奔跑【参数】
    private bool isControled;//是否被控制【参数】
    private bool isSilence;//是否被沉默【参数】
    
    private void Start()
    {
        //1、初始化状态和状态机
        roleMachine = new StateMachine("角色状态机");
        controledMachine = new StateMachine("被控制状态机");
        idle = new State("站立");
        run = new State("奔跑");
        dizziness = new State("眩晕");
        silence = new State("沉默");
        
        //2、设置每个状态的事件
        roleMachine.OnStateEnter += obj => { Debug.LogWarning("角色状态机进入"); };
        //roleMachine.OnStateUpdate += obj => { Debug.LogWarning("角色状态机更新"); };
        roleMachine.OnStateExit += obj => { Debug.LogWarning("角色状态机离开"); };
        
        controledMachine.OnStateEnter += obj => { Debug.LogWarning("被控制状态机进入"); };
        //controledMachine.OnStateUpdate += obj => { Debug.LogWarning("被控制状态机更新"); };
        controledMachine.OnStateExit += obj => { Debug.LogWarning("被控制状态机离开"); };
        
        idle.OnStateEnter += obj => { Debug.LogWarning("站立状态进入"); };
        //idle.OnStateUpdate += obj => { Debug.LogWarning("站立状态更新"); };
        idle.OnStateExit += obj => { Debug.LogWarning("站立状态离开"); };
        
        run.OnStateEnter += obj => { Debug.LogWarning("奔跑状态进入"); };
        //run.OnStateUpdate += obj => { Debug.LogWarning("奔跑状态更新"); };
        run.OnStateExit += obj => { Debug.LogWarning("奔跑状态离开"); };
        
        dizziness.OnStateEnter += obj => { Debug.LogWarning("眩晕状态进入"); };
        //dizziness.OnStateUpdate += obj => { Debug.LogWarning("眩晕状态更新"); };
        dizziness.OnStateExit += obj => { Debug.LogWarning("眩晕状态离开"); };
        
        silence.OnStateEnter += obj => { Debug.LogWarning("沉默状态进入"); };
        //silence.OnStateUpdate += obj => { Debug.LogWarning("沉默状态更新"); };
        silence.OnStateExit += obj => { Debug.LogWarning("沉默状态离开"); };
        
        //3、状态机添加所属状态
        roleMachine.AddState(idle);
        roleMachine.AddState(run);
        roleMachine.AddState(controledMachine);

        controledMachine.AddState(dizziness);
        controledMachine.AddState(silence);

        //4、设置状态与状态之间的过渡
        idle.RegisterStateTransition("奔跑", () => { return isRun; });
        run.RegisterStateTransition("站立", () => { return !isRun; });
        
        controledMachine.RegisterStateTransition("奔跑", () => { return !isControled; });
        run.RegisterStateTransition("被控制状态机", () => { return isControled; });
        
        dizziness.RegisterStateTransition("沉默", () => { return isSilence; });
        silence.RegisterStateTransition("眩晕", () => { return !isSilence; });

        //5、启动主状态机
        roleMachine.EnterState(null,null);
    }

    private void Update()
    {
        isRun = false;
        isControled = false;
        isSilence = false;
        
        //6、调整参数，达成过渡
        if (Input.GetKey(KeyCode.R))
        {
            isRun = true;
        }
        if (Input.GetKey(KeyCode.C))
        {
            isControled = true;
        }
        if (Input.GetKey(KeyCode.S))
        {
            isSilence = true;
        }
    }
}