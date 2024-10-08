using UnityEngine;
using UnityEngine.UI;

public class enemyLife : MonoBehaviour
{
    private CurrencyManager currencyManager;
    public int MaxLife;
    public int currentLife;

    public emptylife lifeBarScript;
    public Slider lifeBar;
    public RectTransform lifeBarObject;
    public Vector3 lifeBarOffset = new Vector3(0, 50, 0);
    public Camera mainCamera;

    public void Awake()
    {
        currencyManager = GameObject.Find("CurrencyManager").GetComponent<CurrencyManager>();
        currentLife = MaxLife;
        lifeBarScript = GetComponentInChildren<emptylife>();

        if (lifeBarScript != null)
        {
            lifeBar = lifeBarScript.slider;
            lifeBarScript.SetMaxLife(MaxLife);
        }

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

        if (lifeBar != null)
        {
            lifeBarScript.SetLife(currentLife);
        }

        if (currentLife <= 0)
        {
            currencyManager.AddCurrencyOnMobDeath(gameObject.name);
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
