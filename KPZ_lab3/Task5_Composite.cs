namespace Task5_Composite
{
    public abstract class LightNode
    {
        public abstract string OuterHTML { get; }
        public abstract string InnerHTML { get; }
    }

    public class LightTextNode : LightNode
    {
        private string _text;
        public LightTextNode(string text) { _text = text; }
        public override string OuterHTML => _text;
        public override string InnerHTML => _text;
    }

    public class LightElementNode : LightNode
    {
        public string TagName { get; }
        public string DisplayType { get; }
        public string ClosingType { get; }
        public List<string> CssClasses { get; } = new List<string>();
        private List<LightNode> _children = new List<LightNode>();

        public LightElementNode(string tagName, string displayType, string closingType)
        {
            TagName = tagName; DisplayType = displayType; ClosingType = closingType;
        }

        public void Add(LightNode node) => _children.Add(node);

        public override string InnerHTML => string.Join("", _children.Select(c => c.OuterHTML));

        public override string OuterHTML
        {
            get
            {
                string classes = CssClasses.Any() ? $" class=\"{string.Join(" ", CssClasses)}\"" : "";
                if (ClosingType == "single") return $"<{TagName}{classes}/>";
                return $"<{TagName}{classes}>{InnerHTML}</{TagName}>";
            }
        }
    }
}