using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMove : MonoBehaviour
{
    public Transform target;
    public Transform character;
    public float speed;
    private Vector2 lastPos;

    private void Start()
    {
        Vector3 pos = transform.position;
        transform.position = pos + transform.forward;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            lastPos = Input.mousePosition;
        else if (Input.GetMouseButton(0))
        {
            Vector2 currentPos = Input.mousePosition;
            Vector2 delta = currentPos - lastPos;

            target.position = target.position + character.right * delta.x * speed;
            Debug.Log($"isMoving speed:{speed} deltaTime:{Time.deltaTime}");
            lastPos = currentPos;
        }
    }
}
