using UnityEngine;

public class enemyLife : MonoBehaviour
{
    private CurrencyManager currencyManager;
    public int MaxLife = 100;
    public int currentLife;

    public emptylife lifeBar;
    public RectTransform lifeBarObject;
    public Vector3 lifeBarOffset = new Vector3(0, 50, 0);
    public Camera mainCamera;

    public void Awake()
    {
        currencyManager = GameObject.Find("CurrencyManager").GetComponent<CurrencyManager>();
        currentLife = MaxLife;
        lifeBar.SetMaxLife(MaxLife);

        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TakeDamage(20);
        }

        if (lifeBarObject != null)
        {
            Vector3 screenPosition = mainCamera.WorldToScreenPoint(transform.position + lifeBarOffset);
            lifeBarObject.position = screenPosition;
        }
    }

    public void TakeDamage(int damage)
    {
        currentLife -= damage;
        currentLife = Mathf.Max(currentLife, 0);
        lifeBar.SetLife(currentLife);

        if (currentLife <= 0)
        {
            currencyManager.AddCurrencyOnDeath(gameObject.name);
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
