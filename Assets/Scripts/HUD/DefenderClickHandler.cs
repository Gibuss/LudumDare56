using UnityEngine;

public class DefenderClickHandler : MonoBehaviour
{
    [SerializeField] private GameObject canvasToShow;
    private Camera mainCamera;

    private void Start()
    {
        if (canvasToShow != null)
        {
            canvasToShow.SetActive(false);
            Debug.Log("Canvas d�sactiv� au d�marrage.");
        }

        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(mainCamera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null)
            {
                if (hit.collider.gameObject == gameObject)
                {
                    ToggleCanvas();
                }
                else
                {
                    HideCanvas();
                }
            }
        }
    }

    private void ToggleCanvas()
    {
        if (canvasToShow != null)
        {
            bool isActive = canvasToShow.activeSelf;
            canvasToShow.SetActive(!isActive);
            Debug.Log($"Le Canvas est maintenant {(isActive ? "cach�" : "affich�")}.");
        }
    }

    private void HideCanvas()
    {
        if (canvasToShow != null && canvasToShow.activeSelf)
        {
            canvasToShow.SetActive(false);
            Debug.Log("Canvas cach�.");
        }
    }
}
