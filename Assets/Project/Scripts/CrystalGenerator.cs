using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalGenerator : MonoBehaviour
{
    public static CrystalGenerator Instance;

    [SerializeField] GameObject crystalPrefab;
    [SerializeField] CrystalSpawnRules spawnRules;

    int counter = 5;
    bool isSpawn;
    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        GameController.Instance.RestartAction += Reset;
        PlatformGenerator.Instance.PlatformNumberAction += SetCrystal;
    }

    private void Reset()
    {
        orderCounter = 5;
        counter = 5;
    }

    void SetCrystal(int _number, Transform _position)
    {
        switch (spawnRules)
        {
            case CrystalSpawnRules.InOrder:
                SpawnInOrder(_position);
                return;
            case CrystalSpawnRules.Random:
                SpawnRandom(_position);
                return;
            default:
                SpawnRandom(_position);
                return;
        }
    }

    int orderCounter = 5;

    void SpawnInOrder(Transform _position)
    {
        if (counter == orderCounter && !isSpawn)
        {
            Spawn(_position);
            isSpawn = true;
            orderCounter--;
        }

        counter--;

        if (counter == 0)
        {
            isSpawn = false;
            counter = 5;

            if (orderCounter == 0)
            {
                orderCounter = 5;
            }
        }
    }

    void SpawnRandom(Transform _position)
    {
        bool is_spawn = Random.Range(0, 2) == 0 ? false : true;

        if ((is_spawn && !isSpawn) || (counter == 0 && !isSpawn))
            Spawn(_position);
        
        if (counter == 0)
        {
            isSpawn = false;
            counter = 5;
        }

        counter--;
    }

    void Spawn(Transform _position)
    {
        var crystal = Instantiate(crystalPrefab, _position.position + Vector3.up, Quaternion.identity);
        crystal.transform.SetParent(_position);
        isSpawn = true;
    }
}

public enum CrystalSpawnRules
{
    Random,
    InOrder,
}
