using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TroncomovilScript : MonoBehaviour {
    public Text txtFrenoMano;
    public Text txtSpeed;
    public Text txtMarcha;
    public float fuerzaMaximaMotor = 100;
    public float anguloMaximoRotacion = 20;
    public float incrementoFrenado = 10;
    private float fuerzaFrenado = 0;
    private float vPos;
    private float hPos;
    bool frenoManoActivo = true;
    float fSpeed;
    private WheelCollider wcFrontL, wcFrontR, wcBackL, wcBackR;


    private void Start() {
        //Material matPilotos = GetComponentInChildren<Material>();
        //matPilotos.SetColor("_EmissionColor", Color.red);


        wcFrontL = GameObject.Find("FrontL").GetComponent<WheelCollider>();
        wcFrontR = GameObject.Find("FrontR").GetComponent<WheelCollider>();
        wcBackL = GameObject.Find("BackL").GetComponent<WheelCollider>();
        wcBackR = GameObject.Find("BackR").GetComponent<WheelCollider>();
    }

    private void Update() {
        fSpeed = (int)GetComponent<Rigidbody>().velocity.magnitude;
        txtSpeed.text = ((int)fSpeed).ToString();

        if (Input.GetKeyDown(KeyCode.F)) {
            frenoManoActivo = !frenoManoActivo;
            if (frenoManoActivo == true) {
                txtFrenoMano.text = "Handbrake: ON";
            } else {
                txtFrenoMano.text = "Handbrake: OFF";
            }
        }
    }


    private void FixedUpdate() {
        //RECOGER COORDENADAS
        vPos = Input.GetAxis("Vertical");
        hPos = Input.GetAxis("Horizontal");

        //RUEDAS DIRECTRICES
        wcFrontL.steerAngle = anguloMaximoRotacion * hPos;
        wcFrontR.steerAngle = anguloMaximoRotacion * hPos;

        if (!frenoManoActivo) {
            if (vPos > 0) {
                txtMarcha.text = "1";
                //RUEDAS MOTRICES
                SoltarFreno();
                wcBackL.motorTorque = fuerzaMaximaMotor * vPos;
                wcBackR.motorTorque = fuerzaMaximaMotor * vPos;
            } else if (vPos < 0 && fSpeed>0.0001) {
                txtMarcha.text = "0";
                Frenar();
            } else if (vPos < 0 && fSpeed<=0.0001) {
                txtMarcha.text = "R";
                SoltarFreno();
                wcBackL.motorTorque = fuerzaMaximaMotor * vPos;
                wcBackR.motorTorque = fuerzaMaximaMotor * vPos;
            }
        } else {
            wcFrontL.brakeTorque = Mathf.Infinity;
            wcFrontR.brakeTorque = Mathf.Infinity;
            wcBackL.brakeTorque = Mathf.Infinity;
            wcBackR.brakeTorque = Mathf.Infinity;
        }
    }

    private void Frenar() {
        fuerzaFrenado = fuerzaFrenado + incrementoFrenado;
        wcFrontL.brakeTorque = fuerzaFrenado;
        wcFrontR.brakeTorque = fuerzaFrenado;
        wcBackL.brakeTorque = fuerzaFrenado;
        wcBackR.brakeTorque = fuerzaFrenado;
    }
    private void SoltarFreno() {
        wcFrontL.brakeTorque = 0;
        wcFrontR.brakeTorque = 0;
        wcBackL.brakeTorque = 0;
        wcBackR.brakeTorque = 0;
    }
}
