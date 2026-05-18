using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/robot-commands")]
public class RobotCommandsController : ControllerBase
{
    private static List<RobotCommand> commands = new()
    {
        new RobotCommand(1, "LEFT", true, DateTime.Now, DateTime.Now),
        new RobotCommand(2, "RIGHT", true, DateTime.Now, DateTime.Now),
        new RobotCommand(3, "MOVE", true, DateTime.Now, DateTime.Now),
        new RobotCommand(4, "PLACE", false, DateTime.Now, DateTime.Now),
        new RobotCommand(5, "REPORT", false, DateTime.Now, DateTime.Now)
    };

    [HttpGet]
    public IEnumerable<RobotCommand> GetAll() => commands;

    [HttpGet("move")]
    public IEnumerable<RobotCommand> GetMove()
        => commands.Where(c => c.IsMoveCommand);

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var cmd = commands.FirstOrDefault(c => c.Id == id);
        if (cmd == null) return NotFound();
        return Ok(cmd);
    }

    [HttpPost]
    public IActionResult Add(RobotCommand newCommand)
    {
        if (newCommand == null) return BadRequest();

        if (commands.Any(c => c.Name == newCommand.Name))
            return Conflict();

        int newId = commands.Count + 1;

        var cmd = new RobotCommand(newId, newCommand.Name, newCommand.IsMoveCommand, DateTime.Now, DateTime.Now, newCommand.Description);

        commands.Add(cmd);

        return Ok(cmd);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, RobotCommand updated)
    {
        var cmd = commands.FirstOrDefault(c => c.Id == id);
        if (cmd == null) return NotFound();

        cmd.Name = updated.Name;
        cmd.Description = updated.Description;
        cmd.IsMoveCommand = updated.IsMoveCommand;
        cmd.ModifiedDate = DateTime.Now;

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var cmd = commands.FirstOrDefault(c => c.Id == id);
        if (cmd == null) return NotFound();

        commands.Remove(cmd);
        return NoContent();
    }
}