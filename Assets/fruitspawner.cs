using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FruitSpawner : MonoBehaviour
{
    public GameObject[] fruitPrefabs;
    public GameObject bombPrefab;  // Add bomb prefab
    public float spawnInterval = 1f;
    private float timer;
    public TextMeshProUGUI scoreText;
    private int score = 0;
    public TextMeshProUGUI highscoreText;
    private int highscore = 0;

    public Image[] hearts;
    private int lives = 3;

    // Define the spawn area
    public float spawnAreaWidth = 10f;
    public float spawnAreaHeight = 5f;

    void Start()
    {
        if (highscoreText == null)
        {
            Debug.LogError("Highscore Text is not assigned in the Inspector!");
            return;
        }

        if (scoreText == null)
        {
            Debug.LogError("Score Text is not assigned in the Inspector!");
            return;
        }

        if (hearts == null || hearts.Length == 0)
        {
            Debug.LogError("Hearts are not assigned in the Inspector!");
            return;
        }

        highscore = PlayerPrefs.GetInt("Highscore", 0);
        highscoreText.text = "Highscore: " + highscore;
        UpdateScoreUI();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnObject();
            timer = 0;
        }
    }

    void SpawnObject()
    {
        // Adjust bomb spawn probability based on score
        float bombSpawnProbability = score > 50 ? 0.3f : 0.1f;  // 30% chance to spawn a bomb if score > 50, else 10%

        Vector2 spawnPosition = new Vector2(
            Random.Range(-spawnAreaWidth / 2, spawnAreaWidth / 2),
            Random.Range(-spawnAreaHeight / 2, spawnAreaHeight / 2)
        );

        if (Random.value < bombSpawnProbability)
        {
            GameObject bomb = Instantiate(bombPrefab, spawnPosition, Quaternion.identity);
            bomb.GetComponent<Rigidbody2D>().AddForce(
                new Vector2(Random.Range(-3f, 3f), Random.Range(5f, 10f)),
                ForceMode2D.Impulse
            );
            bomb.GetComponent<Bomb>().SetSpawner(this);
        }
        else
        {
            int index = Random.Range(0, fruitPrefabs.Length);
            GameObject fruit = Instantiate(fruitPrefabs[index], spawnPosition, Quaternion.identity);
            fruit.GetComponent<Rigidbody2D>().AddForce(
                new Vector2(Random.Range(-3f, 3f), Random.Range(5f, 10f)),
                ForceMode2D.Impulse
            );
            fruit.GetComponent<Fruit>().SetSpawner(this);
        }
    }

    public void IncreaseScore(int value)
    {
        score += value;
        if (score > highscore)
        {
            highscore = score;
            PlayerPrefs.SetInt("Highscore", highscore);
            highscoreText.text = "Highscore: " + highscore;
        }
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        scoreText.text = "Score: " + score;
    }

    public void LoseLife()
    {
        if (lives > 0)
        {
            lives--;
            if (lives < hearts.Length && hearts[lives] != null)
            {
                hearts[lives].enabled = false;
            }

            if (lives <= 0)
            {
                GameOver();
            }
        }
    }

    void GameOver()
    {
        Debug.Log("Game Over");
        SceneManager.LoadScene(1);
    }

    void OnDrawGizmos()
    {
        // Draw a yellow rectangle to visualize the spawn area
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, new Vector3(spawnAreaWidth, spawnAreaHeight, 0));
    }
}
