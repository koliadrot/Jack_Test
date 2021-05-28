using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneController : Singleton<GameSceneController>
{
    #region Field Declarations
    private List<IObserverable> observers = new List<IObserverable>();

    [Header("Player")]
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private PlayerData startPlayerData;
    private Transform playerTransform;
    private PlayerPresenter playerPresenter;
    private PlayerModel playerModel;

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
        CreatePlayer();
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
    private void CreatePlayer()//Create player with setting data
    {
        playerModel = new PlayerModel(startPlayerData);
        playerTransform = Instantiate(playerPrefab, startPlayerData.currentPosition, startPlayerData.currentRotation).transform;
        PlayerView playerView = playerTransform.GetComponent<PlayerView>();
        playerPresenter = new PlayerPresenter(playerView, playerModel);
        OnSetHealthChange(0);
        OnSetExperienceChange(0);
        if (startPlayerData.scene != SceneManager.GetActiveScene().name)
            LoadScene(startPlayerData.scene);
    }
    #endregion

    #region Subject Implementation

    #region Logic Update
    private void Update()
    {
        playerModel.OnMouseClick(mainCamera);
        playerModel.Movement(playerTransform);
    }
    #endregion

    #region Gameplay
    public void Teleport(string scene)//Load current player position and another scene
    {
        if (SceneManager.GetActiveScene().name == "Local 2")
        {
            SavePlayerData("Local 1");
            startPlayerData.currentPosition = startPlayerData.localPosition[0];
        }
        else
        {
            SavePlayerData("Local 2");
            startPlayerData.currentPosition = startPlayerData.localPosition[1];
        }
        LoadScene(scene);
    }
    #endregion

    #region Player Methods
    public void OnSetHealthChange(int damage)//Change player health
    {
        playerModel.SetNewHealth(damage);
    }
    public void OnSetExperienceChange(int exp)//Change player experience
    {
        playerModel.SetExperiencePoint(exp);
    }
    public int OnGetExperiencePoint()//Get player experience
    {
        return playerModel.GetExperiencePoint();
    }
    #endregion

    #region Load and Save
    private void SavePlayerData(string sceneName)//Save current player data
    {
        startPlayerData.experience = playerModel.GetExperiencePoint();
        startPlayerData.health = playerModel.GetHealthPoint();
        startPlayerData.speedMovement = playerModel.GetSpeedMovement();
        startPlayerData.speedRotation = playerModel.GetSpeedRotation();
        startPlayerData.mask = playerModel.GetMask();
        startPlayerData.currentPosition = playerTransform.position;
        startPlayerData.currentRotation = playerTransform.rotation;
        startPlayerData.scene = sceneName;
    }
    private void LoadScene(string scene)//Load other scene
    {
        SceneManager.LoadScene(scene);
    }
    #endregion

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
        SavePlayerData(SceneManager.GetActiveScene().name);
    }
    #endregion

    #endregion
}

