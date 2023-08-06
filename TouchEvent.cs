using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TouchEvent : MonoBehaviour
{
    public GameObject TouchBall,TouchParent;
    public Camera LeftCam, RightCam;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //Ray ray = 
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Vector3 spawnPos = hit.point;
                spawnPos = new Vector3(spawnPos.x, spawnPos.y, -30);
                GameObject ball = Instantiate(TouchBall, Camera.main.transform.position, Quaternion.identity);
                GameObject RigidBall = Instantiate(TouchBall, Camera.main.transform.position, Quaternion.identity);
                ball.GetComponent<BallScript>().MovPos = hit.point;
                ball.transform.parent = TouchParent.transform;
                //ball.GetComponent<Rigidbody>().AddForce(hit.point*1000, ForceMode.VelocityChange);


                RigidBall.GetComponent<BallScript>().Rigid = true;
                RigidBall.transform.parent = TouchParent.transform;
                RigidBall.GetComponent<BallScript>().MovPos = hit.point;
                RigidBall.GetComponent<Rigidbody>().DOMove(Camera.main.transform.position+(hit.point- Camera.main.transform.position)*3f, 0.5f);
                RigidBall.tag = "Untagged";
                //ball.GetComponent<Rigidbody>().MovePosition(hit.point, ForceMode.VelocityChange);

                

            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = LeftCam.ScreenPointToRay(Input.mousePosition);
            //Ray ray = 
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Vector3 spawnPos = hit.point;
                spawnPos = new Vector3(spawnPos.x, spawnPos.y, -30);
                GameObject ball = Instantiate(TouchBall, LeftCam.transform.position, Quaternion.identity);
                GameObject RigidBall = Instantiate(TouchBall, LeftCam.transform.position, Quaternion.identity);
                ball.GetComponent<BallScript>().MovPos = hit.point;
                ball.transform.parent = TouchParent.transform;
                //ball.GetComponent<Rigidbody>().AddForce(hit.point*1000, ForceMode.VelocityChange);


                RigidBall.GetComponent<BallScript>().Rigid = true;
                RigidBall.transform.parent = TouchParent.transform;
                RigidBall.GetComponent<BallScript>().MovPos = hit.point;
                RigidBall.GetComponent<Rigidbody>().DOMove(LeftCam.transform.position + (hit.point - LeftCam.transform.position) * 3f, 0.5f);
                RigidBall.tag = "Untagged";
                //ball.GetComponent<Rigidbody>().MovePosition(hit.point, ForceMode.VelocityChange);



            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = RightCam.ScreenPointToRay(Input.mousePosition);
            //Ray ray = 
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Vector3 spawnPos = hit.point;
                spawnPos = new Vector3(spawnPos.x, spawnPos.y, -30);
                GameObject ball = Instantiate(TouchBall, RightCam.transform.position, Quaternion.identity);
                GameObject RigidBall = Instantiate(TouchBall, RightCam.transform.position, Quaternion.identity);
                ball.GetComponent<BallScript>().MovPos = hit.point;
                ball.transform.parent = TouchParent.transform;
                //ball.GetComponent<Rigidbody>().AddForce(hit.point*1000, ForceMode.VelocityChange);


                RigidBall.GetComponent<BallScript>().Rigid = true;
                RigidBall.transform.parent = TouchParent.transform;
                RigidBall.GetComponent<BallScript>().MovPos = hit.point;
                RigidBall.GetComponent<Rigidbody>().DOMove(RightCam.transform.position + (hit.point - RightCam.transform.position) * 3f, 0.5f);
                RigidBall.tag = "Untagged";
                //ball.GetComponent<Rigidbody>().MovePosition(hit.point, ForceMode.VelocityChange);



            }
        }

    }
}
