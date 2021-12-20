using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject startPanel; // ���� ȭ��
    [SerializeField] GameObject mainPanel; // ���� ȭ��
    [SerializeField] GameObject optionPanel; // �ɼ� ȭ��
    [SerializeField] GameObject pausePanel; // �Ͻ����� ȭ��
    [SerializeField] GameObject gameOverPanel; // ���ӿ��� ȭ��
    bool isPause; // �Ͻ����� ����
    public bool Plus { get; set; }
    public bool Minus { get; set; }
    public bool Multipication { get; set; }
    public bool Division { get; set; }

    private void Awake()
    {
        isPause = false; // �Ͻ��������� ���� ���·� �ʱ�ȭ
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

    // ���� ���� ��ư �޼ҵ�
    public void StartGame()
    {
        mainPanel.SetActive(true);
        startPanel.SetActive(false);
    }

    // ���� ���� ��ư �޼ҵ�
    public void OptionGame()
    {
        optionPanel.SetActive(true);
    }

    // ���� ���� �ɼ� �޼ҵ�
    public void PlusOption(bool check)
    {
        Plus = check;
    }

    // ���� ���� �ɼ� �޼ҵ�
    public void MinusOption(bool check)
    {
        Minus = check;
    }

    // ���� ���� �ɼ� �޼ҵ�
    public void MultiOption(bool check)
    {
        Multipication = check;
    }

    // ������ ���� �ɼ� �޼ҵ�
    public void DivisionOption(bool check)
    {
        Division = check;
    }

    // �ɼ� â�� �ݴ� �޼ҵ�
    public void OptionClose()
    {
        if (!Plus && !Minus && !Multipication && !Division) // ���� false�� ��� �۵����� �ʴ´�
        {
            // �ɼ��� �ϳ� �̻� �������ּ��� â �����
            return;
        }

        optionPanel.SetActive(false);
    }

    // ���� ���� ��ư �޼ҵ�
    public void EndGame()
    {
        Application.Quit();
    }

    // �Ͻ����� UI���� YES�� �����ϸ� ����ȭ������ ���ư��� �޼ҵ�
    public void PauseYes()
    {
        startPanel.SetActive(true);
        mainPanel.SetActive(false);
        pausePanel.SetActive(false);
        Time.timeScale = 1;
        isPause = false;
    }

    // �Ͻ����� UI���� NO�� �����ϸ� ������ �ٽ� �����ϴ� �޼ҵ�
    public void PauseNo()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1;
        isPause = false;
    }

    // ü���� 0�� �Ǹ� ȣ��Ǵ� �޼ҵ�
    // ���� ���� �г��� Ȱ��ȭ�ϰ� ������ ������Ų��
    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0;
        isPause = true;
    }

    // ���� ���� �гο��� ������� ������ ����Ǵ� �޼ҵ�
    public void ReStart()
    {
        mainPanel.SetActive(false);
        mainPanel.SetActive(true);

        // ���� ���� ȭ���� ���� �������� �ǵ�����
        gameOverPanel.SetActive(false);
        Time.timeScale = 1;
        isPause = false;
    }

    // ���� ���� �гο��� ��������� �ʱ⸦ ������ ����Ǵ� �޼ҵ�
    public void BackStartPanel()
    {
        startPanel.SetActive(true);
        mainPanel.SetActive(false);

        // ���� ���� ȭ���� ���� �������� �ǵ�����
        gameOverPanel.SetActive(false);
        Time.timeScale = 1;
        isPause = false;
    }
}
