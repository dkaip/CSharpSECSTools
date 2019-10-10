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
    public class HSMSReader
    {
        private NetworkStream iOStream;
        private Queue<SECSMessage> receivedSECSMessages;
        private EventWaitHandle receivedSECSMessagesWH;

        public HSMSReader()
        {
        }

        public HSMSReader(NetworkStream iOStream, ref Queue<SECSMessage> receivedSECSMessages, ref EventWaitHandle receivedSECSMessagesWH)
        {
            this.iOStream = iOStream;
            this.receivedSECSMessages = receivedSECSMessages;
            this.receivedSECSMessagesWH = receivedSECSMessagesWH;
        }

        internal static void start()
        {
            throw new NotImplementedException();
        }

        internal static void Start()
        {
            throw new NotImplementedException();
        }
    }
}
