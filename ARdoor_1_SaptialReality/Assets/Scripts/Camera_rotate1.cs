using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_rotate : MonoBehaviour
{
    [SerializeField] private GameObject pivot;
    [SerializeField] private float k = 2.5f;

    void Start()
    {
        Camera.main.fieldOfView = 60f;
    }
    void Update()
    {
        Vector3 worldAngle;
        Vector3 offset;
        Transform myTransform = this.transform;
        Transform pivotTransform = pivot.transform;
        Vector3 myPosition = myTransform.position;
        Vector3 pivotPosition = pivotTransform.position;


        offset = myPosition - pivotPosition;
        Vector3 offset2 = offset;
        offset2.x += 5.0f;
        Vector3 offset3 = offset;
        offset3.x += -5.0f;

        float d = offset.magnitude;
        float d2 = offset2.magnitude;
        float d3 = offset3.magnitude;

        worldAngle.y = Mathf.Atan(offset.x / offset.z) * Mathf.Rad2Deg;
        worldAngle.x = Mathf.Atan(-offset.y / Mathf.Sign(offset.z) / Mathf.Sqrt(offset.z * offset.z + offset.x * offset.x)) * Mathf.Rad2Deg;//オイラー格の回転は順番がある
        worldAngle.z = 0.0f;
        myTransform.eulerAngles = worldAngle;
        float fov2 = k * Mathf.Atan(5.0f / d2) * Mathf.Rad2Deg;
        float fov3 = k * Mathf.Atan(5.0f / d3) * Mathf.Rad2Deg;
        float fov = Mathf.Max(fov2, fov3);
        Camera.main.fieldOfView = fov;


    }
}
