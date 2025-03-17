using UnityEngine;

public class PigeonTopDownController : MonoBehaviour
{
    public float moveSpeed = 3f;  // ���Ƹ����ƶ��ٶ�
    private Vector2 inputDir;     // ���뷽��

    void Update()
    {
        // ��ȡ���루Horizontal ��Ӧ A/D �� ��/����Vertical ��Ӧ W/S �� ��/����
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        inputDir = new Vector2(x, y).normalized; // ��һ������֤�Խ����ƶ��ٶȲ���

        // �ƶ�
        transform.Translate(inputDir * moveSpeed * Time.deltaTime, Space.World);

        // �����Ҫ�ø��������ƶ����򣬿��������������ת����� Sprite
        if (inputDir != Vector2.zero)
        {
            // ���㳯��Ƕ�
            float angle = Mathf.Atan2(inputDir.y, inputDir.x) * Mathf.Rad2Deg;
            // ���Ҫ���ӳ����ƶ����򣬿��� Z ����ת
            // transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}
