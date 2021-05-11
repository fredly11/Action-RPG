using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core{
    public class CameraController : MonoBehaviour {

    [SerializeField] float camMoveSpeed;
    [SerializeField] float camRotateSpeed;
    [SerializeField] float camZoomSpeed;


        // Update is called once per frame
        void Update () {

            if(Input.GetKey(KeyCode.A)){
                transform.Translate(new Vector3(0, 0, camMoveSpeed * Time.deltaTime));
            }
            if(Input.GetKey(KeyCode.D)){
                transform.Translate(new Vector3(0, 0, -camMoveSpeed * Time.deltaTime));
            }
            if (Input.GetKey(KeyCode.W))
            {
                transform.Translate(new Vector3(camMoveSpeed * Time.deltaTime, 0, 0));
            }
            if (Input.GetKey(KeyCode.S))
            {
                transform.Translate(new Vector3(-camMoveSpeed * Time.deltaTime, 0, 0));
            }
            if (Input.GetKey(KeyCode.Q))
            {
                transform.Rotate(new Vector3(0, camRotateSpeed * Time.deltaTime, 0));
            }
            if (Input.GetKey(KeyCode.E))
            {
                transform.Rotate(new Vector3(0, -camRotateSpeed*Time.deltaTime, 0) );
            }


            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {

                Camera.main.transform.position -= Camera.main.transform.forward* camZoomSpeed;
            }

            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                Camera.main.transform.position += Camera.main.transform.forward*camZoomSpeed;
            }
        


        }
    }
}
