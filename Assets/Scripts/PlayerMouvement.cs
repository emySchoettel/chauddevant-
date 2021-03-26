using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Touch; 

public class PlayerMouvement : MonoBehaviour
{
    public Helper.directions directionActuelle; 

    [SerializeField] private GameObject[] positions = new GameObject[3];

    public Projectile prefabProjectile; 

    private bool canmove = true; 
    [SerializeField]
    private bool invincibility = false; 
    public GameObject model; 


    private Animator anim; 

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>(); 
         //début du jeu
        GetPositions();
        if(positions.Length != 0)
        {
            gameObject.transform.position = StartPosition();
            directionActuelle = Helper.directions.milieu;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(canmove)
        {
            //Swipe left
            // if(SwipeManager.IsSwipingLeft())
            // {
            if(SwipeSecondScript.swipedLeft && !Projectile.isFired)
            {
                if(directionActuelle == Helper.directions.droite)
                {
                    directionActuelle = Helper.directions.milieu;
                    gameObject.transform.position = positions[1].transform.position; 
                }
                else if(directionActuelle == Helper.directions.milieu)
                {
                    directionActuelle = Helper.directions.gauche;
                    gameObject.transform.position = positions[0].transform.position; 
                }
            }

            //Swipe right
            // else if(SwipeManager.IsSwipingRight())
            // {
            if(SwipeSecondScript.swipedRight && !Projectile.isFired)
            {
                if(directionActuelle == Helper.directions.gauche)
                {
                    directionActuelle = Helper.directions.milieu; 
                    gameObject.transform.position = positions[1].transform.position; 
                }
                else if(directionActuelle == Helper.directions.milieu)
                {
                    directionActuelle = Helper.directions.droite; 
                    gameObject.transform.position = positions[2].transform.position; 
                }
            
            }
        }
        
        var count = Input.touchCount; 

        if(count > 0)
        {
            for(int i = 0; i < count; i++)
            {
                var touch = Input.GetTouch(i);
                if(touch.phase == TouchPhase.Ended)
                {
                    Helper.createProjectile(gameObject);
                }

            }
        }

         if(canmove)
        {
            if(directionActuelle == Helper.directions.gauche)
            {
                if(Input.GetKeyDown(KeyCode.RightArrow))
                {
                    directionActuelle = Helper.directions.milieu;
                    gameObject.transform.position = positions[1].transform.position;
                }
            }
            else if(directionActuelle == Helper.directions.milieu)
            {
                if(Input.GetKeyDown(KeyCode.RightArrow))
                {
                    directionActuelle = Helper.directions.droite;
                    gameObject.transform.position = positions[2].transform.position;
                }
                else if(Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    directionActuelle = Helper.directions.gauche;
                    gameObject.transform.position = positions[0].transform.position;
                }
            }
            else if(directionActuelle == Helper.directions.droite)
            {
                if(Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    directionActuelle = Helper.directions.milieu;
                    gameObject.transform.position = positions[1].transform.position;
                }
            }
        }
    }

    void TriggerVulnerability()
    {
        if(invincibility)
        {
            StartCoroutine(Vulnerability());
        }
    }


    public IEnumerator Vulnerability()
    {
        invincibility = true; 

        anim.SetBool("blink", true);

        yield return new WaitForSeconds(3.0f);

        anim.SetBool("blink", false);

        invincibility = false; 
    }

    private Vector3 StartPosition()
    {
        Vector3 res = new Vector3();
        int rand = Random.Range(0,3);
        switch(rand)
        {
            case 0:
                res = positions[0].transform.position;
            break; 
            case 2:
                res = positions[2].transform.position;
            break; 
            default: 
                res = positions[1].transform.position;
            break; 
        }
        return res;
    }
    private void GetPositions()
    {
        GameObject Emplacement = GameObject.Find("EmplacementsJoueur");
        if(Emplacement != null)
        {
            for(int i = 0; i < Emplacement.transform.childCount; i++)
            {
                positions[i] = Emplacement.transform.GetChild(i).gameObject;
            }
        }
    }

    public void setMove(bool newmove)
    {
        canmove = newmove; 
    }

    public bool getMove()
    {
        return canmove; 
    }

    public bool getInvincibility()
    {
        return invincibility;
    }    

    public void setInvincibility(bool choix)
    {
        invincibility = choix; 
    }
}
