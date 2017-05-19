using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class CardPlay : MonoBehaviour {

    public GameManager manager;
    public CardPickup cardPickup;
    public GoldScript golds;
    public int posession;
    public int attack = 0;
    public int defense = 0;
    public bool hasRanged;
    public int onTurnPlayed = 0;
    public bool movedThisTurn = false;
    public FillHand fill;

    public void ResetTile()
    {
        movedThisTurn = false;
    }

    public void OnClick()
    {
        cardPickup = manager.pickCard;
        bool canAfford = false;

        if (manager.turn == 1)
        {
            if (cardPickup.cost <= golds.p1g)
                canAfford = true;
            else
                canAfford = false;
        }

        if (manager.turn == 2)
        {
            if (cardPickup.cost <= golds.p2g)
                canAfford = true;
            else
                canAfford = false;
        }
        
        bool validPos = false;

        Collider[] colliders = Physics.OverlapSphere(gameObject.transform.position, 2.30f);
        
        foreach (Collider col in colliders)
        {            
            if (col.gameObject.tag.Equals("Hexagon") && col.gameObject.GetComponent<CardPlay>().posession == manager.turn)
                validPos = true;
        }

        if (cardPickup.clicked && cardPickup != null && validPos && canAfford)
        {
            attack = cardPickup.attack;
            defense = cardPickup.defense;
            gameObject.GetComponentsInChildren<TextMesh>()[0].text = attack + "";
            gameObject.GetComponentsInChildren<TextMesh>()[1].text = defense + "";
            cardPickup.played = true;
            if (manager.turn == 1)
            {
                posession = 1;
                gameObject.GetComponent<Renderer>().material.color = Color.blue;
            }
            else if (manager.turn == 2)
            {
                posession = 2;
                gameObject.GetComponent<Renderer>().material.color = Color.red;
            }
            

            if (manager.turn == 1)
            {
                int index = fill.p1Inst.IndexOf(cardPickup.gameObject);                
                fill.p1Hand.RemoveAt(index);               
            }
            else
            {
                int index = fill.p2Inst.IndexOf(cardPickup.gameObject);
                fill.p2Hand.RemoveAt(index);
            }

            if (manager.turn == 1)
                golds.spendFromP1(cardPickup.cost);
            if (manager.turn == 2)
                golds.spendFromP2(cardPickup.cost);

            onTurnPlayed = manager.totalTurn;
            if (cardPickup.hasCharge)
                onTurnPlayed--;

            cardPickup = null;
            manager.pickCard = null;
            fill.RePrint();            
        }
    }

    public void dealDamage(int amount)
    {
        defense -= amount;

        if (defense <= 0)
        {
            SetNeutral();
        }
    }

    public void SetNeutral ()
    {
        posession = 0;
        gameObject.GetComponent<Renderer>().material.color = Color.white;      
        gameObject.GetComponentsInChildren<TextMesh>()[0].text = "";     
        gameObject.GetComponentsInChildren<TextMesh>()[1].text = "";
        attack = 0;
        defense = 0;
    }

    public void SetBlue(int att, int def)
    {
        posession = 1;
        gameObject.GetComponent<Renderer>().material.color = Color.blue;
        gameObject.GetComponentsInChildren<TextMesh>()[0].text = att + "";
        gameObject.GetComponentsInChildren<TextMesh>()[1].text = def + "";
        attack = att;
        defense = def;
    }

    public void SetRed(int att, int def)
    {
        posession = 2;
        gameObject.GetComponent<Renderer>().material.color = Color.red;
        gameObject.GetComponentsInChildren<TextMesh>()[0].text = att + "";
        gameObject.GetComponentsInChildren<TextMesh>()[1].text = def + "";
        attack = att;
        defense = def;
    }

    // Use this for initialization

    void Awake()
    {
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        cardPickup = manager.pickCard;
        fill = GameObject.Find("HandManager").GetComponent<FillHand>();
        golds = GameObject.Find("GoldManager").GetComponent<GoldScript>();
    }
    
	void Start () {
        if (attack == 0)
            gameObject.GetComponentsInChildren<TextMesh>()[0].text = "";
        else
            gameObject.GetComponentsInChildren<TextMesh>()[0].text = attack + "";
        if (defense == 0)
            gameObject.GetComponentsInChildren<TextMesh>()[1].text = "";
        else
            gameObject.GetComponentsInChildren<TextMesh>()[1].text = defense + "";
    }

	
	// Update is called once per frame
	void Update () {
		
	}
}
