using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlaceTower : MonoBehaviour
{
    [SerializeField] private GameObject towerPrefab;
    private bool isTowerThere;

    public void Start()
    {
        isTowerThere = false;
    }

    void OnMouseDown()
    {
        if (Input.GetKey(KeyCode.Mouse0) && !isTowerThere)
        {
            CreateTower();
        }
    }

    public void CreateTower()
    {
        GameObject towerObject = Instantiate(towerPrefab, gameObject.transform);
        isTowerThere = true;
        Debug.Log("Tower Deplaced");
    }
}
