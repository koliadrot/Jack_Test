using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class HitInteractive : InteractiveObject
{
    #region Field Declarations
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
    #endregion

    #region Startup
    protected override void Start()
    {
        base.Start();
        hitField.text = hitValue.ToString();
        hitField.onValueChanged.AddListener(OnHitFieldChanged);//Add method on InputField--->
        hitField.onEndEdit.AddListener(OnHitFieldChanged);//<---
    }
    #endregion

    #region Subject Implementation

    #region Subscribe Methods    
    protected override void SubscribeOnAction(bool status)//Subscribe or unsubscribe at/from event
    {
        if (status)
            InteractiveAction += OnHitAction;
        else
            InteractiveAction -= OnHitAction;
    }
    #endregion

    #region Actions
    private void OnHitAction()//Main action of event
    {
        GameSceneController.Instance.OnSetHealthChange(HitValue);
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

    #region Observer Action
    public override void Notify()
    {
        base.Notify();
    }
    #endregion

    #endregion
}
