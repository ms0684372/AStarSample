using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode : MonoBehaviour
{
    public enum PathNodeState
    {
        NODE_NONE = -1,
        NODE_OPENED,
        NODE_CLOSED
    };

    /// <summary>
    /// 鄰居，相連的節點
    /// </summary>
    public List<PathNode> neighbors;
    /// <summary>
    /// 此節點的座標
    /// </summary>
    [HideInInspector] public Vector3 vPos;
    /// <summary>
    /// 父節點
    /// </summary>
    [HideInInspector] public PathNode parentNode;
    /// <summary>
    /// 和起點的距離權重
    /// </summary>
    [HideInInspector] public float fStartWeights;
    /// <summary>
    /// 和終點的距離權重
    /// </summary>
    [HideInInspector] public float fEndWeights;
    /// <summary>
    /// 起點權重 + 終點權重
    /// </summary>
    [HideInInspector] public float fTotalWeights;
    /// <summary>
    /// Node當前狀態
    /// </summary>
    [HideInInspector] public PathNodeState curNodeState;

    private void Awake()
    {
        vPos = transform.position;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        //畫出跟鄰居之間的線
        if (neighbors != null && neighbors.Count > 0)
        {
            foreach (PathNode neighbor in neighbors)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawLine(transform.position, neighbor.transform.position);
            }
        }
    }
#endif
}
