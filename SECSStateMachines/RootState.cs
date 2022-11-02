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
using System.Data;

#nullable enable

 namespace com.CIMthetics.CSharpSECSTools.SECSStateMachines
{
    /// <summary>
    /// This is the root state of a state machine.  All other states will
    /// be a sub-state of this one.
    /// </summary>
    internal class RootState : State
    {
        internal RootState(StateMachine stateMachine) : base(stateMachine, -1, "RootState")
        {
            // The root state is always active.
            IsActive = true;
        }

        public override void PerformTransition(int transitionNumber)
        {
            throw new ConstraintException($"State RootState has not been implemented to handle PerformTransition for transition {transitionNumber}");
        }
    }
}