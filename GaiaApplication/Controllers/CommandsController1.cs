using System.Runtime.InteropServices.JavaScript;
using GaiaApplication.Commands;
using GaiaApplication.Models;
using GaiaApplication.Services;
using Microsoft.AspNetCore.Mvc;
using NPOI.OpenXmlFormats.Dml;
using NPOI.XWPF.UserModel;

namespace GaiaApplication.Controllers
{

    

    [Route("api/[controller]")]
    [ApiController]
    public class CommandsController : Controller
    {
        //if we need to add new commands we do it here ,add new string
        private readonly List<string> Commands = new List<string> { "concat", "add", "substract" };
        private readonly CommandService _commandService;

        public CommandsController(CommandService command)
        {
            _commandService = command;
        }

        [HttpPost("execute")]
        public async Task<IActionResult> Execute([FromBody] JsonRequestBody body)
        {

            //We use the Command design pattern to encapsulates the requests. In that way it will be easier to add or remove commands in the feauture. The client get the 
            //commands list, and do not know anything about their implementation

            // Calculate the result.Get the input string from the user.And Instantiate the right Command class from reciever.

            CommandsInvoker invoker = new CommandsInvoker();
            CommandOnInput reciever= new CommandOnInput();

            string result = "";
            var LatestCommands = new List<Command>();
            var MonthlyCommands = new List<Command>();
            var cmnd = new Command(body.input_a, body.input_b, body.command, result);

            //if we need to add new commands we do it here , add new case and add new Command class implemanting ICommand and set the reciver command.
            switch (body.command)
            {
                case "add":
                    invoker.SetCommand(new NumbersAddition(reciever, Convert.ToInt32(body.input_a), Convert.ToInt32(body.input_b)));
                    result = invoker.DoSomthing<int>().ToString();      
                    await _commandService.WriteCommandToDB(cmnd);
                    //return as long with the result 3 last actions from the same type
                    LatestCommands = await _commandService.Get3LatestCommandsOfType(cmnd.CommandText);
                    MonthlyCommands = await _commandService.GetMonthlyCommandsOfType(cmnd.CommandText);
                    break;


                case "substract":
                    invoker.SetCommand(new NumbersSubstraction(reciever, Convert.ToInt32(body.input_a), Convert.ToInt32(body.input_b)));
                    result = invoker.DoSomthing<int>().ToString();
                    await _commandService.WriteCommandToDB(cmnd);
                    //return as long with the result 3 last actions from the same type
                    LatestCommands = await _commandService.Get3LatestCommandsOfType(cmnd.CommandText);
                    MonthlyCommands = await _commandService.GetMonthlyCommandsOfType(cmnd.CommandText);
                    break;


                case "concat":
                    invoker.SetCommand(new StringConcantation(reciever, body.input_a, body.input_b));
                    result = invoker.DoSomthing<string>();
                    await _commandService.WriteCommandToDB(cmnd);
                    //return as long with the result 3 last actions from the same type
                    LatestCommands = await _commandService.Get3LatestCommandsOfType(cmnd.CommandText);
                    MonthlyCommands = await _commandService.GetMonthlyCommandsOfType(cmnd.CommandText);
                    break;

                default:
                    return BadRequest("command not found");

            }

            var response = new
            {
                result,
                LatestCommands,
                MonthlyCommands
            };
            return Ok(response);
        }



        [HttpGet("getCommands")]
        public IActionResult getCommands()
        {
            return Ok(Commands);
        }
    }
}
