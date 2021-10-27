using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace App02._1
{
    /// <summary>
    /// Represent a class that contains the book's data.  
    /// </summary>
    public class Book
    {
        const int LINE_LENGTH_STRING = 1000;
        const string ISBN_FORMAT_PATTERN = @"^\d{13}$";
        const string ISBN_FORMAT_PATTERN_WITH_DASH = @"^\d{3}-\d-\d{2}-\d{6}-\d$";

        string _iSBN;
        string _title;

        private bool IsCorrectISBN(string iSBN)
        {
            return Regex.IsMatch(iSBN, ISBN_FORMAT_PATTERN) || Regex.IsMatch(iSBN, ISBN_FORMAT_PATTERN_WITH_DASH);
        }

        public Book(string name, string iSBN, DateTime date, List<Writer> avtors)
        {
            Title = name;
            ISBN = iSBN;
            Date = date;
            Avtors = avtors;
        }
       
        public string ISBN
        {
            get 
            {
                return _iSBN; 
            }
            private set
            {
                if (!IsCorrectISBN(value))
                {
                    throw new ArgumentException($"The {value} input is incorrect");
                }
                
                _iSBN = value.Replace("-","");
            }
        }
        public string Title
        {
            get 
            {
                return _title; 
            }
            private set
            {
                if (value.Length > LINE_LENGTH_STRING)
                {
                    throw new ArgumentException($"The maximum line length is {LINE_LENGTH_STRING} characters");
                }

                _title = value;
            }
        }
        public DateTime Date { get; }
        public List<Writer> Avtors { get; }

        public override bool Equals(object obj)
        {
            return obj != null && obj.GetType() == this.GetType() && (obj as Book).ISBN == this.ISBN;
        }
       
        public override string ToString()
        {
            StringBuilder stringAvtors = new StringBuilder();

            for(int i=0;i<Avtors.Count();i++)
            {
                stringAvtors.Append(Avtors[i]);

                stringAvtors.Append(i == Avtors.Count() - 1 ? "." : ", ");
            }

            return String.Format($"Title: {Title}.\nAvtors: {stringAvtors}\nDate: {Date}\nISBN: {ISBN}.\n\n");
        }
    }
}
