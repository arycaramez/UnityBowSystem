using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class IsometricCamera : MonoBehaviour
{
    public GameObject target;
    public Vector3 offset;

    /*private void FixedUpdate()
    {
        CameraCtrl();
    }*/

    private void Update()
    {
        CameraCtrl();
    }

    private void CameraCtrl() {
        if (!target) return;

        transform.position = target.transform.position + offset;
        transform.rotation = Quaternion.LookRotation(target.transform.position - transform.position);
    }
}
