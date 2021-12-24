using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionSummoner : MonoBehaviour
{
    [SerializeField] GameManager GameManager; // 게임 매니저
    [SerializeField] GameObject GameManagerMain; // 메인 게임 매니저 오브젝트
    [SerializeField] GameObject questionPrefab; // 문제 프리팹
    [SerializeField] GameObject deadLine; // 문제가 닿으면 사라지는 최종 라인 오브젝트
    List<GameObject> questions; // 문제 리스트
    Queue<GameObject> stayQuestions; // 대기 문제 큐
    Coroutine summonQuestion; // 문제 소환 코루틴
    int summonSpeed; // 문제가 떨어지는 속도
    List<int> signsOption;

    void Awake()
    {
        questions = new List<GameObject>();
        stayQuestions = new Queue<GameObject>();
        signsOption = new List<int>();

        GameObject quest;
        for (int i = 0; i < 20; i++) // 20개의 문제 오브젝트를 미리 생성하여 큐에 쌓아둔다
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
        summonSpeed = 0; // 문제가 떨어지는 속도 초기화

        // 사칙연산 옵션 획득
        signsOption.Clear();
        if (GameManager.Plus)
            signsOption.Add(1);
        if (GameManager.Minus)
            signsOption.Add(2);
        if (GameManager.Multipication)
            signsOption.Add(3);
        if (GameManager.Division)
            signsOption.Add(4);

        summonQuestion = StartCoroutine("SummonQuestion"); // 문제들을 소환하는 코루틴
    }

    private void OnDisable()
    {
        StopCoroutine(summonQuestion); // 문제 소환 코루틴 종료

        // 문제들 비활성화
        foreach(GameObject quest in questions)
        {
            quest.SetActive(false);
            stayQuestions.Enqueue(quest);
        }
        questions.Clear(); // 소환된 문제들을 모두 비움
    }

    // 문제들을 소환하는 코루틴
    IEnumerator SummonQuestion()
    {
        GameObject quest;
        // summonTime과 그 반 사이의 시간대로 랜덤하게 문제를 소환
        while (true)
        {
            quest = stayQuestions.Dequeue();
            quest.transform.position = this.transform.position;

            // 문제 낙하 속도 조절
            quest.GetComponent<Question>().Speed = 1.0f - (summonSpeed / 100.0f);
            if (summonSpeed < 70)
            {
                summonSpeed++;
            }

            quest.SetActive(true);
            questions.Add(quest);
            yield return new WaitForSeconds(Random.Range(1.0f, 2.0f));
        }
    }

    // 유저에게 입력받은 수가 정답인지 확인하는 메소드
    public void CheckAllQuestions(int userAnswer)
    {
        int nowpoint;
        for(int i = 0; i < questions.Count; i++)
        {
            nowpoint = questions[i].GetComponent<Question>().Check(userAnswer);
            if (nowpoint != -1)
            {
                questions[i].SetActive(false);
                stayQuestions.Enqueue(questions[i]);
                questions.RemoveAt(i);
                GameManagerMain.GetComponent<GameManagerMain>().GetPoint(nowpoint);
                break;
            }
        }
    }

    // 완전히 떨어진 문제 오브젝트를 다시 대기 상태로 만드는 메소드
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

    // 사칙연산 옵션에 따라 sign(부호)값을 정해주는 메소드
    public int DecisionSign()
    {
        if (signsOption.Count == 0)
            return -1;

        return signsOption[Random.Range(0, int.MaxValue) % signsOption.Count];
    }
}
