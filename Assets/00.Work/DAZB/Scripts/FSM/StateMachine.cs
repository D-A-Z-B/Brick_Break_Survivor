using System;
using System.Collections.Generic;
using BBS.Entities;
using UnityEngine;

namespace BBS.FSM
{
    public class StateMachine
    {
        public EntityState CurrentState { get; private set; }
        
        private Dictionary<string, EntityState> _states;
        
        public StateMachine(List<StateSO> states, Entity entity)
        {
            _states = new Dictionary<string, EntityState>();
            foreach (var state in states)
            {
                try
                {
                    Type type = Type.GetType(state.className);

                    var entityState = Activator.CreateInstance(type, entity, state.stateParam) as EntityState;
                    _states.Add(state.stateName, entityState);
                }
                catch (Exception ex)
                {
                    Debug.LogError($"{state.className} loading errors, Message : {ex.Message}");
                }
            }
        }
        
        public EntityState GetState(string stateName)
            => _states.GetValueOrDefault(stateName);
        
        public void Initialize(string stateName)
        {
            EntityState startState = GetState(stateName);
            Debug.Assert(startState != null, "Start state is null check");
            
            CurrentState = startState;
            CurrentState.Enter();
        }

        public void ChangeState(string newStateName)
        {
            EntityState newState = GetState(newStateName);
            Debug.Assert(newState != null, $"State is null : {newStateName}");
            CurrentState.Exit();
            CurrentState = newState;
            CurrentState.Enter();
        }

        public void UpdateFSM()
        {
            CurrentState.Update();
        }
    }
}
