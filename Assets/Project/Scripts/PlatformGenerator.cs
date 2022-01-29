using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    public static PlatformGenerator Instance;

    [SerializeField] GameObject platformPrefab;
    [SerializeField] Transform startingPlatform;
    [SerializeField] int startingPlatformCount;

    [SerializeField] Transform platformContainer;

    Transform currentPlatform;

    int nextPlatformDirection;

    List<GameObject> levelPlatforms = new List<GameObject>();

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
        GenerateStartingPlatform();
    }

    public void Reset()
    {
        levelPlatforms.ForEach(platform => Destroy(platform));
        levelPlatforms.Clear();
        GenerateStartingPlatform();
    }

    void GenerateStartingPlatform()
    {
        currentPlatform = startingPlatform;
        for (int i = 0; i < startingPlatformCount; i++)
        {
            NextPlatform();
        }
    }

    public void NextPlatform()
    {
        nextPlatformDirection = Random.Range(0, 2);
        if (nextPlatformDirection == 0)
        {
            currentPlatform = Instantiate(platformPrefab, currentPlatform.position + Vector3.right * 2, Quaternion.identity, platformContainer).transform;
        }
        else
        {
            currentPlatform = Instantiate(platformPrefab, currentPlatform.position + Vector3.forward * 2, Quaternion.identity, platformContainer).transform;
        }
        levelPlatforms.Add(currentPlatform.gameObject);
    }
}
