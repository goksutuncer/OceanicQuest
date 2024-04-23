using UnityEngine;
using System;
using System.Linq;

public abstract class StateControllerBase<TState> : MonoBehaviour
{
    [SerializeField] private StateMap[] _stateMap = null;
    [SerializeField] private StateTransitionMap[] _stateTransitionMap = null;

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

    [Serializable]
    public class StateTransitionMap
    {
        [field: SerializeField] public TState StateFromType { get; private set; }

        [field: SerializeField] public TState StateToType { get; private set; }
    }

    public void ChangeState(TState state)
    {
        if (CurrentState.Equals(state))
        {
            return;
        }

        if(!CanTransition(CurrentState, state))
        {
            return;
        }

        StateMap curState = _stateMap.FirstOrDefault(i => i.StateType.Equals(CurrentState));

        if (curState != null)
        {
            curState.State.ExitActions();
        }

        StateMap nextState = _stateMap.FirstOrDefault(i => i.StateType.Equals(state));

        nextState.State.EnterActions();

        CurrentState = nextState.StateType;
    }

    private bool CanTransition(TState stateFrom, TState stateTo)
    {
        StateTransitionMap transition = _stateTransitionMap.FirstOrDefault(i =>
            i.StateFromType.Equals(stateFrom) &&
            i.StateToType.Equals(stateTo));

        return transition != null;
    }

    private void Awake()
    {
        ChangeState(_initialState);
    }
}
