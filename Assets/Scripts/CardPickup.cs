using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CardPickup : MonoBehaviour {

    public bool clicked = false;
    public int cost;
    public int attack;
    public bool hasCharge;
    public bool hasRanged;
    public int defense;
    public bool played = false;
    public FillHand fill;
    GameManager manager;

    void OnMouseDown ()
    {
        bool possessionMatch = false;

        if (manager.turn == 1)
        {
            if (fill.p1Inst.Contains(gameObject))
                possessionMatch = true;
        }

        else if
            (manager.turn == 2)
        {
            if (fill.p2Inst.Contains(gameObject))
                possessionMatch = true;
        }

        if (!clicked && possessionMatch)
        {
            clicked = true;            
            manager.pickCard = gameObject.GetComponent<CardPickup>();
            gameObject.transform.Rotate(new Vector3(-20, 0, 0));
            return;
        }
          
    }

    void OnMouseEnter()
    {
        bool possessionMatch = false;

        if (manager.turn == 1)
        {
            if (fill.p1Inst.Contains(gameObject))
                possessionMatch = true;
        }

        else if
            (manager.turn == 2)
        {
            if (fill.p2Inst.Contains(gameObject))
                possessionMatch = true;
        }

        if (!clicked && possessionMatch)
        {
            gameObject.transform.Translate(new Vector3(0, 0, 0.4f));
        }
    }

    void OnMouseExit()
    {
        bool possessionMatch = false;

        if (manager.turn == 1)
        {
            if (fill.p1Inst.Contains(gameObject))
                possessionMatch = true;
        }

        else if
            (manager.turn == 2)
        {
            if (fill.p2Inst.Contains(gameObject))
                possessionMatch = true;
        }

        if (!clicked && possessionMatch)
            gameObject.transform.Translate(new Vector3(0, 0, -0.4f));
    }

    void OnMouseOver()
    {        
        if (Input.GetMouseButtonDown(1) && clicked)
        {
            clicked = false;
            manager.pickCard = null;
            gameObject.transform.Rotate(new Vector3(20, 0, 0));
            fill.RePrint();
        }
    }

    void Awake()
    {
        fill = GameObject.Find("HandManager").GetComponent<FillHand>();
    }

    // Use this for initialization
    void Start () {
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (gameObject.tag.Equals("MinionCard"))
        {
            
            cost = int.Parse(gameObject.GetComponentsInChildren<TextMesh>()[1].text.ToCharArray()[0].ToString());
            attack = int.Parse(gameObject.GetComponentsInChildren<TextMesh>()[2].text.ToCharArray()[0].ToString());
            defense = int.Parse(gameObject.GetComponentsInChildren<TextMesh>()[3].text.ToCharArray()[0].ToString());
        }
    }
	
	// Update is called once per frame
	void FixedUpdate () {

        if (played)
        {
            clicked = false;
            played = false;
            manager.pickCard = null;
           
        }
		
        if (clicked)
        {
            Vector3 mousePos = Input.mousePosition;
            Vector3 wantedPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 4.0f));
            transform.position = wantedPos;
        }
	}
}
