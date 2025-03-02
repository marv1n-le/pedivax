using Microsoft.AspNetCore.Mvc;
using PediVax.BusinessObjects.DTO.ChildProfileDTO;
using PediVax.Services.IService;

namespace PediVax.Controllers;

[Microsoft.AspNetCore.Components.Route("api/child-profile")]
[ApiController]
public class ChildProfileController : ControllerBase
{
    private readonly IChildProfileService _childProfileService;

    public ChildProfileController(IChildProfileService childProfileService)
    {
        _childProfileService = childProfileService;
    }

    [HttpGet("get-all")]
    public async Task<IActionResult> GetAllChildProfiles()
    {
        try
        {
            var response = await _childProfileService.GetAllChildProfiles();

            if (response == null || response.Count == 0)
            {
                return NotFound();
            }

            return Ok(response);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("get-by-id/{childId}")]
    public async Task<IActionResult> GetChildProfileById(int childId)
    {
        try
        {
            var response = await _childProfileService.GetChildProfileById(childId);
            if (response == null)
                return NotFound();
            return Ok(response);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("get-child-paged/{pageNumber}/{pageSize}")]
    public async Task<IActionResult> GetChildProfilePaged(int pageNumber, int pageSize)
    {
        try
        {
            var (data, totalCount) = await _childProfileService.GetChildProfilePaged(pageNumber, pageSize);
            return Ok(new { Data = data, TotalCount = totalCount });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("get-by-name/{keyword}")]
    public async Task<IActionResult> GetChildProfileByName(string keyword)
    {
        try
        {
            var response = await _childProfileService.GetChildByName(keyword);
            if (response == null || response.Count == 0)
                return NotFound();
            return Ok(response);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost("create-child-profile")]
    public async Task<IActionResult> CreateChildProfile([FromForm] CreateChildProfileDTO createChildProfileDTO)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var response = await _childProfileService.CreateChildProfile(createChildProfileDTO);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("update-child-profile/{id}")]
    public async Task<IActionResult> UpdateChildProfile(int id, [FromForm] UpdateChildProfileDTO updateChildProfileDTO)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var response = await _childProfileService.UpdateChildProfile(id, updateChildProfileDTO);
            if (response == null)
                return NotFound();
            return Ok(response);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete("delete-child-profile/{childProfileId}")]
    public async Task<IActionResult> DeleteChildProfile(int childProfileId)
    {
        try
        {
            var response = await _childProfileService.DeleteChildProfile(childProfileId);
            if (!response)
                return NotFound();
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

}