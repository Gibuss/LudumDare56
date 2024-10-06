using UnityEngine;
using UnityEngine.UI;

public class enemyLife : MonoBehaviour
{
    private CurrencyManager currencyManager;
    public int MaxLife = 100;
    public int currentLife;

    public emptylife lifeBarScript;  // Référence au script emptylife de l'enfant
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
            // Assigne le Slider à partir du script emptylife
            lifeBar = lifeBarScript.slider; // On récupère le Slider du script emptylife
            lifeBarScript.SetMaxLife(MaxLife);
            Debug.Log("Life bar trouvée et assignée correctement : " + lifeBar.name);
        }


        // Vérifie la caméra principale
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

        // Mise à jour de la position de la barre de vie
        if (lifeBarObject != null)
        {
            Vector3 screenPosition = mainCamera.WorldToScreenPoint(transform.position + lifeBarOffset);
            lifeBarObject.position = screenPosition;
        }

        // Log le nom de la barre de vie à chaque frame
        if (lifeBar != null)
        {
            Debug.Log("Nom de la Life Bar : " + lifeBar.name);
        }
    }

    // Réduire la vie de l'ennemi
    public void TakeDamage(int damage)
    {
        currentLife -= damage;
        currentLife = Mathf.Max(currentLife, 0);

        if (lifeBar != null)
        {
            lifeBarScript.SetLife(currentLife); // Utilise le script emptylife pour mettre à jour le Slider
        }

        // Vérifie si l'ennemi est mort
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
