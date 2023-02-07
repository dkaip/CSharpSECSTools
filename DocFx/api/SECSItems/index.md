# SECSItems

It is to be understood that if you are here reading this that you already are
knowledgeable of the SECS standards pertaining to this problem domain.

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
`A`,`B`, and `BO` then, yes, the value of the length bytes is the number of elements
in the array.  For the other SECS item types you will need to do the division.

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

In the real world, when a host or piece of equipment sends an array (SECS item)
that has only 1
element in it (we are not talking about type `A` here) it almost always means that
the item is not to be thought of as an array of 1 item, but, as just the item.

Think of the temperature for a single processing chamber.  You might receive an
item of type `F4`.  Well, in actuality, it is received as an array of `F4`s that
contains 1 element.
This API, when it decodes the incoming stream of data will produce a `F4SECSItem`
instead of producing an `F4ArraySECSItem` containing only 1 element.  The same
pretty much applies to most of the rest of the available SECS item types with
`L`, `A`, and `B` being notable exceptions.

SECS item types `L`, `A` should be easy to figure out while type `B` is just always
treated as an array of bytes.

``` C#
        /// This form of the constructor is not needed much nowadays.  In the past
        /// there were situations where the equipment required that messages
        /// contained SECSItems that had a specified number of length bytes.
        /// This form of the constructor is here to handle these problem child cases.
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
