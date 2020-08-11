using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 
using TMPro;

public class scoresTable : MonoBehaviour
{

    // GameObject Folder
    public GameObject buttonsFolder;
    string currentScore;

    // Public lists/ entries
    public List<EntryScore> EntryList;

    //Not Sure if this is necessary
    public EntryListClass returnScore;

    // Not Sure
    public EntryListClass highScoreTable;

    // input field
    public TMP_InputField nameField;


    // Instantiate Entries
    public Transform entryDisplay;
    public Transform container;
    List<Transform> DisplayScoreList = new List<Transform>();


    // Start is called before the first frame update
    void Awake()
    {

        /*
        string newScore = PlayerPrefs.GetString("Score");
        EntryScore newScoreConversion = JsonUtility.FromJson<EntryScore>(newScore);
        

        highScoreTable.entries = new List<EntryScore>()
        {
            new EntryScore {name = "Bob", score = 4},
            new EntryScore {name = "Joe", score = 5},
            new EntryScore {name = "Dylean", score = 8}
        };

        
        
        //EntryScore entry1 = new EntryScore {name = "worm", score = 1 };
        

        
        
        string json = JsonUtility.ToJson(highScoreTable);
        //print(json);
        PlayerPrefs.SetString("Score", json);
        */
        string newerScore = PlayerPrefs.GetString("Score");
        print(newerScore);
        returnScore = JsonUtility.FromJson<EntryListClass>(newerScore);
        print(returnScore.entries.Count);
        for (int i = 0; i < returnScore.entries.Count; i++)
        {
            print(returnScore.entries[i]);
            print(returnScore.entries[i].name);
        }

        displayList();
        
        


    }
    void displayList()
    {
        for (int i = 0; i < DisplayScoreList.Count; i++)
            Destroy(DisplayScoreList[i].gameObject);
        DisplayScoreList.Clear();

        for (int i = 0; i < returnScore.entries.Count; i++)
        {
            //Instantiate a new item
            //Transform RankedEntry = Instantiate(entryDisplay, container);
            DisplayScoreList.Add(Instantiate(entryDisplay, container));
            DisplayScoreList[i].gameObject.SetActive(true);
            

            //Set Height of the elements of table
            float templateHeight = 5f;
            RectTransform entryRectTransform = DisplayScoreList[i].GetComponent<RectTransform>();
            entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * (i+1));

            //Set Item
            DisplayScoreList[i].Find("RankHeader").GetComponent<TextMeshProUGUI>().text = (i+1).ToString();
            DisplayScoreList[i].Find("Name Header").GetComponent<TextMeshProUGUI>().text = returnScore.entries[i].name;
            DisplayScoreList[i].Find("Score Header").GetComponent<TextMeshProUGUI>().text = returnScore.entries[i].score.ToString();
            
        }

        for (int i = 0; i < DisplayScoreList.Count; i++)
            print(DisplayScoreList[i]);

    }

    int TotalScoreforEntry()
    {
        int totalScore = 0;
        int totalScenes = SceneManager.sceneCountInBuildSettings;

        for (int i = 2; i < (totalScenes - 2); i++)
        {
            
            string levelKey = "Level" + (i-1).ToString() + "RocketsDestroyed";
            print(levelKey);
            totalScore += PlayerPrefs.GetInt(levelKey, 999);
            print(totalScore);
        }
        
        //Return Score
        return totalScore;

    }

    void orderList(List<EntryScore> myScores)
    {
        
        
        for (int i = 0; i < myScores.Count; i++)
        {
            for (int j = i + 1; j < myScores.Count; j++)
            {
                if (myScores[i].score > myScores[j].score)
                {
                    EntryScore placeHolder = myScores[j];
                    myScores[j] = myScores[i];
                    myScores[i] = placeHolder;
                }
                    
            }
        }

        while (myScores.Count > 10)
        {
            myScores.RemoveAt(10);
        }
    }

    [Serializable]
    public class EntryScore
    {
        public string name;
        public int score;
    }

    [Serializable]
    public class EntryListClass
    {
        public List<EntryScore> entries;
    }


    public void newEntry() {

        string newName = nameField.text;
        int newScore = TotalScoreforEntry();

        // New Record
        EntryScore newRecord = new EntryScore {name = newName, score = newScore};

        returnScore.entries.Add(newRecord);


        //OrderList
        orderList(returnScore.entries);
        print(returnScore.entries.Count);
        // Create json and store json.
        string json = JsonUtility.ToJson(returnScore);
        print(json);
        PlayerPrefs.SetString("Score", json);

        buttonsFolder.SetActive(false);


        //Display List
        displayList();




    }

    

}