using UnityEngine;


public class defenderLife : MonoBehaviour
{
    public int MaxLife = 100;
    public int currentLife;

    public emptylife lifeBar;

    public void Start()
    {
        currentLife = MaxLife;
        lifeBar.SetMaxLife(MaxLife);
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.D))
        {
            TakeDamage(20);
        }
    }

    void TakeDamage(int damage)
    {
        currentLife -= damage;
        lifeBar.SetLife(currentLife);

        if(currentLife <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        Destroy(gameObject);
    }
}
