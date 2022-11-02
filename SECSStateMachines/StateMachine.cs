 /*
 * Copyright 2022 Douglas Kaip
 * 
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * 
 * http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governingprivate permissions and
 * limitations under the License.
 */
using Serilog;

using System.Data;
using System.Text;
 
#nullable enable

namespace com.CIMthetics.CSharpSECSTools.SECSStateMachines
{
    public abstract class StateMachine
    {
        internal    State           _rootState;
        internal    volatile State  _currentState;
        public      State           CurrentState { get { return _currentState; } }

        private     bool            _setStateIsBusy = false;
        private     Queue<int>      _workToDoQueue = new Queue<int>();
        internal StateMachine()
        {
            _rootState = new RootState(this);
            _currentState = _rootState;
        }

        /// <summary>
        /// Sets the current state of the state machine.
        /// </summary>
        /// <remarks>
        /// This is a brute force way of setting the state of the state machine.
        /// Typically, this is only used to set the initial state and perhaps to
        /// reset the state machine to its initial condition.  All other state 
        /// changes should normally be performed by using the <c>PerformTransition</c>
        /// method.
        /// <para/>
        /// This method is not thread safe, however, it is very possible that it may
        /// be called recursively.
        /// </remarks>
        /// <exception cref="System.Data.ConstraintException">Thrown when there is no path from the root to the desired state.</exception>
        public void SetState(int newStateID)
        {
            Log.Debug("SetState:Current state is {0}:{1} trying to set state to {2}. ", CurrentState.StateName, CurrentState.StateID, newStateID);

            _workToDoQueue.Enqueue(newStateID);

            if (_setStateIsBusy == true)
            {
                Log.Verbose("Busy is true");
                return;
            }

            _setStateIsBusy = true;
            while(true)
            {
                // Keep going if we have work to do.
                try
                {
                    newStateID = _workToDoQueue.Dequeue();
                    Log.Verbose("got a new state {0} to process", newStateID);
                }
                catch(InvalidOperationException)
                {
                    Log.Verbose("queue was empty we are done for now");
                    // The queue was empty...we are done.
                    _setStateIsBusy = false;
                    return;
                }

                if (newStateID == CurrentState.StateID)
                {
                    /*
                        We are already in the requested state,,,either by a 
                        user's error or due a recursive call.  Just return
                        since there is nothing to do.
                    */
                    Log.Verbose("SetState:already in state {0}:{1}...returning without doing anything", CurrentState.StateName, CurrentState.StateID);
                    return;
                }

                Log.Verbose("aaa got a new state {0} to process", newStateID);
                // Get the path from the root to the desired new State
                Stack<State> pathToState = GetPathToState(_rootState, newStateID);
                if (pathToState.Count() == 0)
                {
                    _workToDoQueue.Clear();
                    _setStateIsBusy = false;
                    throw new ConstraintException("State not found.  There is no path to state " + newStateID + ".");
                }

                State[] pathToStateArray = pathToState.ToArray();
                if (Log.IsEnabled(Serilog.Events.LogEventLevel.Debug))
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("pathToStateNewState [");
                    for(int i = 0; i < pathToStateArray.Length; i++)
                    {
                        sb.Append(pathToStateArray[i].StateID);
                        if (i < pathToStateArray.Length -1)
                        {
                            sb.Append(", ");
                        }
                        else
                        {
                            sb.Append("]");
                        }
                    }

                    Log.Debug(sb.ToString());
                }

                if (CurrentState == _rootState)
                {
                    /*
                        If the current state is the root state we are just starting
                        out so there will be no states to signal "departure" to so
                        just go ahead and mark the state(s) as entered and get on 
                        with life.
                    */
                    for(int i = 0; i < pathToStateArray.Length; i++)
                    {
                        pathToStateArray[i].StateEntered();
                    }

                    _currentState = pathToStateArray[pathToStateArray.Length - 1];

                    _setStateIsBusy = false;
                    return;
                }

                // Log.Verbose("bbb got a new state {0} to process", CurrentState.StateID);
                // // Get the path from the root to the desired new State
                // Stack<State> pathToState2 = GetPathToState(_rootState, CurrentState.StateID);
                // if (pathToState.Count() == 0)
                // {
                //     _workToDoQueue.Clear();
                //     _setStateIsBusy = false;
                //     throw new ConstraintException("State not found.  There is no path to state " + CurrentState.StateID + ".");
                // }

                // State[] pathToStateArray2 = pathToState2.ToArray();
                // if (Log.IsEnabled(Serilog.Events.LogEventLevel.Debug))
                // {
                //     StringBuilder sb = new StringBuilder();
                //     sb.Append("pathToStateNewState2 [");
                //     for(int i = 0; i < pathToStateArray2.Length; i++)
                //     {
                //         sb.Append(pathToStateArray2[i].StateID);
                //         if (i < pathToStateArray2.Length -1)
                //         {
                //             sb.Append(", ");
                //         }
                //         else
                //         {
                //             sb.Append("]");
                //         }
                //     }

                //     Log.Debug(sb.ToString());
                // }

                // Get the path from the root to the current State
                Stack<State> pathToCurrentState = GetPathToState(_rootState, CurrentState.StateID);
                if (pathToCurrentState.Count() == 0)
                {
                    _workToDoQueue.Clear();
                    _setStateIsBusy = false;
                    throw new ApplicationException($"This should never happen.  There is no path to state {CurrentState.StateID}.");
                }

                State[] pathToCurrentStateArray = pathToCurrentState.ToArray();
                if (Log.IsEnabled(Serilog.Events.LogEventLevel.Debug))
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("pathToCurrentState [");
                    for(int i = 0; i < pathToCurrentStateArray.Length; i++)
                    {
                        sb.Append(pathToCurrentStateArray[i].StateID);
                        if (i < pathToCurrentStateArray.Length -1)
                        {
                            sb.Append(", ");
                        }
                        else
                        {
                            sb.Append("]");
                        }
                    }

                    Log.Debug(sb.ToString());
                }

                int divergentPoint = int.MinValue;
                int minimumArrayLength = int.MaxValue;
                minimumArrayLength = Math.Min(minimumArrayLength, pathToStateArray.Length);
                minimumArrayLength = Math.Min(minimumArrayLength, pathToCurrentStateArray.Length);

                for(int i = 0; i < minimumArrayLength; i++)
                {
                    if (pathToStateArray[i] != pathToCurrentStateArray[i])
                    {
                        divergentPoint = i;
                        break;
                    }
                }

                if (divergentPoint == int.MinValue)
                {
                    /*
                        The arrays above were different lengths and where
                        identical where they overlapped so the divergent point
                        is just past the end of the shorter array.
                    */
                    divergentPoint = minimumArrayLength;
                }

                Log.Debug("SetState divergent point={0}", divergentPoint);
                // set departed states first
                for(int i = pathToCurrentStateArray.Length - 1; i >= divergentPoint; i--)
                {
                    Log.Verbose("executing StateDeparted() on state {0}:{1}", pathToCurrentStateArray[i].StateName, pathToCurrentStateArray[i].StateID);
                    pathToCurrentStateArray[i].StateDeparted();
                }

                // now, states entered
                for(int i = divergentPoint; i < pathToStateArray.Length; i++)
                {
                    Log.Verbose("executing StateEntered() on state {0}:{1}", pathToStateArray[i].StateName, pathToStateArray[i].StateID);
                    pathToStateArray[i].StateEntered();
                }

                Log.Verbose("SetState:Setting current state to {0}:{1}", pathToStateArray[pathToStateArray.Length - 1].StateName, pathToStateArray[pathToStateArray.Length - 1].StateID);
                _currentState = pathToStateArray[pathToStateArray.Length - 1];

                _setStateIsBusy = false;
            } // End while(true)
        }

        public virtual void PerformTransition(int transitionNumber)
        {
            CurrentState.PerformTransition(transitionNumber);
        }

        /// <summary>
        /// This method will search the tree of states starting from the <c>State</c>
        /// supplied by the parameter <paramref name="startingState"/> for the
        /// <c>State</c> identified by the <paramref name="stateID"/> parameter.
        /// </summary>
        /// <param name="startingState">a state to start searching from</param>
        /// <param name = "stateID">the stateID of the state to locate</param>
        /// <returns>
        /// A <c>Stack</c> of <c>State</c>s ordered from the <c>State</c> supplied
        /// by the parameter <paramref name="startingState"/> to
        /// the ending state as identified by the parameter <paramref name="stateID"/>.
        /// <para/>
        /// If the <c>State</c> is not found a <c>Stack</c> containing no elements 
        /// will be returned.
        /// </returns>
        private Stack<State> GetPathToState(State startingState, int stateID)
        {
            Stack<State> pathToState = new Stack<State>();

            if (startingState.StateID == stateID)
            {
                pathToState.Push(startingState);
                return pathToState;
            }

            foreach(State state in startingState.SubStates)
            {
                Stack<State> path = GetPathToState(state, stateID);
                if (path.Count() != 0)
                {
                    // If path.Count() != 0 we found the state

                    State[] stateArray = path.ToArray();
                    for(int i = stateArray.Count() - 1; i >= 0; i--)
                    {
                        pathToState.Push(stateArray[i]);
                    }

                    pathToState.Push(startingState);

                    return pathToState;
                }

                // We did not find the state check the next sub-state.
            }

            /*
                A path to the state was not found. 
            */
            pathToState.Clear();
            return pathToState;
        }

        // /// <summary>
        // /// This method will search the tree of states starting from the <c>State</c>
        // /// supplied by the parameter <paramref name="startingState"/> for the
        // /// <c>State</c> identified by the <paramref name="stateID"/> parameter.
        // /// </summary>
        // /// <param name="startingState">a state to start searching from</param>
        // /// <param name = "stateID">the stateID of the state to locate</param>
        // /// <returns>
        // /// A <c>Queue</c> of <c>State</c>s ordered from the ending <c>State</c>
        // /// identified by the parameter <paramref name="stateID"/> to
        // /// the starting state as identified by the parameter <paramref name="startingState"/>.
        // /// <para/>
        // /// If the <c>State</c> is not found a <c>Queue</c> containing no elements 
        // /// will be returned.
        // /// </returns>
        // private Queue<State> GetPathFromState(State startingState, int stateID)
        // {
        //     Queue<State> pathFromState = new Queue<State>();

        //     if (startingState.StateID == stateID)
        //     {
        //         pathFromState.Enqueue(startingState);
        //         return pathFromState;
        //     }

        //     foreach(State state in startingState.SubStates)
        //     {
        //         Queue<State> path = GetPathFromState(state, stateID);
        //         if (path.Count() != 0)
        //         {
        //                 // If path.Count() != 0 we found the state

        //             int end = path.Count();
        //             for(int i = 0; i < end; i++)
        //             {
        //                 State temp = path.Dequeue();
        //                 pathFromState.Enqueue(temp);
        //             }

        //             pathFromState.Enqueue(startingState);

        //             return pathFromState;
        //         }

        //         // We did not find the state check the next sub-state.
        //     }

        //     /*
        //         A path from the state was not found. 
        //     */
        //     pathFromState.Clear();
        //     return pathFromState;
        // }

        /// <summary>
        /// This method returns a <c>List</c> of <c>State</c>s available for
        /// this state machine.  The <c>List</c> of <c>State</c>s returned is
        /// not necessarily in any meaningful order.
        /// <para/>
        /// Note: This list does not include the <c>RootState</c> which is the
        /// node which all of the <c>State</c>s are sub-states of.
        /// </summary>
        /// <remarks>
        /// This method will most likely be used in the case where the state
        /// machine being used is in some Assembly that the user does not have
        /// source access to.  This will allow the user to retrieve all of the
        /// available <c>State</c>s (except for the <c>RootState</c>) and use the 
        /// <c>StateChangeCallback</c> property of the <c>State</c> in order to
        /// assign a callback that will be executed whenever the state is
        /// entered or departed.
        /// </remarks>
        /// <returns>A <c>List</c> of <c>State</c>s available for
        /// this state machine.</returns>
        public List<State> GetStates()
        {
            return _rootState.RetrieveSubStates();
        }
    }
}