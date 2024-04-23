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

    private ColourSorting_GameManager gameManagerScript;

    [HideInInspector] public int wellDoneCounter = 0;
    [HideInInspector] public AudioSource gameAudio;

    //encouragements - DONE
    public AudioClip wellDoneWisper;
    public AudioClip wellDoneMoreForceful;
    public AudioClip wellDoneEncouraging;
    public AudioClip wellDoneBrief;

    //naming picked up colours - DONE
    public AudioClip yellow;
    public AudioClip thisToyIsYellow;
    public AudioClip red;
    public AudioClip thisToyIsRed;
    public AudioClip pink;
    public AudioClip thisToyIsPink;
    public AudioClip green;
    public AudioClip thisToyIsGreen;

    //colour-based directions - DONE
    public AudioClip putThisInTheYellowTray;
    public AudioClip putThisInTheRedTray;
    public AudioClip putThisInThePinkTray;
    public AudioClip putThisInTheGreenTray;

    //naming tray colours - DONE
    public AudioClip thatTrayWasYellow;
    public AudioClip thatTrayWasRed;
    public AudioClip thatTrayWasGreen;
    public AudioClip thatTrayWasPink;

    //naming toys after being dropped - DONE
    public AudioClip thatToyWasYellow;
    public AudioClip thatToyWasRed;
    public AudioClip thatToyWasPink;
    public AudioClip thatToyWasGreen;

    //general directions
    public AudioClip sortToysInToTraysOfTheSameColour;
    public AudioClip matchToysToTheColourOfTheTray;
    public AudioClip matchTheColours;
    public AudioClip toysGoBackIntheTrays;

    //try agains - DONE
    public AudioClip ohhTryAgain;
    public AudioClip tryAgain;



    // Start is called before the first frame update
    void Start()
    {
        //getting player audio
        gameAudio = GetComponent<AudioSource>();
        //get game manager script
        gameManagerScript = GameObject.Find("GameManager").GetComponent<ColourSorting_GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //sounds for when an object is picked up
    //naming the object picked up, or advising user which tray to drop it into
    public void NamePickedUpColour_MaleVoice(int colourNumber)
    {
        int randomNum = Random.Range(0, 3);

        if (colourNumber == 0)
        {
            if (randomNum == 0)
            {
                gameAudio.PlayOneShot(yellow);
            }
            else if ( randomNum == 1)
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

    //sounds for when a user has put the object in the wrong tray
    //i.e. 'try again' or advising which tray to put it into
    public void AdviceForWrongTray_MaleVoice(int colourNumber, int wrongTrayNumber)
    {
        int randomNum = Random.Range(0, 4);

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

    //encouragements
    public void WhenToyPutInCorrectTray()
    {
        //N.B. I have deliberately allowed for random outcomes with no sound effects
        //so sounds won't necessarily be played every time
        //well-done counter prevents sounds effects from being called too often in one wave

        int randomNum = Random.Range(0, 6);

        if (wellDoneCounter < 3)
        {
            if (randomNum == 0)
            {
                gameAudio.PlayOneShot(wellDoneBrief);
                wellDoneCounter++;
            }
            else if (randomNum == 1)
            {
                gameAudio.PlayOneShot(wellDoneEncouraging);
                wellDoneCounter++;
            }
            else if (randomNum == 2)
            {
                gameAudio.PlayOneShot(wellDoneMoreForceful);
                wellDoneCounter++;
            }
            else if (randomNum == 3)
            {
                gameAudio.PlayOneShot(wellDoneWisper);
                wellDoneCounter++;
            }
        }
    }

}
