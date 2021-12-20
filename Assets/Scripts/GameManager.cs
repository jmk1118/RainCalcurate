using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject startPanel; // 시작 화면
    [SerializeField] GameObject mainPanel; // 메인 화면
    [SerializeField] GameObject optionPanel; // 옵션 화면
    [SerializeField] GameObject pausePanel; // 일시정지 화면
    [SerializeField] GameObject gameOverPanel; // 게임오버 화면
    bool isPause; // 일시정지 상태
    public bool Plus { get; set; }
    public bool Minus { get; set; }
    public bool Multipication { get; set; }
    public bool Division { get; set; }

    private void Awake()
    {
        isPause = false; // 일시정지하지 않은 상태로 초기화
        Plus = true;
        Minus = true;
        Multipication = true;
        Division = true;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(mainPanel.activeSelf)
            {
                if(!isPause)
                {
                    isPause = true;
                    Time.timeScale = 0;
                    pausePanel.SetActive(true);
                }
            }
        }
    }

    // 게임 시작 버튼 메소드
    public void StartGame()
    {
        mainPanel.SetActive(true);
        startPanel.SetActive(false);
    }

    // 게임 설정 버튼 메소드
    public void OptionGame()
    {
        optionPanel.SetActive(true);
    }

    // 덧셈 출현 옵션 메소드
    public void PlusOption(bool check)
    {
        Plus = check;
    }

    // 뺄셈 출현 옵션 메소드
    public void MinusOption(bool check)
    {
        Minus = check;
    }

    // 곱셈 출현 옵션 메소드
    public void MultiOption(bool check)
    {
        Multipication = check;
    }

    // 나눗셈 출현 옵션 메소드
    public void DivisionOption(bool check)
    {
        Division = check;
    }

    // 옵션 창을 닫는 메소드
    public void OptionClose()
    {
        if (!Plus && !Minus && !Multipication && !Division) // 전부 false일 경우 작동하지 않는다
        {
            // 옵션을 하나 이상 선택해주세요 창 만들기
            return;
        }

        optionPanel.SetActive(false);
    }

    // 게임 종료 버튼 메소드
    public void EndGame()
    {
        Application.Quit();
    }

    // 일시정지 UI에서 YES를 선택하면 시작화면으로 돌아가는 메소드
    public void PauseYes()
    {
        startPanel.SetActive(true);
        mainPanel.SetActive(false);
        pausePanel.SetActive(false);
        Time.timeScale = 1;
        isPause = false;
    }

    // 일시정지 UI에서 NO를 선택하면 게임을 다시 진행하는 메소드
    public void PauseNo()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1;
        isPause = false;
    }

    // 체력이 0이 되면 호출되는 메소드
    // 게임 오버 패널을 활성화하고 게임을 정지시킨다
    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0;
        isPause = true;
    }

    // 게임 오버 패널에서 재시작을 누르면 실행되는 메소드
    public void ReStart()
    {
        mainPanel.SetActive(false);
        mainPanel.SetActive(true);

        // 게임 오버 화면을 위한 설정들을 되돌린다
        gameOverPanel.SetActive(false);
        Time.timeScale = 1;
        isPause = false;
    }

    // 게임 오버 패널에서 재시작하지 않기를 누르면 실행되는 메소드
    public void BackStartPanel()
    {
        startPanel.SetActive(true);
        mainPanel.SetActive(false);

        // 게임 오버 화면을 위한 설정들을 되돌린다
        gameOverPanel.SetActive(false);
        Time.timeScale = 1;
        isPause = false;
    }
}
