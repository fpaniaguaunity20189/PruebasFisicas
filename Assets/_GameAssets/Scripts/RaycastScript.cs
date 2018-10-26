using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaycastScript : MonoBehaviour {
    public Transform origen;
    public Transform destino;
    public Text texto;
    void Update () {
        if (Input.GetKeyDown(KeyCode.Space)) {
            Disparar();
        }
	}
    private void Disparar() {
        bool hayImpacto = Physics.Raycast(origen.position, origen.forward);
        Debug.DrawRay(origen.position, origen.forward, Color.red, 50);
        texto.text = ""+hayImpacto;
    }
}
