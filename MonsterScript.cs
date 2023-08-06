using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterScript : MonoBehaviour
{
   public enum MonsterType
    {
        old1,
        old2,
        old4,
        old5,
        oldBoss
    }

    float MyYflip;

    public GameObject MyNavPos;

    public MonsterType MyType;

    public float[] MonsterMoveAmount = new float[5];// Old1MovAmount, Old2MovAmount, Old4MovAmount, Old5MovAmount, OldBossMovAmount;

    public float[] MovingTime = new float[] { 2f, 3.63f, 1.67f, 1.5f, 1.5f };

    // º¯º≠¥Î∑Œ ø√√¨¿Ã, ±‰≤ø∏Æ, æ«±Õ, µø±€¿Ã, µŒ∏Ò
    int[] MonsterHealth = new int[] { 1, 2, 3, 2, 10 };

    public AudioClip[] HitSound = new AudioClip[5];

    public AudioClip[] MobJumpSound = new AudioClip[3];
    public AudioSource audioSource;

    public float  UpOffset, DownOffset;

    public float[] LeftOffset = new float[] {0,-30,-150, 90}, RightOffset = new float[] { 0, 70, -60, 180 };

    [SerializeField]
    GameObject[] oldMonster = new GameObject[5];

    public bool OutsideDoor = false, Bottom = false;

    public int ScreenNum;

    // Start is called before the first frame update
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>(); 
        oldMonster[(int)MyType].GetComponent<MonsterMove>().MyMonsterScript = this;
        MyNavPos = Instantiate(MyNavPos, GameObject.Find("PosParent").transform);
    }
    private void OnEnable()
    {
        oldMonster[(int)MyType].GetComponent<MonsterMove>().MyMonsterScript = this;
        
    }
    void Start()
    {
        LeftOffset = new float[] { 0, -30, -150, 90 };
        RightOffset = new float[] { 0, 70, -60, 180 };
        if ( (int)MyType > 1 && (int)MyType != 4) oldMonster[(int)MyType].transform.localScale = oldMonster[(int)MyType].transform.localScale * Random.Range(0.7f, 1.2f);

        

        GameObject Monster = oldMonster[(int)MyType];
        Monster.GetComponent<MonsterMove>().MyNavPos = MyNavPos;
        Monster.GetComponent<MonsterMove>().MyMonsterScript = this;
        Monster.SetActive(true);
        if (MyType == MonsterType.oldBoss) MonsterSpawner.monsterSpawner.BossAppear(oldMonster[(int)MyType]);
        Monster.GetComponent<Animator>().SetFloat("MonsterType", (int)MyType);
        Invoke(nameof(MoveCommand), Random.Range(0.0f,3.5f));

        MovingTime = new float[] { 2f, 3.63f, 1f, 1.5f, 1.5f };
        //ScreenNum--;
    }
    void MoveCommand()
    {
        oldMonster[(int)MyType].GetComponent<MonsterMove>().Move((int)MyType);
    }

    // Update is called once per frame
    void Update()
    {
        
        oldMonster[(int)MyType].GetComponent<MonsterMove>().MyMoveAmount = MonsterMoveAmount[(int)MyType];
    }

    public void SetMyNewDestinationPos()
    {
        Vector3 CurPos = MyNavPos.transform.position;
        Vector3 NewPos = new Vector3(CurPos.x + Random.Range(-50, 50), CurPos.y + Random.Range(-10, 10), CurPos.z);

        if (NewPos.x > RightOffset[ScreenNum]) NewPos = new Vector3(RightOffset[ScreenNum], NewPos.y, NewPos.z);
        if (NewPos.x < LeftOffset[ScreenNum]) NewPos = new Vector3(LeftOffset[ScreenNum], NewPos.y, NewPos.z);
        if (NewPos.y > UpOffset) NewPos = new Vector3(NewPos.x, UpOffset, NewPos.z);
        if (NewPos.y < DownOffset) NewPos = new Vector3(NewPos.x, DownOffset, NewPos.z);


        MyNavPos.transform.position = NewPos;
        
    }
    public void UpsideDown()
    {
        oldMonster[(int)MyType].GetComponent<MonsterMove>().UpsideDown = true;
    }
}
