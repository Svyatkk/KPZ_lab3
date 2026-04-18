namespace KPZ_MK1
{
    // ============================================================
    //  State — element visibility (Visible / Hidden)
    // ============================================================
    //
    //  LightElementNode delegates rendering decisions to the
    //  current IElementState, switching between VisibleState and
    //  HiddenState without any if/else in the node itself.
    // ============================================================

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
        // Renders with display:none injected into the style attribute
        public string WrapHtml(string tagName, string classes, string inner)
            => $"<{tagName}{classes} style=\"display:none\">{inner}</{tagName}>";
    }

    // Partial extension of LightElementNode to add State support
    public partial class LightElementNode
    {
        private IElementState _state = new VisibleState();

        public string CurrentState => _state.Name;

        public void Show() => _state = new VisibleState();
        public void Hide() => _state = new HiddenState();

        // Override OuterHTML to use the current state for rendering
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
