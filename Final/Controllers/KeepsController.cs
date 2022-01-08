using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using Final.Models;
using Final.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Final.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class KeepsController : ControllerBase
  {
    private readonly KeepsService _ks;

    public KeepsController(KeepsService ks)
    {
      _ks = ks;
    }

    [HttpPost]
    [Authorize]

    public async Task<ActionResult<Keep>> Create([FromBody] Keep newKeep)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        newKeep.CreatorId = userInfo.Id;
        Keep keep = _ks.Create(newKeep);
        keep.Creator = userInfo;
        return Ok(keep);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet]

    public ActionResult<List<Keep>> GetAll()
    {
      try
      {
        return Ok(_ks.GetAll());
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{id}")]
    public ActionResult<Keep> GetById(int id)
    {
      try
      {
        return Ok(_ks.GetById(id));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpPut("{id}")]
    [Authorize]

    public async Task<ActionResult<Keep>> Edit([FromBody] Keep editKeep, int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        editKeep.CreatorId = userInfo.Id;
        editKeep.Id = id;
        Keep editedKeep = _ks.Edit(editKeep, userInfo.Id);
        return Ok(editedKeep);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpDelete("{id}")]
    [Authorize]

    public async Task<ActionResult<string>> RemoveAsync(int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        _ks.Remove(id, userInfo.Id);
        return Ok("Keep has been Deleted!");
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

  }
}