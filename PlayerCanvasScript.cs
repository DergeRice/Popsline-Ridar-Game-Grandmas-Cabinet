using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCanvasScript : MonoBehaviour
{
    [SerializeField]
    GameObject Name,BackRound,Timer,Health,EnterBtn;

    [SerializeField]
    GameObject FaceObj,HandCanvas,BottomTouch,FailFaceObj,BottomText,ShootLoad,Shield;
    
    GameObject[] HealthContainer;
    int DamagedAmout = 0;
    Color MyColor;

    [SerializeField]
    Sprite KickSp, FailSp, CommonSp;

    public float SurviveTime = 0 ;

    public string PlayerName = "";

    enum CharSort
    {
        Halmi,
        Horang,
        BlueDragon,
        RedDragon,
        Turtle
    }
    
    public Sprite[] BackBasis = new Sprite[5];
    public Sprite[] PointBlock = new Sprite[5]; 
    public Sprite[] SmileFace = new Sprite[5]; 
    public Sprite[] FailFace = new Sprite[5]; 
    public Sprite[] HandSprite = new Sprite[5]; 
    public Sprite[] FootButton = new Sprite[5]; 

    int MySort;

    GameManager GameManager;
    
    public float SkillUpgradeTime = 60;

    GameObject MyBar;
    PlayerBarScript MyBarScript;
    int SkillNum = 0;

    bool GodMode;
    // Start is called before the first frame update
    void Start()
    {
        
        MySort = Random.Range(0,5);

        HealthContainer = new GameObject[Health.transform.childCount];
        for (int i = 0; i < Health.transform.childCount; i++)
        {
            HealthContainer[i] = Health.transform.GetChild(i).gameObject;
        }
        SetMyChar();
        //CheckPlayerOn();
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();


        MyBar = GameManager.FindWallFromCanvas(gameObject);
        MyBarScript = MyBar.GetComponent<PlayerBarScript>();

       
    }

    void SetMyChar()
    {
        BackRound.GetComponent<Image>().sprite = BackBasis[MySort];
        FaceObj.GetComponent<Image>().sprite = SmileFace[MySort];   
        FailFaceObj.GetComponent<Image>().sprite = FailFace[MySort];
        HandCanvas.GetComponent<Image>().sprite = HandSprite[MySort];   
        BottomTouch.GetComponent<Image>().sprite = FootButton[MySort];   

        for (int i = 0; i < HealthContainer.Length; i++)
        {
            HealthContainer[i].GetComponent<Image>().sprite = PointBlock[MySort];
        } 

    }

    // Update is called once per frame
    void Update()
    {
        Name.GetComponent<Text>().text = PlayerName;
        if(PlayerName != "") Timer.GetComponent<Text>().text = $"{((int)SurviveTime/60).ToString("D2")}:"+(((int)SurviveTime)%60).ToString("D2");
        else Timer.GetComponent<Text>().text = "";

        if(PlayerName != ""){ SurviveTime+=Time.deltaTime; }
        CheckThisBlock();

        if(GameManager.GetMyState(gameObject) == ConstNum.AI) BottomText.GetComponent<Text>().text = "AI Play";
        if(GameManager.GetMyState(gameObject) == ConstNum.Block) BottomText.GetComponent<Text>().text = "Start";

        IndicateShootAvail();
        
    }

    IEnumerator CheckMySkillsAvail()
    {
        yield return new WaitForSeconds(SkillUpgradeTime);
        SkillNum ++;
        DoSkill(SkillNum);
        StartCoroutine(CheckMySkillsAvail());
    }

    public void DoSkill(int Num)
    {
        if(Num  == 1)
        {
            BottomText.GetComponent<Text>().text = "Rapid";
            MyBarScript.SkillNum = 1;
            MyBarScript.SkillBallCount = 3;
        }
        else if(Num == 2)
        {
            BottomText.GetComponent<Text>().text = "Small";
            MyBarScript.SkillNum = 2;
            MyBarScript.SkillBallCount = 3;
        }
        else if(Num == 3)
        {
            BottomText.GetComponent<Text>().text = "+Health";
            DamagedAmout--;
            UpdateMyHealth();
        }
        else if(Num == 4)
        {
            BottomText.GetComponent<Text>().text = "10sec God";
            EnableGodMod(10);
        }
        else if(Num == 5)
        {
            BottomText.GetComponent<Text>().text = "Extreme Fire";
            MyBarScript.SkillNum = 5;
            MyBarScript.SkillBallCount = 3;
            SkillNum = 0;
        }

    }
    public void UpdateMyHealth()
    {
        for(int i = 0; i < HealthContainer.Length; i++)
        {
            HealthContainer[i].SetActive(true);
        }

        for(int i = 0; i < DamagedAmout; i++)
        {
            HealthContainer[i].SetActive(false);
        }

    }
    public void EnableGodMod(int Time)
    {
        Shield.SetActive(true);
       GodMode = true;
       StartCoroutine(DisableGodModeAfterDelay(Time));
    }

    private IEnumerator DisableGodModeAfterDelay(int Time)
    {
        yield return new WaitForSeconds(Time);
        GodMode = false;
        Shield.SetActive(false);
    }
    public void IndicateShootAvail()
    {
       ShootLoad.GetComponent<Image>().fillAmount = (MyBarScript.BallSpawnCoolCur/MyBarScript.BallSpawnCoolMax);
    }

    public void CheckThisBlock()
    {
        if(GameManager.GetMyState(gameObject) == ConstNum.Block)
        {
            //BottomTouch.SetActive(true);
            Health.SetActive(false);
            EnterBtn.SetActive(true);
        }
        
    }

    public void IsFail()
    {
        if(GodMode) return;

        if(MyBarScript.Type != 0 && DamagedAmout < 10)
        {
            FaceObj.SetActive(false);
            FailFaceObj.SetActive(true);
            DamagedAmout ++;

            for (int i = 0; i < DamagedAmout; i++)
            {
                HealthContainer[i].gameObject.SetActive(false);
            }
        }

        if(DamagedAmout >= 10) // Player Died
        {
            Isdead();
            StopCoroutine(CheckMySkillsAvail());
            SkillNum = 0;
        }
        Invoke(nameof(SetNomal),1f);
    }
    public void IsKick()
    {
        if(PlayerName != "")
        {
            //ShowImg.GetComponent<Image>().sprite = KickSp;
            //Invoke(nameof(SetNomal),0.3f);
        }
    }

    public void Isdead()
    {
        MyBarScript.SkillNum = 0;
        CancelInvoke(nameof(SetNomal));
        
        SetThisPlayerBlank();
        GameManager.SetMyState(gameObject,ConstNum.Block);
        SurviveTime = 0;
    }

    public void SetNomal()
    {
        FaceObj.SetActive(true);
        FailFaceObj.SetActive(false);
    }

    public void SetEmptyImg()
    {
        //ShowImg.GetComponent<Image>().sprite = null;
        Health.SetActive(false);
        EnterBtn.SetActive(true);
    }

    void CheckPlayerOn()
    {
        if(PlayerName != "")
        {
            Health.SetActive(true);
            for (int i = 0; i < 10; i++)
            {
                HealthContainer[i].gameObject.SetActive(true);
            }

        }
        else SetEmptyImg();
    }

    

    void SetThisPlayerBlank()
    {
        PlayerName = "";
       // Back.GetComponent<Image>().color = Color.white;
        SetEmptyImg();
        DamagedAmout = 0;
    }

    private void OnMouseDown() 
    {
        
    }

    public void EnterPlayer()
    {
        if(GameManager.GetMyState(gameObject) != ConstNum.Block) return;
        StartCoroutine(CheckMySkillsAvail());

        
        EnterBtn.SetActive(false);
        BottomText.GetComponent<Text>().text = "Nomal";
        Health.SetActive(true);
        ShootLoad.SetActive(true);
        PlayerName = "Default";
        CheckPlayerOn();
        GameManager.SetMyState(gameObject,ConstNum.Player);
    }

    public void EnterAI()
    {
        EnterBtn.SetActive(false);
        Health.SetActive(true);
        BottomText.GetComponent<Text>().text = "Nomal";
        ShootLoad.SetActive(true);
        PlayerName = GameManager.GetMyName(gameObject);
        CheckPlayerOn();
        GameManager.SetMyState(gameObject,ConstNum.AI);
    }

}
