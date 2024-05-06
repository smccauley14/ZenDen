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
    private string voiceGender;

    //MALE AUDIO:
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
    [SerializeField] private AudioClip navyMale;
    [SerializeField] private AudioClip thisToyIsNavyMale;
    [SerializeField] private AudioClip greenMale;
    [SerializeField] private AudioClip thisToyIsGreenMale;
    [SerializeField] private AudioClip purpleMale;
    [SerializeField] private AudioClip thisToyIsPurpleMale;
    [SerializeField] private AudioClip pinkMale;
    [SerializeField] private AudioClip thisToyIsPinkMale;
    [SerializeField] private AudioClip orangeMale;
    [SerializeField] private AudioClip thisToyIsOrangeMale;
    [SerializeField] private AudioClip skyBlueMale;
    [SerializeField] private AudioClip thisToyIsSkyBlueMale;
    [SerializeField] private AudioClip greyMale;
    [SerializeField] private AudioClip thisToyIsGreyMale;
    //colour-based directions
    [SerializeField] private AudioClip putThisInTheYellowTrayMale;
    [SerializeField] private AudioClip putThisInTheRedTrayMale;
    [SerializeField] private AudioClip putThisInTheNavyTrayMale;
    [SerializeField] private AudioClip putThisInTheGreenTrayMale;
    [SerializeField] private AudioClip putThisInThePurpleTrayMale;
    [SerializeField] private AudioClip putThisInThePinkTrayMale;
    [SerializeField] private AudioClip putThisInTheOrangeTrayMale;
    [SerializeField] private AudioClip putThisInTheSkyBlueTrayMale;
    [SerializeField] private AudioClip putThisInTheGreyTrayMale;
    //naming tray colours
    [SerializeField] private AudioClip thatTrayWasYellowMale;
    [SerializeField] private AudioClip thatTrayWasRedMale;
    [SerializeField] private AudioClip thatTrayWasNavyMale;
    [SerializeField] private AudioClip thatTrayWasGreenMale;
    [SerializeField] private AudioClip thatTrayWasPurpleMale;
    [SerializeField] private AudioClip thatTrayWasPinkMale;
    [SerializeField] private AudioClip thatTrayWasOrangeMale;
    [SerializeField] private AudioClip thatTrayWasSkyBlueMale;
    [SerializeField] private AudioClip thatTrayWasGreyMale;
    //naming toys after being dropped
    [SerializeField] private AudioClip thatToyWasYellowMale;
    [SerializeField] private AudioClip thatToyWasRedMale;
    [SerializeField] private AudioClip thatToyWasNavyMale;
    [SerializeField] private AudioClip thatToyWasGreenMale;
    [SerializeField] private AudioClip thatToyWasPurpleMale;
    [SerializeField] private AudioClip thatToyWasPinkMale;
    [SerializeField] private AudioClip thatToyWasOrangeMale;
    [SerializeField] private AudioClip thatToyWasSkyBlueMale;
    [SerializeField] private AudioClip thatToyWasGreyMale;

    //general directions
    [SerializeField] private AudioClip sortToysInToTraysOfTheSameColourMale;
    [SerializeField] private AudioClip matchToysToTheColourOfTheTrayMale;
    [SerializeField] private AudioClip matchTheColoursMale;
    [SerializeField] private AudioClip toysGoBackIntheTraysMale;
    //try agains
    [SerializeField] private AudioClip ohhTryAgainMale;
    [SerializeField] private AudioClip tryAgainMale;

    //FEMALE AUDIO
    //general instruction
    [SerializeField] private AudioClip matchToysToTheColourOfTheTrayFemale;
    //encouragements
    [SerializeField] private AudioClip wellDoneFemale;
    //try again
    [SerializeField] private AudioClip tryAgainFemale;
    //naming colours
    [SerializeField] private AudioClip yellowFemale;
    [SerializeField] private AudioClip redFemale;
    [SerializeField] private AudioClip navyFemale;
    [SerializeField] private AudioClip greenFemale;
    [SerializeField] private AudioClip purpleFemale;
    [SerializeField] private AudioClip pinkFemale;
    [SerializeField] private AudioClip orangeFemale;
    [SerializeField] private AudioClip skyBlueFemale;
    [SerializeField] private AudioClip greyFemale;
    //"this toy is..."
    [SerializeField] private AudioClip thisToyIsNavyFemale;
    [SerializeField] private AudioClip thisToyIsPurpleFemale;
    [SerializeField] private AudioClip thisToyIsOrangeFemale;
    [SerializeField] private AudioClip thisToyIsSkyBlueFemale;
    [SerializeField] private AudioClip thisToyIsGreyFemale;
    //"put this in the ... Tray"
    [SerializeField] private AudioClip putThisInTheNavyTrayFemale;
    [SerializeField] private AudioClip putThisInThePurpleTrayFemale;
    [SerializeField] private AudioClip putThisInTheOrangeTrayFemale;
    [SerializeField] private AudioClip putThisInTheSkyBlueTrayFemale;
    [SerializeField] private AudioClip putThisInTheGreyTrayFemale;
    //naming tray colour
    [SerializeField] private AudioClip thatTrayWasPurpleFemale;
    [SerializeField] private AudioClip thatTrayWasOrangeFemale;
    [SerializeField] private AudioClip thatTrayWasSkyBlueFemale;
    [SerializeField] private AudioClip thatTrayWasGreyFemale;
    //"that toy was..."
    [SerializeField] private AudioClip thatToyWasNavyFemale;
    [SerializeField] private AudioClip thatToyWasOrangeFemale;
    [SerializeField] private AudioClip thatToyWasPurpleFemale;
    [SerializeField] private AudioClip thatToyWasGreyFemale;
    [SerializeField] private AudioClip thatToyWasSkyBlueFemale;

    // Start is called before the first frame update
    void Start()
    {
        //getting player audio
        gameAudio = GetComponent<AudioSource>();
        voiceGender = PlayerPrefs.GetString(SettingKeys.VoiceKey, SettingKeys.VoiceDefaultValue);
    }

    //general directions
    public void GiveUserVerbalDirectionsAtBeginningOfGame()
    {
        int randomNum = Random.Range(0, 4);

        StartCoroutine(AudioDelay(2f));

        if (voiceGender == "Male" && isPreviousAudioFinished)
        {
            isPreviousAudioFinished = false;
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
        else if (voiceGender == "Female" && isPreviousAudioFinished)
        {
            isPreviousAudioFinished = false;
            gameAudio.PlayOneShot(matchToysToTheColourOfTheTrayFemale);
        }
    }

    //sounds for when an object is picked up
    //naming the object picked up, or advising user which tray to drop it into
    public void NamePickedUpColour_MaleVoice(int colourNumber)
    {
        if (voiceGender == "Male")
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
                if (colourNumber == 2)
                {
                    if (randomNum == 0)
                    {
                        gameAudio.PlayOneShot(navyMale);
                    }
                    else if (randomNum == 1)
                    {
                        gameAudio.PlayOneShot(putThisInTheNavyTrayMale);
                    }
                    else
                    {
                        gameAudio.PlayOneShot(thisToyIsNavyMale);
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
                if (colourNumber == 4)
                {
                    if (randomNum == 0)
                    {
                        gameAudio.PlayOneShot(purpleMale);
                    }
                    else if (randomNum == 1)
                    {
                        gameAudio.PlayOneShot(putThisInThePurpleTrayMale);
                    }
                    else
                    {
                        gameAudio.PlayOneShot(thisToyIsPurpleMale);
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
                if (colourNumber == 6)
                {
                    if (randomNum == 0)
                    {
                        gameAudio.PlayOneShot(orangeMale);
                    }
                    else if (randomNum == 1)
                    {
                        gameAudio.PlayOneShot(putThisInTheOrangeTrayMale);
                    }
                    else
                    {
                        gameAudio.PlayOneShot(thisToyIsOrangeMale);
                    }
                }
                if (colourNumber == 7)
                {
                    if (randomNum == 0)
                    {
                        gameAudio.PlayOneShot(skyBlueMale);
                    }
                    else if (randomNum == 1)
                    {
                        gameAudio.PlayOneShot(putThisInTheSkyBlueTrayMale);
                    }
                    else
                    {
                        gameAudio.PlayOneShot(thisToyIsSkyBlueMale);
                    }
                }
                if (colourNumber == 8)
                {
                    if (randomNum == 0)
                    {
                        gameAudio.PlayOneShot(greyMale);
                    }
                    else if (randomNum == 1)
                    {
                        gameAudio.PlayOneShot(putThisInTheGreyTrayMale);
                    }
                    else
                    {
                        gameAudio.PlayOneShot(thisToyIsGreyMale);
                    }
                }
            }
        }
        else if (voiceGender == "Female")
        {
            if (isPreviousAudioFinished == true)
            {
                isPreviousAudioFinished = false;
                StartCoroutine(AudioDelay(2.5f));

                if (colourNumber == 0)
                {
                    gameAudio.PlayOneShot(yellowFemale);
                }
                if (colourNumber == 1)
                {
                    gameAudio.PlayOneShot(redFemale);
                }
                if (colourNumber == 2)
                {
                    int randomNum = Random.Range(0, 3);
                    if (randomNum == 0)
                    {
                        gameAudio.PlayOneShot(navyFemale);
                    }
                    else if(randomNum == 1)
                    {
                        gameAudio.PlayOneShot(putThisInTheNavyTrayFemale);
                    }
                    else if (randomNum == 2)
                    {
                        gameAudio.PlayOneShot(thisToyIsNavyFemale);
                    }
                }
                if (colourNumber == 3)
                {
                    gameAudio.PlayOneShot(greenFemale);
                }
                if (colourNumber == 4)
                {
                    int randomNum = Random.Range(0, 3);
                    if (randomNum == 0)
                    {
                        gameAudio.PlayOneShot(purpleFemale);
                    }
                    else if (randomNum == 1)
                    {
                        gameAudio.PlayOneShot(putThisInThePurpleTrayFemale);
                    }
                    else if (randomNum == 2)
                    {
                        gameAudio.PlayOneShot(thisToyIsPurpleFemale);
                    }
                }
                if (colourNumber == 5)
                {
                    gameAudio.PlayOneShot(pinkFemale);
                }
                if (colourNumber == 6)
                {
                    int randomNum = Random.Range(0, 3);
                    if (randomNum == 0)
                    {
                        gameAudio.PlayOneShot(orangeFemale);
                    }
                    else if (randomNum == 1)
                    {
                        gameAudio.PlayOneShot(putThisInTheOrangeTrayFemale);
                    }
                    else if (randomNum == 2)
                    {
                        gameAudio.PlayOneShot(thisToyIsOrangeFemale);
                    }
                }
                if (colourNumber == 7)
                {
                    int randomNum = Random.Range(0, 3);
                    if (randomNum == 0)
                    {
                        gameAudio.PlayOneShot(skyBlueFemale);
                    }
                    else if (randomNum == 1)
                    {
                        gameAudio.PlayOneShot(putThisInTheSkyBlueTrayFemale);
                    }
                    else if (randomNum == 2)
                    {
                        gameAudio.PlayOneShot(thisToyIsSkyBlueFemale);
                    }
                }
                if (colourNumber == 8)
                {
                    int randomNum = Random.Range(0, 3);
                    if (randomNum == 0)
                    {
                        gameAudio.PlayOneShot(greyFemale);
                    }
                    else if (randomNum == 1)
                    {
                        gameAudio.PlayOneShot(putThisInTheGreyTrayFemale);
                    }
                    else if (randomNum == 2)
                    {
                        gameAudio.PlayOneShot(thisToyIsGreyFemale);
                    }
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

            if (voiceGender == "Male")
            {
                gameAudio.PlayOneShot(ohhTryAgainMale);
            }
            else if (voiceGender == "Female")
            {
                gameAudio.PlayOneShot(tryAgainFemale);
            }
        }
    }

    //sounds for when a user has put the object in the wrong tray
    //i.e. 'try again' or advising which tray to put it into
    public void AdviceForWrongTray(int colourNumber, int wrongTrayNumber)
    {
        if (isPreviousAudioFinished == true)
        {
            int randomNum = Random.Range(0, 4);
            isPreviousAudioFinished = false;
            StartCoroutine(AudioDelay(3f));

            if (voiceGender == "Male")
            {
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
                    if (colourNumber == 2)
                    {
                        gameAudio.PlayOneShot(thatToyWasNavyMale);
                    }
                    if (colourNumber == 3)
                    {
                        gameAudio.PlayOneShot(thatToyWasGreenMale);
                    }
                    if (colourNumber == 4)
                    {
                        gameAudio.PlayOneShot(thatToyWasPurpleMale);
                    }
                    if (colourNumber == 5)
                    {
                        gameAudio.PlayOneShot(thatToyWasPinkMale);
                    }
                    if (colourNumber == 6)
                    {
                        gameAudio.PlayOneShot(thatToyWasOrangeMale);
                    }
                    if (colourNumber == 7)
                    {
                        gameAudio.PlayOneShot(thatToyWasSkyBlueMale);
                    }
                    if (colourNumber == 8)
                    {
                        gameAudio.PlayOneShot(thatToyWasGreyMale);
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
                    else if (wrongTrayNumber == 2)
                    {
                        gameAudio.PlayOneShot(thatTrayWasNavyMale);
                    }
                    else if (wrongTrayNumber == 3)
                    {
                        gameAudio.PlayOneShot(thatTrayWasGreenMale);
                    }
                    else if (wrongTrayNumber == 4)
                    {
                        gameAudio.PlayOneShot(thatTrayWasPurpleMale);
                    }
                    else if (wrongTrayNumber == 5)
                    {
                        gameAudio.PlayOneShot(thatTrayWasPinkMale);
                    }
                    else if (wrongTrayNumber == 6)
                    {
                        gameAudio.PlayOneShot(thatTrayWasOrangeMale);
                    }
                    else if (wrongTrayNumber == 7)
                    {
                        gameAudio.PlayOneShot(thatTrayWasSkyBlueMale);
                    }
                    else if (wrongTrayNumber == 8)
                    {
                        gameAudio.PlayOneShot(thatTrayWasGreyMale);
                    }
                }
            }
            else if (voiceGender == "Female")
            {
                gameAudio.PlayOneShot(tryAgainFemale);
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

            if (voiceGender == "Male")
            {
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
            else if (voiceGender == "Female")
            {
                StartCoroutine(AudioDelay(1.5f));
                gameAudio.PlayOneShot(wellDoneFemale);
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
