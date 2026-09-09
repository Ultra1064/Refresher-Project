using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] Stats myStats;
    [SerializeField] float maxHealth;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myStats.Health = maxHealth;
    }
    public void TakeDamage(float amount)
    {
        myStats.Health -= amount;
    }
}
