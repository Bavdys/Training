using System;

namespace App02._1
{
    /// <summary>
    /// Represent a class that contains the author's data.  
    /// </summary>
    public class Writer
    {
        const int LINE_LENGTH_STRING = 200;
        
        string _firstName;
        string _lastName;

        public Writer(string firstName, string lastName)
        {   
            FirstName = firstName;
            LastName = lastName;
        }
       
        public string FirstName 
        {
            get
            {
                return _firstName;
            }
            set
            {
                if (value.Length > LINE_LENGTH_STRING)
                {
                    throw new ArgumentOutOfRangeException($"The maximum line length is {LINE_LENGTH_STRING} characters");
                }

                _firstName = value;
            }
        }
        
        public string LastName 
        {
            get
            {
                return _lastName;
            }
            set
            {
                if (value.Length > LINE_LENGTH_STRING)
                {
                    throw new ArgumentOutOfRangeException($"The maximum line length is {LINE_LENGTH_STRING} characters");
                }

                _lastName = value;
            }
        }

        public override string ToString()
        {
            return string.Format($"{FirstName} {LastName}");
        }
        public override bool Equals(object obj)
        {
            return obj != null && FirstName == ((Writer)obj).FirstName && LastName == ((Writer)obj).LastName;
        }
    }
}
