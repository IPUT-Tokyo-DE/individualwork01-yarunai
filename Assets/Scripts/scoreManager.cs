using TMPro;  // TextMesh Pro���g�p���邽�߂ɕK�v
using UnityEngine;
using System.Collections;


public class ScoreManager : MonoBehaviour
{
    // �X�R�A�\���p��TextMesh Pro UI�̎Q��
    public TMP_Text scoreText;

    private int lastEffectScore = -1; // �����l�́A�ŏ��̃X�R�A��10�̔{���ɒB�����Ƃ��ɃA�j���[�V���������s������

    private int score;

    // Start is called before the first frame update
    void Start()
    {
        // �����X�R�A��ݒ�
        score = 0;
        UpdateScoreText();
    }

    // �X�R�A�����Z���郁�\�b�h
    IEnumerator FlashScoreText()
    {
        Vector3 originalScale = scoreText.transform.localScale;
        Color originalColor = scoreText.color; // �����F�i���F�j
        Color targetColor = Color.red; // �ԐF�ɕς���

        // �A�j���[�V�����̊���
        float duration = 0.2f;
        float time = 0f;

        // �X�P�[���ƐF��ύX
        while (time < duration)
        {
            // �X�P�[���̕��
            float scale = Mathf.Lerp(1f, 1.5f, time / duration);
            scoreText.transform.localScale = originalScale * scale;

            // �F�̕�ԁi���F����ԐF�j
            scoreText.color = Color.Lerp(originalColor, targetColor, time / duration);

            time += Time.deltaTime;
            yield return null;
        }

        // ���̐F�ƃX�P�[���ɖ߂�
        time = 0f;
        while (time < duration)
        {
            // �X�P�[���̕�ԁi���ɖ߂��j
            float scale = Mathf.Lerp(1.5f, 1f, time / duration);
            scoreText.transform.localScale = originalScale * scale;

            // �F�̕�ԁi�ԐF���物�F�ɖ߂��j
            scoreText.color = Color.Lerp(targetColor, originalColor, time / duration);

            time += Time.deltaTime;
            yield return null;
        }

        // �Ō�Ɍ��̃X�P�[���ƐF�ɖ߂�
        scoreText.transform.localScale = originalScale;
        scoreText.color = originalColor;
    }


    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreText();

        //  �X�R�A��10�̔{�����A�O��̋L�^�ƈႦ�΃A�j���[�V�����Đ��I
        if (score % 10 == 0 && score != lastEffectScore)
        {
            StartCoroutine(FlashScoreText()); // �� �������X�P�[���A�j���[�V�����I
            lastEffectScore = score;
        }
    }


    public void ResetScore()
    {
        score = 0;
        lastEffectScore = -1; // �X�R�A�A�j���̊Ǘ������Z�b�g���Ă���
        UpdateScoreText();    // �\�����X�V
    }



    // �X�R�A�\�����X�V���郁�\�b�h
    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + score.ToString();  // �X�R�A�̕\��
    }
}

