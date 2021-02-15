using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Assertions;


[ExecuteInEditMode]
public class CameraFilter_Homography : MonoBehaviour
{
    [SerializeField] private Material filter;
    private float[] Matrix;
    private float[] InvMatrix;

    public CalMatrix calMatrix;

    void Awake()
    {

    }

    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        Matrix = calMatrix.HomographyMatrix;
        InvMatrix = calMatrix.InvHomographyMatrix;

        filter.SetFloatArray("_Homography", Matrix);
        filter.SetFloatArray("_InvHomography", InvMatrix);

        Graphics.Blit(src, dest, filter);
    }
}