using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionSummoner : MonoBehaviour
{
    [SerializeField] GameManager GameManager; // ���� �Ŵ���
    [SerializeField] GameObject GameManagerMain; // ���� ���� �Ŵ��� ������Ʈ
    [SerializeField] GameObject questionPrefab; // ���� ������
    [SerializeField] GameObject deadLine; // ������ ������ ������� ���� ���� ������Ʈ
    List<GameObject> questions; // ���� ����Ʈ
    Queue<GameObject> stayQuestions; // ��� ���� ť
    Coroutine summonQuestion; // ���� ��ȯ �ڷ�ƾ
    float summonTime; // �ִ� ��ȯ�ð�. �ּ� ��ȯ�ð��� �ִ��� 1/2
    int summonCount; // ���� ��ȯ ��
    List<int> signsOption;

    void Awake()
    {
        questions = new List<GameObject>();
        stayQuestions = new Queue<GameObject>();
        summonTime = 3.0f; // ���� �ִ� ��ȯ �ð� �ʱ�ȭ
        signsOption = new List<int>();

        GameObject quest;
        for (int i = 0; i < 20; i++) // 20���� ���� ������Ʈ�� �̸� �����Ͽ� ť�� �׾Ƶд�
        {
            quest = Instantiate(questionPrefab, this.transform);
            quest.GetComponent<Question>().Summoner = this.gameObject;
            quest.GetComponent<Question>().QuestionIndex = i;
            quest.GetComponent<Question>().DeadLine = deadLine;
            stayQuestions.Enqueue(quest);
        }
    }

    private void OnEnable()
    {
        summonCount = 0; // ���� ��ȯ �� �ʱ�ȭ

        // ��Ģ���� �ɼ� ȹ��
        signsOption.Clear();
        if (GameManager.Plus)
            signsOption.Add(1);
        if (GameManager.Minus)
            signsOption.Add(2);
        if (GameManager.Multipication)
            signsOption.Add(3);
        if (GameManager.Division)
            signsOption.Add(4);

        summonQuestion = StartCoroutine("SummonQuestion"); // �������� ��ȯ�ϴ� �ڷ�ƾ
    }

    private void OnDisable()
    {
        StopCoroutine(summonQuestion);

        foreach(GameObject quest in questions)
        {
            quest.SetActive(false);
            stayQuestions.Enqueue(quest);
        }
        questions.Clear();
    }

    // �������� ��ȯ�ϴ� �ڷ�ƾ
    IEnumerator SummonQuestion()
    {
        GameObject quest;
        // summonTime�� �� �� ������ �ð���� �����ϰ� ������ ��ȯ
        while (true)
        {
            quest = stayQuestions.Dequeue();
            quest.transform.position = this.transform.position;

            // ���� ���� �ӵ� ����
            quest.GetComponent<Question>().Speed = 1.0f - (summonCount / 100.0f);
            if (summonCount < 100)
                summonCount++;

            quest.SetActive(true);
            questions.Add(quest);
            yield return new WaitForSeconds(Random.Range(summonTime / 2, summonTime));
        }
    }

    // �������� �Է¹��� ���� �������� Ȯ���ϴ� �޼ҵ�
    public void CheckAllQuestions(int userAnswer)
    {
        for(int i = 0; i < questions.Count; i++)
        {
            if(questions[i].GetComponent<Question>().Check(userAnswer))
            {
                questions[i].SetActive(false);
                stayQuestions.Enqueue(questions[i]);
                questions.RemoveAt(i);
                GameManagerMain.GetComponent<GameManagerMain>().GetPoint();
                break;
            }
        }
    }

    // ������ ������ ���� ������Ʈ�� �ٽ� ��� ���·� ����� �޼ҵ�
    public void PoolingQuestion(int index)
    {
        for (int i = 0; i < questions.Count; i++)
        {
            if (questions[i].GetComponent<Question>().QuestionIndex == index)
            {
                questions[i].SetActive(false);
                stayQuestions.Enqueue(questions[i]);
                questions.RemoveAt(i);
                GameManagerMain.GetComponent<GameManagerMain>().LosePoint();
                break;
            }
        }
    }

    // ��Ģ���� �ɼǿ� ���� sign(��ȣ)���� �����ִ� �޼ҵ�
    public int DecisionSign()
    {
        if (signsOption.Count == 0)
            return -1;

        return signsOption[Random.Range(0, int.MaxValue) % signsOption.Count];
    }
}
