using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    private AStar m_AStar;
    private PathNodeManager m_PathNodeManager;

    private void Start()
    {
        m_PathNodeManager = PathNodeManager.instance;
        m_PathNodeManager.Init();
        m_AStar = AStar.instance;
        m_AStar.Init();   
    }
}
