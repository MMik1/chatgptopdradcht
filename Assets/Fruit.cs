using UnityEngine;

public class Fruit : MonoBehaviour
{
    private FruitSpawner spawner;
    private bool isHit = false;

    public void SetSpawner(FruitSpawner spawner)
    {
        this.spawner = spawner;
    }

    void OnBecameInvisible()
    {
        Debug.Log("Fruit became invisible: " + gameObject.name);
        if (!isHit)
        {
            if (spawner != null)
            {
                spawner.LoseLife();
            }
        }
        Destroy(gameObject);
    }

    public void Hit()
    {
        Debug.Log("Fruit hit by blade: " + gameObject.name);
        isHit = true;
        if (spawner != null)
        {
            spawner.IncreaseScore(1);
        }
        Destroy(gameObject);
    }
}
