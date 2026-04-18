using System;
using System.Collections.Generic;
using System.Linq;

namespace KPZ_MK1
{

    public abstract class LightNode
    {
        public abstract string OuterHTML { get; }
        public abstract string InnerHTML { get; }
    }

    public class LightTextNode : LightNode
    {
        private readonly string _text;
        public LightTextNode(string text) { _text = text; }
        public override string OuterHTML => _text;
        public override string InnerHTML => _text;
    }

    public class LightElementNode : LightNode
    {
        public string TagName { get; }
        public string DisplayType { get; }
        public string ClosingType { get; }
        public List<string> CssClasses { get; } = new();
        protected readonly List<LightNode> _children = new();
        public int ChildrenCount => _children.Count;

        public LightElementNode(string tagName, string displayType, string closingType)
        {
            TagName = tagName;
            DisplayType = displayType;
            ClosingType = closingType;
        }

        public virtual void Add(LightNode node) => _children.Add(node);
        public virtual void Remove(LightNode node) => _children.Remove(node);

        public override string InnerHTML =>
            string.Join("", _children.Select(c => c.OuterHTML));

        public override string OuterHTML
        {
            get
            {
                string classes = CssClasses.Any()
                    ? $" class=\"{string.Join(" ", CssClasses)}\""
                    : "";
                if (ClosingType == "single") return $"<{TagName}{classes}/>";
                return $"<{TagName}{classes}>{InnerHTML}</{TagName}>";
            }
        }
    }
}
