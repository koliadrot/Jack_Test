using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

//MVP system
//Model is block which to have main gameplay logic of player
public class PlayerModel
{
    #region Field Declarations
    public event Action DeathAction = () => { };
    public event Action<int> ChangeHealthAction = (int health) => { };
    public event Action<int> ChangeExperienceAction = (int mana) => { };

    //Player parameters
    private int experience;
    private int health = 10;
    private float speedMovement = 10;
    private float speedRotation = 8;
    private LayerMask mask = LayerMask.GetMask("Ground", "Ignore Raycast");

    //Cashe parameters
    private Ray ray;
    private RaycastHit hit;
    private Vector3 targetPostion;
    private Vector3 targetDirection;
    private const float yPosition = 0.5f;
    private GameObject lastInterface;

    public PlayerModel() { }
    public PlayerModel(PlayerData playerData)
    {
        experience = playerData.experience;
        health = playerData.health;
        speedMovement = playerData.speedMovement;
        speedRotation = playerData.speedRotation;
        mask = playerData.mask;
    }
    public int GetExperiencePoint() => experience;//Get player experience
    public int GetHealthPoint() => health;//Get player health
    public float GetSpeedMovement() => speedMovement;//Get player speed movement
    public float GetSpeedRotation() => speedRotation;//Get player speed rotation
    public LayerMask GetMask() => mask;//Get player mask
    #endregion

    #region Subject Implementation
    public void SetNewHealth(int damage)//Change player health
    {
        health -= damage;
        if (health > 0)
            ChangeHealthAction(health);
        else
            DeathAction();
    }
    public void SetExperiencePoint(int exp)//Change player experience
    {
        experience += exp;
        ChangeExperienceAction(experience);
    }
    public void OnMouseClick(Camera mainCamera)//Get information and to operation different actions by raycast
    {
        if (Input.GetMouseButtonDown(0))
        {
            ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask))
            {
                if (hit.collider.CompareTag("Ground"))//Get mouse position on plane 
                {
                    targetPostion = new Vector3(hit.point.x, yPosition, hit.point.z);
                }
                if (hit.collider.CompareTag("Interactive"))//Activate main event of interactive objects
                {
                    Interaction();
                }
            }
        }
        else if (Input.GetMouseButtonDown(1))//Switch off last interface of interactive object
        {
            if (lastInterface != null)
                lastInterface.SetActive(false);
        }
    }
    private void Interaction()//Interact with other different objects
    {
        if (lastInterface != hit.collider.GetComponent<InteractiveObject>().interfaceObject.activeSelf)
        {
            lastInterface.SetActive(false);
        }
        if (!hit.collider.GetComponent<InteractiveObject>().interfaceObject.activeSelf)
        {
            lastInterface = hit.collider.GetComponent<InteractiveObject>().interfaceObject;
            hit.collider.GetComponent<InteractiveObject>().interfaceObject.SetActive(true);
        }
        else
        {
            hit.collider.GetComponent<InteractiveObject>().OnAcionInteraction();
        }
    }
    public void Movement(Transform player)//Movement player
    {
        if (player == null) return;
        targetDirection = targetPostion - player.position;
        if (Vector3.SqrMagnitude(targetDirection) > 0.1f && targetPostion != Vector3.zero)//Movement player to last point of mouse
        {
            player.rotation = Quaternion.Slerp(player.rotation, Quaternion.LookRotation(targetDirection), speedRotation * Time.deltaTime);//Rotation with Slerp
            player.position = Vector3.MoveTowards(player.position, targetPostion, speedMovement * Time.deltaTime);
        }
    }
    #endregion
}
