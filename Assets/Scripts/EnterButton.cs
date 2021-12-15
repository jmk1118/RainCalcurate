using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class EnterButton : MonoBehaviour
{
    [SerializeField] QuestionSummoner questionSummoner; // ���� ��ȯ ������Ʈ
    [SerializeField] Text numberScreen; // ��ư�� ������ ���ڰ� ��µ� Text����
    int userAnswer;
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
        userAnswer = int.Parse(numberScreen.text);
        questionSummoner.CheckAllQuestions(userAnswer);
        //Debug.Log(userAnswer);
        numberScreen.text = "";
    }
}
