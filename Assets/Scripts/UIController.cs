using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour, IObserverable
{
    #region Field Declarations
    [SerializeField] private Button restartButton;
    [SerializeField] private GameObject gameOverText;
    #endregion

    #region Startup
    private void Start()
    {
        Subscribe();
    }
    #endregion

    #region Subject Implementation
    private void Subscribe()
    {
        restartButton.onClick.AddListener(Restart);//Add method on button
        GameSceneController.Instance.AddObserver(this);//Subscribe on observer
    }
    private void GameOver()
    {
        restartButton.gameObject.SetActive(true);
        gameOverText.SetActive(true);
    }
    private void Restart()//Restart scene and delete all save data
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("Local 1");
    }

    #region Observer Action
    public void Notify()
    {
        GameOver();
    }
    #endregion
    
    #endregion
}
