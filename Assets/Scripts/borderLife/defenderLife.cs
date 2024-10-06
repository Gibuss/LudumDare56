using UnityEngine;

public class defenderLife : MonoBehaviour
{
    public int MaxLife = 100;
    public int currentLife;

    public emptylife lifeBar;
    public RectTransform lifeBarObject;
    public Vector3 lifeBarOffset = new Vector3(0, 50, 0);
    public Camera mainCamera;

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
        // Cette section peut être supprimée ou maintenue si vous souhaitez garder une méthode de test
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

    // Changer la visibilité de cette méthode à public
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
    }
}
