namespace KPZ_MK1
{
    

    public interface ILightNodeVisitor
    {
        void Visit(LightElementNode element);
        void Visit(LightTextNode text);
    }

    public partial class LightElementNode
    {
        public void Accept(ILightNodeVisitor visitor)
        {
            visitor.Visit(this);
            foreach (var child in _children)
            {
                if (child is LightElementNode el) el.Accept(visitor);
                else if (child is LightTextNode  tx) visitor.Visit(tx);
            }
        }
    }

    public class StatisticsVisitor : ILightNodeVisitor
    {
        private int _elementCount;
        private int _textNodeCount;
        private int _totalCharacters;

        public void Visit(LightElementNode element)
        {
            _elementCount++;
            Console.WriteLine($"[Visitor] Element: <{element.TagName}> children={element.ChildrenCount}");
        }

        public void Visit(LightTextNode text)
        {
            _textNodeCount++;
            _totalCharacters += text.OuterHTML.Length;
        }

        public void Visit(LightElementNode root, bool traverse = true)
        {
            if (traverse) root.Accept(this);
            else          Visit(root);
        }

        public void PrintReport()
        {
            Console.WriteLine($"[Visitor] Stats → elements: {_elementCount}, " +
                              $"text nodes: {_textNodeCount}, " +
                              $"total chars: {_totalCharacters}");
        }
    }
}
