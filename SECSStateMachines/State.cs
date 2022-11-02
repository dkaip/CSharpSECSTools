 using Serilog;

 #nullable enable

 namespace com.CIMthetics.CSharpSECSTools.SECSStateMachines
{
    public delegate void StateEntryExitLogic(State state, StateEntryOrExit entryOrExit);

    public abstract class State
    {
        protected   StateMachine    StateMachine { get; private set; }
        /// <summary>
        /// This is the name of the state in text form.  It is primarily
        /// available for debugging purposes only.
        /// </summary>
        public      string          StateName { get; private set; }
        public      int             StateID { get; private set; }
        public      bool            IsActive { get; internal set; }

        internal    List<State>     SubStates { get; private set; } = new List<State>();

        /// <summary>
        /// This property, when set, will be the method that is called when
        /// this <c>State</c> is entered or left.
        /// </summary>
        public      StateEntryExitLogic?  StateChangeCallback { get; set; }

        internal State(StateMachine stateMachine, int stateID, string stateName)
        {
            StateMachine = stateMachine;
            StateID       = stateID;
            StateName     = stateName;
        }

        /// <summary>
        /// This method informs the <c>State</c> that is has been entered / activated.
        /// </summary>
        internal void StateEntered()
        {
            Log.Verbose("Entered state {0}:{1}", StateName, StateID);
            IsActive = true;

//            StateMachine._currentState = this;

            if (StateChangeCallback != null)
            {
                Log.Debug("State {0}:{1} Executing StateChangeCallback for state entry.", StateName, StateID);
                StateChangeCallback(this, StateEntryOrExit.Entry);
            }
            else
            {
                Log.Verbose("State {0}:{1} has no callback registered.", StateName, StateID);
            }
        }
        
        /// <summary>
        /// This method informs the <c>State</c> that is has been left / deactivated.
        /// </summary>
        internal void StateDeparted()
        {
            Log.Verbose("Exited state {0}:{1}", StateName, StateID);
            IsActive = false;

            if (StateChangeCallback != null)
            {
                Log.Debug("State {0}:{1} Executing StateChangeCallback for state exit.", StateName, StateID);
                StateChangeCallback(this, StateEntryOrExit.Exit);
            }
            else
            {
                Log.Verbose("State {0}:{1} has no callback registered.", StateName, StateID);
            }
        }
        
        // private bool SetStateInternal(int stateID)
        // {
        //     foreach(State state in _subStates)
        //     {
        //         if (state.StateID == stateID)
        //         {
        //             state.StateEntered();
        //             return true;
        //         }
        //         else
        //         {
        //             if (state.SetStateInternal(stateID) == true)
        //             {
        //                 /*
        //                     In short, we do not want to set _currentSubState
        //                     in the event that the specified stateID is not
        //                     part of this State's valid sub-states.
        //                 */
        //                 _currentSubState = state;
        //                 return true;
        //             }
        //         }
        //     }

        //     /*
        //         A sub-state of stateID was not found in this state. 
        //     */
        //     return false;
        // }

        // /// <summary>
        // /// 
        // /// </summary>
        // public virtual void SetState(int stateID)
        // {
        //     bool result = SetStateInternal(stateID);
        //     if (result == false)
        //     {
        //         Console.WriteLine("The sub-state " + stateID + " is not available in state " + StateID + ".");
        //     }
        // }

        // public virtual void ExecuteTransition(int trigger)
        // {
        //     if (_currentSubState != null)
        //     {
        //         _currentSubState.ExecuteTransition(trigger);
        //     }
        // }

        public void AddSubState(State newSubState)
        {
//            Log.Warning("state {0} adding substate {1}:{2}", StateName, newSubState.StateName, newSubState.StateID);
            SubStates.Add(newSubState);
        }

        // internal bool GetPathToState(Stack<State> pathToState, int stateID)
        // {
        //     foreach(State state in SubStates)
        //     {
        //         if (state.StateID == stateID)
        //         {
        //             pathToState.Push(state);
        //             return true;
        //         }
        //         else
        //         {
        //             return GetPathToState(pathToState, stateID);
        //         }
        //     }

        //     /*
        //         A sub-state of stateID was not found in this state. 
        //     */
        //     return false;
        // }

        // internal bool GetPathFromState(Queue<State> pathFromState, int stateID)
        // {
        //     foreach(State state in SubStates)
        //     {
        //         if (state.StateID == stateID)
        //         {
        //             pathFromState.Append(state);
        //             return true;
        //         }
        //         else
        //         {
        //             return GetPathFromState(pathFromState, stateID);
        //         }
        //     }

        //     /*
        //         A sub-state of stateID was not found in this state. 
        //     */
        //     return false;
        // }

        /// <summary>_rootState.SubStates, 
        /// Retrieve the sub-states of this <c>State</c>.
        /// </summary>
        /// <returns>a list containing any sub-states of this <c>State</c></returns>
        public List<State> RetrieveSubStates()
        {
            List<State> substateList = new List<State>();

            foreach(State state in SubStates)
            {
//                Log.Error("{0} added substate {1}", StateName, state.StateName);
                substateList.Add(state);
                substateList.AddRange(state.RetrieveSubStates());
            }

            return substateList;
        }

        public abstract void PerformTransition(int transitionNumber);
    }
}