namespace Task6_Flyweight
{
    using Task5_Composite;

    // Спільний стан (Intrinsic State) - Легковаговик
    public class TagInfoFlyweight
    {
        public string TagName { get; }
        public string DisplayType { get; }
        public string ClosingType { get; }

        public TagInfoFlyweight(string tagName, string displayType, string closingType)
        {
            TagName = tagName;
            DisplayType = displayType;
            ClosingType = closingType;
        }
    }

    // Фабрика легковаговиків
    public class TagInfoFactory
    {
        private Dictionary<string, TagInfoFlyweight> _flyweights = new Dictionary<string, TagInfoFlyweight>();

        public TagInfoFlyweight GetTagInfo(string tagName, string displayType, string closingType)
        {
            string key = $"{tagName}_{displayType}_{closingType}";
            if (!_flyweights.ContainsKey(key))
            {
                _flyweights[key] = new TagInfoFlyweight(tagName, displayType, closingType);
            }
            return _flyweights[key];
        }
    }

    // Контекстний вузол (Extrinsic State + посилання на Flyweight)
    public class LightElementNodeWithFlyweight : LightNode
    {
        private TagInfoFlyweight _tagInfo;
        public List<string> CssClasses { get; } = new List<string>();
        private List<LightNode> _children = new List<LightNode>();

        public LightElementNodeWithFlyweight(TagInfoFlyweight tagInfo)
        {
            _tagInfo = tagInfo;
        }

        public void Add(LightNode node) => _children.Add(node);

        public override string InnerHTML => string.Join("", _children.Select(c => c.OuterHTML));

        public override string OuterHTML
        {
            get
            {
                string classes = CssClasses.Any() ? $" class=\"{string.Join(" ", CssClasses)}\"" : "";
                if (_tagInfo.ClosingType == "single") return $"<{_tagInfo.TagName}{classes}/>";
                return $"<{_tagInfo.TagName}{classes}>{InnerHTML}</{_tagInfo.TagName}>";
            }
        }
    }

    public class BookToHtmlConverter
    {
        public static LightElementNodeWithFlyweight Convert(string[] textLines)
        {
            var factory = new TagInfoFactory();
            var root = new LightElementNodeWithFlyweight(factory.GetTagInfo("div", "block", "paired"));

            for (int i = 0; i < textLines.Length; i++)
            {
                string line = textLines[i];
                LightElementNodeWithFlyweight node;

                if (i == 0) node = new LightElementNodeWithFlyweight(factory.GetTagInfo("h1", "block", "paired"));
                else if (line.Length < 20) node = new LightElementNodeWithFlyweight(factory.GetTagInfo("h2", "block", "paired"));
                else if (line.StartsWith(" ")) node = new LightElementNodeWithFlyweight(factory.GetTagInfo("blockquote", "block", "paired"));
                else node = new LightElementNodeWithFlyweight(factory.GetTagInfo("p", "block", "paired"));

                node.Add(new LightTextNode(line.Trim()));
                root.Add(node);
            }
            return root;
        }
    }
}