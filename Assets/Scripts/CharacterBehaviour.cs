using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBehaviour : MonoBehaviour
{
    private List<Vector3> pathList = new List<Vector3>();
    private MovementData movementData;
    
    private void Start()
    {
        movementData = new MovementData(gameObject, 10f, 0.5f, 0);
    }
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit rh;
            if (Physics.Raycast(r, out rh, Mathf.Infinity, LayerMask.GetMask("Terrain")))
            {
                bool performAStar = AStar.instance.PerformAStar(transform.position, rh.point, ref pathList);
            }
        }
    }

    private void FixedUpdate()
    {
        PerformMove();
    }

    /// <summary>
    /// 執行移動
    /// </summary>
    private void PerformMove()
    {
        if (pathList == null || pathList.Count < 1)
            return;
        
        for (int i = pathList.Count - 1; i >= 0; i--)
        {
            if (Physics.Linecast(transform.position, pathList[i], LayerMask.GetMask("Wall")))
                continue;
            movementData.vTarget = pathList[i];
            break;
        }
        SteeringBehaviour.PathFollowing(movementData);
    }

    private void OnDrawGizmos()
    {//畫路徑
        if (pathList != null && pathList.Count > 0)
        {
            for (int i = 0; i < pathList.Count - 1; i++)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawLine(pathList[i], pathList[i + 1]);
            }
        }
    }
}
