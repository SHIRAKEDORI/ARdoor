using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class Cameraspace : MonoBehaviour
{
    RectTransform rectTransform = null;
    [SerializeField] Transform target = null;

    [SerializeField] Canvas canvas = null;

    void Awake()
    {

        rectTransform = GetComponent<RectTransform>();
        //canvas = GetComponent<Graphic>().canvas;
        //canvas = GetComponent<Graphic>();
    }

    void Update()
    {
        var pos = Vector2.zero;
        var uiCamera = Camera.main;
        var worldCamera = Camera.main;
        var canvasRect = canvas.GetComponent<RectTransform>();

        var screenPos = RectTransformUtility.WorldToScreenPoint(worldCamera, target.position);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, screenPos, uiCamera, out pos);
        rectTransform.localPosition = pos;
        Debug.Log(pos);
    }
}