using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    // キャラクターが移動できる範囲の最小値と最大値
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

        // キャラクターが範囲外に出ないように制限
        pos.x = Mathf.Clamp(pos.x, minX, maxX);  // x座標を範囲内に制限
        pos.y = Mathf.Clamp(pos.y, minY, maxY);  // y座標を範囲内に制限

        transform.position = pos;
    }
}
