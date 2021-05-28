using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ExperienceInteractive : InteractiveObject
{
    #region Field Declarations
    [SerializeField] private int exp = 1;
    [SerializeField] private TMP_InputField expField;

    public int Exp
    {
        get => exp;
        set
        {
            exp = value;
            expField.text = exp.ToString();
        }
    }
    #endregion

    #region Startup
    protected override void Start()
    {
        base.Start();
        expField.text = exp.ToString();
        expField.onValueChanged.AddListener(OnExperienceFieldChanged);//Add method on InputField--->
        expField.onEndEdit.AddListener(OnExperienceFieldChanged);//<---
    }
    #endregion

    #region Subject Implementation

    #region Subscribe Methods
    protected override void SubscribeOnAction(bool status)//Subscribe or unsubscribe at/from event
    {
        if (status)
            InteractiveAction += OnExperienceAction;
        else
            InteractiveAction -= OnExperienceAction;
    }
    #endregion

    #region Actions
    private void OnExperienceAction()//Method for call outside
    {
        GameSceneController.Instance.OnSetExperienceChange(Exp);
    }
    #endregion

    #region UI
    public void OnExperienceFieldChanged(string expIn)//Set Experience value
    {
        if (int.Parse(expIn) > 0)
            Exp = int.Parse(expIn);
        else
            Exp = 0;
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
