using System;
using UnityEngine;

public class BunnyAnimation : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float turnSpeed = 10;
    
    
    //动画组件
    private Animator ani;

    private float hor, ver;

    private void Awake()
    {
        ani = GetComponent<Animator>();
    }

    private void Start()
    {
        // ani.SetFloat("MoveSpeed",1);
        int ms = Animator.StringToHash("MoveSpeed");

        ani.SetFloat(ms, 1);

        //判断当前角色是否正在播放Move动画
        bool isMove = ani.GetCurrentAnimatorStateInfo(
                          0).shortNameHash ==
                      Animator.StringToHash("Move");
        //判断当前角色是否正在播放Move动画
        if (ani.GetCurrentAnimatorStateInfo(
                0).shortNameHash ==
            Animator.StringToHash("Move"))
        {
                        
        }
    }


    private void Update()
    {
        #region Animator Functions

        // if (Input.GetKey(KeyCode.W))
        // {
        //     // ani.SetFloat("Speed",3);
        //     
        //     // ani.SetInteger("MoveSpeed",1);
        //     
        //     // ani.SetBool("CanMove",true);
        //     
        //     // ani.SetTrigger("Move");
        // }
        // else
        // {
        //     // ani.SetFloat("Speed",0);
        //     
        //     // ani.SetInteger("MoveSpeed",0);
        //     
        //     // ani.SetBool("CanMove",false);
        // }

        #endregion

        
        hor = Input.GetAxis("Horizontal");
        ver = Input.GetAxis("Vertical");

        //按下任意方向键
        if (hor != 0 || ver != 0)
        {
            ani.SetBool("CanMove",true);
        }
        else
        {
            ani.SetBool("CanMove",false);   
        }
        
        //判断当前角色是否正在播放移动动画
        if (ani.GetCurrentAnimatorStateInfo(0).IsName("Move"))
        {
            
            //声明一个临时播放速度
            float sp = 0;

            if (ver >= 0)
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    sp = 2;
                }
                else
                {
                    sp = 1;
                }
            }
            else
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    sp = -2;
                }
                else
                {
                    sp = -1;
                }
            }
            
            //前后移动
            transform.position += sp * transform.forward * Time.deltaTime * moveSpeed;
            
            ani.SetFloat("MovePlaySpeed",sp);

            #region 朝着世界的前后左右转身

            // //得到方向向量
            // Vector3 dir = new Vector3(hor,0,ver);
            // //向量转换成四元数
            // Quaternion targetQua = Quaternion.LookRotation(dir);
            // //lerp过去
            // transform.rotation = Quaternion.Lerp(transform.rotation, targetQua, Time.deltaTime * turnSpeed);
            #endregion

            #region 左右旋转偏移

            transform.eulerAngles += Vector3.up * turnSpeed * hor;

            #endregion
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ani.SetTrigger("Attack");
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            moveSpeed *= 2;
            
            // ani.SetFloat("MovePlaySpeed",
            //     ani.GetFloat("MovePlaySpeed") * 2);
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            moveSpeed /= 2;
            // ani.SetFloat("MovePlaySpeed",
            //     ani.GetFloat("MovePlaySpeed") / 2);
        }
    }
    
    
    
}