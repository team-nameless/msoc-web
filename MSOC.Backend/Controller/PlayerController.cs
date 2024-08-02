using Microsoft.AspNetCore.Mvc;
using MSOC.Backend.Service;

namespace MSOC.Backend.Controller;

[ApiController]
[Route("api/player")]
public class PlayerController : ControllerBase
{
    private MaimaiInquiryService _maimai;
    
    public PlayerController(MaimaiInquiryService maimai)
    {
        _maimai = maimai;
    }

    [HttpPost("query")]
    public async Task<IActionResult> QueryUserByFriendCode([FromForm] ulong? friendCode)
    {
        if (friendCode == null)
        {
            return BadRequest("Friend code is either null or empty.");
        }

        var needed = await _maimai.PerformFriendCodeLookup(friendCode);

        if (needed.Length == 0)
        {
            return BadRequest("Unable to retrieve needed information.");
        }
        
        var name = needed[0].TextContent;
        var rating = needed[1].TextContent;

        return new JsonResult(new Dictionary<string, string>
        {
            ["name"] = name,
            ["rating"] = rating
        });
    }
}