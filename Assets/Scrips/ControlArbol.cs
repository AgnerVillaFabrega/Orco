using UnityEngine;
using System.Collections;

public class ControlArbol : MonoBehaviour {

	public int numGolpesParaCaer = 3;
    public int numBalasParaCaer = 3;
    Animator anim;
    SpriteRenderer rend;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();   
        rend = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool GolpeOrco(){
        bool resp = false;
        numGolpesParaCaer--;
        if (numGolpesParaCaer <= 0)
        {
            anim.SetTrigger("Cayendo");
            resp = true;
        }
        return resp;
    }
    public bool RecibirDisparo(){
        bool resp = false;
        numBalasParaCaer--;
        switch (numBalasParaCaer)
        {
            case 2:
                rend.color = new Color(1f/242,1f/155,1f/155,1f);
                break;
            case 1: 
                rend.color = new Color(1f/216,1f/10,1f/10);
                break;
        }
        if (numBalasParaCaer <= 0)
        {
            anim.SetTrigger("Cayendo");
            resp = true;
        }
        return resp;
        
    }
}
