using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerSlot : MonoBehaviour, IDropHandler
{
    [SerializeField] private GameObject targetObject;
    private CurrencyManager currencyManager;

    public void Awake()
    {
        currencyManager = GameObject.Find("CurrencyManager").GetComponent<CurrencyManager>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop2");

        if (eventData.pointerDrag.GetComponent<DragTower>() != null)
        {
            if (targetObject.transform.childCount > 0)
            {
                Debug.Log("Une tour est déjà présente sur l'objet cible !");
                return;
            }

            if(currencyManager.SubstractCurrencyOnTowerPlacement(eventData.pointerDrag.GetComponent<DragTower>().GetPrefabTower().name))
            {
                GameObject towerObject = Instantiate(eventData.pointerDrag.GetComponent<DragTower>().GetPrefabTower(), targetObject.transform);
                towerObject.transform.localPosition = Vector2.zero;

                Debug.Log("Tour placée sur l'objet cible");
            } else
            {
                Debug.Log("on a pas l'argent");
            }
            

            
        }
    }
}
