using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    //Consts
    private const float ProtectionTime = 5f;
    private const int Damage = 1;

    //Data
    public bool IsInCombat { get; private set; }
    
    //Enemy Data
    public Combat Enemy { get; private set; }
    private List<Combat> waitingEnemies = new();
    private List<Combat> enemiesOnProtection = new();

    //Actions
    public Action OnNewEnemyEvent;

    public void SetInCombat(bool isInCombat) => IsInCombat = isInCombat;

    private void AddEnemy(Combat newEnemy)
    {
        Enemy = newEnemy;
        OnNewEnemyEvent?.Invoke();
    }

    public void TryAddNewEnemy(Combat newEnemy)
    {
        //If no enemy is waiting for attack
        if (Enemy == null)
        {
            AddEnemy(newEnemy);
            return;
        }

        //If enemy has been attacked before
        //and his protection has not disappeared
        if (enemiesOnProtection.Contains(newEnemy))
            return;

        //If new enemy already is on the list
        if (waitingEnemies.Contains(newEnemy))
            return;

        waitingEnemies.Add(newEnemy);
    }

    private void ProtectEnemy(Combat attackedEnemy)
    {
        enemiesOnProtection.Add(attackedEnemy);
        StartCoroutine(RemoveProtection(attackedEnemy));
    }

    private void SetNewEnemyFromWaitingList()
    {
        if (waitingEnemies.Count <= 0)
        {
            Enemy = null;
            return;
        }

        //Set first enemy from the waiting list
        AddEnemy(waitingEnemies[0]);
    }

    public void Attack()
    {
        if(Enemy.gameObject.TryGetComponent(out Health health))
            health.DealDamage(Damage);

        ProtectEnemy(Enemy);
        SetNewEnemyFromWaitingList();
    }

    IEnumerator RemoveProtection(Combat attackedEnemy)
    {
        yield return new WaitForSeconds(ProtectionTime);
        enemiesOnProtection.Remove(attackedEnemy);
    }
}
