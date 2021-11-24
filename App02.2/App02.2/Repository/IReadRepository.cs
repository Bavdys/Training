using System.Collections.Generic;

namespace App02._2
{
    public interface IReadRepository
    {
       List<Login> Read(string path);
    }
}
