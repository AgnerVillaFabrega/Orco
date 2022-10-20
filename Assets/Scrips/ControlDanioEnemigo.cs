﻿using UnityEngine;
using System.Collections;

public class ControlDanioEnemigo : MonoBehaviour {
	Collider2D colliderEnem = null;
	public int delayBajarPuntosEnemigo = 3;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.name.Equals("Enemigo")&& colliderEnem == null)
		{
			Debug.Log("Colision con el enemigo");
			colliderEnem = other;
			Invoke("BajarPuntosEnemigo", delayBajarPuntosEnemigo);
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other == colliderEnem)
		{
			Debug.Log("Salir de Colision con el enemigo");
			colliderEnem = null;
			CancelInvoke("BajarPuntosEnemigo");
		}
	}
	void BajarPuntosEnemigo(){
		Debug.Log("BajarPuntosEnemigo");
		colliderEnem.gameObject.GetComponent<ControlEnemigo>().BajarPuntosPorOrcoCerca();
	}
}
