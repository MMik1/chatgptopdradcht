using UnityEngine;

public class Blade : MonoBehaviour
{
    void Update()
    {
        // Move the blade to the mouse position
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mousePosition;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Fruit"))
        {
            Fruit fruit = collision.GetComponent<Fruit>();
            if (fruit != null)
            {
                fruit.Hit();  // Call the Hit method on the fruit
            }
        }
        else if (collision.CompareTag("Bomb"))
        {
            Bomb bomb = collision.GetComponent<Bomb>();
            if (bomb != null)
            {
                bomb.Hit();  // Call the Hit method on the bomb
            }
        }
    }
}
