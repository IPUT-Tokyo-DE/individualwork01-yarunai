using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonScaleOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Vector3 hoverScale = new Vector3(2.2f, 2.2f, 2.2f); // ägëÂÉTÉCÉY
    private float speed = 10f;

    private Vector3 defaultScale;
    private Vector3 targetScale;

    private RectTransform rectTransform;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        defaultScale = rectTransform.localScale;
        targetScale = defaultScale;
    }

    void Update()
    {
        //rectTransform.localScale = Vector3.Lerp(rectTransform.localScale, targetScale, Time.deltaTime * speed);
        //Debug.Log("Scale now: " + rectTransform.localScale);
        rectTransform.localScale = targetScale;

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("OnPointerEnter");
        targetScale = hoverScale;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        targetScale = defaultScale;
    }
}
