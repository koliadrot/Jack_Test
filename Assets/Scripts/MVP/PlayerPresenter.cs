using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//MVP system
//Presenter is block which to connect blocks "view" and "model"
public class PlayerPresenter
{
    private PlayerView playerView;
    private PlayerModel playerModel;

    public PlayerPresenter(PlayerView playerView, PlayerModel playerModel)
    {
        this.playerView = playerView;
        this.playerModel = playerModel;
        Subscribe();
    }

    private void Subscribe()
    {
        playerModel.DeathAction += Death;
        playerModel.ChangeHealthAction += ChangeHealth;
        playerModel.ChangeExperienceAction += ChangeExperience;
    }

    private void Unsubscribe()
    {
        playerModel.DeathAction -= Death;
        playerModel.ChangeHealthAction -= ChangeHealth;
        playerModel.ChangeExperienceAction -= ChangeExperience;
    }

    private void Death()
    {
        playerView.Death();
        Unsubscribe();
    }

    private void ChangeHealth(int health)
    {
        playerView.ChangedHealth(health);
    }

    private void ChangeExperience(int exp)
    {
        playerView.ChangedExperience(exp);
    }
}
