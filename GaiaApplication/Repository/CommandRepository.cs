using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

public class CommandRepository
{
    private readonly AppDbContext _context;

    public CommandRepository(AppDbContext context)
    {
        _context = context;
    }

    // Get all commands
    public async Task<List<Command>> GetAllCommandsAsync()
    {
        return await _context.Commands.ToListAsync();
    }



    // Get command by ID
    public async Task<Command?> GetCommandByIdAsync(string id)
    {
        return await _context.Commands.FindAsync(id);
    }



    // Add new command
    public async Task AddCommandAsync(Command command)
    {
        command.Id = Guid.NewGuid().ToString();
        _context.Commands.Add(command);
        await _context.SaveChangesAsync();
    }

    // Update an existing command
    public async Task UpdateCommandAsync(Command command)
    {
        _context.Commands.Update(command);
        await _context.SaveChangesAsync();
    }

    // Delete a command
    public async Task DeleteCommandAsync(int id)
    {
        var command = await _context.Commands.FindAsync(id);
        if (command != null)
        {
            _context.Commands.Remove(command);
            await _context.SaveChangesAsync();
        }
    }
}
