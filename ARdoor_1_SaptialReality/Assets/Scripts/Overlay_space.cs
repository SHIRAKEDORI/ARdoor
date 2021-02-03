using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Overlay_space : MonoBehaviour
{
    RectTransform rectTransform = null;
    [SerializeField] Transform target = null;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        rectTransform.position = RectTransformUtility.WorldToScreenPoint(Camera.main, target.position);
        Debug.Log(rectTransform.position);

    }
}