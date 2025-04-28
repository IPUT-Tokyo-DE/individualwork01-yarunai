using TMPro;  // TextMesh Proを使用するために必要
using UnityEngine;
using System.Collections;


public class ScoreManager : MonoBehaviour
{
    // スコア表示用のTextMesh Pro UIの参照
    public TMP_Text scoreText;

    private int lastEffectScore = -1; // 初期値は、最初のスコアが10の倍数に達したときにアニメーションを実行させる

    private int score;

    // Start is called before the first frame update
    void Start()
    {
        // 初期スコアを設定
        score = 0;
        UpdateScoreText();
    }

    // スコアを加算するメソッド
    IEnumerator FlashScoreText()
    {
        Vector3 originalScale = scoreText.transform.localScale;
        Color originalColor = scoreText.color; // 初期色（黄色）
        Color targetColor = Color.red; // 赤色に変える

        // アニメーションの期間
        float duration = 0.2f;
        float time = 0f;

        // スケールと色を変更
        while (time < duration)
        {
            // スケールの補間
            float scale = Mathf.Lerp(1f, 1.5f, time / duration);
            scoreText.transform.localScale = originalScale * scale;

            // 色の補間（黄色から赤色）
            scoreText.color = Color.Lerp(originalColor, targetColor, time / duration);

            time += Time.deltaTime;
            yield return null;
        }

        // 元の色とスケールに戻す
        time = 0f;
        while (time < duration)
        {
            // スケールの補間（元に戻す）
            float scale = Mathf.Lerp(1.5f, 1f, time / duration);
            scoreText.transform.localScale = originalScale * scale;

            // 色の補間（赤色から黄色に戻す）
            scoreText.color = Color.Lerp(targetColor, originalColor, time / duration);

            time += Time.deltaTime;
            yield return null;
        }

        // 最後に元のスケールと色に戻す
        scoreText.transform.localScale = originalScale;
        scoreText.color = originalColor;
    }


    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreText();

        //  スコアが10の倍数かつ、前回の記録と違えばアニメーション再生！
        if (score % 10 == 0 && score != lastEffectScore)
        {
            StartCoroutine(FlashScoreText()); // ← ここがスケールアニメーション！
            lastEffectScore = score;
        }
    }


    public void ResetScore()
    {
        score = 0;
        lastEffectScore = -1; // スコアアニメの管理もリセットしておく
        UpdateScoreText();    // 表示も更新
    }



    // スコア表示を更新するメソッド
    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + score.ToString();  // スコアの表示
    }
}

