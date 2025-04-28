using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject startText;
    public GameObject gameOverText;
    public GameObject scoreUI;
    public GameObject returnButton; // ← 追加！
    public ScoreManager scoreManager; // ← 追加！


    public GameObject player1; // Square
    public GameObject player2; // Circle

    private Vector3 player1StartPos;
    private Quaternion player1StartRot;
    private Rigidbody2D rb1;

    private Vector3 player2StartPos;
    private Quaternion player2StartRot;
    private Rigidbody2D rb2;

    void Start()
    {
        Time.timeScale = 0f; // ゲーム全体を止める！
        // 初期位置とリジッドボディ取得
        rb1 = player1.GetComponent<Rigidbody2D>();
        rb2 = player2.GetComponent<Rigidbody2D>();

        player1StartPos = player1.transform.position;
        player1StartRot = player1.transform.rotation;

        player2StartPos = player2.transform.position;
        player2StartRot = player2.transform.rotation;

        // タイトル画面だけ表示
        startText.SetActive(true);
        gameOverText.SetActive(false);
        scoreUI.SetActive(false);
        returnButton.SetActive(false); // ← 追加！

        player1.SetActive(false);
        player2.SetActive(false);
    }

    void RemoveAllEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Collider2D col = enemy.GetComponent<Collider2D>();
            if (col != null) col.enabled = false;  // 当たり判定を無効化

            enemy.SetActive(false);  // まず見えなくして動作も止める
            Destroy(enemy);          // 次のフレームで削除
        }
    }

    public void StartGame()
    {
        RemoveAllEnemies(); // 敵を消す（後述）

        Time.timeScale = 1f; // ゲームを動かす！

        startText.SetActive(false);
        scoreUI.SetActive(true);

        player1.SetActive(true);
        player2.SetActive(true);

        rb1.linearVelocity = Vector2.zero;
        rb1.angularVelocity = 0f;

        rb2.linearVelocity = Vector2.zero;
        rb2.angularVelocity = 0f;
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        gameOverText.SetActive(true);
        returnButton.SetActive(true); // ← 追加！
    }

    public void ReturnToStart()
    {
        Time.timeScale = 0f;

        startText.SetActive(true);
        gameOverText.SetActive(false);
        returnButton.SetActive(false); // ← 追加！
        scoreUI.SetActive(false);

        player1.SetActive(false);
        player2.SetActive(false);

        player1.transform.position = player1StartPos;
        player1.transform.rotation = player1StartRot;
        rb1.linearVelocity = Vector2.zero;
        rb1.angularVelocity = 0f;

        player2.transform.position = player2StartPos;
        player2.transform.rotation = player2StartRot;
        rb2.linearVelocity = Vector2.zero;
        rb2.angularVelocity = 0f;

        scoreManager.ResetScore(); // スコアをリセット！

        RemoveAllEnemies(); // 敵を消す（後述）
    }

}
