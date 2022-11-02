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

 #nullable enable

 namespace com.CIMthetics.CSharpSECSTools.SECSStateMachines.HSMSConnectionSM
{
    /// <summary>
    /// This class provides and implementation of the SEMI E37 HSMS Connection
    /// State machine.
    /// </summary>
    /// <remarks>
    /// The initial state of this state machine will be set to <c>HSMSConnectionSMStates.NoState</c>.
    /// </remarks>
    public class HSMSConnectionSM : StateMachine
    {
        /// <summary>
        /// Creates the state machine.
        /// </summary>
        /// <remarks>
        /// The initial state will be <c>HSMSConnectionSMStates.NoState</c>.
        /// </remarks>
        public HSMSConnectionSM()
        {
            /*
                Create the states that are to be part of this state machine.
                In addition, create any sub-states that may be needed for
                those states.
            */
            NoState noState = new NoState(this);

            NotConnected notConnected = new NotConnected(this);

            Connected connected = new Connected(this);
            connected.AddSubState(new NotSelected(this));
            connected.AddSubState(new Selected(this));

            // Add the states to the root state.
            _rootState.AddSubState(noState);
            _rootState.AddSubState(notConnected);
            _rootState.AddSubState(connected);

            // Set the "starting state" of the state machine.
            SetState((int)HSMSConnectionSMStates.NoState);
        }
    }
}