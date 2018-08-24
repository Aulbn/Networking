using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public abstract class Controller : MonoBehaviour{
	[SerializeField] private State[] _states;
	private readonly Dictionary<Type, State> _stateDictionary = new Dictionary<Type, State> ();
	public State CurrentState;

	void Awake(){
		Initialize ();
		foreach (State state in _states) {
			State instance = Instantiate (state);
			instance.Initialize (this);
			_stateDictionary.Add (instance.GetType (), instance);

			if (CurrentState != null)
				continue;
			CurrentState = instance;
			CurrentState.Enter ();
		}
	}

	protected virtual void Initialize(){
		
	} 

	private void Update(){
		CurrentState.Update ();
	}

	private void FixedUpdate(){
		CurrentState.FixedUpdate ();
	}

	public T GetState<T>(){
		Type type = typeof(T);
		if (!_stateDictionary.ContainsKey (type))
			throw new NullReferenceException ("No state od type " + type + " found");
		return(T)Convert.ChangeType (_stateDictionary [type], type);
	}

	public void TransitionTo<T>(){
		CurrentState.Exit ();
		CurrentState = GetState<T> () as State;
		CurrentState.Enter ();
	}
}
