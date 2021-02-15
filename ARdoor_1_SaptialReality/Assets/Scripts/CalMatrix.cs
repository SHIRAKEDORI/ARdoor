using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[ExecuteInEditMode]
public class CalMatrix : MonoBehaviour
{
    // Start is called before the first frame update
    RectTransform rectTransform = null;

    private Vector4 vector;

    private Vector4 vector2;
    private Vector4 vector3;
    private Vector4 vector4;
    [SerializeField] Transform target = null;
    [SerializeField] Transform target2 = null;
    [SerializeField] Transform target3 = null;
    [SerializeField] Transform target4 = null;


    private float imageWidth; // インスタンス変数追加
    private float imageHeight; // インスタンス変数追加
    public float[] HomographyMatrix;
    public float[] InvHomographyMatrix;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    float[] GetMatrix(Vector4 vec1, Vector4 vec2, Vector4 vec3, Vector4 vec4)
    {
        imageWidth = 1920.0f;
        imageHeight = 1080.0f;

        vec1.x = vec1.x / imageWidth;
        vec2.x = vec2.x / imageWidth;
        vec3.x = vec3.x / imageWidth;
        vec4.x = vec4.x / imageWidth;


        vec1.y = vec1.y / imageHeight;
        vec2.y = vec2.y / imageHeight;
        vec3.y = vec3.y / imageHeight;
        vec4.y = vec4.y / imageHeight;


        HomographyMatrix = new float[9];

        var sx = (vec1.x - vec2.x) + (vec3.x - vec4.x);
        var sy = (vec1.y - vec2.y) + (vec3.y - vec4.y);

        var dx1 = vec2.x - vec3.x;
        var dx2 = vec4.x - vec3.x;
        var dy1 = vec2.y - vec3.y;
        var dy2 = vec4.y - vec3.y;

        var z = (dx1 * dy2) - (dy1 * dx2);
        var g = ((sx * dy2) - (sy * dx2)) / z;
        var h = ((sy * dx1) - (sx * dy1)) / z;
        HomographyMatrix[0] = vec2.x - vec1.x + g * vec2.x;
        HomographyMatrix[1] = vec4.x - vec1.x + h * vec4.x;
        HomographyMatrix[2] = vec1.x;
        HomographyMatrix[3] = vec2.y - vec1.y + g * vec2.y;
        HomographyMatrix[4] = vec4.y - vec1.y + h * vec4.y;
        HomographyMatrix[5] = vec1.y;
        HomographyMatrix[6] = g;
        HomographyMatrix[7] = h;
        HomographyMatrix[8] = 1.0f;

        //Debug.Log(HomographyMatrix);
        return HomographyMatrix;

    }

    float[] GetInvMatrix(float[] HgM)
    {
        var i11 = HgM[0];
        var i12 = HgM[1];
        var i13 = HgM[2];
        var i21 = HgM[3];
        var i22 = HgM[4];
        var i23 = HgM[5];
        var i31 = HgM[6];
        var i32 = HgM[7];
        var i33 = 1.0f;
        var det = 1.0f / (
            +(i11 * i22 * i33)
            + (i12 * i23 * i31)
            + (i13 * i21 * i32)
            - (i13 * i22 * i31)
            - (i12 * i21 * i33)
            - (i11 * i23 * i32)
        );

        InvHomographyMatrix = new float[9];

        InvHomographyMatrix[0] = (i22 * i33 - i23 * i32) / det;
        InvHomographyMatrix[1] = (-i12 * i33 + i13 * i32) / det;
        InvHomographyMatrix[2] = (i12 * i23 - i13 * i22) / det;
        InvHomographyMatrix[3] = (-i21 * i33 + i23 * i31) / det;
        InvHomographyMatrix[4] = (i11 * i33 - i13 * i31) / det;
        InvHomographyMatrix[5] = (-i11 * i23 + i13 * i21) / det;
        InvHomographyMatrix[6] = (i21 * i32 - i22 * i31) / det;
        InvHomographyMatrix[7] = (-i11 * i32 + i12 * i31) / det;
        InvHomographyMatrix[8] = (i11 * i22 - i12 * i21) / det;

        return InvHomographyMatrix;
    }
    void Update()
    {
        rectTransform.position = RectTransformUtility.WorldToScreenPoint(Camera.main, target.position);
        vector = rectTransform.position;

        rectTransform.position = RectTransformUtility.WorldToScreenPoint(Camera.main, target2.position);
        vector2 = rectTransform.position;

        rectTransform.position = RectTransformUtility.WorldToScreenPoint(Camera.main, target3.position);
        vector3 = rectTransform.position;

        rectTransform.position = RectTransformUtility.WorldToScreenPoint(Camera.main, target4.position);
        vector4 = rectTransform.position;


        HomographyMatrix = GetMatrix(vector, vector2, vector3, vector4);
        InvHomographyMatrix = GetInvMatrix(HomographyMatrix);
    }
}
