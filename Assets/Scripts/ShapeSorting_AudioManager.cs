using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeSorting_AudioManager : MonoBehaviour
{
    [HideInInspector] public AudioSource gameAudio;
    private ShapeSorting_GameManager gameManagerScript;
    private bool isPreviousAudioFinished = true;
    private int maleOrFemale = 1;

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



    // Start is called before the first frame update
    void Start()
    {
        gameAudio = GetComponent<AudioSource>();
        gameManagerScript = GameObject.Find("GameManager").GetComponent<ShapeSorting_GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GiveUserVerbalDirectionsAtBeginningOfGame()
    {
        //isPreviousAudioFinished = false;
        //StartCoroutine(AudioDelay(1.5f));

        gameManagerScript.readyToSortAgain = false;
        StartCoroutine(gameManagerScript.PlayDelay(1.5f));

        if (maleOrFemale == 1)
        {
            gameAudio.PlayOneShot(putShapesInTheRightHolesMale);
        }
        
        /*
        else if (maleOrFemale == 2)
        {
            gameAudio.PlayOneShot(p);
        }
        */
    }

    public void NamePickedUpShape(string shapeName)
    {
        if (maleOrFemale == 1 && isPreviousAudioFinished)
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

        if (maleOrFemale == 1)
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
        /*
        else if (maleOrFemale == 1)
        */      

    }

    public void TryAgainWithShapeName(string shapeName)
    {
        gameManagerScript.readyToSortAgain = false;
        StartCoroutine(gameManagerScript.PlayDelay(1.5f));

        if (maleOrFemale == 1 && isPreviousAudioFinished)
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

        if (isPreviousAudioFinished)
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
    }

    public void ShapeInWrongHole(string shapeName)
    {
        int randomNum;

        if (maleOrFemale == 1)
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

            }
        }
    }

    private IEnumerator AudioDelay(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        isPreviousAudioFinished = true;
    }

}
