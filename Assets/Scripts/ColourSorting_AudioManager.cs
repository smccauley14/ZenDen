using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script holds the bespoke sound effects recorded for the colour-sorting game

public class ColourSorting_AudioManager : MonoBehaviour
{
    /*
    colour array number code:
    0 = yellow
    1 = red
    2 = navy blue
    3 = green
    4 = purple
    5 = pink
    6 = orange
    7 = sky blue
    8 = grey
    */

    [HideInInspector] public int wellDoneCounter = 0;
    [HideInInspector] public AudioSource gameAudio;
    private bool isPreviousAudioFinished = true;

    //encouragements
    [SerializeField] private AudioClip wellDoneWisperMale;
    [SerializeField] private AudioClip wellDoneMoreForcefulMale;
    [SerializeField] private AudioClip wellDoneEncouragingMale;
    public AudioClip wellDoneBrief;

    //naming picked up colours
    [SerializeField] private AudioClip yellowMale;
    [SerializeField] private AudioClip thisToyIsYellowMale;
    [SerializeField] private AudioClip redMale;
    [SerializeField] private AudioClip thisToyIsRedMale;
    [SerializeField] private AudioClip pinkMale;
    [SerializeField] private AudioClip thisToyIsPinkMale;
    [SerializeField] private AudioClip greenMale;
    [SerializeField] private AudioClip thisToyIsGreenMale;

    //colour-based directions
    [SerializeField] private AudioClip putThisInTheYellowTrayMale;
    [SerializeField] private AudioClip putThisInTheRedTrayMale;
    [SerializeField] private AudioClip putThisInThePinkTrayMale;
    [SerializeField] private AudioClip putThisInTheGreenTrayMale;

    //naming tray colours
    [SerializeField] private AudioClip thatTrayWasYellowMale;
    [SerializeField] private AudioClip thatTrayWasRedMale;
    [SerializeField] private AudioClip thatTrayWasGreenMale;
    [SerializeField] private AudioClip thatTrayWasPinkMale;

    //naming toys after being dropped
    [SerializeField] private AudioClip thatToyWasYellowMale;
    [SerializeField] private AudioClip thatToyWasRedMale;
    [SerializeField] private AudioClip thatToyWasPinkMale;
    [SerializeField] private AudioClip thatToyWasGreenMale;

    //general directions
    [SerializeField] private AudioClip sortToysInToTraysOfTheSameColourMale;
    [SerializeField] private AudioClip matchToysToTheColourOfTheTrayMale;
    [SerializeField] private AudioClip matchTheColoursMale;
    [SerializeField] private AudioClip toysGoBackIntheTraysMale;

    //try agains
    [SerializeField] private AudioClip ohhTryAgainMale;
    [SerializeField] private AudioClip tryAgainMale;

    // Start is called before the first frame update
    void Start()
    {
        //getting player audio
        gameAudio = GetComponent<AudioSource>();
    }

    //general directions
    public void GiveUserVerbalDirectionsAtBeginningOfGame()
    {
        int randomNum = Random.Range(0, 4);

        isPreviousAudioFinished = false;
        StartCoroutine(AudioDelay(2.5f));

        if (randomNum == 0)
        {
            gameAudio.PlayOneShot(sortToysInToTraysOfTheSameColourMale);
        }
        else if (randomNum == 1)
        {
            gameAudio.PlayOneShot(matchToysToTheColourOfTheTrayMale);
        }
        else if (randomNum == 2)
        {
            gameAudio.PlayOneShot(matchTheColoursMale);
        }
        else
        {
            gameAudio.PlayOneShot(toysGoBackIntheTraysMale);
        }
    }

    //sounds for when an object is picked up
    //naming the object picked up, or advising user which tray to drop it into
    public void NamePickedUpColour_MaleVoice(int colourNumber)
    {
        int randomNum = Random.Range(0, 3);

        if (isPreviousAudioFinished == true)
        {
            
            isPreviousAudioFinished = false;
            StartCoroutine(AudioDelay(2.5f));

            if (colourNumber == 0)
            {
                if (randomNum == 0)
                {
                    gameAudio.PlayOneShot(yellowMale);
                }
                else if (randomNum == 1)
                {
                    gameAudio.PlayOneShot(putThisInTheYellowTrayMale);
                }
                else
                {
                    gameAudio.PlayOneShot(thisToyIsYellowMale);
                }
            }

            if (colourNumber == 1)
            {
                if (randomNum == 0)
                {
                    gameAudio.PlayOneShot(redMale);
                }
                else if (randomNum == 1)
                {
                    gameAudio.PlayOneShot(putThisInTheRedTrayMale);
                }
                else
                {
                    gameAudio.PlayOneShot(thisToyIsRedMale);
                }
            }

            if (colourNumber == 3)
            {
                if (randomNum == 0)
                {
                    gameAudio.PlayOneShot(greenMale);
                }
                else if (randomNum == 1)
                {
                    gameAudio.PlayOneShot(putThisInTheGreenTrayMale);
                }
                else
                {
                    gameAudio.PlayOneShot(thisToyIsGreenMale);
                }
            }

            if (colourNumber == 5)
            {
                if (randomNum == 0)
                {
                    gameAudio.PlayOneShot(pinkMale);
                }
                else if (randomNum == 1)
                {
                    gameAudio.PlayOneShot(putThisInThePinkTrayMale);
                }
                else
                {
                    gameAudio.PlayOneShot(thisToyIsPinkMale);
                }
            }
        }
    }

    public void IfPlacedBetweenTrays()
    {
        if (isPreviousAudioFinished == true)
        {
            isPreviousAudioFinished = false;
            StartCoroutine(AudioDelay(2.5f));
            gameAudio.PlayOneShot(ohhTryAgainMale);
        }
    }

    //sounds for when a user has put the object in the wrong tray
    //i.e. 'try again' or advising which tray to put it into
    public void AdviceForWrongTray_MaleVoice(int colourNumber, int wrongTrayNumber)
    {
        int randomNum = Random.Range(0, 4);

        if (isPreviousAudioFinished == true)
        {
            isPreviousAudioFinished = false;
            StartCoroutine(AudioDelay(3f));

            if (randomNum == 0)
            {
                gameAudio.PlayOneShot(tryAgainMale);
            }
            else if (randomNum == 1)
            {
                gameAudio.PlayOneShot(ohhTryAgainMale);
            }
            else if (randomNum == 2)
            {
                if (colourNumber == 0)
                {
                    gameAudio.PlayOneShot(thatToyWasYellowMale);
                }
                if (colourNumber == 1)
                {
                    gameAudio.PlayOneShot(thatToyWasRedMale);
                }
                if (colourNumber == 3)
                {
                    gameAudio.PlayOneShot(thatToyWasGreenMale);
                }
                if (colourNumber == 5)
                {
                    gameAudio.PlayOneShot(thatToyWasPinkMale);
                }
            }
            else if (randomNum == 3)
            {
                if (wrongTrayNumber == 0)
                {
                    gameAudio.PlayOneShot(thatTrayWasYellowMale);
                }
                else if (wrongTrayNumber == 1)
                {
                    gameAudio.PlayOneShot(thatTrayWasRedMale);
                }
                else if (wrongTrayNumber == 3)
                {
                    gameAudio.PlayOneShot(thatTrayWasGreenMale);
                }
                else if (wrongTrayNumber == 5)
                {
                    gameAudio.PlayOneShot(thatTrayWasPinkMale);
                }
            }
        }
    }

    //encouragements
    public void WhenToyPutInCorrectTray()
    {
        //N.B. I have deliberately allowed for random outcomes with no sound effects
        //meaning sounds won't necessarily be played every time
        //the 'wellDoneCounter' prevents these sounds effects from being called too often in one wave

        int randomNum = Random.Range(0, 4);

        if (wellDoneCounter < 3 && isPreviousAudioFinished == true)
        {
            //add 1 to counter
            wellDoneCounter++;

            isPreviousAudioFinished = false;

            if (randomNum == 0)
            {
                StartCoroutine(AudioDelay(1f));
                gameAudio.PlayOneShot(wellDoneBrief);
            }
            else if (randomNum == 1)
            {
                StartCoroutine(AudioDelay(1.5f));
                gameAudio.PlayOneShot(wellDoneEncouragingMale);
            }
            else if (randomNum == 2)
            {
                StartCoroutine(AudioDelay(1.5f));
                gameAudio.PlayOneShot(wellDoneMoreForcefulMale);
            }
            else if (randomNum == 3)
            {
                StartCoroutine(AudioDelay(1.5f));
                gameAudio.PlayOneShot(wellDoneWisperMale);
            }
        }
    }

    //Coroutine to prevent another audio clip from being triggered too soon
    private IEnumerator AudioDelay(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        isPreviousAudioFinished = true;
    }
}
