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

    [HideInInspector] public AudioSource gameAudio;

    //public sound clips that are called in other scripts

    //encouragements
    public AudioClip wellDoneWisper;
    public AudioClip wellDoneMoreForceful;
    public AudioClip wellDoneEncouraging;
    public AudioClip wellDoneBrief;

    //naming picked up colours
    public AudioClip yellow;
    public AudioClip thisToyIsYellow;
    public AudioClip red;
    public AudioClip thisToyIsRed;
    public AudioClip pink;
    public AudioClip thisToyIsPink;
    public AudioClip green;
    public AudioClip thisToyIsGreen;

    //naming tray colours
    public AudioClip thatTrayWasYellow;
    public AudioClip thatTrayWasRed;
    public AudioClip thatTrayWasGreen;
    public AudioClip thatTrayWasPink;

    //naming toys after being dropped
    public AudioClip thatToyWasYellow;
    public AudioClip thatToyWasRed;
    public AudioClip thatToyWasPink;
    public AudioClip thatToyWasGreen;

    //general directions
    public AudioClip sortToysInToTraysOfTheSameColour;
    public AudioClip matchToysToTheColourOfTheTray;
    public AudioClip matchTheColours;
    public AudioClip toysGoBackIntheTrays;

    //try agains
    public AudioClip ohhTryAgain;
    public AudioClip tryAgain;

    //colour-based directions
    public AudioClip putThisInTheYellowTray;
    public AudioClip putThisInTheRedTray;
    public AudioClip putThisInThePinkTray;
    public AudioClip putThisInTheGreenTray;

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

    //generates
    private int RandomClipSelector(int min, int max)
    {
        int randomNum = Random.Range(min, max+1);

        if (randomNum == 0)
        {
            return 1;
        }
        else
        {
            return 2;
        }

    }

    public void NamePickedUpColour_MaleVoice(int colourNumber)
    {

        int randomNum = Random.Range(0, 2);

        if (colourNumber == 0)
        {
            if (randomNum == 0)
            {
                gameAudio.PlayOneShot(yellow);
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
            else
            {
                gameAudio.PlayOneShot(thisToyIsPink);
            }
        }


    }

}
