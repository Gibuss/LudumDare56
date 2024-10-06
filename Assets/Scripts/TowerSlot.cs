using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerSlot : MonoBehaviour, IDropHandler
{
    private CurrencyManager currencyManager;

    public void Awake()
    {
        currencyManager = GameObject.Find("CurrencyManager").GetComponent<CurrencyManager>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag.GetComponent<DragTower>() != null)
        {
            if (transform.childCount > 0)
            {
                return;
            }

            if (currencyManager.SubstractCurrencyOnTowerPlacement(eventData.pointerDrag.GetComponent<DragTower>().GetPrefabTower().name))
            {
                GameObject towerObject = Instantiate(eventData.pointerDrag.GetComponent<DragTower>().GetPrefabTower(), transform);
                towerObject.transform.localPosition = Vector2.zero;
            }
        }
    }
}
