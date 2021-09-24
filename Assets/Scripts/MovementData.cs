using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementData
{
    public GameObject gameObject;
    public float fSpeed;
    public float fArriveRange;
    public float fTurnForce;
    public Vector3 vTarget;

    public MovementData(GameObject _gameObject,float _fSpeed, float _fArriveRange, float _fTurnForce)
    {
        gameObject = _gameObject;
        fSpeed = _fSpeed;
        fArriveRange = _fArriveRange;
        fTurnForce = _fTurnForce;
    }
}
