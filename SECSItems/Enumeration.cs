using System;
using System.Runtime.Serialization.Formatters;
namespace SECSItems
{
    public abstract class Enumeration : IComparable
    {
        private int _value;
        private string _name;

        public Enumeration (int value, string name)
        {
            _value = value;
            _name = name;
        }

        public int ValueOf ()
        {
            return _value;
        }

        public override string ToString ()
        {
            return _name;
        }

        public int CompareTo (object obj)
        {
            return _value.CompareTo (((Enumeration)obj)._value);
        }
    }
}
