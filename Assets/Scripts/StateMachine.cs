using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDC
{
    public class StateMachine
    {
        Stack<State> _states;
        List<Effect> _effects;
        List<Effect> _effectsToRemove;
        State _newState;
        Player _player;
        bool _isAdding;
        bool _isRemoving;
        bool _isReplaceing;

        public StateMachine(Player player)
        {
            _player = player;
            _states = new Stack<State>();
            _effects = new List<Effect>();
            _effectsToRemove = new List<Effect>();
        }

        public void AddState(State newState, bool isReplacing)
        {
            _isAdding = true;
            _isReplaceing = isReplacing;
            _newState = newState;
        }

        public void RemoveState()
        {
            _isRemoving = true;
        }

        public void ProcessStateChanges()
        {
            if (_isRemoving && (_states.Count != 0))
            {
                _states.Pop();

                if (_states.Count != 0)
                {
                    _states.Peek().Resume();
                }

                _isRemoving = false;
            }

            if (_isAdding)
            {
                if (_states.Count != 0)
                {
                    if (_isReplaceing)
                    {
                        _states.Pop();
                        _isReplaceing = false;
                    }
                    else
                    {
                        _states.Peek().Pause();
                    }
                }

                _states.Push(_newState);
                _isAdding = false;
            }

            foreach(Effect effect in _effectsToRemove)
            {
                _effects.Remove(effect);
            }
        }

        public void ProcessState()
        {
            if (_states.Count != 0)
            {
                _states.Peek().Update();
            }

            foreach(Effect effect in _effects)
            {
                effect.Update();
            }

        }
        
        public State GetActiveState()
        {
            return _states.Peek();
        }

        public void AddEffect(Effect effect)
        {
            _effects.Add(effect);
            effect.Init(_player, this);
        }

        public void RemoveEffect(Effect effect)
        {
            _effectsToRemove.Add(effect);
        }
    }
}
