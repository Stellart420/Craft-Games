using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlatformGenerator : MonoBehaviour
{
    #region Singleton
    public static PlatformGenerator Instance;
    void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
    #endregion

    [SerializeField] int startingPlatformCount = 50;
    [SerializeField] Transform startPoint;
    [SerializeField] Transform startPlatform;
    [SerializeField] string poolTag = "Platform";
    [SerializeField] Transform platformContainer;

    int difficultSize = 0;
    Transform lastPlatform;
    Platform currentPlatform;

    int nextPlatformDirection;

    List<Platform> levelPlatforms = new List<Platform>();

    Vector3 startingPlatformScale = Vector3.one;

    ObjectPool objectPooler;
    GameController gameController;
    public UnityAction<int, Transform> PlatformNumberAction;

    delegate void CreatePlatformDelegate();
    CreatePlatformDelegate create;

    int platformNumber = 0;

    void Start()
    {
        objectPooler = ObjectPool.Instance;
        gameController = GameController.Instance;
        CheckDifficult();
        GameController.Instance.RestartAction += Reset;
        startingPlatformScale = startPlatform.transform.localScale;
        var platform = startPlatform.GetComponent<Platform>();
        platform.Init(() => NextPlatform());
        levelPlatforms.Add(platform);
        GenerateStartingPlatform();
    }

    public void Reset()
    {
        CheckDifficult();
        levelPlatforms.ForEach(platform => platform.Reset());
        levelPlatforms.Clear();
        GameObject start_GO = objectPooler.SpawnFromPool(poolTag, platformContainer);
        start_GO.transform.localScale = startingPlatformScale;
        levelPlatforms.Add(start_GO.GetComponent<Platform>());
        platformNumber = 0;
        GenerateStartingPlatform();
    }

    void GenerateStartingPlatform()
    {
        lastPlatform = startPoint;
        for (int i = 0; i < startingPlatformCount; i++)
        {
            NextPlatform();
        }
    }

    public void NextPlatform()
    {
        if (create != null)
            create();
    }


    //todo Еще один вариант создания платворм
    //void CreatePlatform(int _size)
    //{
    //    nextPlatformDirection = Random.Range(0, 2);
    //    if (nextPlatformDirection == 0)
    //    {
    //        currentPlatform = Instantiate(platformPrefab, currentPlatform.position + Vector3.right * 2, Quaternion.identity, platformContainer).transform;
    //        currentPlatform.localScale = currentPlatform.localScale + Vector3.forward * _size;
    //    }
    //    else
    //    {
    //        currentPlatform = Instantiate(platformPrefab, currentPlatform.position + Vector3.forward * 2, Quaternion.identity, platformContainer).transform;
    //        currentPlatform.localScale = currentPlatform.localScale + Vector3.right * _size;

    //    }
    //    currentPlatform.name = $"Platform_{platformNumber + 1}_{_size}";
    //    levelPlatforms.Add(currentPlatform.gameObject);
    //    PlatformNumberAction(platformNumber, currentPlatform.transform);
    //    platformNumber++;
    //}

    void CreatePlatform()
    {
        nextPlatformDirection = UnityEngine.Random.Range(0, 2);
        GameObject platform_GO;

        if (nextPlatformDirection == 0)
        {
            platform_GO = objectPooler.SpawnFromPool(poolTag, lastPlatform.position + Vector3.right * 2, Quaternion.identity, platformContainer);
            currentPlatform = platform_GO.GetComponent<Platform>();
        }
        else
        {
            platform_GO = objectPooler.SpawnFromPool(poolTag, lastPlatform.position + Vector3.forward * 2, Quaternion.identity, platformContainer);
            currentPlatform = platform_GO.GetComponent<Platform>();
        }

        lastPlatform = platform_GO.transform;
        currentPlatform.Init(NextPlatform);
        levelPlatforms.Add(currentPlatform);

        GameObject platform_GO_sup;
        for (int i = difficultSize; i > 0; i--)
        {
            if (nextPlatformDirection == 0)
            {
                platform_GO_sup = objectPooler.SpawnFromPool(poolTag, currentPlatform.transform.position + Vector3.back * 2 * i, Quaternion.identity, platformContainer);
            }
            else
            {
                platform_GO_sup = objectPooler.SpawnFromPool(poolTag, currentPlatform.transform.position + Vector3.left * 2 * i, Quaternion.identity, platformContainer);
            }
            levelPlatforms.Add(platform_GO_sup.GetComponent<Platform>());
        }

        PlatformNumberAction(levelPlatforms.Count, currentPlatform.transform);
        platformNumber++;
    }

    void CheckDifficult()
    {
        create = null;
        switch (gameController.Difficult)
        {
            case LevelDifficult.Easy:
                difficultSize = 2;
                create += CreatePlatform;
                return;
            case LevelDifficult.Normal:
                difficultSize = 1;
                create += CreatePlatform;
                return;
            case LevelDifficult.Hard:
                difficultSize = 0;
                create += CreatePlatform;
                return;
            default: return;
        }
    }
}
