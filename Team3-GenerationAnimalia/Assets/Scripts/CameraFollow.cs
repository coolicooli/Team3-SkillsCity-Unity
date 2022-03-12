using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] public Vector3 target;

    // Start is called before the first frame update
    void LateUpdate()
        {
            transform.position = target;
        }
}
