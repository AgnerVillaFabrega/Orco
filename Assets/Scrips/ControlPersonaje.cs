using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ControlPersonaje : MonoBehaviour {
	 
	Rigidbody2D rgb;
    Animator anim;
	public float maxVel = 5f;
	bool haciaDerecha = true;
    public Slider slider;
    public Text txt;
    public float energy = 100;

    public int costoGolpeAire = 1;
    public int costoGolpeArbol = 3;
    public int premioArbol = 15;

    bool enFire1 = false;
    ControlArbol ctrArbol =null;
    public GameObject hacha = null;
    
    public bool jumping = false;
    public float yJumpForce = 300;
    Vector2 jumpForce;

    public int costoBala = 20;
	// Use this for initialization
	void Start () {
		rgb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        hacha = GameObject.Find("/orc/orc_body/orc _R_arm/orc _R_hand/orc_weapon");
	}
	void Update() {
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Muriendo"))
        {
            if(energy <= 0){
                energy = 0;
                anim.SetTrigger("Muriendo");
            }
        }else
            return;
            
        if (Mathf.Abs(Input.GetAxis("Fire1"))>0.01f){
            if (enFire1 == false){
                enFire1 = true;
                hacha.GetComponent<CircleCollider2D>().enabled = false;
                anim.SetTrigger("attack");
                if(ctrArbol != null){
                    if(ctrArbol.GolpeOrco()){
                        energy+= premioArbol;
                        if(energy > 100)
                            energy =100;
                        
                    }else{
                        energy -= costoGolpeArbol;
                    }
                }else{
                    energy -= costoGolpeAire;
                    energy--;
                }
            }
        }else
        {
            enFire1 = false;
        }
        slider.value = energy;
        txt.text = energy.ToString();
    }
    public void HabilitarTriggerHacha(){
        hacha.GetComponent<CircleCollider2D>().enabled = true;
    }
	void FixedUpdate () {
		float v= Input.GetAxis("Horizontal");
        Vector2 vel = new Vector2(0,rgb.velocity.y);
        v *= maxVel;
        vel.x = v;
        rgb.velocity = vel;

        anim.SetFloat("speed",vel.x);
		if (haciaDerecha && v < 0)
        {
            haciaDerecha = false;
            Flip();
        }else if (!haciaDerecha && v > 0){
            haciaDerecha = true;
            Flip();
        }
        if(Input.GetAxis("Jump")>0.01f){
            if (!jumping)
            {
                jumping = true;
                jumpForce.x = 0f;
                jumpForce.y = yJumpForce;
                rgb.AddForce(jumpForce);
            }
        }else
            jumping = false;
        
	}
	void Flip(){
        var s = transform.localScale;
        s.x *= -1;
        transform.localScale = s;
    }
    public void SetControlArbol(ControlArbol ctr){
        ctrArbol = ctr;
    }
    public void RecibirBala(){
        energy -= costoBala;
    }
}
