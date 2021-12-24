using UnityEngine;
using UnityEngine.UI;

public class ClearButton : MonoBehaviour
{
    [SerializeField] Text numberScreen; // ��ư�� ������ ���ڰ� ��µ� Text����
    Button button;

    /// <summary>
    /// screenText �ʱ�ȭ
    /// ��ư ������Ʈ�� ButtonClick �޼ҵ� ����
    /// </summary>
    private void Awake()
    {
        button = this.GetComponent<Button>();
    }

    private void Start()
    {
        button.onClick.AddListener(ButtonClick);
    }

    /// <summary>
    /// ��ư�� Ŭ���ϸ� ����Ǵ� �Լ�
    /// �Էµ� ���ڷ� ���� ó���� �Ѵ�
    /// �Էµ� ���ڸ� �����Ѵ�
    /// </summary>
    public void ButtonClick()
    {
        numberScreen.text = "";
    }
}
