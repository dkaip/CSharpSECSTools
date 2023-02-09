# SECSItems

It is to be understood that if you are here reading this that you already are
knowledgeable of the SEMI standards (SECS in particular) pertaining to this problem domain.

## SECS Overview

This overview is just a brief overview to help in the understanding of this API.

With the exception of the SECS item type of `L`(List) all of the SECS item types
described in the standard are actually arrays of the type.  For example, if
some received data has an item type of `U4`(unsigned 4 byte integer) then its
&quot;value&quot; is an array of `U4`s.  The value of an item's length bytes indicates
how many **bytes** (not elements) are in the array.  The number of elements in
the array is determined by dividing the number of bytes in the array by the size
of the item.  So, if the value of an item's length bytes is 4 and the elements of the
array are `U4`s you would divide 4 by 4 and end up with the number of elements
being 1.  If (in this same case) the value of the length bytes is 16 the
division would be 16 divided by 4 indicating that there are 4 elements in the
array.

Keep in mind, the value of an item's length bytes is the length of the data in **bytes**
not elements.  In the case of SECS item types that have a length of 1 byte
`A`,`B`, `BO`, `I1`, and `U1` then, yes, the value of the length bytes is the number of elements
in the array.  For the other SECS item types you will need to do the division in
order to determine the number of elements.

There are a maximum of 3 length bytes available to express the length of a SECS
data item.  This allows for 2<sup>24</sup> - 1 or 16777215 elements in arrays that have elements
with a length of 1, 2<sup>23</sup> - 1 or 8388607 elements in arrays that have elements
with a length of 2, 2<sup>22</sup> - 1 or 4194303 elements in arrays that have elements
with a length of 4, and 2<sup>21</sup> - 1 or 2097151 elements in arrays that have elements
with a length of 8.

## The Value Add

With the quick overview above out of the way we may continue.

Dealing with SECS items that are in their &quot;transmission&quot; form is ungainly
to say the least. This is why this API has been created.  It allows a programmer to
deal with SECS items in a manner that is much more natural to the language they
currently are working in, in this case C#.

The following table shows the SECS item types and their corresponding C# data types:

| SECS Item Code | C# Data Type         | C# SECS Item Type |
| -------------- | -------------------- | ----------------- |
| L              | LinkedList&lt;SECSItem&gt; | ListSECSItem      |
| B              | byte[]               | BinarySECSItem    |
| BO             | bool                 | BooleanSECSItem   |
| JIS-8          | not implemented yet  |                   |
| C-2            | not implemented yet  |                   |
| I8             | Int64                | I8SECSItem        |
| I1             | sbyte                | I1SECSItem        |
| I2             | Int16                | I2SECSItem        |
| I4             | Int32                | I4SECSItem        |
| F8             | double               | F8SECSItem        |
| F4             | float                | F4SECSItem        |
| U8             | UInt64               | U8SECSItem        |
| U1             | byte                 | U1SECSItem        |
| U2             | UInt16               | U2SECSItem        |
| U4             | UInt32               | U4SECSItem        |

The following table shows the SECS item types and their corresponding C# data types
in the case where their value is thought of as an array:

| SECS Item Code | C# Data Type         | C# SECS Item Type      |
| -------------- | -------------------- | ---------------------- |
| BO             | bool[]               | BooleanArraySECSItem   |
| I8             | Int64[]              | I8ArraySECSItem        |
| I1             | sbyte[]              | I1ArraySECSItem        |
| I2             | Int16[]              | I2ArraySECSItem        |
| I4             | Int32[]              | I4ArraySECSItem        |
| F8             | double[]             | F8ArraySECSItem        |
| F4             | float[]              | F4ArraySECSItem        |
| U8             | UInt64[]             | U8ArraySECSItem        |
| U1             | byte[]               | U1ArraySECSItem        |
| U2             | UInt16[]             | U2ArraySECSItem        |
| U4             | UInt32[]             | U4ArraySECSItem        |


### Receiving a SECS message

This API does not provide the functionality to be able to send and receive
SECS messages.  It only provides the functionality to be able to encode, decode,
and process the SECS item data that may be in the message.
Check out the 'SECSCommUtils' API for send and receive type functionality.

Once a message is received the data that is after the message header (the payload)
is passed as an argument to `SECSItemFactory.GenerateSECSItem` which is a `static`
factory class that will extract the information and return an appropriate `SECSItem`.
Something like this:

```csharp
    byte[] rawSECSData;

    .
    . // Its value gets set here somewhere.
    .

    SECSItem secsItem = SECSItemFactory.GenerateSECSItem(rawSECSData);
```

`SECSItem` is an `abstract` base class for all of the `SECSItem`s.  You will need
to determine what that actual class of the `SECSItem` is before you can do much
with it.

For example:

```csharp
    if (secsItem.getType() == typeof(ListSECSItem))
    {
        // Oh, it is a List
        ListSECSItem newSECSItem = (ListSECSItem)secsItem;
        .
        . // Life is good now process the contents of the message.
        .
    }
    else if (secsItem.getType() == typeof(U4ArraySECSItem))
    {
        // Oh, it is an array of U4s
        U4ArraySECSItem newSECSItem = (U4ArraySECSItem)secsItem;
        .
        . // Life is good now process the contents of the message.
        .
    }
    else if (secsItem.getType() == typeof(U4SECSItem))
    {
        // Oh, it is a U4
        U4SECSItem newSECSItem = (U4SECSItem)secsItem;
        .
        . // Life is good now process the contents of the message.
        .
    }

```

The following may provide for faster processing if that is important:

```csharp
    if (secsItem.ItemFormatCode == SECSItemFormatCode.L)
    {
        // Oh, it is a List
        ListSECSItem newSECSItem = (ListSECSItem)secsItem;
        .
        . // Life is good now process the contents of the message.
        .
    }
    else if (secsItem.ItemFormatCode == SECSItemFormatCode.U4))
    {
        if (secsItem.LengthInBytes == 4)
        {
            // Oh, it is a U4
            U4SECSItem newSECSItem = (U4SECSItem)secsItem;
            .
            . // Life is good now process the contents of the message.
            .
        }
        else
        {
            // Oh, it is an array of zero or more U4s
            U4ArraySECSItem newSECSItem = (U4ArraySECSItem)secsItem;
            .
            . // Life is good now process the contents of the message.
            .
        }
    }
```

Generally a list (`L`) will be received as the payload of an incoming message,
however, that is not always the case.  An example is an older form of an `S1F3`
message that is sent as an array of `U1` - `U8` or `I1` - `I8`.

In the real world, when a host or piece of equipment sends an array (SECS item)
that has only 1
element in it (we are not talking about type `A` or `B` here) it almost always means that
the item is not to be thought of as an array of 1 item, but, as just the item.

Think of the temperature for a single processing chamber.  You might receive an
item of type `F4`.  In actuality, it is received as an array of `F4`s that
contains 1 element.
This API, when it decodes the incoming stream of data will produce a `F4SECSItem`
instead of producing an `F4ArraySECSItem` containing only 1 element.  The same
pretty much applies to most of the rest of the available SECS item types with
`L`, `A`, and `B` being notable exceptions.  If the received item has a length
of zero the array form, in this example `F4ArraySECSItem`, will be returned.

SECS item types `L`, `A` should be easy to figure out while type `B` is just always
treated as an array of bytes (a `BinarySECSItem`).

### Sending a SECS message

This API does not provide the functionality to be able to send and receive
SECS messages.  It only provides the functionality to be able to encode, decode,
and process the SECS item data that may be in the message.
Check out the 'SECSCommUtils' API for send and receive type functionality.

Preparing the data to be sent is much easier.  All that needs to be done is to
call the `SECSItems`'s `EncodeForTransport` method.  This will return a `byte[]`
value of the `SECSItem` that is in a transmission form.  If this method is called
on a `ListSECSItem` all of the contents of the list and any sub-lists will be 
encoded.  The resulting `byte[]` can be appended to the end of a SECS message
header and submitted to the transport layer for message delivery.

## Constructors

There are basically two types of `SECSItem`s implemented in this API.  They are value
types and reference types.  If the constructor argument is a value type
(`bool`, `float`, `double`, `UInt64`, etc.) the `SECSItem` will have two constructors
available.  For example:

```csharp
    F4SECSItem secsItem = new F4SECSItem(3.141592);

    or

    F4SECSItem secsItem = new F4SECSItem(3.141592, SECSItemNumLengthBytes.TWO);
```

The first form creates a `F4SECSItem` with the specified value and sets it up to
use one length byte for the data item length when it is encoded for transport.

The second form creates a `F4SECSItem` with the specified value and sets it up to
use the specified number of length bytes for the data item length when it is
encoded for transport.  The number of length bytes will be set to be the **greater
of** the number of length bytes specified or the number it actually takes to
contain the contents of the `SECSItem`s value.

This second form of the constructor is not needed much nowadays.  In the past
there were situations where the equipment required that messages
contained SECSItems that had a specified number of length bytes.
This form of the constructor is here to handle these problem child cases.

The `SECSItem`s that take object references as their arguments also have the
two forms of constructors mentioned above.  In addition they have a form taking
no arguments.  For example:

```csharp
    F4ArraySECSItem secsItem = new F4ArraySECSItem();
```

This creates a `SECSItem` that has a zero length.  This does not
seem to happen too often, but, there are times when it is needed.  If you need
to create a `SECSItem` with a zero length you will need to use the form
of the object that takes object references as constructor argument.
Another example:

```csharp
    BooleanArraySECSItem secsItem = new BooleanArraySECSItem();
    vs
    BooleanSECSItem secsItem = new BooleanSECSItem(false);

    or

    U8ArraySECSItem secsItem = new U8ArraySECSItem();
    vs
    U8SECSItem secsItem = new U8SECSItem(0);

```

## General Rules

- When a `SECSItem` is created using data in transmission format it will end up with the
number of length bytes specified in the data.
- When a `SECSItem` is created using other constructor forms it will be created with
the minimum number of length bytes possible to contain the data. If the number of length bytes is
specified it will use the number specified **or** a number that will be able  to
contain the data.  Whichever is larger.
- If you need to send an item with a length of zero use an Array form and pass
a zero length array or `null` for the data argument.

## Creating zero length items

If the situation requires and you need to create zero length items use the reference types
(array form) of the item you with to create / send.
