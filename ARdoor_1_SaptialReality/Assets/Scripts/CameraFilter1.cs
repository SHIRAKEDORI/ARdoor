
/*using UnityEngine;


[ExecuteInEditMode]
public class CameraFilter1 : MonoBehaviour
{

    [SerializeField] private Material filter;
    private float imageWidth; // インスタンス変数追加
    private float imageHeight; // インスタンス変数追加
    private Matrix4x4 matrix;
    private Matrix4x4 unityCoordToWarperCoordMatrix; // インスタンス変数追加
    private Matrix4x4 warperCoordToUnityCoordMatrix; // インスタンス変数追加

    public CalMatrix calMatrix;

    void Awake()
    {
        // warperで変換行列を求める際に使った画像サイズが必要、実際には外部から与える?
        imageWidth = 1920.0f;
        imageHeight = 1080.0f;

        // X0.0～1.0を0.0～1440.0に、Y0.0～1.0を0.0～-900.0になるよう反転・引き延ばした上で、Yに900.0足してY900.0～0.0にする
        unityCoordToWarperCoordMatrix = Matrix4x4.TRS(new Vector3(0.0f, imageHeight, 0.0f), Quaternion.identity, new Vector3(imageWidth, -imageHeight, 1.0f));

        // warper座標をUnity座標に戻すには逆行列で変換すればよいはず...
        warperCoordToUnityCoordMatrix = unityCoordToWarperCoordMatrix.inverse;
    }

    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        Graphics.Blit(src, dest, filter);
    }

    void Update()
    {
        // warperが生成した行列、実際には外部から与える?
        matrix = calMatrix.HomographyMatrix;
        /*
        matrix.SetColumn(0, vec1);
        matrix.SetColumn(1, new Vector4(0.13807f, 0.89541f, 0.00000f, 0.00028f));
        matrix.SetColumn(2, new Vector4(0.00000f, 0.00000f, 1.00000f, 0.00000f));
        matrix.SetColumn(3, new Vector4(149.00000f, 105.00000f, 0.00000f, 1.00000f));
        */
// warper行列の前後に追加の変換を入れてシェーダーに与える
//Matrix4x4 compositeMatrix = warperCoordToUnityCoordMatrix * matrix * unityCoordToWarperCoordMatrix;

// SetMatrixはインスタンスメソッドなので、Materialをfilterに変更
//filter.SetMatrix("_mv", compositeMatrix);
//}
//}
