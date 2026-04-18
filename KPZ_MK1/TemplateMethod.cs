namespace KPZ_MK1
{
    // ============================================================
    //  Template Method — Lifecycle hooks
    // ============================================================
    //
    //  Render() is the TEMPLATE METHOD: it defines a fixed
    //  algorithm (OnCreated → build HTML → OnRendered) but lets
    //  subclasses override each individual hook.
    // ============================================================

    public partial class LightElementNode
    {
        // --- hook overrides (can be subclassed further) ---
        protected virtual void OnCreated()  =>
            Console.WriteLine($"[OnCreated]  <{TagName}>");

        protected virtual void OnRendered() =>
            Console.WriteLine($"[OnRendered] <{TagName}>");

        // Template Method
        public string Render()
        {
            OnCreated();
            string html = OuterHTML;
            OnRendered();
            return html;
        }
    }
}
