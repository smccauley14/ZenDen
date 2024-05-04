using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ColourSorting_ColourSelector : MonoBehaviour
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

    //number indexes used to select prefabs and colours
    private int prefabSelector;
    private int colourSelector;

    //to identify the colours that our selected at any given point
    [HideInInspector] public int currentLeft = 0;//yellow at start
    [HideInInspector] public int currentMiddle = 5;//pink at start
    [HideInInspector] public int currentRight = 3;//green at start

    //holding all available colours for user to choose between
    [SerializeField] private Color[] allColours;

    //holds the 3 materials used to give colour to the prefabs
    [SerializeField] private Material[] prefabMaterials;

    //UI
    [SerializeField] private Button[] colourMenuButtons;
    [SerializeField] private Button[] colourIconLeft;
    [SerializeField] private Button[] colourIconMiddle;
    [SerializeField] private Button[] colourIconRight;
    [SerializeField] private GameObject menuIcons;
    [SerializeField] private GameObject background;

    private ColourSorting_GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        ResetPrefabMaterialColour();

        //getting gameManager script
        gameManager = GameObject.Find("GameManager").GetComponent<ColourSorting_GameManager>();

        //adding all required listeners to UI buttons
        AddListeners();
    }

    // Update is called once per frame
    void Update()
    {



    }


    //method to assign value to 'prefabNumber' based on which colour selecting icon has been pressed
    void PrefabSelector(int prefabNumber)
    {
        prefabSelector = prefabNumber;
        ActivateSelectorMenu();
    }

    //method to assign value to colourNumber based on user choice. Also calls method to change colour of prefab
    void ColourSelector(int colourNumber)
    {
        colourSelector = colourNumber;
        SetPrefabMaterialColour(prefabSelector, colourSelector);
        DeactivateSelectorMenu();
    }

    //sets the selected prefab to the selected colour (provided the colour isn't already attached to another prefab)
    void SetPrefabMaterialColour(int prefabNumber, int colourNumber)
    {
        if (prefabNumber == 0 && colourNumber != currentLeft && colourNumber != currentMiddle && colourNumber != currentRight)
        {
            prefabMaterials[prefabSelector].color = allColours[colourNumber];
            ActivateCorrectIcon(prefabNumber, colourNumber);
            currentLeft = colourNumber;
        }
        else if (prefabNumber == 1 && colourNumber != currentLeft && colourNumber != currentMiddle && colourNumber != currentRight)
        {
            prefabMaterials[prefabSelector].color = allColours[colourNumber];
            ActivateCorrectIcon(prefabNumber, colourNumber);
            currentMiddle = colourNumber;

        }
        else if (prefabNumber == 2 && colourNumber != currentLeft && colourNumber != currentMiddle && colourNumber != currentRight)
        {
            prefabMaterials[prefabSelector].color = allColours[colourNumber];
            ActivateCorrectIcon(prefabNumber, colourNumber);
            currentRight = colourNumber;
        }
    }

    //turns on the colour selector UI
    void ActivateSelectorMenu()
    {
        background.SetActive(true);
        menuIcons.SetActive(true);
        gameManager.UIisActive = true;
    }

    //turns off the colour selector UI
    void DeactivateSelectorMenu()
    {
        background.SetActive(false);
        menuIcons.SetActive(false);
        gameManager.UIisActive = false;
    }

    //activates the correct UI icon colour, corresponding to the current colour
    void ActivateCorrectIcon(int iconNumber, int colourNumber)
    {
        if (iconNumber == 0)
        {
            colourIconLeft[currentLeft].gameObject.SetActive(false);
            colourIconLeft[colourNumber].gameObject.SetActive(true);
        }
        else if (iconNumber == 1)
        {
            colourIconMiddle[currentMiddle].gameObject.SetActive(false);
            colourIconMiddle[colourNumber].gameObject.SetActive(true);
        }
        else if (iconNumber == 2)
        {
            colourIconRight[currentRight].gameObject.SetActive(false);
            colourIconRight[colourNumber].gameObject.SetActive(true);
        }
    }

    //turns off the whole array of buttons (NOT CURRENTLY REQUIRED)
    void SetAllIconsInactive(Button[] iconNumber)
    {
        for (int i = 0; i < iconNumber.Length; i++)
        {
            iconNumber[i].gameObject.SetActive(false);
        }
    }

    //adding all required listeners to all UI buttons
    void AddListeners()
    {
        for (int i = 0; i <= colourIconLeft.Length; i++)
        {
            int index = i; // Capture the current value of i - required because of how lambda expressions work
            colourMenuButtons[i].onClick.AddListener(() => ColourSelector(index));
            colourIconLeft[i].onClick.AddListener(() => PrefabSelector(0));
            colourIconMiddle[i].onClick.AddListener(() => PrefabSelector(1));
            colourIconRight[i].onClick.AddListener(() => PrefabSelector(2));
        }
    }

    //reset the prefab colours back to the initial colour scheme
    void ResetPrefabMaterialColour()
    {
        prefabMaterials[0].color = allColours[0];//yellow
        currentLeft = 0;

        prefabMaterials[1].color = allColours[5];//pink
        currentMiddle = 5;

        prefabMaterials[2].color = allColours[3];//green
        currentRight = 3;
    }

    //resets prefab colour scheme back to original upon quitting
    private void OnApplicationQuit()
    {
        ResetPrefabMaterialColour();
    }

}
