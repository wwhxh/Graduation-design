using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StateSystem
{
    /// <summary>
    /// Character properties : Health
    /// </summary>
    public class State {
        public float maxHealth;

        public State(float maxHealth) {
            this.maxHealth = maxHealth;
        }

        public void copy (State other) {
            maxHealth = other.maxHealth;
        }

    }

    /// <summary>
    /// Character attack properties : damage, attackSpeed, attackRange, AttackColdDown
    /// </summary>
    public class AttackState {
        public float damage;
        public float attackSpeed = 1;
        public float attackRange;

        public AttackState (float damage, float attackSpeed, float attackRange){
            this.damage = damage;
            this.attackSpeed = attackSpeed;
            this.attackRange = attackRange;
        }

        public float AttackColdDown {
            get { return 2 / attackSpeed; }
        }

        public void copy(AttackState other){
            this.damage = other.damage;
            this.attackSpeed = other.attackSpeed;
            this.attackRange = other.attackRange;
        }
    }

    public State baseState {
        get;
        private set;
    }

    public AttackState baseAttackState {
        get;
        private set;
    }

    public float CurrentHealth {
        get;
        set;
    }

    public bool hasAttackAction;

    public void init(State state,AttackState attackState){
        if(state == null){
            Debug.LogError("Initial Error, the State is NULL");
            return;
        }
        this.baseState = state;
        CurrentHealth = this.baseState.maxHealth;
        if(attackState == null){
            return;
        }
        hasAttackAction = true;
    }

}
