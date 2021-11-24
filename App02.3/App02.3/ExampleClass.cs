using Listener.Attributes;

namespace App02._3
{
    [TrackingEntity(true)]
    class ExampleClass
    {
        [TrackingProperty("firstName",false)]
        public string firstName;
        public string LastName { get; set; }
        [TrackingProperty("Age",true)]
        public int Age { get; set; }

        public ExampleClass(string firstName, string lastName, int age)
        {
            this.firstName = firstName;
            LastName = lastName;
            Age = age;
        }
    }
}
