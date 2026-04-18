using System.Collections.Generic;

namespace KPZ_MK1
{
    

    public partial class LightElementNode
    {
        public IEnumerable<LightNode> DepthFirstIterator()
        {
            yield return this;
            foreach (var child in _children)
            {
                if (child is LightElementNode el)
                    foreach (var desc in el.DepthFirstIterator())
                        yield return desc;
                else
                    yield return child;
            }
        }

        public IEnumerable<LightNode> BreadthFirstIterator()
        {
            var queue = new Queue<LightNode>();
            queue.Enqueue(this);
            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                yield return current;
                if (current is LightElementNode el)
                    foreach (var child in el._children)
                        queue.Enqueue(child);
            }
        }
    }
}
