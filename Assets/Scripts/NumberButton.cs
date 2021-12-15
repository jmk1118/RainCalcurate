using UnityEngine;
using UnityEngine.UI;
using System.Text;

/// <summary>
/// ���� �Է� ��ư�� Ŭ����
/// </summary>
public class NumberButton : MonoBehaviour
{
    [SerializeField] int buttonNumber; // �� ��ư�� ���� ����
    [SerializeField] Text numberScreen; // ��ư�� ������ ���ڰ� ��µ� Text����
    StringBuilder screenText; // ����� stringbuilder
    Button button; // �� ��ü�� �Ҵ�� ��ư

    /// <summary>
    /// screenText �ʱ�ȭ
    /// ��ư ������Ʈ�� ButtonClick �޼ҵ� ����
    /// </summary>
    private void Awake()
    {
        screenText = new StringBuilder();
        button = this.GetComponent<Button>();
    }

    private void Start()
    {
        button.onClick.AddListener(ButtonClick);
    }

    /// <summary>
    /// ��ư�� Ŭ���ϸ� ����Ǵ� �Լ�
    /// ���� �ؽ�Ʈ�� ���ڸ� �߰��Ѵ�
    /// </summary>
    public void ButtonClick()
    {
        screenText.Clear();
        screenText.Append(numberScreen.text);
        if(screenText.Length < 9) // 10 �̻��� int���� ������ �Ѿ��
        {
            screenText.Append(buttonNumber);
            numberScreen.text = screenText.ToString();
        }
        else
        {
            // ���� ó��
        }
    }
}
