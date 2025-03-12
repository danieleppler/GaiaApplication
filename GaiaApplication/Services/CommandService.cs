using Microsoft.EntityFrameworkCore;
using NPOI.POIFS.Storage;

namespace GaiaApplication.Services
{
    public class CommandService
    {
        private readonly CommandRepository _commandRepository;

        public CommandService(CommandRepository repo) {
            _commandRepository = repo;
        }

        public async Task WriteCommandToDB(Command command)
        {
            await _commandRepository.AddCommandAsync(command);
        }


        //get 3 latest commands
        public async Task<List<Command>> Get3LatestCommandsOfType(string type)
        {
            List<Command> commands = await _commandRepository.GetAllCommandsAsync();
            return commands.Where(c => c.CommandText == "concat").OrderByDescending(c => c.ExecutionTime).Take(3).ToList();
        }

        //get all command of the same type from start of the month
        public async Task<List<Command>> GetMonthlyCommandsOfType(string type)
        {
            List<Command> commands = await _commandRepository.GetAllCommandsAsync();
            return commands.Where(c => c.CommandText == "concat" && c.ExecutionTime.Month == DateTime.UtcNow.Month).OrderByDescending(c => c.ExecutionTime).ToList();
        }


    }
}
