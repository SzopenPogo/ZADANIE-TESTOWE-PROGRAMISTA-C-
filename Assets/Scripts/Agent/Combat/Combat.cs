using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    //Consts
    private const float ProtectionTime = 5f;

    //Data
    
    //Enemy Data
    public Combat ActiveEnemy { get; private set; }
    public List<Combat> WaitingEnemies { get; private set; } = new();
    public List<Combat> EnemiesOnProtection { get; private set; } = new();

    //Actions
    public Action OnWaitingEvent;
    public Action OnAttackEvent;

    private void AddWaitingEnemy(Combat newEnemy)
    {
        //If New Enemy is on protection
        if (EnemiesOnProtection.Contains(newEnemy))
            return;

        //If New Enemy already is in Waiting Enemies
        if (WaitingEnemies.Contains(newEnemy))
            return;

        WaitingEnemies.Add(newEnemy);
        newEnemy.OnWaitingEvent?.Invoke();
    }

    private void RemoveWaitingEnemy(Combat enemy)
    {
        if (!WaitingEnemies.Contains(enemy))
            return;

        WaitingEnemies.Remove(enemy);
    }

    public void AddEnemy(Combat newEnemy)
    {
        AddWaitingEnemy(newEnemy);

        //Try to set This Agent to New Enemy waiting list
        if(!newEnemy.WaitingEnemies.Contains(this))
            newEnemy.AddWaitingEnemy(this);
    }

    private void ProtectEnemy(Combat attackedEnemy)
    {
        EnemiesOnProtection.Add(attackedEnemy);
        StartCoroutine(RemoveProtection(attackedEnemy));
    }

    private IEnumerator RemoveProtection(Combat attackedEnemy)
    {
        yield return new WaitForSeconds(ProtectionTime);
        EnemiesOnProtection.Remove(attackedEnemy);
    }

    private bool TrySetActiveEnemy(Combat enemy)
    {
        if(ActiveEnemy != null)
            return false;

        ActiveEnemy = enemy;
        return true;
    }

    private void AttackEnemy(Combat enemy)
    {
        //Try set enemy as an Active Enemy
        if (!TrySetActiveEnemy(enemy))
            return;
        if (!enemy.TrySetActiveEnemy(this))
            return;

        //Invoke Events
        OnAttackEvent?.Invoke();
        enemy.OnAttackEvent.Invoke();

        //Set protection
        ProtectEnemy(enemy);
        enemy.ProtectEnemy(this);

        //Remove from waiting list
        RemoveWaitingEnemy(enemy);
        enemy.RemoveWaitingEnemy(this);
    }

    public void TryAttackEnemy()
    {
        if (ActiveEnemy != null)
            return;

        foreach (Combat enemy in WaitingEnemies)
        {
            if (enemy.ActiveEnemy != null)
                continue;

            AttackEnemy(enemy);
            break;
        }
    }
}
