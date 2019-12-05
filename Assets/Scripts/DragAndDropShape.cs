using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDropShape : MonoBehaviour {

    Vector3 objectPos;
    Vector3 firstPos;
    BoxCollider2D BoxCollider;
    public bool releaseObject = false;
    public GameObject placementFX;
    GameObject Slot;
    bool slotEnabled = false;
    //Sounds
    AudioSource auSource;
    AudioClip auDragAndDrop;
    void Start()
    {
        Sprite ObjectSprite = gameObject.GetComponent<SpriteRenderer>().sprite;
        BoxCollider = gameObject.GetComponent<BoxCollider2D>();
        auSource = GetComponent<AudioSource>();
        //BoxCollider.size = new Vector3(LevelManager.Instance.draggingObject.GetComponent<SpriteRenderer>().size.x, LevelManager.Instance.draggingObject.GetComponent<SpriteRenderer>().size.y*2, 0);
        
    }
    void Update()
    {
        if (ShapeDokuManager.Instance.isHintUsed)
        {
            transform.position = GameObject.Find(gameObject.name).transform.position;
            ShapeDokuManager.Instance.RandomObject();
            auSource.Play();
            ShapeDokuManager.Instance.correctPlacementCount++;
            Debug.Log(ShapeDokuManager.Instance.correctPlacementCount.ToString());
            Instantiate(placementFX, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            Destroy(gameObject.GetComponent("DragAndDropShape"));
            ShapeDokuManager.Instance.isHintUsed = false;
        }
    }
    void OnMouseDown()
    {
        objectPos = Camera.main.WorldToScreenPoint(transform.position);
        firstPos = transform.position;
        if (slotEnabled==true)
        { 
        if (Slot.GetComponent<BoxCollider2D>().enabled==false)
        Slot.GetComponent<BoxCollider2D>().enabled = true;
        }
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
            transform.position = ShapeDokuManager.Instance.droppingAreaPos;
            ShapeDokuManager.Instance.RandomObject();
            auSource.Play();
            ShapeDokuManager.Instance.correctPlacementCount++;
            //Debug.Log(ShapeDokuManager.Instance.correctPlacementCount.ToString());
            Instantiate(placementFX, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            Slot.GetComponent<BoxCollider2D>().enabled = false;
            Slot.name = gameObject.name;
            ShapeDokuManager.Instance.nullCount -= 1;
            //Debug.Log(ShapeDokuManager.Instance.nullCount.ToString());
            slotEnabled = true;
        }
        else
        {
            transform.position = firstPos;
        }


    }
    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.name=="Null")
        {
            
            releaseObject = true;
            ShapeDokuManager.Instance.droppingAreaPos = other.transform.position;
            Slot = other.gameObject;
        }



    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.name == "Null")
        {

            releaseObject = true;
            ShapeDokuManager.Instance.droppingAreaPos = other.transform.position;
            Slot = other.gameObject;
        }


    }
    void OnTriggerExit2D(Collider2D other)
    {
        other.gameObject.name = "Null";
        releaseObject = false;
        
        
    }
    }
