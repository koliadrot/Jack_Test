using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneController : Singleton<GameSceneController>
{
    #region Field Declarations
    private List<IObserverable> observers = new List<IObserverable>();

    [Header("Environment")]
    [SerializeField] private Transform ground;
    public float GroundX => Mathf.Pow(ground.localScale.x, 2f) * 0.9f;
    public float GroundZ => Mathf.Pow(ground.localScale.z, 2f) * 0.9f;

    [Header("Pool Objects")]
    [SerializeField] private List<Pools> pools = new List<Pools>();

    [System.Serializable]
    private class Pools //Pools Objects divided on tags
    {
        public string name;
        public List<Pool> pool;

        [System.Serializable]
        public class Pool
        {
            public string name;
            public GameObject prefab;
            public int size = 1;
            public SpawnTypePools typeSpawn = SpawnTypePools.Randomly;
            public InteractiveData interData;
        }
    }
    private enum SpawnTypePools
    {
        Randomly,
        Definitely
    }
    #endregion

    #region Startup
    private void Start()
    {
        CreatePool();
    }
    private void CreatePool()//Create pool spawn objects
    {
        foreach (var objectPool in pools)
        {
            foreach (var objectList in objectPool.pool)
            {
                for (int k = 0; k < objectList.size; k++)
                {
                    GameObject obj = Instantiate(objectList.prefab);
                    switch (objectList.typeSpawn)//Choice necessary type of spawn
                    {
                        case SpawnTypePools.Randomly:
                            obj.transform.position = new Vector3(Random.Range(-GroundX, GroundX), 1f, Random.Range(-GroundZ, GroundZ));
                            break;

                        case SpawnTypePools.Definitely:
                            if (objectList.interData != null)
                                obj.transform.position = objectList.interData.position;
                            else
                                goto case SpawnTypePools.Randomly;
                            break;
                    }
                    obj.transform.SetParent(this.transform); //Set as children of Spawn Manager
                }
            }
        }
    }
    #endregion

    #region Subject Implementation

    #region Observer
    public void AddObserver(IObserverable observer)//Add observable subscriber
    {
        observers.Add(observer);
    }

    public void RemoveObserver(IObserverable observer)//Remove observable subscriber
    {
        observers.Remove(observer);
    }

    public void EndGameNotifyObservers()//Send notification about start action observables subscribers
    {
        foreach (IObserverable observer in observers)
        {
            observer.Notify();
        }
    }
    #endregion

    #region Application
    private void OnApplicationQuit()
    {
        PlayerPrefs.DeleteAll();
    }
    #endregion

    #endregion
}

