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
    public class NotSelected : State
    {
        public NotSelected(StateMachine stateMachine) : base(stateMachine, (int)HSMSConnectionSMStates.NotSelected, HSMSConnectionSMStates.NotSelected.ToString()) {}

        public override void PerformTransition(int transitionNumber)
        {
            if (transitionNumber == (int)HSMSConnectionSMTransitions.Transition3)
            {
                Log.Verbose("State NotSelected performing transition {0}.", HSMSConnectionSMTransitions.Transition3.ToString());
                StateMachine.SetState((int)HSMSConnectionSMStates.NotConnected);
            }
            else if (transitionNumber == (int)HSMSConnectionSMTransitions.Transition4)
            {
                Log.Verbose("State NotSelected performing transition {0}.", HSMSConnectionSMTransitions.Transition4.ToString());
                StateMachine.SetState((int)HSMSConnectionSMStates.Selected);
            }
            else if (transitionNumber == (int)HSMSConnectionSMTransitions.Transition6)
            {
                Log.Verbose("State NotSelected performing transition {0}.", HSMSConnectionSMTransitions.Transition6.ToString());
                StateMachine.SetState((int)HSMSConnectionSMStates.NotConnected);
            }
            else
            {
                string transitionNumberString = ((HSMSConnectionSMTransitions)transitionNumber).ToString();
                throw new InvalidOperationException($"State NotSelected cannot perform transistion {transitionNumberString}.");
            }
        }
    }
}