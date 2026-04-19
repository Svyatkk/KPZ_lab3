namespace KPZ_MK1
{

    public interface IElementState
    {
        string Name { get; }
        string WrapHtml(string tagName, string classes, string inner);
    }

    public class VisibleState : IElementState
    {
        public string Name => "Visible";
        public string WrapHtml(string tagName, string classes, string inner)
            => $"<{tagName}{classes}>{inner}</{tagName}>";
    }

    public class HiddenState : IElementState
    {
        public string Name => "Hidden";
        public string WrapHtml(string tagName, string classes, string inner)
            => $"<{tagName}{classes} style=\"display:none\">{inner}</{tagName}>";
    }

    public partial class LightElementNode
    {
        private IElementState _state = new VisibleState();

        public string CurrentState => _state.Name;

        public void Show() => _state = new VisibleState();
        public void Hide() => _state = new HiddenState();

        public string StateHTML
        {
            get
            {
                string classes = CssClasses.Count > 0
                    ? $" class=\"{string.Join(" ", CssClasses)}\""
                    : "";
                if (ClosingType == "single") return $"<{TagName}{classes}/>";
                return _state.WrapHtml(TagName, classes, InnerHTML);
            }
        }
    }
}
