using UnityEngine.EventSystems;
using UnityEngine;

public class MouseOnUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public static bool isMouseOver = false;
    public static GameObject gameObj;

    void Start()
    {

    }

    public void OnPointerEnter(PointerEventData eventData) 
    {
        isMouseOver = true;
        gameObj = gameObject;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isMouseOver = false;
        gameObj = gameObject;
    }
}
