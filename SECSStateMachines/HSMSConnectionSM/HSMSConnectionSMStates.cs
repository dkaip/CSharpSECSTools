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
    /// This <c>enum</c> contains all of the states for the <c>HSMSConnectionSM</c>
    /// state machine.  Refer to the SEMI E-37 standard for more information.
    /// </summary>
    public enum HSMSConnectionSMStates
    {
        NoState = 0,
        NotConnected = 1,
        Connected = 2,
        NotSelected = 3,
        Selected = 4
    }
}