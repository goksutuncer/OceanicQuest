using UnityEngine;
using System;
using System.Linq;

public abstract class StateControllerBase<TState> : MonoBehaviour
{
    [SerializeField] private StateMap[] _stateMap = null;

    [SerializeField] private TState _initialState;

    public TState CurrentState { get; private set; }

    [Serializable]
    public class StateMap
    {
        [SerializeField] private TState _stateType;
        public TState StateType => _stateType;

        [SerializeField] private StateBase _state;
        public StateBase State => _state;
    }

    public void ChangeState(TState state)
    {
        StateMap curState = _stateMap.FirstOrDefault(i => i.StateType.Equals(CurrentState));

        if (curState != null)
        {
            curState.State.ExitActions();
        }

        StateMap nextState = _stateMap.FirstOrDefault(i => i.StateType.Equals(state));

        nextState.State.EnterActions();
    }

    private void Awake()
    {
        ChangeState(_initialState);
    }
}
