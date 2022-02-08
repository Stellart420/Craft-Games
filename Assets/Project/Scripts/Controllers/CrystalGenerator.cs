using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalGenerator : MonoBehaviour
{
    #region Singleton
    public static CrystalGenerator Instance;
    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    #endregion

    [SerializeField] string poolTag = "Crystal";
    [SerializeField] GameObject crystalPrefab;
    [SerializeField] CrystalSpawnRules spawnRules;
    [SerializeField] Transform crystalContainer;
    int counter = 5;
    bool isSpawn;

    delegate void SpawnDelegate(Transform pos);
    SpawnDelegate SpawnAction;

    List<Crystal> crystals = new List<Crystal>();

    ObjectPool objectPooler;


    private void Start()
    {
        objectPooler = ObjectPool.Instance;

        CheckCrystalSpawnType();
        GameController.Instance.RestartAction += Reset;
        PlatformGenerator.Instance.PlatformNumberAction += SetCrystal;
    }

    private void Reset()
    {
        crystals.ForEach(crystal => crystal.Reset());
        crystals.Clear();
        orderCounter = 5;
        counter = 5;
        CheckCrystalSpawnType();
    }

    void SetCrystal(int _counter, Transform _position)
    {
        //_counter дл€ проверки и создани€ кристала на дорожках шириной 3 и 2 использовать не стал 
        // не увидел по€сн€ющего пункта в “«
        SpawnAction(_position);
    }

    void CheckCrystalSpawnType()
    {
        SpawnAction = null;
        switch (spawnRules)
        {
            case CrystalSpawnRules.InOrder:
                SpawnAction += SpawnInOrder;
                return;
            case CrystalSpawnRules.Random:
                SpawnAction += SpawnRandom;
                return;
            default:
                SpawnAction += SpawnRandom;
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
        var crystal = objectPooler.SpawnFromPool(poolTag, _position.position + Vector3.up, Quaternion.identity, crystalContainer).GetComponent<Crystal>();
        crystal.Init(() => GameController.Instance.AddScore(1));
        isSpawn = true;
        crystals.Add(crystal);
    }
}

public enum CrystalSpawnRules
{
    Random,
    InOrder,
}
