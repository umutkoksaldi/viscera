using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using UnityEngine;
using Debug = UnityEngine.Debug;

[System.Serializable]
public class AttackScript : MonoBehaviour {

    public GameObject first;
    public GameObject second;
    public GameObject[] boardTiles;
    public GameManager manager;

    public void ResetTiles()
    {
        foreach (GameObject tile in boardTiles)
        {
            tile.GetComponent<CardPlay>().ResetTile();
        }
    }

    // Use this for initialization
    void Awake () {
        first = null;
        second = null;
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void CommenceAttack ()
    {
        if (first.tag.Equals("Hexagon") && second.tag.Equals("Hexagon")
            && first.GetComponent<CardPlay>().posession != second.GetComponent<CardPlay>().posession)
        {            
            bool validAttack = true;

            Collider[] colliders = Physics.OverlapSphere(first.transform.position, 2.30f);     

            if (!colliders.Contains(second.gameObject.GetComponent<MeshCollider>()))
            {
                validAttack = false;
            }                                   
                        

            if ((manager.turn == 1 && first.GetComponent<CardPlay>().posession == 2) || (manager.turn == 2 && first.GetComponent<CardPlay>().posession == 1))
            {
                validAttack = false;
            }          

            if (manager.totalTurn <= first.GetComponent<CardPlay>().onTurnPlayed)
            {
                validAttack = false;
            }

            if (first.GetComponent<CardPlay>().movedThisTurn)
                validAttack = false;

            if (first.GetComponent<CardPlay>().attack == 0)
                validAttack = false;

            Debug.Log(validAttack);
            if (validAttack)
            {
                int firstAtt = first.GetComponent<CardPlay>().attack;
                int firstDef = first.GetComponent<CardPlay>().defense;
                int secAtt = second.GetComponent<CardPlay>().attack;
                int secDef = second.GetComponent<CardPlay>().defense;
                int firstPos = first.GetComponent<CardPlay>().posession;
                int secPos = second.GetComponent<CardPlay>().posession;
                second.gameObject.GetComponent<CardPlay>().movedThisTurn = true;
                first.gameObject.GetComponent<CardPlay>().movedThisTurn = true;                

                Debug.Log(secDef);
                Debug.Log(firstAtt);

                int newSecDef = secDef - firstAtt;
                int newFirstDef = firstDef - secAtt;

                first.GetComponent<CardPlay>().defense = newFirstDef;
                second.GetComponent<CardPlay>().defense = newSecDef;

                if (newSecDef > 0 && newFirstDef > 0)
                {
                    if (firstPos == 1)
                    {
                        second.GetComponent<CardPlay>().SetRed(secAtt, newSecDef);
                        first.GetComponent<CardPlay>().SetBlue(firstAtt, newFirstDef);
                    }

                    if (firstPos == 2)
                    {
                        second.GetComponent<CardPlay>().SetBlue(secAtt, newSecDef);
                        first.GetComponent<CardPlay>().SetRed(firstAtt, newFirstDef);
                    }
                }

                if (newSecDef <= 0 && newFirstDef > 0)
                {
                    if (firstPos == 1)
                    {
                        second.GetComponent<CardPlay>().SetBlue(firstAtt, newFirstDef);
                        first.GetComponent<CardPlay>().SetNeutral();
                    }

                    if (firstPos == 2)
                    {
                        second.GetComponent<CardPlay>().SetRed(firstAtt, newFirstDef);
                        first.GetComponent<CardPlay>().SetNeutral();
                    }
                }

                else if (newFirstDef <= 0 && newSecDef > 0)
                {
                    if (firstPos == 1)
                    {
                        second.GetComponent<CardPlay>().SetRed(secAtt, newSecDef);
                        first.GetComponent<CardPlay>().SetNeutral();
                    }

                    if (firstPos == 2)
                    {
                        second.GetComponent<CardPlay>().SetBlue(secAtt, newSecDef);
                        first.GetComponent<CardPlay>().SetNeutral();
                    }

                }

                else if (newFirstDef <= 0 && newSecDef <= 0)
                {
                    first.GetComponent<CardPlay>().SetNeutral();
                    second.GetComponent<CardPlay>().SetNeutral();
                }
               

            }
        }
        first = null;
        second = null;
    }

    void CommenceAttackOnHero()
    {
        bool validAttack = true;

        Collider[] colliders = Physics.OverlapSphere(first.transform.position, 2.30f);

        if ((manager.turn == 1 && first.GetComponent<CardPlay>().posession == 2) || (manager.turn == 2 && first.GetComponent<CardPlay>().posession == 1))
        {
            validAttack = false;
        }

        if (!colliders.Contains(second.gameObject.GetComponent<SphereCollider>()))
        {
            validAttack = false;
        }

        if (manager.totalTurn <= first.GetComponent<CardPlay>().onTurnPlayed)
        {
            validAttack = false;
        }

        if (first.GetComponent<CardPlay>().movedThisTurn)
            validAttack = false;

        if (first.GetComponent<CardPlay>().posession == 1 && second.name.Equals("P2Hero") && validAttack)
        {
            manager.p2HeroDamage(int.Parse(first.GetComponentsInChildren<TextMesh>()[0].text));
            first.GetComponent<CardPlay>().movedThisTurn = true;
        }

        if (first.GetComponent<CardPlay>().posession == 2 && second.name.Equals("P1Hero") && validAttack)
        {
            manager.p1HeroDamage(int.Parse(first.GetComponentsInChildren<TextMesh>()[0].text));
            first.GetComponent<CardPlay>().movedThisTurn = true;
        }

        first = null;
        second = null;
    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0) && first == null)
        {           
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit) && hit.transform.gameObject.tag.Equals("Hexagon"))
            {
                first = hit.transform.gameObject;
            }
        }
        else if (Input.GetMouseButtonDown(0) && first != null && second == null)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.tag.Equals("Hero"))
                {
                    second = hit.transform.gameObject;
                    CommenceAttackOnHero();                  
                }
                if (hit.transform.gameObject.tag.Equals("Hexagon"))
                {
                    second = hit.transform.gameObject;
                    CommenceAttack();
                }
            }
        }

    }
}
