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
    [SerializeField] private Button exitButton;
    [SerializeField] private Button levelOneButton;
    [SerializeField] private Button levelTwoButton;
    [SerializeField] private Button levelThreeButton;
    [SerializeField] private int level;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Image gameOverImage;
    [SerializeField] private Image breatheInMessage;
    [SerializeField] private Image breatheOutMessage;

    [SerializeField] private AudioClip breatheInFemaleVoice;
    [SerializeField] private AudioClip breatheOutFemaleVoice;
    [SerializeField] private AudioClip breatheInMaleVoice;
    [SerializeField] private AudioClip breatheOutMaleVoice;
    private AudioSource gameAudio;

    private int currentIndex = 0;
    private int score = 0;
    private bool allButtonsPressed = false;
    private int greenButtonIndex;
    private int redButtonIndex;

    private const float MessageDisplayDuration = 3f;
    private const float TimerDuration = 10f;
    private float timer;

    private string voiceGender = "female";
    private const string sensoryRoomScene = "School Sensory Room";

    private void Start()
    {
        InitializeGame();
        AttachButtonListeners();
        HighlightActiveLevel();
        SetupLevelButtonListeners();

        gameAudio = GetComponent<AudioSource>();
    }

    private void SetupLevelButtonListeners()
    {
        levelOneButton.onClick.AddListener(() => SetNewLevel(1));
        levelTwoButton.onClick.AddListener(() => SetNewLevel(2));
        levelThreeButton.onClick.AddListener(() => SetNewLevel(3));
    }

    private void InitializeGame()
    {
        if (level == 2 || level == 1)
        {
            if (level == 2)
            {
                ShuffleButtons();
            }
            
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
            var temp = fingerButtons[i];
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
        scoreText.gameObject.SetActive(true);
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

    public void SetNewLevel(int newLevel)
    {
        level = newLevel;
        HighlightActiveLevel();
        ResetGame();
    }

    public void ResetGame()
    {
        FilterGameEndButtons(false);
        scoreText.gameObject.SetActive(false);
        currentIndex = 0;
        score = 0;
        StopCoroutine(nameof(UpdateTimer));
        RemoveButtonListeners();
        InitializeGame();
        AttachButtonListeners();
    }

    public void LoadSensoryRoomScene() => SceneManager.LoadScene(sensoryRoomScene);

    private void FilterGameEndButtons(bool status)
    {
        restartButton.gameObject.SetActive(status);
        exitButton.gameObject.SetActive(status);
        gameOverImage.gameObject.SetActive(status);
    }

    private void RemoveButtonListeners()
    {
        for (int i = 0; i < fingerButtons.Length; i++)
        {
            int buttonIndex = i;
            fingerButtons[i].onClick.RemoveAllListeners();
        }
    }

    private void FingerClicked(int buttonIndex)
    {
        if (level == 3 && buttonIndex == redButtonIndex)
        {
            HandleGameOver();
            return;
        }

        if (level != 3 && (breatheInMessage.IsActive() || breatheOutMessage.IsActive() || buttonIndex != currentIndex))
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
        {
            FilterGameEndButtons(true);
        }
    }

    private void HandleGameOver() => FilterGameEndButtons(true);

    private IEnumerator ShowBreatheMessages()
    {

        foreach (var button in fingerButtons)
            button.interactable = false;

        breatheInMessage.gameObject.SetActive(true);
        PlayBreathingAudio(breatheInFemaleVoice, breatheInMaleVoice);
        yield return new WaitForSeconds(MessageDisplayDuration);

        breatheInMessage.gameObject.SetActive(false);
        breatheOutMessage.gameObject.SetActive(true);
        PlayBreathingAudio(breatheOutFemaleVoice, breatheOutMaleVoice);
        yield return new WaitForSeconds(MessageDisplayDuration);

        breatheOutMessage.gameObject.SetActive(false);

        foreach (var button in fingerButtons)
            button.interactable = true;

        if (allButtonsPressed && level != 3)
        {
            FilterGameEndButtons(true);
        }
    }

    private void PlayBreathingAudio(AudioClip female, AudioClip male)
    {
        if (voiceGender == "female")
            gameAudio.PlayOneShot(female);
        if (voiceGender == "male")
            gameAudio.PlayOneShot(male);
    }

    private void StartTimer()
    {
        timer = TimerDuration;
        StartCoroutine(nameof(UpdateTimer));
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

    private void ResetTimer() => timer = TimerDuration;

    public void RestartGame() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    private void SetButtonImageTransparent(Image buttonImage) => buttonImage.color = new Color(0f, 0f, 0f, 0f);

    private void HighlightActiveLevel()
    {
        levelThreeButton.GetComponent<Outline>().enabled = false;
        levelTwoButton.GetComponent<Outline>().enabled = false;
        levelOneButton.GetComponent<Outline>().enabled = false;

        if (level == 3)
        {
            levelThreeButton.GetComponent<Outline>().enabled = true;
        }
        if (level == 2)
        {
            levelTwoButton.GetComponent<Outline>().enabled = true;
        }
        if (level == 1)
        {
            levelOneButton.GetComponent<Outline>().enabled = true;
        }
    }
}
