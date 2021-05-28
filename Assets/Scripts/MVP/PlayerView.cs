using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//MVP system
//View is block which to show all visualization(Animation,Text,Music etc.)
public class PlayerView : MonoBehaviour
{
    #region Field Declarations
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI experienceText;
    [SerializeField] private TextMeshProUGUI healthText;
    #endregion

    #region Subject Implementation
    public void Death()//Activate, when player have zero or less health points
    {
        GameSceneController.Instance.EndGameNotifyObservers();
        gameObject.SetActive(false);
    }
    public void ChangedHealth(int health)//Show health point
    {
        healthText.text = health.ToString();
    }

    public void ChangedExperience(int exp)//Show experience point
    {
        experienceText.text = exp.ToString();
    }
    #endregion
}
