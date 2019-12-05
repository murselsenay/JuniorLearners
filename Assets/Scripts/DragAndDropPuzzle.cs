using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDropPuzzle : MonoBehaviour {

    Vector3 objectPos;
    Vector3 firstPos;
    BoxCollider2D BoxCollider;
    public bool releaseObject = false;
    public GameObject placementFX;

    //Sounds
    AudioSource auSource;
    AudioClip auDragAndDrop;
    void Start()
    {

        BoxCollider = gameObject.GetComponent<BoxCollider2D>();
        auSource = GetComponent<AudioSource>();
        //BoxCollider.size = new Vector3(LevelManager.Instance.draggingObject.GetComponent<SpriteRenderer>().size.x, LevelManager.Instance.draggingObject.GetComponent<SpriteRenderer>().size.y*2, 0);
        Vector2 objSize = gameObject.GetComponent<SpriteRenderer>().sprite.bounds.size;
        gameObject.GetComponent<BoxCollider2D>().size = objSize;
        gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(0, 0);
    }
    void Update()
    {
        if (PuzzleManager.Instance.isHintUsed)
        {
            transform.position = GameObject.Find(gameObject.name).transform.position;
            PuzzleManager.Instance.RandomObject();
            auSource.Play();
            PuzzleManager.Instance.correctPlacementCount++;
            Debug.Log(PuzzleManager.Instance.correctPlacementCount.ToString());
            Instantiate(placementFX, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            Destroy(gameObject.GetComponent("DragAndDropPuzzle"));
            PuzzleManager.Instance.isHintUsed = false;
        }
    }
    void OnMouseDown()
    {
        objectPos = Camera.main.WorldToScreenPoint(transform.position);
        firstPos = transform.position;
    }


    void OnMouseDrag()
    {
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, objectPos.z);
        transform.position = Camera.main.ScreenToWorldPoint(mousePos);
    }

    void OnMouseUp()
    {
        if (releaseObject == true)
        {
            transform.position =PuzzleManager.Instance.droppingAreaPos;
            PuzzleManager.Instance.RandomObject();
            auSource.Play();
            PuzzleManager.Instance.correctPlacementCount++;
            Debug.Log(PuzzleManager.Instance.correctPlacementCount.ToString());
            Instantiate(placementFX, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            Destroy(gameObject.GetComponent("DragAndDropPuzzle"));
        }
        else
        {
            transform.position = firstPos;
        }


    }
    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.name == gameObject.name)
        {
            
            releaseObject = true;
            PuzzleManager.Instance.droppingAreaPos = other.transform.position;
            
        }



    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.name == gameObject.name)
        {

            releaseObject = true;
            PuzzleManager.Instance.droppingAreaPos = other.transform.position;
        }


    }
    void OnTriggerExit2D(Collider2D other)
    {
        releaseObject = false;
        
        
    }
    }
