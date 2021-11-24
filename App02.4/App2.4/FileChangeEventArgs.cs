using System;

namespace App2._4
{
    public class FileChangeEventArgs:EventArgs
    {
        public FileChangeEventArgs(string path)
        {
            if(string.IsNullOrEmpty(path))
            {
                throw new ArgumentException("Value cannot by empty");
            }

            Path = path;
        }
        public string Path { get; }
    }
}
