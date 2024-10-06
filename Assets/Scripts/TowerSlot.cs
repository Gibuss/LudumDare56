using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerSlot : MonoBehaviour, IDropHandler
{
    [SerializeField] private GameObject targetObject;

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop2");

        if (eventData.pointerDrag.GetComponent<DragTower>() != null)
        {
            if (targetObject.transform.childCount > 0)
            {
                Debug.Log("Une tour est d�j� pr�sente sur l'objet cible !");
                return;
            }

            GameObject towerObject = Instantiate(eventData.pointerDrag.GetComponent<DragTower>().GetPrefabTower(), targetObject.transform);
            towerObject.transform.localPosition = Vector2.zero;

            Debug.Log("Tour plac�e sur l'objet cible");
        }
    }
}
