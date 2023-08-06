using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBarScript : MonoBehaviour
{

    GameManager GameManager;
    public int Type = 0; // -1: AI / 0: Block / 1: Player 
    // Start is called before the first frame update
    
    public float BallSpawnCoolMax = 0.01f ,BallSpawnCoolCur = 0.01f;

    [SerializeField]

    GameObject Center,Ball,AIBallShoot,MyCanvas;


    bool CanSpawnBall;

    public int SkillNum, SkillBallCount;
    void Start()
    {
       
        BallSpawnCoolCur = BallSpawnCoolMax;
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
         MyCanvas = GameManager.FindCanvasFromWall(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        BallTimeCount();
        if(Type == ConstNum.AI)
        {
            AIBallSpawn();
        }
    }

    void AIBallSpawn()
    {
        if(CanSpawnBall) 
        {
            GameObject InsBall;
            Vector3 SpwanPos = new Vector3(AIBallShoot.transform.position.x-Center.transform.position.x+transform.position.x,5,AIBallShoot.transform.position.z-Center.transform.position.z+transform.position.z) ;
            InsBall = Instantiate(Ball,SpwanPos,Quaternion.Euler(90,0,0));
            InsBall.GetComponent<Rigidbody>().AddForce(-(Center.transform.position-SpwanPos));
            
            BallSpawnCoolCur = 0;
        }
    }
    void BallTimeCount()
    {
        BallSpawnCoolCur += Time.deltaTime;
        if(BallSpawnCoolCur >= BallSpawnCoolMax) 
        {
            BallSpawnCoolCur = BallSpawnCoolMax;
            CanSpawnBall =true;
        }
        else
        {
            CanSpawnBall = false;
        }
    }

    private void OnCollisionEnter(Collision other) 
    {
        
        if(other.collider.tag == "Ball")
        {
            if(Type == ConstNum.AI) //AI
            {
                if(Random.value < 0.1f)
                {
                    //GetComponent<BoxCollider>().isTrigger = true;
                    //Invoke(nameof(SetBoxBlock),0.1f);
                    transform.GetChild(0).gameObject.GetComponent<DetectionScript>().OnCollisionEnter(other);
                    //Destroy(other.gameObject);
                }
                else GameManager.HitWall(gameObject);
                
            }
            if(Type == ConstNum.Player) // Player
            {
                transform.GetChild(0).gameObject.GetComponent<DetectionScript>().OnCollisionEnter(other);
            }
        }
        
        if(other.collider.tag == "TouchPoint")
        {
            if(CanSpawnBall)
            {
                GameObject InsBall;
                Vector3 SpwanPos = new Vector3(other.transform.position.x-Center.transform.position.x+transform.position.x,2,other.transform.position.z-Center.transform.position.z+transform.position.z) ;
                InsBall = Instantiate(Ball,SpwanPos,Quaternion.Euler(90,0,0));
                InsBall.GetComponent<Rigidbody>().AddForce(-(Center.transform.position-SpwanPos));
                BallSpawnCoolCur = 0;
                if(SkillNum == 1 && SkillBallCount > 0)
                {
                    InsBall.GetComponent<HeadScript>().Rapid.SetActive(true);
                    InsBall.GetComponent<Rigidbody>().velocity = InsBall.GetComponent<Rigidbody>().velocity*1.4f;
                    InsBall.GetComponent<HeadScript>().speedLimit *= 1.4f;
                    SkillBallCount--;
                }
                if(SkillNum == 2 && SkillBallCount > 0)
                {
                    InsBall.transform.localScale = new Vector3(1.3f,1.3f,1.3f);
                    SkillBallCount--;
                }
                if(SkillNum == 5 && SkillBallCount > 0)
                {
                    InsBall.GetComponent<HeadScript>().Fire.SetActive(true);
                    InsBall.layer = 10;
                    SkillBallCount--;
                }
                
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "TouchPoint")
        {
            if(CanSpawnBall)
            {
                GameObject InsBall;
                Vector3 SpwanPos = new Vector3(other.transform.position.x-Center.transform.position.x+transform.position.x,2,other.transform.position.z-Center.transform.position.z+transform.position.z) ;
                InsBall = Instantiate(Ball,SpwanPos,Quaternion.Euler(90,0,0));
                InsBall.GetComponent<Rigidbody>().AddForce(-(Center.transform.position-SpwanPos));
                BallSpawnCoolCur = 0;
            }
        }
    }

    void SetBoxBlock()
    {
        //GetComponent<BoxCollider>().isTrigger = false;
    }
}
