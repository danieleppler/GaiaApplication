using NPOI.SS.Formula.Functions;

namespace GaiaApplication.Commands
{
    public class CommandsInvoker
    {
        private ICommand _command;

        public void SetCommand(ICommand command)
        {
            _command = command;
        }

        public T DoSomthing<T>()
        {
           return (T) _command.Execute();
        }

    }
}
