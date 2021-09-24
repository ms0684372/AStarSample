using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStar
{
    private AStar() { }
    private static AStar _instance;
    public static AStar instance
    {
        get
        {
            if (_instance == null)
                _instance = new AStar();
            return _instance;
        }
    }

    private List<PathNode> openList;
    private List<PathNode> closeList;
    private PathNodeManager pathNodeManager;

    public void Init()
    {
        openList = new List<PathNode>();
        closeList = new List<PathNode>();
        pathNodeManager = PathNodeManager.instance;
    }

    /// <summary>
    /// 執行AStar
    /// </summary>
    public bool PerformAStar(Vector3 startPos, Vector3 endPos, ref List<Vector3> pathList)
    {
        openList.Clear();
        closeList.Clear();
        PathNode startNode = pathNodeManager.GetNodeFromPosition(startPos);
        PathNode endNode = pathNodeManager.GetNodeFromPosition(endPos);
        //Debug.Log("StartNode:" + startNode);
        //Debug.Log("EndNode:" + endNode);

        //防呆
        if (startNode == null)
        {
            Debug.Log("startNode == null");
            return false;
        }
        if (endNode == null)
        {
            Debug.Log("endNode == null");
            return false;
        }

        //若起點跟終點已經為同一點，直接建立路徑
        if (startNode == endNode)
        {
            BuildPath(startPos, endPos, startNode, endNode, ref pathList);
            return true;
        }

        pathNodeManager.ClearNodeInfo();
        PathNode nextNode = null;
        PathNode curNode = null;
        openList.Add(startNode);
        while (openList.Count > 0)
        {
            curNode = GetBestNode();
            //防呆
            if (curNode == null)
            {
                Debug.Log("curNode == null");
                return false;
            }

            //找到終點，可以去建立路徑了
            if (curNode == endNode)
            {
                BuildPath(startPos, endPos, startNode, endNode, ref pathList);
                return true;
            }

            Vector3 vDistance;
            for (int i = 0; i < curNode.neighbors.Count; i++)
            {
                nextNode = curNode.neighbors[i];
                if (nextNode.curNodeState == PathNode.PathNodeState.NODE_CLOSED)
                    continue;
                if (nextNode.curNodeState == PathNode.PathNodeState.NODE_OPENED)
                {//計算下一個節點和起點的距離權重
                    vDistance = nextNode.vPos - curNode.vPos;
                    float fNewStartWeights = curNode.fStartWeights + vDistance.magnitude;
                    if (fNewStartWeights < nextNode.fStartWeights)
                    {
                        nextNode.fStartWeights = fNewStartWeights;
                        nextNode.fTotalWeights = nextNode.fStartWeights + nextNode.fEndWeights;
                        nextNode.parentNode = curNode;
                    }
                    continue;
                }
                vDistance = nextNode.vPos - curNode.vPos;
                float fDistance = vDistance.magnitude;
                if (Physics.Raycast(curNode.vPos, vDistance, fDistance, LayerMask.GetMask("Wall")))
                    continue;

                nextNode.curNodeState = PathNode.PathNodeState.NODE_OPENED;
                nextNode.fStartWeights = curNode.fStartWeights + fDistance;

                vDistance = endNode.vPos - nextNode.vPos;
                nextNode.fEndWeights = vDistance.magnitude;
                nextNode.fTotalWeights = nextNode.fStartWeights + nextNode.fEndWeights;
                nextNode.parentNode = curNode;
                openList.Add(nextNode);
            }
            curNode.curNodeState = PathNode.PathNodeState.NODE_CLOSED;
        }
        return false;
    }

    /// <summary>
    /// 建立路徑
    /// </summary>
    private void BuildPath(Vector3 startPos, Vector3 endPos, PathNode startNode, PathNode endNode, ref List<Vector3> pathList)
    {
        pathList.Clear();

        pathList.Add(startPos);
        if (startNode == endNode)
            pathList.Add(startNode.vPos);
        else
        {
            PathNode parentNode = endNode;
            while (parentNode != null)
            {
                pathList.Insert(1, parentNode.vPos);
                parentNode = parentNode.parentNode;
            }
        }
        pathList.Add(endPos);
    }

    /// <summary>
    /// 從openList中取得最佳節點
    /// </summary>
    private PathNode GetBestNode()
    {
        PathNode returnNode = null;
        float fMinWeight = 99999f;
        foreach (PathNode node in openList)
        {
            if (node.fTotalWeights < fMinWeight)
            {
                fMinWeight = node.fTotalWeights;
                returnNode = node;
            }
        }
        openList.Remove(returnNode);
        return returnNode;
    }
}
