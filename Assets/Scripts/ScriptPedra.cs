using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptPedra {

public class ScriptPedra : MonoBehaviour {

		void OnTriggerEnter2D(Collider2D col)
		{
			if (col.CompareTag ("Player")) {
				Destroy (this.gameObject);
				col.gameObject.GetComponent<PlayerController> ().pontuacaoPlayer++;
			}
		}
}}