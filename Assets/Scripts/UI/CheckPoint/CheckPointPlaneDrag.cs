using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CheckPointPlaneDrag : MonoBehaviour, IPointerClickHandler, IDragHandler
{


    // Start is called before the first frame update
    public void OnDrag(PointerEventData eventData)
    {
        Vector3 globalMousePos;
        var transform = GetComponent<RectTransform>();
        var dragObjRect = transform;
        var dragUI = transform.parent.GetComponent<RectTransform>();
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle
            (dragObjRect, eventData.position, eventData.pressEventCamera, out globalMousePos))
        {
            dragUI.position = new Vector3(globalMousePos.x, globalMousePos.y-175, globalMousePos.z);
            dragUI.rotation = dragObjRect.rotation;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Vector3 globalMousePos;
        var transform = GetComponent<RectTransform>();
        var dragObjRect = transform;
        var dragUI = transform.parent.GetComponent<RectTransform>();
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle
            (dragObjRect, eventData.position, eventData.pressEventCamera, out globalMousePos))
        {
            dragUI.position = new Vector3(globalMousePos.x, globalMousePos.y - 175, globalMousePos.z);
            dragUI.rotation = dragObjRect.rotation;
        }
    }
}
