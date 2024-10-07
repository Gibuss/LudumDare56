using UnityEngine;

public class defenderLife : MonoBehaviour
{
    public int MaxLife = 100;
    public int currentLife;

    public emptylife lifeBar;
    public RectTransform lifeBarObject;
    public Vector3 lifeBarOffset = new Vector3(0, 50, 0);
    public Camera mainCamera;
    public GameObject endMenu;

    public void Awake()
    {
        endMenu.SetActive(false);
        Time.timeScale = 1f;
    }
    public void Start()
    {
        currentLife = MaxLife;
        lifeBar.SetMaxLife(MaxLife);

        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            TakeDamage(20);
        }

        if (lifeBarObject != null)
        {
            Vector3 screenPosition = mainCamera.WorldToScreenPoint(transform.position);
            lifeBarObject.position = screenPosition + lifeBarOffset;
        }
    }

    public void TakeDamage(int damage)
    {
        currentLife -= damage;
        lifeBar.SetLife(currentLife);

        if (currentLife <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
        Debug.Log("Le défenseur est mort");
        endMenu.SetActive(true);
        Time.timeScale = 0f;
    }
}
