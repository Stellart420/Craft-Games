using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlatformGenerator : MonoBehaviour
{
    public static PlatformGenerator Instance;

    [SerializeField] GameObject platformPrefab;
    [SerializeField] Transform startingPoint;
    [SerializeField] Transform startingPlatform;
    [SerializeField] int startingPlatformCount;

    [SerializeField] Transform platformContainer;
    [SerializeField] LevelDifficult difficult;

    int difficultSize = 0;
    Transform currentPlatform;

    int nextPlatformDirection;

    List<GameObject> levelPlatforms = new List<GameObject>();

    Vector3 startingPlatformScale = Vector3.one;

    public UnityAction<int, Transform> PlatformNumberAction;

    delegate void CreatePlatformDelegate();
    CreatePlatformDelegate create;

    int platformNumber = 0;
    void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    void Start()
    {
        CheckDifficult();
        GameController.Instance.RestartAction += Reset;
        startingPlatformScale = startingPlatform.localScale;
        levelPlatforms.Add(startingPlatform.gameObject);
        GenerateStartingPlatform();
    }

    public void Reset()
    {
        CheckDifficult();
        levelPlatforms.ForEach(platform => Destroy(platform));
        levelPlatforms.Clear();
        GameObject start_platform = Instantiate(platformPrefab, Vector3.zero, Quaternion.identity, platformContainer);
        start_platform.transform.localScale = startingPlatformScale;
        levelPlatforms.Add(start_platform);
        platformNumber = 0;
        GenerateStartingPlatform();
    }

    void GenerateStartingPlatform()
    {
        currentPlatform = startingPoint;
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
        if (nextPlatformDirection == 0)
        {
            currentPlatform = Instantiate(platformPrefab, currentPlatform.position + Vector3.right * 2, Quaternion.identity, platformContainer).transform;
        }
        else
        {
            currentPlatform = Instantiate(platformPrefab, currentPlatform.position + Vector3.forward * 2, Quaternion.identity, platformContainer).transform;
        }
        currentPlatform.name = $"Platform_{platformNumber + 1}_0";
        levelPlatforms.Add(currentPlatform.gameObject);
        GameObject platform;
        for (int i = difficultSize; i > 0; i--)
        {
            if (nextPlatformDirection == 0)
            {
                platform = Instantiate(platformPrefab, currentPlatform.position + Vector3.back * 2 * i, Quaternion.identity, platformContainer);
            }
            else
            {
                platform = Instantiate(platformPrefab, currentPlatform.position + Vector3.left * 2 * i, Quaternion.identity, platformContainer);
            }
            platform.name = $"Platform_{platformNumber + 1}_{i}";
            levelPlatforms.Add(platform);
        }
        PlatformNumberAction(levelPlatforms.Count, currentPlatform.transform);
        platformNumber++;
    }

    void CheckDifficult()
    {
        create = null;
        switch (difficult)
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
            default:return;
        }
    }
}

public enum LevelDifficult
{
    Easy,
    Normal,
    Hard,
}
