using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FillHand : MonoBehaviour {

    public List<GameObject> p1Hand;
    public List<GameObject> p1Deck;
    public List<GameObject> p2Hand;
    public List<GameObject> p2Deck;
    public List<GameObject> p1Inst;
    public List<GameObject> p2Inst;
    public GameManager manager;

    public void RePrint()
    {
        foreach (GameObject card in p1Inst)
        {
            Destroy(card);
        }
        foreach (GameObject card in p2Inst)
        {
            Destroy(card);
        }

        p1Inst.Clear();
        p2Inst.Clear();

        int p1length = -p1Hand.Count / 2;
        int p2length = -p2Hand.Count / 2;
        Transform p1Object = GameObject.Find("Player1Hand").transform;
        Transform p2Object = GameObject.Find("Player2Hand").transform;

        foreach (GameObject card in p1Hand)
        {
            p1Inst.Add(Instantiate(card, new Vector3(p1Object.localPosition.x + p1length * 1.150f, p1Object.localPosition.y, p1Object.localPosition.z), Quaternion.identity));
            p1length += 1;
        }

        foreach (GameObject card in p2Hand)
        {
            p2Inst.Add(Instantiate(card, new Vector3(p2Object.localPosition.x + p2length * 1.150f, p2Object.localPosition.y, p2Object.localPosition.z), Quaternion.identity));
            p2length += 1;
        }

        foreach (GameObject card in p2Inst)
        {
            card.transform.Rotate(0f, 180f, 0f);
        }
    }

    public void Draw()
    {
        if (manager.turn == 1 && p1Hand.Count < 5)
        {
            if (p1Deck.Count > 1)
            {
                int num = UnityEngine.Random.Range(0, p1Deck.Count);
                p1Hand.Add(p1Deck[num]);
                p1Deck.RemoveAt(num);
            }

            else
            {
                manager.p1HeroDamage(manager.fatigueDamage1);
                manager.fatigueDamage1++;
            }
        }
        
        else if (manager.turn == 1 && p1Hand.Count == 5)
        {            

            if (p1Deck.Count > 1)
            {
                int num = UnityEngine.Random.Range(0, p1Deck.Count);
                p1Deck.RemoveAt(num);
            }

            else
            {
                manager.p1HeroDamage(manager.fatigueDamage1);
                manager.fatigueDamage1++;
            }
        }

        if (manager.turn == 2 && p2Hand.Count < 5)
        {
            if (p2Deck.Count > 0)
            {
                int num = UnityEngine.Random.Range(0, p2Deck.Count);
                p2Hand.Add(p2Deck[num]);
                p2Deck.RemoveAt(num);
            }
            else
            {
                manager.p2HeroDamage(manager.fatigueDamage2);
                manager.fatigueDamage2++;
            }
        }

        else if (manager.turn == 2 && p2Hand.Count == 5)
        {
            
            if (p2Deck.Count > 0)
            {
                int num = UnityEngine.Random.Range(0, p2Deck.Count);
                p2Deck.RemoveAt(num);
            }
            else
            {
                manager.p2HeroDamage(manager.fatigueDamage2);
                manager.fatigueDamage2++;
            }
        }

        foreach (GameObject card in p1Inst)
        {
            Destroy(card);
        }
        foreach (GameObject card in p2Inst)
        {
            Destroy(card);
        }

        p1Inst.Clear();
        p2Inst.Clear();

        int p1length = -p1Hand.Count / 2;
        int p2length = -p2Hand.Count / 2;
        Transform p1Object = GameObject.Find("Player1Hand").transform;
        Transform p2Object = GameObject.Find("Player2Hand").transform;

        foreach (GameObject card in p1Hand)
        {
            p1Inst.Add(Instantiate(card, new Vector3(p1Object.localPosition.x + p1length * 1.150f, p1Object.localPosition.y, p1Object.localPosition.z), Quaternion.identity));
            p1length += 1;
        }

        foreach (GameObject card in p2Hand)
        {
            p2Inst.Add(Instantiate(card, new Vector3(p2Object.localPosition.x + p2length * 1.150f, p2Object.localPosition.y, p2Object.localPosition.z), Quaternion.identity));
            p2length += 1;
        }

        foreach (GameObject card in p2Inst)
        {
            card.transform.Rotate(0f, 180f, 0f);
        }
    }

    void Awake()
    {
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

	// Use this for initialization
	void Start ()
	{
	    int num = UnityEngine.Random.Range(0, p1Deck.Count);
	    p1Hand.Add(p1Deck[num]);
        p1Deck.RemoveAt(num);
	    num = UnityEngine.Random.Range(0, p1Deck.Count);
	    p1Hand.Add(p1Deck[num]);
        p1Deck.RemoveAt(num);
	    num = UnityEngine.Random.Range(0, p1Deck.Count);
	    p1Hand.Add(p1Deck[num]);

	    num = UnityEngine.Random.Range(0, p2Deck.Count);
        p2Hand.Add(p2Deck[num]);
	    p2Deck.RemoveAt(num);
	    num = UnityEngine.Random.Range(0, p2Deck.Count);
	    p2Hand.Add(p2Deck[num]);
	    p2Deck.RemoveAt(num);
	    num = UnityEngine.Random.Range(0, p2Deck.Count);
	    p2Hand.Add(p2Deck[num]);

	    int p1length = -p1Hand.Count / 2;
	    int p2length = -p2Hand.Count / 2;
	    Transform p1Object = GameObject.Find("Player1Hand").transform;
	    Transform p2Object = GameObject.Find("Player2Hand").transform;

        foreach (GameObject card in p1Hand)
        {            
            p1Inst.Add(Instantiate(card, new Vector3(p1Object.localPosition.x + p1length * 1.150f, p1Object.localPosition.y, p1Object.localPosition.z), Quaternion.identity));
            p1length += 1;
        }

	    foreach (GameObject card in p2Hand)
	    {
            p2Inst.Add(Instantiate(card, new Vector3(p2Object.localPosition.x + p2length * 1.150f, p2Object.localPosition.y, p2Object.localPosition.z), Quaternion.identity));	        
	        p2length += 1;
	    }

	    foreach (GameObject card in p2Inst)
	    {
	        card.transform.Rotate(0f, 180f, 0f);
	    }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
