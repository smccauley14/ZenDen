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
    [SerializeField] private AudioClip wellDoneWisper;
    [SerializeField] private AudioClip wellDoneMoreForceful;
    [SerializeField] private AudioClip wellDoneEncouraging;
    public AudioClip wellDoneBrief;

    //naming picked up colours
    [SerializeField] private AudioClip yellow;
    [SerializeField] private AudioClip thisToyIsYellow;
    [SerializeField] private AudioClip red;
    [SerializeField] private AudioClip thisToyIsRed;
    [SerializeField] private AudioClip pink;
    [SerializeField] private AudioClip thisToyIsPink;
    [SerializeField] private AudioClip green;
    [SerializeField] private AudioClip thisToyIsGreen;

    //colour-based directions
    [SerializeField] private AudioClip putThisInTheYellowTray;
    [SerializeField] private AudioClip putThisInTheRedTray;
    [SerializeField] private AudioClip putThisInThePinkTray;
    [SerializeField] private AudioClip putThisInTheGreenTray;

    //naming tray colours
    [SerializeField] private AudioClip thatTrayWasYellow;
    [SerializeField] private AudioClip thatTrayWasRed;
    [SerializeField] private AudioClip thatTrayWasGreen;
    [SerializeField] private AudioClip thatTrayWasPink;

    //naming toys after being dropped
    [SerializeField] private AudioClip thatToyWasYellow;
    [SerializeField] private AudioClip thatToyWasRed;
    [SerializeField] private AudioClip thatToyWasPink;
    [SerializeField] private AudioClip thatToyWasGreen;

    //general directions
    [SerializeField] private AudioClip sortToysInToTraysOfTheSameColour;
    [SerializeField] private AudioClip matchToysToTheColourOfTheTray;
    [SerializeField] private AudioClip matchTheColours;
    [SerializeField] private AudioClip toysGoBackIntheTrays;

    //try agains
    [SerializeField] private AudioClip ohhTryAgain;
    [SerializeField] private AudioClip tryAgain;

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
            gameAudio.PlayOneShot(sortToysInToTraysOfTheSameColour);
        }
        else if (randomNum == 1)
        {
            gameAudio.PlayOneShot(matchToysToTheColourOfTheTray);
        }
        else if (randomNum == 2)
        {
            gameAudio.PlayOneShot(matchTheColours);
        }
        else
        {
            gameAudio.PlayOneShot(toysGoBackIntheTrays);
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
                    gameAudio.PlayOneShot(yellow);
                }
                else if (randomNum == 1)
                {
                    gameAudio.PlayOneShot(putThisInTheYellowTray);
                }
                else
                {
                    gameAudio.PlayOneShot(thisToyIsYellow);
                }
            }

            if (colourNumber == 1)
            {
                if (randomNum == 0)
                {
                    gameAudio.PlayOneShot(red);
                }
                else if (randomNum == 1)
                {
                    gameAudio.PlayOneShot(putThisInTheRedTray);
                }
                else
                {
                    gameAudio.PlayOneShot(thisToyIsRed);
                }
            }

            if (colourNumber == 3)
            {
                if (randomNum == 0)
                {
                    gameAudio.PlayOneShot(green);
                }
                else if (randomNum == 1)
                {
                    gameAudio.PlayOneShot(putThisInTheGreenTray);
                }
                else
                {
                    gameAudio.PlayOneShot(thisToyIsGreen);
                }
            }

            if (colourNumber == 5)
            {
                if (randomNum == 0)
                {
                    gameAudio.PlayOneShot(pink);
                }
                else if (randomNum == 1)
                {
                    gameAudio.PlayOneShot(putThisInThePinkTray);
                }
                else
                {
                    gameAudio.PlayOneShot(thisToyIsPink);
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
            gameAudio.PlayOneShot(ohhTryAgain);
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
                gameAudio.PlayOneShot(tryAgain);
            }
            else if (randomNum == 1)
            {
                gameAudio.PlayOneShot(ohhTryAgain);
            }
            else if (randomNum == 2)
            {
                if (colourNumber == 0)
                {
                    gameAudio.PlayOneShot(thatToyWasYellow);
                }
                if (colourNumber == 1)
                {
                    gameAudio.PlayOneShot(thatToyWasRed);
                }
                if (colourNumber == 3)
                {
                    gameAudio.PlayOneShot(thatToyWasGreen);
                }
                if (colourNumber == 5)
                {
                    gameAudio.PlayOneShot(thatToyWasPink);
                }
            }
            else if (randomNum == 3)
            {
                if (wrongTrayNumber == 0)
                {
                    gameAudio.PlayOneShot(thatTrayWasYellow);
                }
                else if (wrongTrayNumber == 1)
                {
                    gameAudio.PlayOneShot(thatTrayWasRed);
                }
                else if (wrongTrayNumber == 3)
                {
                    gameAudio.PlayOneShot(thatTrayWasGreen);
                }
                else if (wrongTrayNumber == 5)
                {
                    gameAudio.PlayOneShot(thatTrayWasPink);
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
                gameAudio.PlayOneShot(wellDoneEncouraging);
            }
            else if (randomNum == 2)
            {
                StartCoroutine(AudioDelay(1.5f));
                gameAudio.PlayOneShot(wellDoneMoreForceful);
            }
            else if (randomNum == 3)
            {
                StartCoroutine(AudioDelay(1.5f));
                gameAudio.PlayOneShot(wellDoneWisper);
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
