namespace GaiaApplication.Commands
{
    public class StringConcantation : ICommand
    {
        private readonly CommandOnInput _commandOnInput;
        private string _string_a, _string_b;

        public StringConcantation(CommandOnInput commandOnInput, string a, string b)
        {
            _commandOnInput = commandOnInput;
            _string_a = a;
            _string_b = b;
        }

        public Object Execute()
        {
            return _commandOnInput.concatStrings(_string_a, _string_b);
        }
    }
}
