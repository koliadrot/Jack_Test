using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour, IObserverable
{
    #region Field Declarations
    [Header("Player Parameters")]
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Transform player;
    [SerializeField] private int experience;
    [SerializeField] private int health;
    [SerializeField] private float speedMovement = 5f;
    [SerializeField] private float speedRotation = 1f;
    [SerializeField] private LayerMask mask;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI expText;
    [SerializeField] private TextMeshProUGUI healthText;

    private Vector3 targetPostion;
    private Vector3 targetDirection;
    private const float yPosition = 0.5f;
    private GameObject lastInterface;

    public int Experience
    {
        get => experience;
        set
        {
            experience = value;
            expText.text = experience.ToString();
        }
    }
    public int Health
    {
        get => health;
        set
        {
            health = value;
            healthText.text = health.ToString();
            if (health <= 0)
                GameSceneController.Instance.EndGameNotifyObservers();//GameOver
        }
    }
    #endregion

    #region Startup
    private void Start()
    {
        expText.text = experience.ToString();
        healthText.text = health.ToString();
        GameSceneController.Instance.AddObserver(this);//Subscribe on observer
        LoadData(SceneManager.GetActiveScene().name);//Load player postion
    }
    #endregion

    #region Subject Implementation

    #region Load and Save data
    private void SaveData(string sceneName)//Save on player position
    {
        PlayerPrefs.SetString(name + sceneName, JsonUtility.ToJson(transform.position, true));
        PlayerPrefs.Save();
    }
    private void LoadData(string sceneName)//Load on player position
    {
        if (PlayerPrefs.HasKey(name + sceneName))
            transform.position = JsonUtility.FromJson<Vector3>(PlayerPrefs.GetString(name + sceneName));
    }
    #endregion

    #region Logic Update
    private void Update()
    {
        Movement();
    }
    #endregion

    #region GamePlay
    private void Movement()//Movement player and interactive with objects
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask))
            {
                if (hit.collider.CompareTag("Ground"))//Get mouse position on plane 
                {
                    targetPostion = new Vector3(hit.point.x, yPosition, hit.point.z);
                }
                if (hit.collider.CompareTag("Interactive"))//Activate main event of interactive objects
                {
                    if (lastInterface != hit.collider.GetComponent<InteractiveObject>().interfaceObject.activeSelf)
                        lastInterface.SetActive(false);
                    if (!hit.collider.GetComponent<InteractiveObject>().interfaceObject.activeSelf)
                    {
                        lastInterface = hit.collider.GetComponent<InteractiveObject>().interfaceObject;
                        hit.collider.GetComponent<InteractiveObject>().interfaceObject.SetActive(true);
                    }
                    else
                        hit.collider.GetComponent<InteractiveObject>().OnAcionInteraction();
                }
            }
        }
        if (Input.GetMouseButtonDown(1))//Switch off last interface of interactive object
        {
            if (lastInterface != null)
                lastInterface.SetActive(false);
        }
        targetDirection = targetPostion - player.position;
        if (Vector3.SqrMagnitude(targetDirection) > 0.1f && targetPostion != Vector3.zero)//Movement player to last point of mouse
        {
            player.rotation = Quaternion.Slerp(player.rotation, Quaternion.LookRotation(targetDirection), speedRotation * Time.deltaTime);//Rotation with Slerp
            player.position = Vector3.MoveTowards(player.position, targetPostion, speedMovement * Time.deltaTime);
        }
    }
    private void GameOver()
    {
        Destroy(gameObject);
    }
    private void LoadScene()
    {
        if (SceneManager.GetActiveScene().name == "Local 1")
            SceneManager.LoadScene("Local 2");
        else
            SceneManager.LoadScene("Local 1");
    }
    #endregion

    #region Collisions
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Teleport"))
        {
            SaveData(SceneManager.GetActiveScene().name);
            LoadScene();
        }
    }
    #endregion

    #region Observer Action
    public void Notify()
    {
        GameOver();
    }
    #endregion

    #endregion
}
