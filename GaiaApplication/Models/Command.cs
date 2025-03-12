using System.ComponentModel.DataAnnotations;

public class Command
{

    public Command()
    {

    }
    public Command(string input_a, string input_b, string command, string result)
    {
        this.InputA = input_a;
        this.InputB = input_b;
        this.CommandText = command;
        this.Result = result;
    }

    [Key]
    public string Id { get; set; }  
    public string InputA { get; set; }
    public string InputB { get; set; }
    public string CommandText { get; set; }
    public string Result { get; set; }
    public DateTime ExecutionTime { get; set; } = DateTime.UtcNow; 
}
