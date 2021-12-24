using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class BackButton : MonoBehaviour
{
    [SerializeField] Text numberScreen; // ��ư�� ������ ���ڰ� ��µ� Text����
    StringBuilder screenText;
    Button button;

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
    /// �Էµ� ���ڷ� ���� ó���� �Ѵ�
    /// �Էµ� ���ڸ� �����Ѵ�
    /// </summary>
    public void ButtonClick()
    {
        if (numberScreen.text.Length <= 0)
            return;

        screenText.Clear();
        screenText.Append(numberScreen.text);
        screenText.Remove(screenText.Length - 1, 1);
        numberScreen.text = screenText.ToString();
    }
}
