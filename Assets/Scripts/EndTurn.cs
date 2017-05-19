using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTurn : MonoBehaviour {

    GameManager manager;
    GameObject mainCamera;
    GameObject secondaryCamera;
    private GoldScript golds;
    private AttackScript attack;
    public FillHand fill;
    

    // Use this for initialization
    void Awake()
    {
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        mainCamera = GameObject.Find("Main Camera");
        secondaryCamera = GameObject.Find("Secondary Camera");
        secondaryCamera.SetActive(false);
        fill = GameObject.Find("HandManager").GetComponent<FillHand>();
        attack = GameObject.Find("GameBoard").GetComponent<AttackScript>();
        golds = GameObject.Find("GoldManager").GetComponent<GoldScript>();
    }

    void OnMouseDown()
    {
        if (manager.turn == 1)
        {
            manager.turn = 2;
            mainCamera.SetActive(false);
            secondaryCamera.SetActive(true);
            golds.addGoldToP2(3);
           
        }
        else if (manager.turn == 2)
        {
            manager.turn = 1;
            mainCamera.SetActive(true);
            secondaryCamera.SetActive(false);
            golds.addGoldToP1(3);
        }

        manager.totalTurn++;
        attack.ResetTiles();
        fill.Draw();        

    }

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
