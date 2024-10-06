using UnityEngine;
using UnityEngine.UI;

public class enemyLife : MonoBehaviour
{
    private CurrencyManager currencyManager;
    public int MaxLife = 100;
    public int currentLife;

    public emptylife lifeBarScript;  // R�f�rence au script emptylife de l'enfant
    public Slider lifeBar;            // Type de la barre de vie (Slider)
    public RectTransform lifeBarObject;
    public Vector3 lifeBarOffset = new Vector3(0, 50, 0);
    public Camera mainCamera;

    public void Awake()
    {
        // Initialiser le CurrencyManager
        currencyManager = GameObject.Find("CurrencyManager").GetComponent<CurrencyManager>();
        currentLife = MaxLife;

        // Cherche le script emptylife parmi les enfants
        lifeBarScript = GetComponentInChildren<emptylife>();

        if (lifeBarScript != null)
        {
            // Assigne le Slider � partir du script emptylife
            lifeBar = lifeBarScript.slider; // On r�cup�re le Slider du script emptylife
            lifeBarScript.SetMaxLife(MaxLife);
            Debug.Log("Life bar trouv�e et assign�e correctement : " + lifeBar.name);
        }


        // V�rifie la cam�ra principale
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TakeDamage(20); // Test pour voir si la barre de vie fonctionne
        }

        // Mise � jour de la position de la barre de vie
        if (lifeBarObject != null)
        {
            Vector3 screenPosition = mainCamera.WorldToScreenPoint(transform.position + lifeBarOffset);
            lifeBarObject.position = screenPosition;
        }

        // Log le nom de la barre de vie � chaque frame
        if (lifeBar != null)
        {
            Debug.Log("Nom de la Life Bar : " + lifeBar.name);
        }
    }

    // R�duire la vie de l'ennemi
    public void TakeDamage(int damage)
    {
        currentLife -= damage;
        currentLife = Mathf.Max(currentLife, 0);

        if (lifeBar != null)
        {
            lifeBarScript.SetLife(currentLife); // Utilise le script emptylife pour mettre � jour le Slider
        }

        // V�rifie si l'ennemi est mort
        if (currentLife <= 0)
        {
            currencyManager.AddCurrencyOnMobDeath(gameObject.name);
            Die();
        }
    }

    // Fonction pour tuer l'ennemi
    void Die()
    {
        Destroy(gameObject);
    }
}
