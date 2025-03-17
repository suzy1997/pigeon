using UnityEngine;
using UnityEngine.UI;

public class FriesController : MonoBehaviour
{
    [Header("Steal Settings")]
    public float stealTime = 3f;           // 需要长按 3 秒
    private float currentProgress = 0f;    // 当前累积时间

    [Header("UI References")]
    public GameObject progressBarObject;   // 进度条的父物体
    public Image progressFill;             // 进度条的填充 Image

    private bool pigeonInRange = false;    // 是否有鸽子在触发器范围
    private bool isStealing = false;       // 是否正在偷的过程

    void Start()
    {
        // 开始时隐藏进度条
        if (progressBarObject != null)
            progressBarObject.SetActive(false);
    }

    void Update()
    {
        if (pigeonInRange)
        {
            HandleStealing();
        }
    }

    private void HandleStealing()
    {
        // 1. 检测玩家是否长按鼠标
        bool mouseHeld = Input.GetMouseButton(0); // 左键按住

        // 2. 检测玩家是否按了左右移动键
        //    如果你是用 W/A/S/D 控制，可以分别判断
        //    这里简单用 Horizontal 输入轴检测
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // 如果按了左右键(或任意方向键)，就重置进度并中断
        if (Mathf.Abs(horizontal) > 0.01f || Mathf.Abs(vertical) > 0.01f)
        {
            ResetProgress();
            return;
        }

        if (mouseHeld)
        {
            // 玩家在长按鼠标且未移动
            currentProgress += Time.deltaTime;
            isStealing = true;

            // 显示进度条
            if (progressBarObject != null && !progressBarObject.activeSelf)
                progressBarObject.SetActive(true);

            // 更新进度条可视
            if (progressFill != null)
            {
                float fillAmount = currentProgress / stealTime; // 0 ~ 1
                progressFill.fillAmount = fillAmount;
            }

            // 达到偷取所需时间
            if (currentProgress >= stealTime)
            {
                OnStealSuccess();
            }
        }
        else
        {
            // 如果鼠标没有按住，则重置进度
            if (isStealing)
            {
                ResetProgress();
            }
        }
    }

    private void OnStealSuccess()
    {
        Debug.Log("Steal Success! You got the fries!");
        // 可以在此让薯条消失、或切换到“叼住薯条”状态
        // 比如：gameObject.SetActive(false);

        // 也可以隐藏进度条
        if (progressBarObject != null)
            progressBarObject.SetActive(false);

        // 重置变量
        currentProgress = 0f;
        isStealing = false;
    }

    private void ResetProgress()
    {
        currentProgress = 0f;
        isStealing = false;
        // 重置进度条
        if (progressFill != null)
            progressFill.fillAmount = 0f;
    }

    // 当鸽子进入触发器范围
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // 假设鸽子标签是 "Player"
        {
            pigeonInRange = true;
            Debug.Log("Pigeon in range. Ready to steal.");
        }
    }

    // 当鸽子离开触发器范围
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            pigeonInRange = false;
            // 离开范围则重置进度 & 隐藏进度条
            ResetProgress();
            if (progressBarObject != null)
                progressBarObject.SetActive(false);
        }
    }
}
