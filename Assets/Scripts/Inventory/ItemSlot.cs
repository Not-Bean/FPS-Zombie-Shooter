using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    int[] data;
    public void OnDrop(PointerEventData eventData)
    {
        GameObject draggedObject = eventData.pointerDrag;

        if (draggedObject != null)
        {
            RectTransform draggedRectTransform = draggedObject.GetComponent<RectTransform>();
            draggedRectTransform.anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            draggedObject.GetComponent<Item>().Slot(data);
        }
    }
}
