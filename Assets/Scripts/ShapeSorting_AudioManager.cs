using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeSorting_AudioManager : MonoBehaviour
{
    [HideInInspector] public AudioSource gameAudio;
    private ShapeSorting_GameManager gameManagerScript;
    private bool isPreviousAudioFinished = true;
    private string voiceGender;

    //CORRECT SOUND
    [SerializeField] private AudioClip correctSound;
    [SerializeField] private AudioClip pickedUpSound;
    [SerializeField] private AudioClip wrongSound;

    //MALE VOICE AUDIO:
    //naming shapes upon lifing:
    [SerializeField] private AudioClip thatsACircleMale;
    [SerializeField] private AudioClip thatsAPentagonMale;
    [SerializeField] private AudioClip thatsARectangleMale;
    [SerializeField] private AudioClip thatsASquareMale;
    [SerializeField] private AudioClip thatsAStarMale;
    [SerializeField] private AudioClip thatsAnArchMale;
    [SerializeField] private AudioClip rectangleMale;
    //general instruction
    [SerializeField] private AudioClip putShapesInTheRightHolesMale;
    //try agains generic
    [SerializeField] private AudioClip tryAgainMale;
    [SerializeField] private AudioClip tryAgain2Male;
    [SerializeField] private AudioClip tryAgainUpliftingMale;
    [SerializeField] private AudioClip thatWasTheWrongHoleMale;
    //try agains with name shape
    [SerializeField] private AudioClip tryAgainThatWasACircleMale;
    [SerializeField] private AudioClip tryAgainThatsAPentagonMale;
    [SerializeField] private AudioClip tryagainThatsARectangleMale;
    [SerializeField] private AudioClip tryAgainThatsASquareMale;
    [SerializeField] private AudioClip tryAgainThatsAnArchMale;
    //well dones
    [SerializeField] private AudioClip wellDoneMale;
    [SerializeField] private AudioClip wellDoneEncouragingMale;
    [SerializeField] private AudioClip wellDoneSolidarityMale;
    //when all shapes are sorted:
    [SerializeField] private AudioClip thatWasGreatMale;

    //FEMALE VOICE AUDIO:
    //general instruction
    [SerializeField] private AudioClip putShapesInTheRightHolesFemale;

    //naming shapes
    [SerializeField] private AudioClip thatsACircleFemale;
    [SerializeField] private AudioClip thatsAStarFemale;
    [SerializeField] private AudioClip thatsASquareFemale;
    [SerializeField] private AudioClip thatsARectangleFemale;
    [SerializeField] private AudioClip thatsAnArchFemale;
    [SerializeField] private AudioClip thatsAPentagonFemale;
    //well done
    [SerializeField] private AudioClip wellDoneFemale;
    //try agains
    [SerializeField] private AudioClip tryAgainFemale;
    [SerializeField] private AudioClip thatWasTheWrongHoleFemale;

    // Start is called before the first frame update
    void Start()
    {
        gameAudio = GetComponent<AudioSource>();
        gameManagerScript = GameObject.Find("GameManager").GetComponent<ShapeSorting_GameManager>();
        voiceGender = PlayerPrefs.GetString(SettingKeys.VoiceKey, SettingKeys.VoiceDefaultValue);
        //GiveUserVerbalDirectionsAtBeginningOfGame();

    }

    public void PickedUpSound() => gameAudio.PlayOneShot(pickedUpSound);
    public void CorrectSound() => gameAudio.PlayOneShot(correctSound);
    public void WrongSound() => gameAudio.PlayOneShot(wrongSound);

    public void GiveUserVerbalDirectionsAtBeginningOfGame()
    {
        gameManagerScript.readyToSortAgain = false;
        StartCoroutine(gameManagerScript.PlayDelay(1.5f));

        if (voiceGender == "Male")
        {
            gameAudio.PlayOneShot(putShapesInTheRightHolesMale);
        }
        
        else if (voiceGender == "Female")
        {
            gameAudio.PlayOneShot(putShapesInTheRightHolesFemale);
        }
    }

    public void NamePickedUpShape(string shapeName)
    {
        if (voiceGender == "Male" && isPreviousAudioFinished)
        {
            isPreviousAudioFinished = false;
            StartCoroutine(AudioDelay(1.5f));

            if (shapeName == "Star")
            {
                gameAudio.PlayOneShot(thatsAStarMale);
            }
            else if (shapeName == "Arch")
            {
                gameAudio.PlayOneShot(thatsAnArchMale);
            }
            else if (shapeName == "Pentagon")
            {
                gameAudio.PlayOneShot(thatsAPentagonMale);
            }
            else if (shapeName == "Circle")
            {
                gameAudio.PlayOneShot(thatsACircleMale);
            }
            else if (shapeName == "Square")
            {
                gameAudio.PlayOneShot(thatsASquareMale);
            }
            else if (shapeName == "Rectangle")
            {
                int randomNum = Random.Range(0, 2);
                if (randomNum == 0)
                {
                    gameAudio.PlayOneShot(thatsARectangleMale);
                }
                else if (randomNum == 1)
                {
                    gameAudio.PlayOneShot(rectangleMale);
                }
            }
        }
        else if (voiceGender == "Female" && isPreviousAudioFinished)
        {
            isPreviousAudioFinished = false;
            StartCoroutine(AudioDelay(1.5f));

            if (shapeName == "Star")
            {
                gameAudio.PlayOneShot(thatsAStarFemale);
            }
            else if (shapeName == "Arch")
            {
                gameAudio.PlayOneShot(thatsAnArchFemale);
            }
            else if (shapeName == "Pentagon")
            {
                gameAudio.PlayOneShot(thatsAPentagonFemale);
            }
            else if (shapeName == "Circle")
            {
                gameAudio.PlayOneShot(thatsACircleFemale);
            }
            else if (shapeName == "Square")
            {
                gameAudio.PlayOneShot(thatsASquareFemale);
            }
            else if (shapeName == "Rectangle")
            {
                gameAudio.PlayOneShot(thatsARectangleFemale);
            }
        }
    }


    public void RandomTryAgainAudio(string shapeName)
    {
        int randomNum = Random.Range(0, 2);

        if (randomNum == 0)
        {
            GenericTryAgainForWrongHole();
        }
        else if (randomNum == 1)
        {
            TryAgainWithShapeName(shapeName);
        }
    }

    public void GenericTryAgainForWrongHole()
    {
        gameManagerScript.readyToSortAgain = false;
        StartCoroutine(gameManagerScript.PlayDelay(1.5f));

        if (voiceGender == "Male")
        {
            int randomNum = Random.Range(0, 4);

            if (randomNum == 0)
            {
                gameAudio.PlayOneShot(tryAgainMale);
            }
            else if (randomNum == 1)
            {
                gameAudio.PlayOneShot(tryAgain2Male);
            }
            else if (randomNum == 2)
            {
                gameAudio.PlayOneShot(tryAgainUpliftingMale);
            }
            else if (randomNum == 3)
            {
                gameAudio.PlayOneShot(thatWasTheWrongHoleMale);
            }
        }
        else if (voiceGender == "Female")
        {
            int randomNum = Random.Range(0, 2);
            if (randomNum == 0)
            {
                gameAudio.PlayOneShot(tryAgainFemale);
            }
            else if (randomNum == 1)
            {
                gameAudio.PlayOneShot(thatWasTheWrongHoleFemale);
            }
        }
    }

    public void TryAgainWithShapeName(string shapeName)
    {
        gameManagerScript.readyToSortAgain = false;
        StartCoroutine(gameManagerScript.PlayDelay(1.5f));

        if (voiceGender == "Male" && isPreviousAudioFinished)
        {
            isPreviousAudioFinished = false;
            StartCoroutine(AudioDelay(1.5f));

            if (shapeName == "Star")
            {
                gameAudio.PlayOneShot(tryAgainMale);//needs new audio
            }
            else if (shapeName == "Arch")
            {
                gameAudio.PlayOneShot(tryAgainThatsAnArchMale);
            }
            else if (shapeName == "Pentagon")
            {
                gameAudio.PlayOneShot(tryAgainThatsAPentagonMale);
            }
            else if (shapeName == "Circle")
            {
                gameAudio.PlayOneShot(tryAgainThatWasACircleMale);
            }
            else if (shapeName == "Square")
            {
                gameAudio.PlayOneShot(tryAgainThatsASquareMale);
            }
            else if (shapeName == "Rectangle")
            {
                gameAudio.PlayOneShot(tryagainThatsARectangleMale);
            }
        }
    }

    public void WellDone()
    {
        gameManagerScript.readyToSortAgain = false;
        StartCoroutine(gameManagerScript.PlayDelay(1.5f));

        if (voiceGender == "Male" && isPreviousAudioFinished)
        {
            StartCoroutine(AudioDelay(1f));
            int randomNum = Random.Range(0, 3);

            if (randomNum == 0)
            {
                gameAudio.PlayOneShot(wellDoneMale);
            }
            else if (randomNum == 1)
            {
                gameAudio.PlayOneShot(wellDoneSolidarityMale);
            }
            else if (randomNum == 2)
            {
                gameAudio.PlayOneShot(wellDoneEncouragingMale);
            }
        }
        else if (voiceGender == "Female" && isPreviousAudioFinished)
        {
            StartCoroutine(AudioDelay(1f));
            gameAudio.PlayOneShot(wellDoneFemale);
        }
    }

    //NOT SURE I UNDERSTAND HOW TEAM WANTS THIS USED
    public void ShapeInWrongHole(string shapeName)
    {
        int randomNum;

        if (voiceGender == "Male")
        {
            if (shapeName == "Star")
            {
                randomNum = Random.Range(0, 2);
                if (randomNum == 0)
                {
                    gameAudio.PlayOneShot(tryAgainMale);
                }
                else if (randomNum == 1)
                {
                    gameAudio.PlayOneShot(tryAgainMale);
                }
            }
            else if (shapeName == "Arch")
            {
                gameAudio.PlayOneShot(thatsAnArchMale);
            }
            else if (shapeName == "Pentagon")
            {
                gameAudio.PlayOneShot(thatsAPentagonMale);
            }
            else if (shapeName == "Circle")
            {
                gameAudio.PlayOneShot(thatsACircleMale);
            }
            else if (shapeName == "Square")
            {
                gameAudio.PlayOneShot(thatsASquareMale);
            }
            else if (shapeName == "Rectangle")
            {
                //DON'T HAVE
            }
        }
    }

    private IEnumerator AudioDelay(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        isPreviousAudioFinished = true;
    }
}
