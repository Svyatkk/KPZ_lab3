using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Завдання 1: Адаптер");
        Task1_Adapter.ILogger logger = new Task1_Adapter.FileLoggerAdapter(new Task1_Adapter.FileWriter());
        logger.Warn("Test warning to file");

        Console.WriteLine("\nЗавдання 2: Декоратор");
        Task2_Decorator.IHero hero = new Task2_Decorator.Warrior();
        hero = new Task2_Decorator.WeaponDecorator(hero, "Sword");
        hero = new Task2_Decorator.ArmorDecorator(hero);
        Console.WriteLine(hero.GetDescription());

        Console.WriteLine("\nЗавдання 3: Міст");
        Task3_Bridge.Shape circle = new Task3_Bridge.Circle(new Task3_Bridge.RasterRenderer());
        circle.Draw();

        Console.WriteLine("\nЗавдання 4: Проксі");
        var checker = new Task4_Proxy.SmartTextChecker();
        var locker = new Task4_Proxy.SmartTextReaderLocker(checker, @"restricted.*\.txt");
        locker.Read("public_doc.txt");
        locker.Read("restricted_doc.txt"); 

        Console.WriteLine("\nЗавдання 5: Компонувальник");
        var ul = new Task5_Composite.LightElementNode("ul", "block", "paired");
        var li = new Task5_Composite.LightElementNode("li", "block", "paired");
        li.Add(new Task5_Composite.LightTextNode("Item 1"));
        ul.Add(li);
        Console.WriteLine(ul.OuterHTML);

        Console.WriteLine("\nЗавдання 6: Легковаговик");
        string[] bookText = { 
            "Romeo and Juliet", 
            "ACT V", 
            " Scene I. Mantua.", 
            "A long description of the scene that definitely exceeds twenty characters to become a paragraph." 
        };
        
        long memoryBefore = GC.GetTotalMemory(true);
        var htmlTree = Task6_Flyweight.BookToHtmlConverter.Convert(bookText);
        long memoryAfter = GC.GetTotalMemory(true);
        
        Console.WriteLine(htmlTree.OuterHTML);
        Console.WriteLine($"Використана пам'ять: {memoryAfter - memoryBefore} bytes");
    }
}