using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringBehaviour
{
    public static void PathFollowing(MovementData movementData)
    {
        Transform transform = movementData.gameObject.transform;
        Vector3 curPos = transform.position;
        Vector3 vForward = transform.forward;
        Vector3 vec = movementData.vTarget - curPos;
        vec.y = 0;
        float fDist = vec.magnitude;
        vec.Normalize();
        float fLimit = movementData.fSpeed * Time.deltaTime;

        if (fDist < fLimit)
        {
            return;
        }

        float fDot = Vector3.Dot(vec, vForward);
        if (fDot > 1)
        {
            fDot = 1;
            transform.forward = vec;
        }
        else
        {
            float fSinLen = Mathf.Sqrt(1 - fDot * fDot);
            float fDot2 = Vector3.Dot(transform.right, vec);
            float fMag = 0.1f;

            if (fDot < 0)
                fMag = 1;
            if (fDot2 < 0)
                fMag = -fMag;

            vForward = vForward + transform.right * fSinLen * fMag * 2 + transform.right * movementData.fTurnForce;
            vForward.Normalize();
            transform.forward = vForward;
        }
        transform.position = curPos + transform.forward * movementData.fSpeed * Time.deltaTime;
    }
}
