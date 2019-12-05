using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShapeDokuManager : MonoBehaviour {

    public static ShapeDokuManager Instance { get; private set; }


    internal int order = 0;

    //sudoku
    string[] questionArray = { "Circle", "Square", "Null", "Star", "Triangle", "Null", "Circle", "Square", "Square", "Triangle", "Star", "Circle", "Star", "Null", "Null", "Triangle" };
    string[] solutionArray = { "Circle", "Square", "Triangle", "Star", "Triangle", "Star", "Circle", "Square", "Square", "Triangle", "Star", "Circle", "Star", "Circle", "Square", "Triangle" };
    internal int nullCount;
    //dragging objects
    string draggingFolderName;
    Sprite draggingObjectSprite;
    public GameObject draggingObject;
    public List<GameObject> draggingObjectClone = new List<GameObject>();
    internal Vector3 draggingObjectPos;

    internal int correctPlacementCount = 0;

    //dropping area
    string droppingFolderName;
    Sprite droppingAreaSprite;
    public GameObject droppingArea;
    public List<GameObject> droppingAreaClone = new List<GameObject>();
    internal Vector3 droppingAreaPos;


    //Slots
    public List<GameObject> Slots = new List<GameObject>();

    public GameObject RandomObjectSlot;
    internal Vector3 RandomObjectSlotPos;
    int slotOrder = 1;
    string slotName;
    string topicName;
    //fireworks
    bool playFirework = true;
    public GameObject finishLevelFirework;
    AudioSource auSource;
    AudioClip auFirework;

    //nextLevel window
    public GameObject nextLevelWindow;
    public GameObject goHomeWindow;
    int levelNo;
    int lastLevel;

    //Hints
    internal bool isHintUsed = false;
    // Use this for initialization
    void Start()
    {
        Instance = this;

        levelNo = PlayerPrefs.GetInt("LevelNo");
        lastLevel = PlayerPrefs.GetInt("LastLevel");
        StartGame(PlayerPrefs.GetString("MatchingLevel"));

        auFirework = Resources.Load("Sounds/Fireworks") as AudioClip;
        //Debug.Log(draggingObjectSprite.Length.ToString());
        //Debug.Log(draggingObjectClone.Count.ToString());

        for (int i=0;i<questionArray.Length;i++)
        {
            if(questionArray[i]=="Null")
            {
                nullCount++;
            }
        }

        

        produceSlots();
        produceDroppingArea();

        produceSudokuObject("Square", -1.5f,0,4);
        produceSudokuObject("Triangle", -0.5f,4,8);
        produceSudokuObject("Star", 0.5f, 8, 12);
        produceSudokuObject("Circle",1.5f, 12, 16);
        order = 0;
        placeSlots();
        placeDroppingArea();
        
        RandomObject();

    }

    // Update is called once per frame
    void Update()
    {
        if(nullCount==0)
        {
            
            for(int i=0;i<16;i++)
            {
                questionArray[i] = droppingAreaClone[i].name;
                Debug.Log(questionArray[i]); 
                
            }
            nullCount = 1;
            
            if (CheckMatch())
            {
                StartCoroutine("FinishLevel");
            }
            else
            {
                Debug.Log("kaybettin");
            }
        }
    }

    void FixedUpdate()
    {
        
         
    }
   
    internal void produceSudokuObject(string objectName, float objectPosY,int start,int limit)
    {
        draggingObjectSprite = Resources.Load<Sprite>("ShapeDoku-1/"+objectName);
        for (int i =start ; i < limit; i++)
        {
            draggingObjectClone.Add(Instantiate(draggingObject, new Vector2(-0.5f, objectPosY), Quaternion.identity));
            draggingObjectClone[i].name = objectName;
            order++;
            draggingObjectClone[i].GetComponent<SpriteRenderer>().sprite = draggingObjectSprite;
        }
    }
    
    internal void RandomObject()
    {
        

    }
   void produceDroppingArea()
    {
        for (int i = 0; i < 16; i++)
        {
            droppingAreaClone.Add(Instantiate(droppingArea, new Vector2(0, 0), Quaternion.identity));
            droppingAreaClone[i].name = questionArray[i];
            droppingAreaSprite= Resources.Load<Sprite>("ShapeDoku-1/"+droppingAreaClone[i].name);
            droppingAreaClone[i].GetComponent<SpriteRenderer>().sprite = droppingAreaSprite;
            order++;
        }
    }

   
    void produceSlots()
    {
        for (int i = 0; i < 16; i++)
        {
            Slots.Add(GameObject.Find("Slot" + slotOrder.ToString()));
            slotOrder++;
        }
    }
    void placeSlots()
    {
        //Slots Positioning


        

            RandomObjectSlotPos = new Vector3(Screen.width / 1.3f, Screen.height / 2, 1);
            RandomObjectSlot.gameObject.transform.position = Camera.main.ScreenToWorldPoint(RandomObjectSlotPos);
       
    }
    void placeDroppingArea()
    {

        for (int i = 0; i < 16; i++)
        {

            droppingAreaClone[i].transform.position = Slots[i].transform.position;
        }


    }
    void placeDraggingObject()
    {
      

    }
    int fireworkCount = 0;
   IEnumerator FinishLevel()
    {
        auSource = GetComponent<AudioSource>();
        auSource.PlayOneShot(auFirework);
        while (fireworkCount < 10)
        {
            int randomX = Random.Range(-5, 5);
            int randomY = Random.Range(-4, 4);
            Instantiate(finishLevelFirework, new Vector2(randomX, randomY), Quaternion.identity);
            fireworkCount++;
            yield return new WaitForSeconds(0.4f);
        }
        auSource.Stop();
        if (levelNo == lastLevel)
        {
            goHomeWindow.SetActive(true);
        }
        else
        {
            nextLevelWindow.SetActive(true);
        }

        StopCoroutine("FinishLevel");

    }
    
    public void NextLevel()
    {

        levelNo++;
        PlayerPrefs.SetInt("LevelNo", levelNo);
        topicName = PlayerPrefs.GetString("TopicName");
        PlayerPrefs.SetString("MatchingLevel", topicName + "-" + levelNo.ToString());
        Debug.Log(PlayerPrefs.GetString("MatchingLevel"));
        SceneManager.LoadScene("Puzzle");

    }

    void StartGame(string folderName)
    {

        nextLevelWindow.SetActive(false);
        goHomeWindow.SetActive(false);
       /* droppingAreaSprite = Resources.LoadAll<Sprite>(folderName);
        draggingObjectSprite = Resources.LoadAll<Sprite>(folderName);*/
    }

    public void GoHome()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void Hint()
    {
        isHintUsed = true;
    }
    bool CheckMatch()
    {
        if (solutionArray.Length != questionArray.Length)
            return false;
        for (int i = 0; i < solutionArray.Length; i++)
        {
            if (solutionArray[i] != questionArray[i])
                return false;
        }
        return true;
    }
}
