using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ColourSorting_ColourSelector : MonoBehaviour
{

    public int prefabSelector;
    public int colourSelector;

    private int currentLeft;
    private int currentMiddle;
    private int currentRight;

    public Color[] allColours;
    [SerializeField] Material[] prefabMaterials;


    //UI
    [SerializeField] private Button[] colourMenuButtons;
    [SerializeField] private Button[] colourIconLeft;
    [SerializeField] private Button[] colourIconMiddle;
    [SerializeField] private Button[] colourIconRight;
    //[SerializeField] private GameObject[] menuIcons;
    [SerializeField] private GameObject menuIcons;
    [SerializeField] private GameObject background;


    // Start is called before the first frame update
    void Start()
    {


        

        ResetPrefabMaterialColour();

        AddListeners();
        

        //colourButton1.onClick.AddListener();
        //colourButton2.onClick.AddListener(SetPrefabMaterialColour);


        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*
    void RetainCurrentNumbers()
    {
        currentLeft
    }
    */

    void PrefabSelector(int prefabNumber)
    {
        prefabSelector = prefabNumber;
        ActivateSelectorMenu();
    }

    void ColourSelector(int colourNumber)
    {

        colourSelector = colourNumber;

        SetPrefabMaterialColour(prefabSelector, colourSelector);

        DeactivateSelectorMenu();

    }

    void SetPrefabMaterialColour(int prefabNumber, int colourNumber)
    {

        //prefabMaterials[prefabSelector].color = allColours[colourNumber];

        //TEST
        if (prefabNumber == 0 && colourNumber != currentLeft && colourNumber != currentMiddle && colourNumber != currentRight)
        {
            prefabMaterials[prefabSelector].color = allColours[colourNumber];
            currentLeft = colourNumber;
        }
        else if (prefabNumber == 1 && colourNumber != currentLeft && colourNumber != currentMiddle && colourNumber != currentRight)
        {
            prefabMaterials[prefabSelector].color = allColours[colourNumber];
            currentMiddle = colourNumber;
        }
        else if (prefabNumber == 2 && colourNumber != currentLeft && colourNumber != currentMiddle && colourNumber != currentRight)
        {
            prefabMaterials[prefabSelector].color = allColours[colourNumber];
            currentRight = colourNumber;
        }

    }


    void ResetPrefabMaterialColour()
    {
        prefabMaterials[0].color = allColours[0];//yellow
        currentLeft = 0;

        prefabMaterials[1].color = allColours[5];//pink
        currentMiddle = 5;

        prefabMaterials[2].color = allColours[3];//green
        currentRight = 3;

    }

    void ActivateSelectorMenu()
    {
        background.SetActive(true);
        menuIcons.SetActive(true);
    }

    void DeactivateSelectorMenu()
    {
        background.SetActive(false);
        menuIcons.SetActive(false);
    }


    void ActivateCorrectIcon(int iconNumber, int colourNumber)
    {
        if (iconNumber == 0)
        {
            //colourIconLeft[colourNumber].SetActive
        }
        else if (iconNumber == 1)
        {

        }
        else
        {

        }
        
    }


    void AddListeners()
    {
        
        for (int i = 0; i <= colourIconLeft.Length; i++)
        {
            
            int index = i; // Capture the current value of i - required because of how lambda expression work

            colourMenuButtons[i].onClick.AddListener(() => ColourSelector(index));

            colourIconLeft[i].onClick.AddListener(() => PrefabSelector(0));
            colourIconMiddle[i].onClick.AddListener(() => PrefabSelector(1));
            colourIconRight[i].onClick.AddListener(() => PrefabSelector(2));

        }


    }

    //resets prefab colours back to original upon quitting
    private void OnApplicationQuit()
    {

        ResetPrefabMaterialColour();
    }

}
