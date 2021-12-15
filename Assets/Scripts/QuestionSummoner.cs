using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionSummoner : MonoBehaviour
{
    [SerializeField] GameObject GameManagerMain; // ���� ���� �Ŵ��� ������Ʈ
    [SerializeField] float summonTime; // �ִ� ��ȯ�ð�. �ּ� ��ȯ�ð��� �ִ��� 1/2
    [SerializeField] GameObject questionPrefab; // ���� ������
    [SerializeField] GameObject deadLine; // ������ ������ ������� ���� ���� ������Ʈ
    List<GameObject> questions; // ���� ����Ʈ
    Queue<GameObject> stayQuestions; // ��� ���� ť

    void Awake()
    {
        questions = new List<GameObject>();
        stayQuestions = new Queue<GameObject>();
    }

    private void Start()
    {
        GameObject quest;
        for(int i = 0; i < 20; i++) // 20���� ���� ������Ʈ�� �̸� �����Ͽ� ť�� �׾Ƶд�
        {
            quest = Instantiate(questionPrefab, this.transform);
            quest.GetComponent<Question>().Summoner = this.gameObject;
            quest.GetComponent<Question>().QuestionIndex = i;
            quest.GetComponent<Question>().DeadLine = deadLine;
            stayQuestions.Enqueue(quest);
        }
        StartCoroutine("SummonQuestion"); // �������� ��ȯ�ϴ� �ڷ�ƾ
    }

    // �������� ��ȯ�ϴ� �ڷ�ƾ
    IEnumerator SummonQuestion()
    {
        GameObject quest;
        questions.Add(stayQuestions.Dequeue());
        // summonTime�� �� �� ������ �ð���� �����ϰ� ������ ��ȯ
        while (true)
        {
            quest = stayQuestions.Dequeue();
            quest.transform.position = this.transform.position;
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
}
