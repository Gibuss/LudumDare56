using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerSlot : MonoBehaviour, IDropHandler
{
    private bool towerAlreadyThere;

    public void Awake()
    {
        towerAlreadyThere = false;
    }
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop2");
        if (eventData.pointerDrag.GetComponent<DragTower>() != null && !towerAlreadyThere)
        {
            GameObject towerObject = Instantiate(eventData.pointerDrag.GetComponent<DragTower>().GetPrefabTower(), gameObject.GetComponent<RectTransform>());
            towerAlreadyThere = true;
        }
    }
}
