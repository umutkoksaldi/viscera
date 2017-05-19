using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GoldScript : MonoBehaviour
{
    public GameObject p1Gold;
    public GameObject p2Gold;
    
    public int p1g;
    public int p2g;

    void Awake()
    {
        p1Gold = GameObject.Find("P1Gold");
        p2Gold = GameObject.Find("P2Gold");
        p1g = int.Parse(p1Gold.GetComponent<TextMesh>().text);
        p2g = int.Parse(p2Gold.GetComponent<TextMesh>().text);
    }

    public void addGoldToP1(int amount)
    {
        p1g += amount;
        p1Gold.GetComponent<TextMesh>().text = p1g + "";
    }

    public void addGoldToP2(int amount)
    {
        p2g += amount;
        p2Gold.GetComponent<TextMesh>().text = p2g + "";
    }

    public void spendFromP1(int amount)
    {
        p1g -= amount;
        p1Gold.GetComponent<TextMesh>().text = p1g + "";
    }

    public void spendFromP2(int amount)
    {
        p2g -= amount;
        p2Gold.GetComponent<TextMesh>().text = p2g + "";
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
