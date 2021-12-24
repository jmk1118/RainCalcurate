using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class Question : MonoBehaviour
{
    int firstNumber; // ù��° �ǿ�����
    int secondNumber; // �ι�° �ǿ�����
    int sign; // ������
    char[] signs = new char[5] { ' ', '+', '-', 'x', '/' }; // �ؽ�Ʈ�� ǥ���ϱ� ���� ���� �迭
    int answer; // ����
    StringBuilder screenText; // ������ ǥ�õǴ� �ؽ�Ʈâ
    Text text; // �ؽ�Ʈ ������Ʈ
    Coroutine move;
    float startTime;

    public GameObject Summoner { get; set; } // QuestionSummoner ������Ʈ ����� ����
    public GameObject DeadLine { get; set; } // ���� ������Ʈ�� ������� ���� ��Ÿ���� ������Ʈ
    public int QuestionIndex { get; set; } // QuestionSummoner ���������� ��ȯ�� Question ������Ʈ�� index
    public float Speed { get; set; } // �������� �ӵ�

    private void Awake()
    {
        screenText = new StringBuilder();
        text = this.transform.GetChild(0).GetComponent<Text>();
    }

    private void OnEnable()
    {
        firstNumber = Random.Range(1, 10); // ù��° �ǿ����ڿ� 1~9 ǥ��
        secondNumber = Random.Range(1, 10); // �ι�° �ǿ����ڿ� 1~9 ǥ��
        startTime = Time.time;
        // sign = Random.Range(1, 5); // ������ +,-,x,/ �� �ϳ� ǥ��
        sign = Summoner.GetComponent<QuestionSummoner>().DecisionSign();
        switch(sign)
        {
            // ����
            case 1: 
                answer = firstNumber + secondNumber;
                break;

            // ����
            // ù��° �ǿ����ڰ� �ι�° �ǿ����ں��� ���� ���, ���� ��ġ�� �ٲ۴�
            case 2: 
                if(firstNumber < secondNumber)
                {
                    int minusSwitch = firstNumber;
                    firstNumber = secondNumber;
                    secondNumber = minusSwitch;
                }
                answer = firstNumber - secondNumber;
                break;

            // ����
            case 3:
                answer = firstNumber * secondNumber;
                break;

            // ������
            // (ù��° �ǿ����� * �ι�° �ǿ�����) / �ι�° �ǿ����ڰ� �ǵ��� �����Ѵ�
            case 4:
                firstNumber = firstNumber * secondNumber;
                answer = firstNumber / secondNumber;
                break;

            default:
                break;
        }

        // �ؽ�Ʈ�� ǥ��
        screenText.Clear();
        screenText.Append(firstNumber);
        screenText.Append(signs[sign]);
        screenText.Append(secondNumber);

        text.text = screenText.ToString();

        transform.position = transform.position + Vector3.right * Random.Range(-150, 150);

        move = StartCoroutine("Move"); // �Ʒ��� �������� �ڷ�ƾ
    }

    private void OnDisable()
    {
        StopCoroutine(move); // ��Ȱ��ȭ�Ǹ� move �ڷ�ƾ�� �����Ѵ�
    }

    // �Ʒ��� �������ٰ� �ı��Ǵ� �ڷ�ƾ
    IEnumerator Move() 
    {
        int dropLength = (int)(transform.position.y - DeadLine.transform.position.y) / 10; // �������� ����

        while (transform.position.y > DeadLine.transform.position.y + 30)
        {
            yield return new WaitForSeconds(Speed);
            transform.position = transform.position + Vector3.down * dropLength;
        }

        Summoner.GetComponent<QuestionSummoner>().PoolingQuestion(QuestionIndex);

        yield break; // �ڷ�ƾ ����
    }

    // �μ��� ���� ���ڰ� �������� �ƴ��� üũ�ϴ� �޼ҵ�
    public int Check(int UserAnswer)
    {
        if(UserAnswer == answer)
            return (int)(10 * (10 - (Time.time - startTime))); // ����
        else
            return -1; // ����
    }
}
