using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour {

    public GameObject LevelSelectionPanel;
    public GameObject Level1Obj;
    public GameObject Level2Obj;
    public GameObject Level3Obj;
    public GameObject Level4Obj;
    public GameObject Level5Obj;
    public GameObject Level6Obj;

    string topicName;
    int levelNo;
   
	// Use this for initialization
	void Start () {
        LevelSelectionPanel.SetActive(false);
        Level1Obj.SetActive(false);
        Level2Obj.SetActive(false);
        Level3Obj.SetActive(false);
        Level4Obj.SetActive(false);
        Level5Obj.SetActive(false);
        Level6Obj.SetActive(false);

        PlayerPrefs.SetInt("LevelNo", 1);

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LevelSelectionPanelShow()
    {
        Level1Obj.SetActive(true);
        Level2Obj.SetActive(true);
        Level3Obj.SetActive(true);
        Level4Obj.SetActive(true);
        Level5Obj.SetActive(true);
        Level6Obj.SetActive(true);
        LevelSelectionPanel.SetActive(true);
        GameObject[] LevelImgObj = GameObject.FindGameObjectsWithTag("LevelImg");
        
        
        topicName =EventSystem.current.currentSelectedGameObject.name;
        PlayerPrefs.SetString("TopicName", topicName);

        
        foreach (GameObject LevelImg in LevelImgObj)
        LevelImg.GetComponent<Image>().sprite = Resources.Load<Sprite>("Topics/" + topicName);

        if(topicName=="Fruit-Puzzle")

        if (topicName == "Animals-Matching" || topicName == "Animals-Matching2")
        {

            LevelCount5();
        }

        if (topicName == "Motor-Matching")
        {

            LevelCount2();
        }

        if (topicName == "Car-Puzzle" && topicName=="Fruit-Puzzle")
        {
            LevelCount6();
        }
    }
    public void LevelSelectionPanelHide()
    {
        LevelSelectionPanel.SetActive(false);
    }
    public void Level1()
    {
        levelNo = 1;
        PlayerPrefs.SetInt("LevelNo", levelNo);
        PlayerPrefs.SetString("MatchingLevel",topicName+"-1");
        LoadMatchingLevel();
    }
    public void Level2()
    {
        levelNo = 2;
        PlayerPrefs.SetInt("LevelNo", levelNo);
        PlayerPrefs.SetString("MatchingLevel", topicName + "-2");
        LoadMatchingLevel();
    }
    public void Level3()
    {
        levelNo = 3;
        PlayerPrefs.SetInt("LevelNo", levelNo);
        PlayerPrefs.SetString("MatchingLevel", topicName + "-3");
        LoadMatchingLevel();
    }
    public void Level4()
    {
        levelNo = 4;
        PlayerPrefs.SetInt("LevelNo", levelNo);
        PlayerPrefs.SetString("MatchingLevel", topicName + "-4");
        LoadMatchingLevel();
    }
    public void Level5()
    {
        levelNo = 5;
        PlayerPrefs.SetInt("LevelNo", levelNo);
        PlayerPrefs.SetString("MatchingLevel", topicName + "-5");
        LoadMatchingLevel();
    }
    public void Level6()
    {
        levelNo = 6;
        PlayerPrefs.SetInt("LevelNo", levelNo);
        PlayerPrefs.SetString("MatchingLevel", topicName + "-6");
        LoadMatchingLevel();
    }
    void LoadMatchingLevel()
    {
        if (topicName.IndexOf("Puzzle")>=0)
        {
            SceneManager.LoadScene("Puzzle");
        }
        else if (topicName.IndexOf("Matching") >= 0)
        {
            SceneManager.LoadScene("Matching");
        }
            
    }


    void LevelCount1()
    {
        Level1Obj.SetActive(true);
        Level2Obj.SetActive(false);
        Level3Obj.SetActive(false);
        Level4Obj.SetActive(false);
        Level5Obj.SetActive(false);
        Level6Obj.SetActive(false);
        PlayerPrefs.SetInt("LastLevel", 1);
    }
    void LevelCount2()
    {
        Level1Obj.SetActive(true);
        Level2Obj.SetActive(true);
        Level3Obj.SetActive(false);
        Level4Obj.SetActive(false);
        Level5Obj.SetActive(false);
        Level6Obj.SetActive(false);
        PlayerPrefs.SetInt("LastLevel", 2);
    }
    void LevelCount3()
    {
        Level1Obj.SetActive(true);
        Level2Obj.SetActive(true);
        Level3Obj.SetActive(true);
        Level4Obj.SetActive(false);
        Level5Obj.SetActive(false);
        Level6Obj.SetActive(false);
        PlayerPrefs.SetInt("LastLevel", 3);
    }
    void LevelCount4()
    {
        Level1Obj.SetActive(true);
        Level2Obj.SetActive(true);
        Level3Obj.SetActive(true);
        Level4Obj.SetActive(true);
        Level5Obj.SetActive(false);
        Level6Obj.SetActive(false);
        PlayerPrefs.SetInt("LastLevel", 4);
    }
    void LevelCount5()
    {
        Level1Obj.SetActive(true);
        Level2Obj.SetActive(true);
        Level3Obj.SetActive(true);
        Level4Obj.SetActive(true);
        Level5Obj.SetActive(true);
        Level6Obj.SetActive(false);
        PlayerPrefs.SetInt("LastLevel", 5);
    }
    void LevelCount6()
    {
        Level1Obj.SetActive(true);
        Level2Obj.SetActive(true);
        Level3Obj.SetActive(true);
        Level4Obj.SetActive(true);
        Level5Obj.SetActive(true);
        Level6Obj.SetActive(true);
        PlayerPrefs.SetInt("LastLevel", 6);
    }
}
