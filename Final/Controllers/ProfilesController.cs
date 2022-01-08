using System;
using System.Collections.Generic;
using Final.Models;
using Final.Services;
using Microsoft.AspNetCore.Mvc;

namespace GoodEats.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class ProfilesController : ControllerBase
  {
    private readonly KeepsService _ks;

    public ProfilesController(KeepsService ks)
    {
      _ks = ks;
    }

    [HttpGet("{id}/keeps")]
    public ActionResult<List<Keep>> Get(string id)
    {
      try
      {
        List<Keep> keeps = _ks.GetByCreatorId(id);
        return Ok(keeps);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

  }
}