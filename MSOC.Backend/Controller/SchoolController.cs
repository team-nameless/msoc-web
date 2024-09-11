using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MSOC.Backend.Controller.RequestModel;
using MSOC.Backend.Database.Models;
using MSOC.Backend.Middleware;
using MSOC.Backend.Service;

namespace MSOC.Backend.Controller;

[Route("api/schools")]
[ApiController]
public class SchoolController : ControllerBase
{
    private readonly SchoolDatabaseService _schoolDatabase;

    public SchoolController(SchoolDatabaseService schoolDatabase)
    {
        _schoolDatabase = schoolDatabase;
    }

    /// <summary>
    ///     Get a school from given ID.
    /// </summary>
    /// <param name="schoolId">School ID, within range 1-256.</param>
    [HttpGet("get")]
    [ProducesResponseType(typeof(School), 200, "application/json")]
    public IActionResult GetSchool(
        [FromQuery(Name = "school_id")] int schoolId
    )
    {
        if (schoolId is < 1 or > 256) return BadRequest("ID can only be [1-256]");

        var foundedTrack = _schoolDatabase.Schools
            .AsNoTracking()
            .FirstOrDefault(track => track.Id == schoolId);

        if (foundedTrack == null) return NotFound();

        return Ok(foundedTrack);
    }

    /// <summary>
    ///     Add a school to database.
    /// </summary>
    /// <param name="school">School object.</param>
    [HttpPost("add")]
    [ApiKeyAuthorize]
    public IActionResult AddSchool([FromBody] SchoolRequestModel school)
    {
        _schoolDatabase.Schools.Add(new School
        {
            Name = school.Name,
            Type = school.Type
        });

        _schoolDatabase.SaveChanges();

        return Ok();
    }
}