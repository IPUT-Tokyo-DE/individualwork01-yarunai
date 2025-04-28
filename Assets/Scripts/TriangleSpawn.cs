using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.EventSystems;

public class FallingObjectSpawner : MonoBehaviour
{
    public GameObject objectPrefab;
    private float spawnY = 5f;


    private float spawnInterval = 1f; // �����X�|�[���Ԋu�i�b�j



    private int maxObjects = 100; // �����ɑ��݂���ő吔

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
            // �ő吔�𐧌�
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

        int edge = Random.Range(0, 4); // 0: ��, 1: �E, 2: ��, 3: ��

        switch (edge)
        {
            case 0: // ��
                spawnPosition = new Vector3(Random.Range(-camWidth, camWidth), camHeight + 1f, 0);
                moveDirection = Vector3.down;
                break;
            case 1: // �E
                spawnPosition = new Vector3(camWidth + 1f, Random.Range(-camHeight, camHeight), 0);
                moveDirection = Vector3.left;
                break;
            case 2: // ��
                spawnPosition = new Vector3(Random.Range(-camWidth, camWidth), -camHeight - 1f, 0);
                moveDirection = Vector3.up;
                break;
            case 3: // ��
                spawnPosition = new Vector3(-camWidth - 1f, Random.Range(-camHeight, camHeight), 0);
                moveDirection = Vector3.right;
                break;
        }

        GameObject obj = Instantiate(objectPrefab, spawnPosition, Quaternion.identity);
        var falling = obj.AddComponent<FallingObject>();
        falling.SetMoveDirection(moveDirection); // �ړ�������n��
    }



    public ScoreManager scoreManager;

    void CheckForObjectsOutsideScreen()
    {
        float camHeight = Camera.main.orthographicSize;
        float camWidth = camHeight * Camera.main.aspect;

        foreach (var obj in FindObjectsOfType<FallingObject>())
        {
            Vector3 pos = obj.transform.position;

            // ��ʊO�ɏo�����ǂ����𔻒�
            if (pos.y > camHeight + 1f || pos.y < -camHeight - 1f ||
                pos.x > camWidth + 1f || pos.x < -camWidth - 1f)
            {
                obj.Deactivate(); // �������~�߂�
                scoreManager.AddScore(1);
                StartCoroutine(DestroyAfterFrame(obj.gameObject)); // �� �����ύX
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
            gameObject.SetActive(false); // �� �ǉ��I
        }


        void Update()
        {
            if (!isActive) return;
            transform.position += moveDirection * speed * Time.deltaTime;
        }
    }





    // �X�|�[���Ԋu���X�R�A�ɉ����ĕύX
    void UpdateSpawnInterval()
    {
        if (score >= 50)
        {
            spawnInterval = Mathf.Max(0.1f, spawnInterval - 0.1f * Time.deltaTime); // �ŏ�0.1�b
        }
        else if (score >= 30)
        {
            spawnInterval = Mathf.Max(0.5f, spawnInterval - 0.1f * Time.deltaTime); // 50�_�ő��x�A�b�v
        }
        else if (score >= 10)
        {
            spawnInterval = Mathf.Max(1f, spawnInterval - 0.1f * Time.deltaTime); // 10�_�ő��x�A�b�v
        }
    }

    IEnumerator DestroyAfterFrame(GameObject obj)
    {
        yield return new WaitForSeconds(0.05f);
        if (obj != null) Destroy(obj);
    }
}



