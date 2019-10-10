/*
 * Copyright 2019 Douglas Kaip
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
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;
using com.CIMthetics.CSharpSECSTools.SECSCommUtils;

namespace SECSCommUtils
{
    public class HSMSWriter
    {
        private NetworkStream iOStream;
        private Queue<SECSConnection.TransientMessage> messagesToSend;
        private EventWaitHandle messageToSendWaitHandle;

        public HSMSWriter()
        {
        }

        public HSMSWriter(NetworkStream iOStream, Queue<SECSConnection.TransientMessage> messagesToSend, EventWaitHandle messageToSendWaitHandle)
        {
            this.iOStream = iOStream;
            this.messagesToSend = messagesToSend;
            this.messageToSendWaitHandle = messageToSendWaitHandle;
        }

        internal static void Start()
        {
            throw new NotImplementedException();
        }
    }
}
