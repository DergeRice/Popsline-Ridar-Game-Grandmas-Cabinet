using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Spine.Unity;

public class MonsterSpawner : MonoBehaviour
{
    public static MonsterSpawner monsterSpawner;

    public GameObject MonsterPrefeb, MonsterParent;

    List<float> BossScaleList = new List<float>() { 4, 3.5f, 3, 3, 4, 3.5f, 3};
    int bossCount = 0;
    const float WallFront = -60;
    const float WallInside = -75;

    const float BottomPos = -28;
    const float TopPos = 10;

    const float Screen1Left = 66;
    const float Screen1Right = -31;

    const float Screen2Left = -44;
    const float Screen2Right = -144;

    const float Screen3Left = 77;
    const float Screen3Right = 177;

    public int SpawnMobCount = 30;

    public float CurTime = 0 , EachPazeTime;

    bool InPaze2 = false;
    public Coroutine newSpawn;

    int CountOfMonster;
    List<GameObject> MonstersList = new List<GameObject>();

    //new Vector3(UnityEngine.Random.Range(-144, 177)
    // Start is called before the first frame update
    void Awake()
    {
        monsterSpawner = this;
        //for(int i  = 0; i < SpawnMobCount; i++)
        //{
        //    //SpawnMonster();
        //}

        //for (int i = 0; i < 3; i++)
        //{
        //    SpawnBoss();
        //}

        Paze1();
    }

    [ContextMenu("Paze 1")]
    public void Paze1()
    {
        ClearMonster();
        if (InPaze2) { DoorManager.doorManager.returnToPaze1(); }
        InPaze2 = false;

        SpawnMonster(1, 1, true, 3, true);
        SpawnMonster(1, 2, true, 3, true);
        SpawnMonster(1, 3, true, 3, true);

        //mob2 1,2,3 dis bot *2
        SpawnMonster(2, 1, true, 2, true);
        SpawnMonster(2, 2, true, 2, true);
        SpawnMonster(2, 3, true, 2, true);


        //mob2 1,2,3 dis top *4 3 3
        SpawnMonster(2, 1, false, 4, true);
        SpawnMonster(2, 2, false, 3, true);
        SpawnMonster(2, 3, false, 3, true);


        //mob3 1,2,3 dis bot *1
        SpawnMonster(3, 1, true, 1, true);
        SpawnMonster(3, 2, true, 1, true);
        SpawnMonster(3, 3, true, 1, true);

        //mob3 1,2,3 dis top *1
        SpawnMonster(3, 1, false, 1, true);
        SpawnMonster(3, 2, false, 1, true);
        SpawnMonster(3, 3, false, 1, true);

        //mob2 1,2,3 dis bot *2
        SpawnMonster(4, 1, true, 4, true);
        SpawnMonster(4, 2, true, 3, true);
        SpawnMonster(4, 3, true, 3, true);

        Invoke(nameof(MakeSortingMob), 0.1f);
    }

    [ContextMenu("Paze 2")]
    public void Paze2()
    {
        ClearMonster();
        InPaze2 = true;
        DoorManager.doorManager.Paze2Event();

        //mob3 1,2,3 dis bot *1
        SpawnMonster(3, 2, true, 2, false);
        SpawnMonster(3, 3, true, 2, false);

        //mob3 1,2,3 dis top *1
        SpawnMonster(3, 1, false, 3, false);
        SpawnMonster(3, 2, false, 1, false);
        SpawnMonster(3, 3, false, 1, false);

        //mob2 1,2,3 dis bot *2
        SpawnMonster(4, 2, true, 2, false);
        SpawnMonster(4, 3, true, 2, false);

        SpawnMonster(5, 1, true, 3, false);
        SpawnMonster(5, 2, true, 2, false);
        SpawnMonster(5, 3, true, 2, false);

        //SpawnBoss();

        Invoke(nameof(MakeSortingMob), 0.1f);
    }

    // Update is called once per frame
    void Update() 
    {
        CurTime += Time.deltaTime;
        if(CurTime > EachPazeTime)
        {
            CurTime = 0;
            if (InPaze2) Paze1();
            else Paze2();
        }

        if(GameObject.FindGameObjectsWithTag("Monster").Length == 0)
        {
            CurTime = 0;
            if (InPaze2) Paze1();
            else Paze2();
        }
    }

    void SpawnMonster(int MonsterNum, int ScreenNum, bool Bottom, int Count, bool OutsideRoom)
    {
        if (Count == 0) return;
        StartCoroutine(SpawnMonsterWithDelay(MonsterNum, ScreenNum, Bottom, Count, OutsideRoom));
    }

    IEnumerator SpawnMonsterWithDelay(int MonsterNum, int ScreenNum, bool Bottom, int Count, bool OutsideRoom)
    {
        Vector3 SpawnPos = Vector3.zero;

        switch (ScreenNum)
        {
            case 1:
                SpawnPos = new Vector3(UnityEngine.Random.Range(Screen1Left,Screen1Right), SpawnPos.y, SpawnPos.z);
                break;
            case 2:
                SpawnPos = new Vector3(UnityEngine.Random.Range(Screen2Left, Screen2Right), SpawnPos.y, SpawnPos.z);
                break;
            case 3:
                SpawnPos = new Vector3(UnityEngine.Random.Range(Screen3Left, Screen3Right), SpawnPos.y, SpawnPos.z);
                break;
        }

        if (Bottom)
        {
           
            if (ScreenNum == 1 && Bottom) { SpawnPos = new Vector3(SpawnPos.x, -25f, SpawnPos.z); }
            else SpawnPos = new Vector3(SpawnPos.x, BottomPos, SpawnPos.z);
        }
        else
        {
            if (OutsideRoom)
            {
                SpawnPos = new Vector3(SpawnPos.x, 9, SpawnPos.z);
            }
            else SpawnPos = new Vector3(SpawnPos.x, 9, SpawnPos.z);


        }

        if (OutsideRoom)
        {
            SpawnPos = new Vector3(SpawnPos.x, SpawnPos.y, WallFront);
        }
        else
        {
            SpawnPos = new Vector3(SpawnPos.x, SpawnPos.y, WallInside);
            
        }

        
        //Vector3 SpawnPos =
        var SpawnedMob = Instantiate(MonsterPrefeb, SpawnPos, Quaternion.identity);

        
        //int MonsterNum = (int)(UnityEngine.Random.value * 100) % 5;
        SpawnedMob.transform.parent = MonsterParent.transform;

        
        switch (MonsterNum)
        {
            case 1:
                SpawnedMob.transform.position = new Vector3(SpawnedMob.transform.position.x, UnityEngine.Random.Range(-22, 4), WallFront);
                SpawnedMob.GetComponent<MonsterScript>().MyType = MonsterScript.MonsterType.old1;
                break;
            case 2:
                SpawnedMob.GetComponent<MonsterScript>().MyType = MonsterScript.MonsterType.old2;
                break;
            case 3:
                SpawnedMob.GetComponent<MonsterScript>().MyType = MonsterScript.MonsterType.old4;
                break;
            case 4:
                SpawnedMob.GetComponent<MonsterScript>().MyType = MonsterScript.MonsterType.old5;
                break;
            case 5:
                SpawnedMob.GetComponent<MonsterScript>().MyType = MonsterScript.MonsterType.oldBoss;
                break;
        }

        if (Bottom == false)
        {
            SpawnedMob.GetComponent<MonsterScript>().UpsideDown();
            //SpawnedMob.transform.GetChild(0).rotation = Quaternion.Euler(SpawnedMob.transform.GetChild(0).rotation.x, SpawnedMob.transform.GetChild(0).rotation.y, 180);
        }

        if (OutsideRoom)
        {
            SpawnedMob.GetComponent<MonsterScript>().OutsideDoor = true;
        }
        else
        {

            SpawnedMob.GetComponent<MonsterScript>().OutsideDoor = false;
        }

        if (Bottom)
        {
            SpawnedMob.GetComponent<MonsterScript>().Bottom = true;
        }
        else
        {

            SpawnedMob.GetComponent<MonsterScript>().Bottom = false;
        }

       

        SpawnedMob.GetComponent<MonsterScript>().ScreenNum = ScreenNum;

        yield return new WaitForSeconds(0.8f);

        SpawnMonster(MonsterNum, ScreenNum, Bottom, --Count, OutsideRoom);
    }

    public void BossAppear(GameObject boss)
    {
        if (bossCount >= BossScaleList.Count) bossCount = 0;
        boss.transform.localScale = new Vector3(BossScaleList[bossCount], BossScaleList[bossCount], BossScaleList[bossCount]);
        bossCount++;
        
    }

    public void MakeSortingMob()
    {
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");

        // 몬스터들을 List에 추가
        List<GameObject> monsterList = new List<GameObject>(monsters);

        // 정렬 기준으로 사용할 Comparer 생성
        Comparer<GameObject> comparer = Comparer<GameObject>.Create((monster1, monster2) =>
        {
            // 스케일을 기준으로 비교
            float scale1 = monster1.transform.localScale.x;
            float scale2 = monster2.transform.localScale.x;

            // 오름차순으로 정렬
            return scale1.CompareTo(scale2);
        });

        // 몬스터들을 정렬
        monsterList.Sort(comparer);

        // 정렬된 몬스터들의 순서에 따라 order in layer 설정
        for (int i = 0; i < monsterList.Count; i++)
        {
            MeshRenderer TempRender;
            if(monsterList[i].TryGetComponent<MeshRenderer>(out TempRender))
            {
                TempRender.sortingOrder = -i;
            }

            monsterList[i].transform.position = new Vector3(monsterList[i].transform.position.x, monsterList[i].transform.position.y, monsterList[i].transform.position.z - (i * 0.02f));
        }
    }
    public void ClearMonster()
    {
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("MonsterPar");
        for(int i = 0; i < monsters.Length; i++)
        {
            Destroy(monsters[i]);
        }
    }


    public void SpawnNewMonster(int MonsterNum, int ScreenNum, bool Bottom, int Count, bool OutsideRoom)
    {
        newSpawn =  StartCoroutine(NewSpawn( MonsterNum,  ScreenNum,  Bottom,  Count,  OutsideRoom));
    }
    IEnumerator NewSpawn(int MonsterNum, int ScreenNum, bool Bottom, int Count, bool OutsideRoom)
    {
        yield return new WaitForSeconds(3f);
        SpawnMonster( MonsterNum,  ScreenNum,  Bottom,  Count,  OutsideRoom);
    }

    }
