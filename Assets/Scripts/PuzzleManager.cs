using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PuzzleManager : MonoBehaviour {


    public static PuzzleManager Instance { get; private set; }


    internal int order = 1;



    //dragging objects
    string draggingFolderName;
    Sprite[] draggingObjectSprite;
    public GameObject draggingObject;
    public List<GameObject> draggingObjectClone = new List<GameObject>();
    internal Vector3 draggingObjectPos;

    internal int correctPlacementCount = 0;

    //dropping area
    string droppingFolderName;
    Sprite[] droppingAreaSprite;
    public GameObject droppingArea;
    public List<GameObject> droppingAreaClone = new List<GameObject>();
    internal Vector3 droppingAreaPos;


    //Slots
    public List<GameObject> Slots = new List<GameObject>();
    internal Vector3 Slot1Pos;
    internal Vector3 Slot2Pos;
    internal Vector3 Slot3Pos;
    internal Vector3 Slot4Pos;
    internal Vector3 Slot5Pos;
    internal Vector3 Slot6Pos;
    internal Vector3 Slot7Pos;
    internal Vector3 Slot8Pos;
    internal Vector3 Slot9Pos;
    public GameObject RandomObjectSlot;
    internal Vector3 RandomObjectSlotPos;
    int slotOrder = 1;
    string topicName;
    int objectAlignment;
    string slotName;

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


        topicName = PlayerPrefs.GetString("TopicName");
        if (topicName == "Car-Puzzle")
        {
            objectAlignment = 0;
        }
        else if (topicName == "Fruit-Puzzle")
        {
            objectAlignment = 1;
        }


        if (objectAlignment==1)
        {
            slotName = "SlotH";
        }
        if (objectAlignment == 0)
        {
            slotName = "Slot";
        }



        produceSlots(droppingAreaSprite.Length);
        

        produceDraggingObject(draggingObjectSprite.Length);
        order = 1;
        produceDroppingArea(droppingAreaSprite.Length);
        placeSlots();
        placeDroppingArea(droppingAreaSprite.Length);
        placeDraggingObject(draggingObjectSprite.Length);
        RandomObject();

    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        auSource = GetComponent<AudioSource>();
        if (correctPlacementCount == draggingObjectSprite.Length && playFirework == true)
        {
            playFirework = false;
            auSource.PlayOneShot(auFirework);
            StartCoroutine("FinishLevel");

        }
    }

    internal void RandomObject()
    {

        int randomValue = Random.Range(0, draggingObjectClone.Count);
        //Debug.Log(randomValue.ToString());
        if (draggingObjectClone.Count != 0)
        {
            switch (randomValue)
            {
                case 0:
                    draggingObjectClone[0].SetActive(true);
                    draggingObjectClone.RemoveAt(0);
                    break;
                case 1:
                    draggingObjectClone[1].SetActive(true);
                    draggingObjectClone.RemoveAt(1);
                    break;
                case 2:
                    draggingObjectClone[2].SetActive(true);
                    draggingObjectClone.RemoveAt(2);
                    break;
                case 3:
                    draggingObjectClone[3].SetActive(true);
                    draggingObjectClone.RemoveAt(3);
                    break;
                case 4:
                    draggingObjectClone[4].SetActive(true);
                    draggingObjectClone.RemoveAt(4);
                    break;
                case 5:
                    draggingObjectClone[5].SetActive(true);
                    draggingObjectClone.RemoveAt(5);
                    break;
                case 6:
                    draggingObjectClone[6].SetActive(true);
                    draggingObjectClone.RemoveAt(6);
                    break;
                case 7:
                    draggingObjectClone[7].SetActive(true);
                    draggingObjectClone.RemoveAt(7);
                    break;
                case 8:
                    draggingObjectClone[8].SetActive(true);
                    draggingObjectClone.RemoveAt(8);
                    break;
            }
        }

    }
    void produceDroppingArea(int count)
    {
        for (int i = 0; i < count; i++)
        {
            droppingAreaClone.Add(Instantiate(droppingArea, new Vector2(0, 0), Quaternion.identity));
            droppingAreaClone[i].name = "Area" + order.ToString();
            order++;
            droppingAreaClone[i].GetComponent<SpriteRenderer>().sprite = droppingAreaSprite[i];
        }
    }

    void produceDraggingObject(int count)
    {
        for (int i = 0; i < count; i++)
        {
            draggingObjectClone.Add(Instantiate(draggingObject, new Vector2(0, 0), Quaternion.identity));
            draggingObjectClone[i].name = "Area" + order.ToString();
            order++;
            draggingObjectClone[i].GetComponent<SpriteRenderer>().sprite = draggingObjectSprite[i];
            draggingObjectClone[i].SetActive(false);
        }
    }
    void produceSlots(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Slots.Add(GameObject.Find(slotName + slotOrder.ToString()));
            slotOrder++;
        }
    }
    void placeSlots()
    {
        //Slots Positioning


        if (objectAlignment == 0)
        {
            RandomObjectSlotPos = new Vector3(Screen.width / 2, Screen.height / 6, 1);
            RandomObjectSlot.gameObject.transform.position = Camera.main.ScreenToWorldPoint(RandomObjectSlotPos);
        }
        if (objectAlignment == 1)
        {

            RandomObjectSlotPos = new Vector3(Screen.width / 1.3f, Screen.height / 2, 1);
            RandomObjectSlot.gameObject.transform.position = Camera.main.ScreenToWorldPoint(RandomObjectSlotPos);
        }
        }
    void placeDroppingArea(int count)
    {

        for (int i = 0; i < count; i++)
        {

            droppingAreaClone[i].transform.position = Slots[i].transform.position;
        }


    }
    void placeDraggingObject(int count)
    {
        for (int i = 0; i < count; i++)
        {
            draggingObjectClone[i].transform.position = RandomObjectSlot.transform.position;

        }

    }
    int i = 0;
    IEnumerator FinishLevel()
    {

        while (i < 10)
        {
            int randomX = Random.Range(-5, 5);
            int randomY = Random.Range(-4, 4);
            Instantiate(finishLevelFirework, new Vector2(randomX, randomY), Quaternion.identity);
            i++;
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
        droppingAreaSprite = Resources.LoadAll<Sprite>(folderName);
        draggingObjectSprite = Resources.LoadAll<Sprite>(folderName);
    }

    public void GoHome()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void Hint()
    {
        isHintUsed = true;
    }
}

