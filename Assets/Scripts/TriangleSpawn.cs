using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.EventSystems;

public class FallingObjectSpawner : MonoBehaviour
{
    public GameObject objectPrefab;
    private float spawnY = 5f;


    private float spawnInterval = 1f; // 初期スポーン間隔（秒）



    private int maxObjects = 100; // 同時に存在する最大数

    int score = 0;

    [SerializeField]
    TextMeshProUGUI scoreText;


    void Start()
    {
        StartCoroutine(SpawnFallingObjects());
    }


    void Update()
    {
        CheckForObjectsOutsideScreen();
        UpdateSpawnInterval();
    }


    IEnumerator SpawnFallingObjects()
    {
        while (true)
        {
            // 最大数を制限
            int currentObjects = FindObjectsOfType<FallingObject>().Length;

            if (currentObjects < maxObjects)
            {
                SpawnFallingObject();
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }







    void SpawnFallingObject()
    {
        Vector3 spawnPosition = Vector3.zero;
        Vector3 moveDirection = Vector3.zero;

        float camHeight = Camera.main.orthographicSize;
        float camWidth = camHeight * Camera.main.aspect;

        int edge = Random.Range(0, 4); // 0: 上, 1: 右, 2: 下, 3: 左

        switch (edge)
        {
            case 0: // 上
                spawnPosition = new Vector3(Random.Range(-camWidth, camWidth), camHeight + 1f, 0);
                moveDirection = Vector3.down;
                break;
            case 1: // 右
                spawnPosition = new Vector3(camWidth + 1f, Random.Range(-camHeight, camHeight), 0);
                moveDirection = Vector3.left;
                break;
            case 2: // 下
                spawnPosition = new Vector3(Random.Range(-camWidth, camWidth), -camHeight - 1f, 0);
                moveDirection = Vector3.up;
                break;
            case 3: // 左
                spawnPosition = new Vector3(-camWidth - 1f, Random.Range(-camHeight, camHeight), 0);
                moveDirection = Vector3.right;
                break;
        }

        GameObject obj = Instantiate(objectPrefab, spawnPosition, Quaternion.identity);
        var falling = obj.AddComponent<FallingObject>();
        falling.SetMoveDirection(moveDirection); // 移動方向を渡す
    }



    public ScoreManager scoreManager;

    void CheckForObjectsOutsideScreen()
    {
        float camHeight = Camera.main.orthographicSize;
        float camWidth = camHeight * Camera.main.aspect;

        foreach (var obj in FindObjectsOfType<FallingObject>())
        {
            Vector3 pos = obj.transform.position;

            // 画面外に出たかどうかを判定
            if (pos.y > camHeight + 1f || pos.y < -camHeight - 1f ||
                pos.x > camWidth + 1f || pos.x < -camWidth - 1f)
            {
                obj.Deactivate(); // 動きを止める
                scoreManager.AddScore(1);
                StartCoroutine(DestroyAfterFrame(obj.gameObject)); // ← ここ変更
            }
        }
    }

    public class FallingObject : MonoBehaviour
    {
        private Vector3 moveDirection = Vector3.down;
        private float speed = 6f;
        private bool isActive = true;

        public void SetMoveDirection(Vector3 direction)
        {
            moveDirection = direction.normalized;
        }

        public void Deactivate()
        {
            isActive = false;
            gameObject.SetActive(false); // ← 追加！
        }


        void Update()
        {
            if (!isActive) return;
            transform.position += moveDirection * speed * Time.deltaTime;
        }
    }





    // スポーン間隔をスコアに応じて変更
    void UpdateSpawnInterval()
    {
        if (score >= 50)
        {
            spawnInterval = Mathf.Max(0.1f, spawnInterval - 0.1f * Time.deltaTime); // 最小0.1秒
        }
        else if (score >= 30)
        {
            spawnInterval = Mathf.Max(0.5f, spawnInterval - 0.1f * Time.deltaTime); // 50点で速度アップ
        }
        else if (score >= 10)
        {
            spawnInterval = Mathf.Max(1f, spawnInterval - 0.1f * Time.deltaTime); // 10点で速度アップ
        }
    }

    IEnumerator DestroyAfterFrame(GameObject obj)
    {
        yield return new WaitForSeconds(0.05f);
        if (obj != null) Destroy(obj);
    }
}



