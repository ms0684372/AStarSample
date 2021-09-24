using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PathNodeManager
{
    private PathNodeManager() { }
    private static PathNodeManager _instance = null;
    public static PathNodeManager instance
    {
        get
        {
            if (_instance == null)
                _instance = new PathNodeManager();
            return _instance;
        }
    }

    /// <summary>
    /// 當前場景中的所有節點
    /// </summary>
    private List<PathNode> NodeList;

    /// <summary>
    /// 初始化
    /// </summary>
    public void Init()
    {
        NodeList = new List<PathNode>();
        PathNode[] Nodes = Object.FindObjectsOfType<PathNode>();
        foreach (PathNode node in Nodes)
        {
            node.parentNode = null;
            node.fTotalWeights = node.fStartWeights = node.fEndWeights = 0;
            node.curNodeState = PathNode.PathNodeState.NODE_NONE;
            NodeList.Add(node);
        }
    }

    /// <summary>
    /// 清空節點資訊
    /// </summary>
    public void ClearNodeInfo()
    {
        foreach (PathNode node in NodeList)
        {
            node.parentNode = null;
            node.fTotalWeights = node.fStartWeights = node.fEndWeights = 0;
            node.curNodeState = PathNode.PathNodeState.NODE_NONE;
        }
    }

    /// <summary>
    /// 尋找離傳入座標最近的節點
    /// </summary>
    public PathNode GetNodeFromPosition(Vector3 pos)
    {
        PathNode returnNode = null;
        PathNode tempNode = null;

        float min = 9999999f;
        for (int i = 0; i < NodeList.Count; i++)
        {
            tempNode = NodeList[i];

            //先打射線判定，若有牆壁則直接continue
            if (Physics.Linecast(pos, tempNode.vPos, 1 << LayerMask.NameToLayer("Wall")))
            {
                continue;
            }

            Vector3 tempVec = tempNode.vPos - pos;
            //tempVec.y = 0;
            float sqrMag = tempVec.sqrMagnitude;
            if (sqrMag < min)
            {
                min = sqrMag;
                returnNode = tempNode;
            }
        }

        return returnNode;
    }
}
