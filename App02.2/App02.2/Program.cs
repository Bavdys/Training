using System;

namespace App02._2
{
    class Program
    {
        static void Main(string[] args)
        {
            Configuration configuration = new Configuration();

            try
            {
                IReadRepository xmlRead = new XMLRepository();
                configuration.LoadFromFile(@"Config\XMLConfiguration.xml", xmlRead);

                Console.WriteLine("Logins:");
                Console.WriteLine(configuration.ToString());

                Console.WriteLine("Incorrect Loggins:");
                Console.WriteLine(configuration.IncorrectLogins());

                IWriteRepository jsonWrite = new JSONRepository();
                configuration.SaveLoginsConfigurationToJson(jsonWrite);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
