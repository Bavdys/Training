
namespace App02._2
{
    
    public class Window
    {
        public Window() { }

        public Window(string title, int? top, int? left, int? width, int? height)
        {
            Title = title;
            Top = top ?? 0;
            Left = left ?? 0;
            Width = width ?? 400;
            Height = height ?? 150;
        }
        
        public string Title { get; set; }   
        public int? Top { get; set; }
        public int? Left { get; set; }
        public int? Width { get; set; }
        public int? Height { get; set; }

        public override string ToString()
        {
            return string.Format($"{Title}({(Top.HasValue ? Top.ToString() : " ? ")}, " +
                        $"{(Left.HasValue ? Left.ToString() : "?")}, {(Width.HasValue ? Width.ToString() : "?")}, " +
                        $"{(Height.HasValue ? Height.ToString() : "?")})");
        }
    }
}
