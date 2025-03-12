﻿using Microsoft.AspNetCore.Mvc.Controllers;

namespace GaiaApplication.Commands
{
    public class NumbersAddition : ICommand
    {
        private readonly CommandOnInput _commandOnInput;
        private int _number_a, _number_b;

        public NumbersAddition(CommandOnInput commandOnInput , int a, int b)
        {
            _commandOnInput = commandOnInput;
            _number_a = a;
            _number_b = b;
        }

        public Object Execute()
        {
           return _commandOnInput.addNumbers(_number_a,_number_b);
        }
    }
}
