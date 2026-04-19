namespace KPZ_MK1
{

    public partial class LightElementNode
    {
        protected virtual void OnCreated()  =>
            Console.WriteLine($"[OnCreated]  <{TagName}>");

        protected virtual void OnRendered() =>
            Console.WriteLine($"[OnRendered] <{TagName}>");

        public string Render()
        {
            OnCreated();
            string html = OuterHTML;
            OnRendered();
            return html;
        }
    }
}
