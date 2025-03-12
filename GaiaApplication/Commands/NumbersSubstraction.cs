namespace GaiaApplication.Commands
{
    public class NumbersSubstraction : ICommand
    {
        private readonly CommandOnInput _commandOnInput;
        private int _number_a, _number_b;

        public NumbersSubstraction(CommandOnInput commandOnInput, int a, int b)
        {
            _commandOnInput = commandOnInput;
            _number_a = a;
            _number_b = b;
        }

        public Object Execute()
        {
            return _commandOnInput.SubstractNumbers(_number_a, _number_b);
        }
    }
}
