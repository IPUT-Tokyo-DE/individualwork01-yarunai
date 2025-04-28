using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    // �L�����N�^�[���ړ��ł���͈͂̍ŏ��l�ƍő�l
    private float minX = 0.5f;
    private float maxX = 7.37f;
    private float minY = -4.5f;
    private float maxY = 4.5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 pos = transform.position;
        pos.x += horizontalInput * 0.2f;
        pos.y += verticalInput * 0.2f;

        // �L�����N�^�[���͈͊O�ɏo�Ȃ��悤�ɐ���
        pos.x = Mathf.Clamp(pos.x, minX, maxX);  // x���W��͈͓��ɐ���
        pos.y = Mathf.Clamp(pos.y, minY, maxY);  // y���W��͈͓��ɐ���

        transform.position = pos;
    }
}
