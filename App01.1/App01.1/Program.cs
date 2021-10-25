using System;

namespace App01._1
{
    class Program
    {
        static void Main(string[] args)
        {
            TextMaterial textMaterial = new TextMaterial("Text material content", "Text discription content");
            VideoMaterial videoMaterial = new VideoMaterial("URI Content", VideoFormat.Avi, new byte[] { 5, 65, 77, 34, 98, 44, 150, 15 },
                "URI Image", "Text discription content");
            NetworkResource networkResource = new NetworkResource("URI Content", TypeLink.Audio, "Text discription content");

            Material[] material = new Material[] { textMaterial, videoMaterial, networkResource };
            
            Lesson lesson = new Lesson(material,new byte[] { 77,50,120,70,46,29,22,55},"Text discription content");

            Console.WriteLine("Lesson materials:");
            ShowMaterials(lesson.Materials);

            Console.WriteLine("Lesson:");
            Console.WriteLine(lesson.ToString());

            Console.WriteLine("LessonType:");
            Console.WriteLine(lesson.GetLessonType().ToString());
        }
        static void ShowMaterials(Material[] materials)
        {
            foreach (var item in materials)
                Console.WriteLine(item);
        }
    }
}
