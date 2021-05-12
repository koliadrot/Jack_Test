using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class HitInteractive : InteractiveObject
{
    #region Field Declarations
    public event Action InteractiveAction = () => { };

    [SerializeField] private bool hit = true;
    [SerializeField] private Toggle hitBox;
    [SerializeField] private int hitValue = 2;
    [SerializeField] private TMP_InputField hitField;

    public int HitValue
    {
        get => hitValue;
        set
        {
            hitValue = value;
            hitField.text = hitValue.ToString();
        }
    }
    public bool Hit
    {
        get => hit;
        set
        {
            hit = value;
            SubscribeOnHit(Hit);
        }
    }
    #endregion

    #region Startup
    protected override void Start()
    {
        base.Start();
        hitField.text = hitValue.ToString();
        hitBox.isOn = hit;
        interfaceObject.transform.GetChild(3).gameObject.SetActive(true);
        hitField.onValueChanged.AddListener(OnHitFieldChanged);//Add method on InputField--->
        hitField.onEndEdit.AddListener(OnHitFieldChanged);//<---
        hitBox.onValueChanged.AddListener((bool status) => OnHitChanged());//Add method on checkBox
    }
    #endregion

    #region Subject Implementation

    #region Subscribe Methods
    private void OnHitChanged()//Method for switch on or switch off in the game
    {
        Hit = hitBox.isOn;
    }
    private void SubscribeOnHit(bool status)//Subscribe or unsubscribe at/from event
    {
        if (status)
            InteractiveAction += OnHitAction;
        else
            InteractiveAction -= OnHitAction;
    }
    #endregion

    #region Subscribe Methods
    private void OnHitAction()//Main action of event
    {
        player.Health -= HitValue;
    }
    public override void OnAcionInteraction()//Method for call outside
    {
        if (player != null)
            InteractiveAction();
    }
    #endregion

    #region UI
    private void OnHitFieldChanged(string hitValueIn)//Set Hit value
    {
        if (int.Parse(hitValueIn) > 0)
            HitValue = int.Parse(hitValueIn);
        else
            HitValue = 0;
    }
    #endregion

    #region Collisions
    protected override void OnTriggerEnter(Collider other)//Activate, when object to come in zone
    {
        base.OnTriggerEnter(other);
        if (other.CompareTag("Player"))
            SubscribeOnHit(Hit);//Subscribe actions at event
    }
    protected override void OnTriggerExit(Collider other)//Activate, when object to come out zone
    {
        base.OnTriggerExit(other);
        if (other.CompareTag("Player"))
            SubscribeOnHit(false);//Unsubscribe actions from event
    }
    #endregion

    #region Observer Action
    public override void Notify()
    {
        base.Notify();
    }
    #endregion

    #endregion
}
