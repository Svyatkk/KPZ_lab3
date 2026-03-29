namespace Task6_Flyweight
{
    using Task5_Composite;

    public class LightElementFactory
    {
        private Dictionary<string, LightElementNode> _nodes = new Dictionary<string, LightElementNode>();

        public LightElementNode GetElementNode(string tagName, string displayType, string closingType)
        {
            string key = $"{tagName}_{displayType}_{closingType}";
            if (!_nodes.ContainsKey(key))
            {
                _nodes[key] = new LightElementNode(tagName, displayType, closingType);
            }
            // Повертаємо новий екземпляр, але в реальному Flyweight ми б винесли 
            // стан (children) назовні. Для спрощення прикладу створюємо базу.
            return new LightElementNode(tagName, displayType, closingType); 
        }
    }

    public class BookToHtmlConverter
    {
        public static LightElementNode Convert(string[] textLines)
        {
            var factory = new LightElementFactory();
            var root = factory.GetElementNode("div", "block", "paired");

            for (int i = 0; i < textLines.Length; i++)
            {
                string line = textLines[i];
                LightElementNode node;

                if (i == 0) node = factory.GetElementNode("h1", "block", "paired");
                else if (line.Length < 20) node = factory.GetElementNode("h2", "block", "paired");
                else if (line.StartsWith(" ")) node = factory.GetElementNode("blockquote", "block", "paired");
                else node = factory.GetElementNode("p", "block", "paired");

                node.Add(new LightTextNode(line.Trim()));
                root.Add(node);
            }
            return root;
        }
    }
}