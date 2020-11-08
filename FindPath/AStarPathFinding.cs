using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilty;
using Random = UnityEngine.Random;

public class AStarPathFinding : MonoBehaviour {

    //格子的初始坐标值
    public const float cubeOriginPos = -5;
    //格子的初始缩放值
    public const float cubeOriginScale = 0.5f;

    [Header("障碍物比例（%）")]
    public float obsticleScale = 30;

    [Header("寻路起点")]
    public Vector2 startPos = Vector2.zero;
    [Header("寻路终点")]
    public Vector2 endPos = Vector2.one;

    //所有格子的脚本
    private CubeGrid[,] allCubes;
    
    //格子的长度
    private int grid_Length;
    //格子的宽度
    private int grid_Width;

    private List<CubeGrid> openList;
    private List<CubeGrid> closeList;

    //结果路径栈
    private Stack<CubeGrid> path;

    private void Awake()
    {
        openList = new List<CubeGrid>();
        closeList = new List<CubeGrid>();
        path = new Stack<CubeGrid>();
    }

    private void Start()
    {
        Init();
        Debug.Log(grid_Length);
        //A*运算
        AStarCount();
    }

    /// <summary>
    /// 初始化
    /// </summary>
    private void Init()
    {
        //计算格子网格的长度（纵向的格子数量）
        grid_Length = (int)(-cubeOriginPos * transform.localScale.x * 2 / cubeOriginScale);
        //计算格子网格的宽度（横向的格子数量）
        grid_Width = (int)(-cubeOriginPos * transform.localScale.z * 2 / cubeOriginScale);

        //实例化二维数组
        allCubes = new CubeGrid[grid_Length,grid_Width];
        
        for (int i = 0; i < grid_Length; i++)
        {
            for (int j = 0; j < grid_Width; j++)
            {
                //计算每个格子的x坐标
                float x = cubeOriginPos * transform.localScale.x + cubeOriginScale * i + cubeOriginScale / 2;
                //计算每个格子的z坐标
                float z = cubeOriginPos * transform.localScale.z + cubeOriginScale * j + cubeOriginScale / 2;
                //生成格子
                GameObject currentGrid = PrefabManager.GetInstance().
                    CreateGameObjectByPrefab("CubeGrid",
                    new Vector3(x,0,z), Quaternion.identity);
                //获取格子组件
                CubeGrid cubeGrid = currentGrid.GetComponent<CubeGrid>();
                //设置格子所对应的坐标【3,4】
                cubeGrid.x = i;
                cubeGrid.z = j;
                //随机一个数
                int randomNum = Random.Range(1, 101);
                //如果当前随机数进入障碍物概率范围
                if (randomNum <= obsticleScale)
                {
                    //设置格子类型为障碍物
                    cubeGrid.SetGridType(GridType.Obsticle);
                }
                //放置到二维数组
                allCubes[i, j] = cubeGrid;
            }
        }
        
        //通过数组找到起点,并设置为起点类型
        allCubes[(int) startPos.x,
            (int) startPos.y].SetGridType(GridType.Start);
        //通过数组找到终点,并设置为终点类型
        allCubes[(int) endPos.x,
            (int) endPos.y].SetGridType(GridType.End);
    }

    /// <summary>
    /// A*寻路
    /// </summary>
    private void AStarCount()
    {
        //将寻路起点放置到OpenList
        openList.Add(allCubes[(int) startPos.x,
            (int) startPos.y]);

        //循环运算
        while (true)
        {
            //排序
            openList.Sort();
            //找到此次排序中，F值最小的格子
            CubeGrid currentGrid = openList[0];
            //如果当前格子是终点
            if (currentGrid.GetGridType() == GridType.End)
            {
                //生成路径
                PushCubePath(currentGrid);
                break;
            }
            
            //遍历周边的8个格子
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    //除去当前中心格子
                    if (i == 0 && j == 0)
                        continue;
                    
                    //计算新格子的坐标
                    int x = currentGrid.x + i;
                    int z = currentGrid.z + j;
                    
                    //排除特殊情况
                    if (x < 0 || z < 0
                    || x >= allCubes.GetLength(0)
                    || z >= allCubes.GetLength(1))
                        continue;
                    //排除障碍
                    if (allCubes[x, z].GetGridType() == GridType.Obsticle)
                        continue;
                    //排除已经成为过发现者
                    if(closeList.Contains(allCubes[x,z]))
                        continue;
                    
                    //声明一个G
                    int g = 0;
                    
                    if (i == 0 || j == 0)
                    {
                        g = currentGrid.G + 10;
                    }
                    else
                    {
                        g = currentGrid.G + 14;
                    }
                    
                    //如果该格子的G值从未计算过，则设置
                    //如果当前次的运算得到的结果G，比原本的G值要小，则更新
                    if (allCubes[x,z].G == 0 || g < allCubes[x, z].G)
                    {
                        allCubes[x, z].G = g;
                        //更新发现者
                        allCubes[x, z].finder = currentGrid;
                    }

                    //计算横向的x步数
                    int hx = (int)endPos.x - x;
                    //计算纵向的z步数
                    int hy = (int)endPos.y - z;

                    //计算H值
                    allCubes[x, z].H = hx > 0 ? hx : -hx + hy > 0 ? hy : -hy;
                    //计算F值
                    allCubes[x, z].F = allCubes[x, z].G + allCubes[x, z].H;

                    //如果OpenList中不包含当前格子
                    if (!openList.Contains(allCubes[x, z]))
                    {
                        //放进去
                        openList.Add(allCubes[x, z]);
                    }
                }
            }
            
            //将当前中心格子放置到CloseList
            closeList.Add(currentGrid);
            //从OpenList中移除
            openList.Remove(currentGrid);

            if (openList.Count == 0)
            {
                Debug.Log("Can Not Find Path!!!");
                break;
            }
        }
    }

    /// <summary>
    /// 压栈路点
    /// </summary>
    /// <param name="cubeGrid"></param>
    private void PushCubePath(CubeGrid cubeGrid)
    {
        //当前格子压栈
        path.Push(cubeGrid);
        
        if(cubeGrid.finder != null)
        {
            PushCubePath(cubeGrid.finder);
        }
        else
        {
            // ShowPath();
            StartCoroutine(ShowPathSlowly());
        }
    }

    IEnumerator ShowPathSlowly()
    {
        while (path.Count != 0)
        {
            yield return new WaitForSeconds(0.1f);
            path.Pop().SetGridColor(Color.magenta);
        }
    }

    private void ShowPath()
    {
        while (path.Count != 0)
        {
            path.Pop().SetGridColor(Color.black);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
    }
}