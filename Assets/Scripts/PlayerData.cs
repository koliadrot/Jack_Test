using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "New Player Data", menuName = "Player Data")]
public class PlayerData : ScriptableObject
{
    public int experience;
    public int health;
    public float speedMovement;
    public float speedRotation;
    public LayerMask mask;
    public Vector3 currentPosition = Vector3.zero;
    public Quaternion currentRotation = Quaternion.identity;
    public string scene = "Local 1";
    public Vector3[] localPosition;
}
