using UnityEngine;

public class PigeonTopDownController : MonoBehaviour
{
    public float moveSpeed = 3f;  // 控制鸽子移动速度
    private Vector2 inputDir;     // 输入方向

    void Update()
    {
        // 获取输入（Horizontal 对应 A/D 或 ←/→，Vertical 对应 W/S 或 ↑/↓）
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        inputDir = new Vector2(x, y).normalized; // 归一化，保证对角线移动速度不变

        // 移动
        transform.Translate(inputDir * moveSpeed * Time.deltaTime, Space.World);

        // 如果需要让鸽子面向移动方向，可以在这里调整旋转或更换 Sprite
        if (inputDir != Vector2.zero)
        {
            // 计算朝向角度
            float angle = Mathf.Atan2(inputDir.y, inputDir.x) * Mathf.Rad2Deg;
            // 如果要鸽子朝向移动方向，可在 Z 轴旋转
            // transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}
