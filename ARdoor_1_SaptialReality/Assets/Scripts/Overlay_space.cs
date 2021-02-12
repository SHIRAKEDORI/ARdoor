using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Overlay_space : MonoBehaviour
{
    RectTransform rectTransform = null;
    RectTransform rectTransform2 = null;
    RectTransform rectTransform3 = null;
    RectTransform rectTransform4 = null;
    [SerializeField] Transform target = null;
    [SerializeField] Transform target2 = null;
    [SerializeField] Transform target3 = null;
    [SerializeField] Transform target4 = null;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        rectTransform2 = GetComponent<RectTransform>();
        rectTransform3 = GetComponent<RectTransform>();
        rectTransform4 = GetComponent<RectTransform>();
    }

    void Update()
    {
        rectTransform.position = RectTransformUtility.WorldToScreenPoint(Camera.main, target.position);
        Debug.Log(rectTransform.position);
        rectTransform2.position = RectTransformUtility.WorldToScreenPoint(Camera.main, target2.position);
        Debug.Log(rectTransform2.position);
        rectTransform3.position = RectTransformUtility.WorldToScreenPoint(Camera.main, target3.position);
        Debug.Log(rectTransform3.position);
        rectTransform4.position = RectTransformUtility.WorldToScreenPoint(Camera.main, target4.position);
        Debug.Log(rectTransform4.position);

    }
}