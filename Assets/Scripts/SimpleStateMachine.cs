using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace FOGPIUtilities.StateMachine
{
    public class SimpleStateMachine : MonoBehaviour
    {
        [Header("States")]
        [HideInInspector]

        public List<SimpleState> states;

        public string stateName;

        protected SimpleState state = null;

        protected void SetState(SimpleState _state)
        {
            if (state != null)
            {
                state.OnExit();
            }

            state = _state;
            stateName = nameof(state);

            state.OnStart();
        }

        public void ChangeState(string _stateName)
        {
            foreach(SimpleState s in states)
            {
                if (s.GetType().ToString().ToLower() == _stateName.ToLower())
                {
                    SetState(s);
                    return;
                }
            }
        }


        void FixedUpdate()
        {
            if (state == null)
                return;

            state.OnUpdate();
        }
    }
}
