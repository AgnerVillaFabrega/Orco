using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ControlEnemigo : MonoBehaviour {
	public float velocidad = -1f;
    Rigidbody2D rgd;
    Animator anim;

    public GameObject bulletPrototype;

    public Slider slider;
    public Text txt;

    public int energy = 100;
    void Start()
    {
        rgd = GetComponent<Rigidbody2D>(); 
        anim = GetComponent<Animator>();

    }

    void Update(){
        if (energy<= 0)
        {
            energy = 0;
            anim.SetTrigger("Muriendo");
        } 
        slider.value = energy;
        txt.text = energy.ToString();
    }
    void FixedUpdate()
    {
        Vector2 vector2 = new Vector2(velocidad,0);
        rgd.velocity = vector2;
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Caminando")&& Random.value< 1f/(60f*3f))
        {
            anim.SetTrigger("Apuntando");
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Apuntando"))
        {
            if ( Random.value< 1f/3f){
                anim.SetTrigger("Disparando");
            }else{
                anim.SetTrigger("Caminando");
            }
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        Flip();
    }
    void Flip(){
        velocidad *= -1;
        var s = transform.localScale;
        s.x *= -1;
        transform.localScale = s;
    }
    public void Disparar(){
        anim.SetTrigger("Apuntando");
    }
    public void EmitirBala(){
        GameObject bulletCopy = Instantiate(bulletPrototype);
		bulletCopy.transform.position = new Vector3(transform.position.x,transform.position.y,-1f);
		bulletCopy.GetComponent<ControlBala>().direction = new Vector3(transform.localScale.x,0,0);
        energy--;
    }
    public void BajarPuntosPorOrcoCerca(){
        energy-=3;
    }
}
