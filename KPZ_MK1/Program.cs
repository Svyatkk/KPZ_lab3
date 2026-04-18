using System;

namespace KPZ_MK1
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("=== KPZ MK1 — LightHTML ===");
            var div = new LightElementNode("div", "block", "paired");
            div.CssClasses.Add("container");
            div.Add(new LightTextNode("Hello LightHTML!"));
            Console.WriteLine(div.OuterHTML);
        }
    }
}
