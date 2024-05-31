using UnityEngine;

public class Bomb : MonoBehaviour
{
    private FruitSpawner spawner;

    public void SetSpawner(FruitSpawner spawner)
    {
        this.spawner = spawner;
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    public void Hit()
    {
        if (spawner != null)
        {
            spawner.LoseLife();  // Decrease a life when the bomb is hit
        }
        Destroy(gameObject);
    }
}
