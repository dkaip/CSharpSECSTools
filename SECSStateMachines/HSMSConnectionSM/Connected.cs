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

#nullable enable

namespace com.CIMthetics.CSharpSECSTools.SECSStateMachines.HSMSConnectionSM
{
    public class Connected : State
    {
        public Connected(StateMachine stateMachine) : base(stateMachine, (int)HSMSConnectionSMStates.Connected, HSMSConnectionSMStates.Connected.ToString())
        {
            StateChangeCallback = StateChangedCallbackHandler;
        }

        private void StateChangedCallbackHandler(State state, StateEntryOrExit entryOrExit)
        {
            if (entryOrExit == StateEntryOrExit.Entry)
            {
                StateMachine.SetState((int)HSMSConnectionSMStates.NotSelected); 
            }
            else
            {
                Log.Debug("State {0}:{1} has no action for deactivation.", state.StateName, state.StateID);
            }
        }

        public override void PerformTransition(int transitionNumber)
        {
            if (transitionNumber == (int)HSMSConnectionSMTransitions.Transition3)
            {
                Log.Verbose("State Connected performing transition {0}.", HSMSConnectionSMTransitions.Transition5.ToString());
                StateMachine.SetState((int)HSMSConnectionSMStates.NotSelected);
            }
            else
            {
                string transitionNumberString = ((HSMSConnectionSMTransitions)transitionNumber).ToString();
                throw new InvalidOperationException($"State Connected cannot perform transistion {transitionNumberString}.");
            }
        }
    }
}