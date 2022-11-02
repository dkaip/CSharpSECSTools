/*
 * Copyright 2019-2022 Douglas Kaip
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
 * See the License for the specific language governing permissSECSStateMachinesTestsions and
 * limitations under the License.
 */
using NUnit.Framework;

using System.Data;

using com.CIMthetics.CSharpSECSTools.SECSStateMachines;
using com.CIMthetics.CSharpSECSTools.SECSStateMachines.HSMSConnectionSM;

namespace com.CIMthetics.CSharpSECSTools.SECSStateMachinesTests;
[TestFixture ()]
public class HSMSConnectionSMTests
{
    /// <summary>
    /// Test the <c>HSMSConnectionSMStates</c> enum.
    /// It tests the number of elements, the value of the elements and
    /// the names.
    /// </summary>
    [Test ()]
    public void Test01 ()
    {
        // Make sure there are only 5 items in the enum
        var values = Enum.GetNames(typeof(HSMSConnectionSMStates));
        Assert.IsTrue(values.Length == 5);

        // Verify the values
        Assert.IsTrue((int)HSMSConnectionSMStates.NoState == 0);
        Assert.IsTrue((int)HSMSConnectionSMStates.NotConnected == 1);
        Assert.IsTrue((int)HSMSConnectionSMStates.Connected == 2);
        Assert.IsTrue((int)HSMSConnectionSMStates.NotSelected == 3);
        Assert.IsTrue((int)HSMSConnectionSMStates.Selected == 4);

        // Verify the names...just in case someone overrides ToString
        Assert.IsTrue(String.Equals(HSMSConnectionSMStates.NoState.ToString(), "NoState"));
        Assert.IsTrue(String.Equals(HSMSConnectionSMStates.NotConnected.ToString(), "NotConnected"));
        Assert.IsTrue(String.Equals(HSMSConnectionSMStates.Connected.ToString(), "Connected"));
        Assert.IsTrue(String.Equals(HSMSConnectionSMStates.NotSelected.ToString(), "NotSelected"));
        Assert.IsTrue(String.Equals(HSMSConnectionSMStates.Selected.ToString(), "Selected"));
    }

    /// <summary>
    /// Test the <c>HSMSConnectionSMTransitions</c> enum.
    /// It tests the number of elements, the value of the elements and
    /// the names.
    /// </summary>
    [Test ()]
    public void Test02 ()
    {
        // Make sure there are only 6 items in the enum
        var values = Enum.GetNames(typeof(HSMSConnectionSMTransitions));
        Assert.IsTrue(values.Length == 6);

        // Verify the values
        Assert.IsTrue((int)HSMSConnectionSMTransitions.Transition1 == 1);
        Assert.IsTrue((int)HSMSConnectionSMTransitions.Transition2 == 2);
        Assert.IsTrue((int)HSMSConnectionSMTransitions.Transition3 == 3);
        Assert.IsTrue((int)HSMSConnectionSMTransitions.Transition4 == 4);
        Assert.IsTrue((int)HSMSConnectionSMTransitions.Transition5 == 5);
        Assert.IsTrue((int)HSMSConnectionSMTransitions.Transition6 == 6);
    }

    /// <summary>
    /// Test that the states in the state machine <c>HSMSConnectionSM</c>
    /// are what they should be.
    /// </summary>
    [Test ()]
    public void Test11 ()
    {
        HSMSConnectionSM sm = new HSMSConnectionSM();
        List<State> states = sm.GetStates();

        Assert.IsTrue(states.Count() == 5);

        foreach(State state in states)
        {
            if (state.GetType() == typeof(NoState))
            {
                Assert.IsTrue(state.StateID == (int)HSMSConnectionSMStates.NoState);
                Assert.IsTrue(String.Equals(state.StateName, HSMSConnectionSMStates.NoState.ToString()));
            }
            else if (state.GetType() == typeof(NotConnected))
            {
                Assert.IsTrue(state.StateID == (int)HSMSConnectionSMStates.NotConnected);
                Assert.IsTrue(String.Equals(state.StateName, HSMSConnectionSMStates.NotConnected.ToString()));
            }
            else if (state.GetType() == typeof(Connected))
            {
                Assert.IsTrue(state.StateID == (int)HSMSConnectionSMStates.Connected);
                Assert.IsTrue(String.Equals(state.StateName, HSMSConnectionSMStates.Connected.ToString()));
            }
            else if (state.GetType() == typeof(NotSelected))
            {
                Assert.IsTrue(state.StateID == (int)HSMSConnectionSMStates.NotSelected);
                Assert.IsTrue(String.Equals(state.StateName, HSMSConnectionSMStates.NotSelected.ToString()));
            }
            else if (state.GetType() == typeof(Selected))
            {
                Assert.IsTrue(state.StateID == (int)HSMSConnectionSMStates.Selected);
                Assert.IsTrue(String.Equals(state.StateName, HSMSConnectionSMStates.Selected.ToString()));
            }
            else
            {
                Assert.Fail($"Unexpected state type of {state.StateName}:{state.StateID}");
            }
        }
    }

    /// <summary>
    /// Test that the initial state in a newly created <c>HSMSConnectionSM</c>
    /// is what it should be.
    /// </summary>
    [Test ()]
    public void Test12 ()
    {
        HSMSConnectionSM sm = new HSMSConnectionSM();
        Assert.IsTrue(sm.CurrentState.StateID == (int)HSMSConnectionSMStates.NoState);
        Assert.IsTrue(String.Equals(sm.CurrentState.StateName, HSMSConnectionSMStates.NoState.ToString()));
    }

    /// <summary>
    /// Test that Transition1 for <c>HSMSConnectionSM</c>
    /// ends up in the expected state.
    /// </summary>
    [Test ()]
    public void Test13 ()
    {
        HSMSConnectionSM sm = new HSMSConnectionSM();
        sm.PerformTransition((int)HSMSConnectionSMTransitions.Transition1);
        Assert.IsTrue(sm.CurrentState.StateID == (int)HSMSConnectionSMStates.NotConnected);
    }

    /// <summary>
    /// Test that Transition2 for <c>HSMSConnectionSM</c>
    /// ends up in the expected state.
    /// </summary>
    [Test ()]
    public void Test14 ()
    {
        HSMSConnectionSM sm = new HSMSConnectionSM();
        sm.PerformTransition((int)HSMSConnectionSMTransitions.Transition1);
        sm.PerformTransition((int)HSMSConnectionSMTransitions.Transition2);
        Assert.IsTrue(sm.CurrentState.StateID == (int)HSMSConnectionSMStates.NotSelected);
    }

    /// <summary>
    /// Test that Transition4 for <c>HSMSConnectionSM</c>
    /// ends up in the expected state.
    /// </summary>
    [Test ()]
    public void Test15 ()
    {
        HSMSConnectionSM sm = new HSMSConnectionSM();
        sm.PerformTransition((int)HSMSConnectionSMTransitions.Transition1);
        sm.PerformTransition((int)HSMSConnectionSMTransitions.Transition2);
        sm.PerformTransition((int)HSMSConnectionSMTransitions.Transition4);
        Assert.IsTrue(sm.CurrentState.StateID == (int)HSMSConnectionSMStates.Selected);
    }

    /// <summary>
    /// Test that Transition5 for <c>HSMSConnectionSM</c>
    /// ends up in the expected state.
    /// </summary>
    [Test ()]
    public void Test16 ()
    {
        HSMSConnectionSM sm = new HSMSConnectionSM();
        sm.PerformTransition((int)HSMSConnectionSMTransitions.Transition1);
        sm.PerformTransition((int)HSMSConnectionSMTransitions.Transition2);
        sm.PerformTransition((int)HSMSConnectionSMTransitions.Transition4);
        sm.PerformTransition((int)HSMSConnectionSMTransitions.Transition5);
        Assert.IsTrue(sm.CurrentState.StateID == (int)HSMSConnectionSMStates.NotSelected);
    }

    /// <summary>
    /// Test that Transition6 for <c>HSMSConnectionSM</c>
    /// ends up in the expected state.
    /// </summary>
    [Test ()]
    public void Test17 ()
    {
        HSMSConnectionSM sm = new HSMSConnectionSM();
        sm.PerformTransition((int)HSMSConnectionSMTransitions.Transition1);
        sm.PerformTransition((int)HSMSConnectionSMTransitions.Transition2);
        sm.PerformTransition((int)HSMSConnectionSMTransitions.Transition4);
        sm.PerformTransition((int)HSMSConnectionSMTransitions.Transition5);
        sm.PerformTransition((int)HSMSConnectionSMTransitions.Transition6);
        Assert.IsTrue(sm.CurrentState.StateID == (int)HSMSConnectionSMStates.NotConnected);
    }

    // TODO add tests for invalid transitions

    // TODO consider test for Transition3

    /// <summary>
    /// Test error message for a missing state for <c>HSMSConnectionSM</c>.
    /// </summary>
    [Test ()]
    public void Test31 ()
    {
        HSMSConnectionSM sm = new HSMSConnectionSM();

        var exception = Assert.Catch(() => sm.SetState(77));

        Assert.IsInstanceOf(typeof(ConstraintException), exception);

        Assert.IsTrue (exception.Message.Contains ("State not found.  There is no path to state 77."));
    }

}
