using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject[] PlayerWalls = new GameObject[12];
    [SerializeField]
    GameObject[] PlayerCanvas = new GameObject[12];
    List<int> usedCanvasNum = new List<int>();

    [SerializeField]
    GameObject TouchPoint;

    // Start is called before the first frame update
    void OnEnable()
    {
        for (int i = 0; i < Random.Range(5,10); i++)
        {
            CreateAI();
        }
    }

    IEnumerator CheckMouseInput()
    {
        while (true)
        {
            if (Input.GetMouseButton(0))
            {
                Vector3 mousePosition = Input.mousePosition;
                Vector3 spawnPosition = Camera.main.ScreenToWorldPoint(mousePosition)+(Vector3.down*20);

                Destroy(Instantiate(TouchPoint,spawnPosition,Quaternion.identity),0.1f);
            }

            yield return new WaitForSeconds(0.03f); // 입력 확인 간격 설정
        }
    }

    private void Start() {
        StartCoroutine(CheckMouseInput());
    }

    void Update()
    {
        
        // if (Input.GetMouseButton(0))
        // {
        //     Vector3 mousePosition = Input.mousePosition;
        //     Vector3 spawnPosition = Camera.main.ScreenToWorldPoint(mousePosition)+(Vector3.down*20);

        //     Destroy(Instantiate(TouchPoint,spawnPosition,Quaternion.identity),3f);
        //     print("dd");
        // }
    }

    void CreateAI()
    {
        int TempInt;
        do
        {
            TempInt = Random.Range(0, PlayerCanvas.Length);
        }
        while (usedCanvasNum.Contains(TempInt));
        usedCanvasNum.Add(TempInt);

        PlayerCanvas[TempInt].GetComponent<PlayerCanvasScript>().PlayerName = "PLAYER" + (TempInt + 1);
        PlayerWalls[TempInt].GetComponent<PlayerBarScript>().Type = -1;
    }

    public void HitWall(GameObject wall)
    {
        FindCanvasFromWall(wall).GetComponent<PlayerCanvasScript>().IsKick();
    }

    public void HitDetection(GameObject wall)
    {
        FindCanvasFromWall(wall).GetComponent<PlayerCanvasScript>().IsFail();
    }
    public string GetMyName(GameObject GB)
    {
        for (int i = 0; i < PlayerCanvas.Length; i++)
        {
            if(PlayerCanvas[i] == GB)
            {
                
                return "P" + (i + 1);
            }
        }
        return "";
    }

    public void SetMyState(GameObject GB, int num)
    {
        FindWallFromCanvas(GB).GetComponent<PlayerBarScript>().Type = num;
    }

    public int GetMyState(GameObject GB)
    {
        return FindWallFromCanvas(GB).GetComponent<PlayerBarScript>().Type;
    }

    public GameObject FindCanvasFromWall(GameObject wall)
    {
        for (int i = 0; i < PlayerWalls.Length; i++)
        {
            if(PlayerWalls[i]==wall)
            {
                return PlayerCanvas[i];
            }
        }

        return null;
    }


    public GameObject FindWallFromCanvas(GameObject Canvas)
    {
        for (int i = 0; i < PlayerCanvas.Length; i++)
        {
            if(PlayerCanvas[i] == Canvas)
            {
                return PlayerWalls[i];
            }
        }

        return null;
    }
    [ContextMenu("Spwan AI")]
    public void SpwanAI()
    {
        int RandomAI;
        int LoopCnt = 0 ;
        List<GameObject> BlockWall = new List<GameObject>();

        for (int i = 0; i < PlayerWalls.Length; i++)
        {
            if(PlayerWalls[i].GetComponent<PlayerBarScript>().Type == ConstNum.Block)
            {
                BlockWall.Add(PlayerWalls[i]);
            }
        }
        do
        {
            RandomAI = Random.Range(0,PlayerCanvas.Length);
            LoopCnt++;

            if (LoopCnt >= 100)
            {
                return;
            }

        }while(PlayerWalls[RandomAI].GetComponent<PlayerBarScript>().Type != ConstNum.Block);
        
        PlayerCanvas[RandomAI].GetComponent<PlayerCanvasScript>().EnterAI();
    }
}

static class ConstNum
{
    public const int AI = -1;
    public const int Block = 0;
    public const int Player = 1;
}
