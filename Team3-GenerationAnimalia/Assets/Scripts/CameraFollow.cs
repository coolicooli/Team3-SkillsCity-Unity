using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] public Vector3 target;

    void LateUpdate()
        {
            transform.position = target;
        }
}
