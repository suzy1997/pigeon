using UnityEngine;
using UnityEngine.UI;

public class FriesController : MonoBehaviour
{
    [Header("Steal Settings")]
    public float stealTime = 3f;           // ��Ҫ���� 3 ��
    private float currentProgress = 0f;    // ��ǰ�ۻ�ʱ��

    [Header("UI References")]
    public GameObject progressBarObject;   // �������ĸ�����
    public Image progressFill;             // ����������� Image

    private bool pigeonInRange = false;    // �Ƿ��и����ڴ�������Χ
    private bool isStealing = false;       // �Ƿ�����͵�Ĺ���

    void Start()
    {
        // ��ʼʱ���ؽ�����
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
        // 1. �������Ƿ񳤰����
        bool mouseHeld = Input.GetMouseButton(0); // �����ס

        // 2. �������Ƿ��������ƶ���
        //    ��������� W/A/S/D ���ƣ����Էֱ��ж�
        //    ������� Horizontal ��������
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // ����������Ҽ�(�����ⷽ���)�������ý��Ȳ��ж�
        if (Mathf.Abs(horizontal) > 0.01f || Mathf.Abs(vertical) > 0.01f)
        {
            ResetProgress();
            return;
        }

        if (mouseHeld)
        {
            // ����ڳ��������δ�ƶ�
            currentProgress += Time.deltaTime;
            isStealing = true;

            // ��ʾ������
            if (progressBarObject != null && !progressBarObject.activeSelf)
                progressBarObject.SetActive(true);

            // ���½���������
            if (progressFill != null)
            {
                float fillAmount = currentProgress / stealTime; // 0 ~ 1
                progressFill.fillAmount = fillAmount;
            }

            // �ﵽ͵ȡ����ʱ��
            if (currentProgress >= stealTime)
            {
                OnStealSuccess();
            }
        }
        else
        {
            // ������û�а�ס�������ý���
            if (isStealing)
            {
                ResetProgress();
            }
        }
    }

    private void OnStealSuccess()
    {
        Debug.Log("Steal Success! You got the fries!");
        // �����ڴ���������ʧ�����л�������ס������״̬
        // ���磺gameObject.SetActive(false);

        // Ҳ�������ؽ�����
        if (progressBarObject != null)
            progressBarObject.SetActive(false);

        // ���ñ���
        currentProgress = 0f;
        isStealing = false;
    }

    private void ResetProgress()
    {
        currentProgress = 0f;
        isStealing = false;
        // ���ý�����
        if (progressFill != null)
            progressFill.fillAmount = 0f;
    }

    // �����ӽ��봥������Χ
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // ������ӱ�ǩ�� "Player"
        {
            pigeonInRange = true;
            Debug.Log("Pigeon in range. Ready to steal.");
        }
    }

    // �������뿪��������Χ
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            pigeonInRange = false;
            // �뿪��Χ�����ý��� & ���ؽ�����
            ResetProgress();
            if (progressBarObject != null)
                progressBarObject.SetActive(false);
        }
    }
}
