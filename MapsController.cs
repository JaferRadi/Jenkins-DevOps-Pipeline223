using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/maps")]
public class MapsController : ControllerBase
{
    private static List<Map> maps = new()
    {
        new Map(1, 5, 5, "MOON", DateTime.Now, DateTime.Now),
        new Map(2, 10, 10, "EARTH", DateTime.Now, DateTime.Now)
    };

    [HttpGet]
    public IEnumerable<Map> GetAll() => maps;

    [HttpGet("square")]
    public IEnumerable<Map> GetSquare()
        => maps.Where(m => m.Rows == m.Columns);

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var map = maps.FirstOrDefault(m => m.Id == id);
        if (map == null) return NotFound();
        return Ok(map);
    }

    [HttpPost]
    public IActionResult Add(Map newMap)
    {
        if (newMap == null) return BadRequest();

        int newId = maps.Count + 1;

        var map = new Map(newId, newMap.Columns, newMap.Rows, newMap.Name, DateTime.Now, DateTime.Now, newMap.Description);

        maps.Add(map);

        return Ok(map);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, Map updated)
    {
        var map = maps.FirstOrDefault(m => m.Id == id);
        if (map == null) return NotFound();

        map.Name = updated.Name;
        map.Columns = updated.Columns;
        map.Rows = updated.Rows;
        map.ModifiedDate = DateTime.Now;

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var map = maps.FirstOrDefault(m => m.Id == id);
        if (map == null) return NotFound();

        maps.Remove(map);
        return NoContent();
    }

    [HttpGet("{id}/{x}-{y}")]
    public IActionResult CheckCoordinate(int id, int x, int y)
    {
        if (x < 0 || y < 0) return BadRequest();

        var map = maps.FirstOrDefault(m => m.Id == id);
        if (map == null) return NotFound();

        bool isOnMap = x < map.Columns && y < map.Rows;

        return Ok(isOnMap);
    }
}