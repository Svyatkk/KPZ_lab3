using System.Collections.Generic;

namespace KPZ_MK1
{
   
    public interface ICommand
    {
        void Execute();
        void Undo();
    }

    /// <summary>Command: add a child node to a parent element.</summary>
    public class AddNodeCommand : ICommand
    {
        private readonly LightElementNode _parent;
        private readonly LightNode _child;

        public AddNodeCommand(LightElementNode parent, LightNode child)
        {
            _parent = parent;
            _child  = child;
        }

        public void Execute() => _parent.Add(_child);
        public void Undo()    => _parent.Remove(_child);
    }

    /// <summary>Invoker: keeps history and enables Undo.</summary>
    public class CommandHistory
    {
        private readonly Stack<ICommand> _history = new();

        public void Execute(ICommand cmd)
        {
            cmd.Execute();
            _history.Push(cmd);
        }

        public void Undo()
        {
            if (_history.Count > 0)
                _history.Pop().Undo();
        }
    }
}
