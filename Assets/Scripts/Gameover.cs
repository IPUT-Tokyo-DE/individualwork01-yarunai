using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public GameObject gameOverText;

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log($"{gameObject.name} collided with {collision.gameObject.name}");
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Time.timeScale = 0f;
            //Debug.Log("Game Over!");
            FindObjectOfType<GameManager>().GameOver();
            if (gameOverText != null)
            {
                gameOverText.SetActive(true);
            }
        }
    }

}
