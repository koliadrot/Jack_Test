using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class DescriptionInteractive : InteractiveObject
{
    #region Field Declarations
    [SerializeField] private TextMeshProUGUI descriptionText;
    #endregion

    #region Subject Implementation

    #region Subscribe Methods
    protected override void SubscribeOnAction(bool status)//Subscribe or unsubscribe at/from event
    {
        if (status)
            InteractiveAction += OnDescriptionAction;
        else
            InteractiveAction -= OnDescriptionAction;
    }
    #endregion

    #region Actions
    private void OnDescriptionAction()//Main action of event
    {
        descriptionText.text = "Hello! I'm " + name.Replace("(", " ").Split()[0];
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
