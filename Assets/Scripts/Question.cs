using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class Question : MonoBehaviour
{
    int firstNumber; // 첫번째 피연산자
    int secondNumber; // 두번째 피연산자
    int sign; // 연산자
    char[] signs = new char[5] { ' ', '+', '-', 'x', '/' }; // 텍스트에 표시하기 위한 문자 배열
    int answer; // 정답
    StringBuilder screenText; // 문제가 표시되는 텍스트창
    Text text; // 텍스트 컴포넌트
    Coroutine move;
    float startTime;

    public GameObject Summoner { get; set; } // QuestionSummoner 오브젝트 저장용 변수
    public GameObject DeadLine { get; set; } // 문제 오브젝트가 사라지는 선을 나타내는 오브젝트
    public int QuestionIndex { get; set; } // QuestionSummoner 오브젝에서 소환한 Question 오브젝트의 index
    public float Speed { get; set; } // 떨어지는 속도

    private void Awake()
    {
        screenText = new StringBuilder();
        text = this.transform.GetChild(0).GetComponent<Text>();
    }

    private void OnEnable()
    {
        firstNumber = Random.Range(1, 10); // 첫번째 피연산자에 1~9 표시
        secondNumber = Random.Range(1, 10); // 두번째 피연산자에 1~9 표시
        startTime = Time.time;
        // sign = Random.Range(1, 5); // 연산자 +,-,x,/ 중 하나 표시
        sign = Summoner.GetComponent<QuestionSummoner>().DecisionSign();
        switch(sign)
        {
            // 덧셈
            case 1: 
                answer = firstNumber + secondNumber;
                break;

            // 뺄셈
            // 첫번째 피연산자가 두번째 피연사자보다 작을 경우, 서로 위치를 바꾼다
            case 2: 
                if(firstNumber < secondNumber)
                {
                    int minusSwitch = firstNumber;
                    firstNumber = secondNumber;
                    secondNumber = minusSwitch;
                }
                answer = firstNumber - secondNumber;
                break;

            // 곱셈
            case 3:
                answer = firstNumber * secondNumber;
                break;

            // 나눗셈
            // (첫번째 피연산자 * 두번째 피연산자) / 두번째 피연산자가 되도록 변경한다
            case 4:
                firstNumber = firstNumber * secondNumber;
                answer = firstNumber / secondNumber;
                break;

            default:
                break;
        }

        // 텍스트에 표시
        screenText.Clear();
        screenText.Append(firstNumber);
        screenText.Append(signs[sign]);
        screenText.Append(secondNumber);

        text.text = screenText.ToString();

        transform.position = transform.position + Vector3.right * Random.Range(-150, 150);

        move = StartCoroutine("Move"); // 아래로 떨어지는 코루틴
    }

    private void OnDisable()
    {
        StopCoroutine(move); // 비활성화되면 move 코루틴을 종료한다
    }

    // 아래로 떨어지다가 파괴되는 코루틴
    IEnumerator Move() 
    {
        int dropLength = (int)(transform.position.y - DeadLine.transform.position.y) / 10; // 떨어지는 길이

        while (transform.position.y > DeadLine.transform.position.y + 30)
        {
            yield return new WaitForSeconds(Speed);
            transform.position = transform.position + Vector3.down * dropLength;
        }

        Summoner.GetComponent<QuestionSummoner>().PoolingQuestion(QuestionIndex);

        yield break; // 코루틴 종료
    }

    // 인수로 받은 숫자가 정답인지 아닌지 체크하는 메소드
    public int Check(int UserAnswer)
    {
        if(UserAnswer == answer)
            return (int)(10 * (10 - (Time.time - startTime))); // 정답
        else
            return -1; // 오답
    }
}
