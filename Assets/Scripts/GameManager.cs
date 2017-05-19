using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameManager : MonoBehaviour {

    public CardPlay playCard;
    public CardPickup pickCard;
    public int turn = 1;
    public int totalTurn = 1;
    public int fatigueDamage1 = 1;
    public int fatigueDamage2 = 1;
    public GameObject p1Hero;
    public int p1Health;
    public int p2Health;
    public bool p1Won = false;
    public bool p2Won = false;
    public GameObject p2Hero;
    public GameObject insta;

	// Use this for initialization
	void Start () {
		p1Hero = GameObject.Find("P1Hero");
	    p2Hero = GameObject.Find("P2Hero");

	    p1Health = int.Parse(p1Hero.GetComponentInChildren<TextMesh>().text);
	    p2Health = int.Parse(p2Hero.GetComponentInChildren<TextMesh>().text);
    }

    public void p1HeroDamage(int damage)
    {
        p1Health -= damage;
        p1Hero.GetComponentInChildren<TextMesh>().text = p1Health + "";

        if (p1Health <= 0)
        {
            p2Won = true;
        }
    }

    public void p2HeroDamage(int damage)
    {
        p2Health -= damage;
        p2Hero.GetComponentInChildren<TextMesh>().text = p2Health + "";

        if (p2Health <= 0)
        {
            p1Won = true;
        }
    }

    void OnGUI()
    {
        if (p1Won)
        {
            GUI.Label(new Rect(10, 10, 100, 20), "Player 1 Won");
        }

        else if (p2Won)
        {
            GUI.Label(new Rect(10, 10, 100, 20), "Player 2 Won");
        }

        
        
    }

    // Update is called once per frame
    void Update () {

		if (Input.GetMouseButtonDown(0) && pickCard != null && pickCard.clicked)
        {            
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit[] hit = Physics.RaycastAll(ray);

            if (hit.Length != 0)
            {
                RaycastHit target = hit[0];

                for (int i = 0; i < hit.Length; i++)
                    if (hit[i].transform.gameObject.tag.Equals("Hexagon"))
                    {
                        target = hit[i];
                        break;
                    }                
                if (target.transform.gameObject.tag.Equals("Hexagon"))
                {                    
                    playCard = target.transform.gameObject.GetComponent<CardPlay>();                    
                    playCard.OnClick();
                }
               
            }
        }
	}
}
