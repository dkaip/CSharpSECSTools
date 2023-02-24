# SECSTee

This utility has been created to allow for the insertion of the equivalent of a plumbing &quot;T&quot; into a &quot;pipe&quot;.  The &quot;pipe&quot; in this case is actually an HSMS connection between two endpoints.  When configured and used it will pass messages between the intended endpoints of the &quot;pipe&quot; that is formed by the HSMS connections, however, it will also send the same messages to one or more third parties.

The messages sent to the third party (or parties) may be configured to be all of the messages that travel through the &quot;pipe&quot; or they may be restricted to only those messages originating from one end or the other.  Keep in mind, there is no meta data added to the messages by this utility so whatever receives and processes these messages may need some means to figure out which end of the &quot;pipe&quot; originated the message.

The source and the destination should not notice there is a &quot;middle man&quot; in between them. However, messages originated by any third parties, other than HSMS control messages, will be ignored by `SECSTee`.  In other words, third parties will only be able to receive &quot;copies&quot; of the messages sent between the two ends of the original &quot;pipe&quot;, but, they will not be able to interfere with or modify the communication between them.
