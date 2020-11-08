using System;
using UnityEngine;
using System.Collections;

public enum GridType
{
    Normal,
    Obsticle,
    Start,
    End
}

public class CubeGrid : MonoBehaviour,IComparable<CubeGrid>
{

    //格子坐标
    public int x;
    public int z;

    //格子的估量代价
    public int F;
    public int G;
    public int H;

    //格子类型
    private GridType gridType;
    
    //发现者
    public CubeGrid finder;

    private MeshRenderer _meshRenderer;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    /// <summary>
    /// 获取格子类型
    /// </summary>
    /// <returns></returns>
    public GridType GetGridType()
    {
        return gridType;
    }

    /// <summary>
    /// 设置格子类型
    /// </summary>
    /// <param name="_gridType"></param>
    public void SetGridType(GridType _gridType)
    {
        gridType = _gridType;
        switch (_gridType)
        {
            case GridType.Start:
                SetGridColor(Color.red);
                break;
            case GridType.End:
                SetGridColor(Color.green);
                break;
            case GridType.Obsticle:
                SetGridColor(Color.blue);
                break;
            default:
                SetGridColor(Color.white);
                break;
        }
    }

    /// <summary>
    /// 设置格子颜色
    /// </summary>
    /// <param name="_color"></param>
    public void SetGridColor(Color _color)
    {
        _meshRenderer.material.color = _color;
    }

    public int CompareTo(CubeGrid other)
    {
        if (this.F < other.F)
            return -1;
        if (this.F > other.F)
            return 1;
        return 0;
    }
}