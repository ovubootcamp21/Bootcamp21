using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    [SerializeField] private Transform targetTransform;
    [SerializeField] private Vector3 camOffset = Vector3.zero;
    [SerializeField] private Vector3 camHitOffset = Vector3.zero;
    [SerializeField] private LayerMask wallLayer;

    void LateUpdate()
    {
        RaycastHit hit;

        Vector3 dir = transform.position - targetTransform.position;
        Debug.DrawRay(targetTransform.position, dir, Color.green);
        float dist = camOffset.z * -1;

        if (Physics.Raycast(targetTransform.position, dir, out hit, dist, wallLayer))
        {
            transform.position = targetTransform.position + camHitOffset;
        }
        else
        {
            transform.position = targetTransform.position + camOffset;
        }
    }
}