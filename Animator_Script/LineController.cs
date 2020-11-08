using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{

    private LineRenderer _lineRenderer;

    private List<Vector3> positions;

    private RaycastHit hit;

    private void Awake()
    {
        positions = new List<Vector3>();
        _lineRenderer = GetComponent<LineRenderer>();
    }

    private void Start()
    {
        //设置顶点个数
        _lineRenderer.positionCount = 2;

        //设置顶点坐标
        _lineRenderer.SetPosition(0, Vector3.zero);
        _lineRenderer.SetPosition(1, Vector3.up * 10);
        // _lineRenderer.SetPosition(2,Vector3.right * 10);

        // _lineRenderer.SetPositions();
    }

    private void Update()
    {
        if (!Input.GetMouseButton(0))
            return;

        //将鼠标坐标转换为物理射线
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);


        //发射物理射线
        if (Physics.Raycast(ray, out hit))
        {
            //将当前点添加到List
            positions.Add(hit.point);
            //设置LineRenderer的顶点个数
            _lineRenderer.positionCount = positions.Count;
            //设置坐标
            //必须在调用 SetPositions 之前调用 positionCount。另外，SetPositions 会忽略索引超出 positionCount 的点。
            _lineRenderer.SetPositions(positions.ToArray());
        }
    }
}