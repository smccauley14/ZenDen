using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class FingerTipGame : MonoBehaviour
{
    [SerializeField] private Button[] fingerButtons;
    [SerializeField] private Button restartButton;
    [SerializeField] private int level;
    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private TextMeshProUGUI scoreText;

    private int currentIndex = 0;
    private int score = 0;
    private bool allButtonsPressed = false;
    private int greenButtonIndex;
    private int redButtonIndex;

    private const float MessageDisplayDuration = 3f;
    private const float TimerDuration = 10f;
    private float timer;

    private void Start()
    {
        InitializeGame();
        AttachButtonListeners();
    }

    private void InitializeGame()
    {
        if (level == 2)
        {
            ShuffleButtons();
            InitializeButtons();
        }
        else if (level == 3)
        {
            InitializeLevel3();
            StartTimer();
        }
    }

    private void ShuffleButtons()
    {
        for (int i = 0; i < fingerButtons.Length; i++)
        {
            int randomIndex = Random.Range(i, fingerButtons.Length);
            Button temp = fingerButtons[i];
            fingerButtons[i] = fingerButtons[randomIndex];
            fingerButtons[randomIndex] = temp;
        }
    }

    private void InitializeButtons()
    {
        for (int i = 0; i < fingerButtons.Length; i++)
        {
            var buttonImage = fingerButtons[i].GetComponent<Image>();
            if (buttonImage != null)
            {
                if (i == 0)
                    buttonImage.color = Color.green;
                else
                    SetButtonImageTransparent(buttonImage);
            }
        }
    }

    private void InitializeLevel3()
    {
        SetNextSelections();
    }

    private void SetNextSelections()
    {
        if (currentIndex == 0)
        {
            greenButtonIndex = Random.Range(0, fingerButtons.Length);
            do
            {
                redButtonIndex = Random.Range(0, fingerButtons.Length);
            } while (redButtonIndex == greenButtonIndex);
        }
        else
        {
            greenButtonIndex = redButtonIndex;
            do
            {
                redButtonIndex = Random.Range(0, fingerButtons.Length);
            } while (redButtonIndex == greenButtonIndex);
        }

        for (int i = 0; i < fingerButtons.Length; i++)
        {
            var buttonImage = fingerButtons[i].GetComponent<Image>();
            if (buttonImage != null)
            {
                if (i == greenButtonIndex)
                    buttonImage.color = Color.green;
                else if (i == redButtonIndex)
                    buttonImage.color = Color.red;
                else
                    SetButtonImageTransparent(buttonImage);
            }
        }
    }

    private void AttachButtonListeners()
    {
        for (int i = 0; i < fingerButtons.Length; i++)
        {
            int buttonIndex = i;
            fingerButtons[i].onClick.AddListener(() => FingerClicked(buttonIndex));
        }
    }

    private void FingerClicked(int buttonIndex)
    {
        if (level == 3 && buttonIndex == redButtonIndex)
        {
            HandleGameOver();
            return;
        }

        if (level != 3 && (messageText.enabled || buttonIndex != currentIndex))
            return;

        SetButtonImageTransparent(fingerButtons[currentIndex].GetComponent<Image>());

        if (level != 3)
        {
            StartCoroutine(ShowBreatheMessages());
            currentIndex++;

            if (currentIndex >= fingerButtons.Length)
                allButtonsPressed = true;
            else
                fingerButtons[currentIndex].GetComponent<Image>().color = Color.green;
        }

        if (level == 3 && buttonIndex == greenButtonIndex)
        {
            score++;
            scoreText.text = "Score: " + score.ToString();
            SetNextSelections();
            ResetTimer();
        }

        if (allButtonsPressed && level != 3)
            restartButton.gameObject.SetActive(true);
    }

    private void HandleGameOver()
    {
        messageText.text = "Game Over!";
        restartButton.gameObject.SetActive(true);
    }

    private IEnumerator ShowBreatheMessages()
    {
        messageText.enabled = true;
        foreach (var button in fingerButtons)
            button.interactable = false;

        messageText.text = "Breathe In";
        yield return new WaitForSeconds(MessageDisplayDuration);

        messageText.text = "Breathe Out";
        yield return new WaitForSeconds(MessageDisplayDuration);

        messageText.text = "";

        foreach (var button in fingerButtons)
            button.interactable = true;

        messageText.enabled = false;

        if (allButtonsPressed && level != 3)
            restartButton.gameObject.SetActive(true);
    }

    private void StartTimer()
    {
        timer = TimerDuration;
        StartCoroutine(UpdateTimer());
    }

    private IEnumerator UpdateTimer()
    {
        while (timer > 0f)
        {
            yield return new WaitForSeconds(1f);
            timer -= 1f;
        }
        HandleGameOver();
    }

    private void ResetTimer()
    {
        timer = TimerDuration;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void SetButtonImageTransparent(Image buttonImage)
    {
        buttonImage.color = new Color(0f, 0f, 0f, 0f);
    }
}
