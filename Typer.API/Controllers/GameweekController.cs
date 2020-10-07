﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Typer.Application.Commands.Gameweek.CreateGameweek;
using Typer.Application.Commands.Gameweek.DeleteGameweek;
using Typer.Application.Commands.Gameweek.UpdateGameweek;
using Typer.Application.Queries.Gameweek.GetCurrentSeasonGameweeks;
using Typer.Application.Queries.Gameweek.GetGameweeksBySeasonId;

namespace Typer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameweekController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GameweekController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateGameweek([FromBody]CreateGameweekCommand command)
        {
            var id = await _mediator.Send(command);
            return Ok(id.GameweekId);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGameweek([FromRoute] int id)
            => Ok(await _mediator.Send(new DeleteGameweekCommand
            {
                GameweekId = id
            }));

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> UpdateGameweek([FromBody] UpdateGameweekCommand command)
            => Ok(await _mediator.Send(command));

        [HttpGet("getCurrentSeasonGameweeks")]
        public async Task<IActionResult> GetCurrentSeasonGameweeks()
            => Ok(await _mediator.Send(new GetCurrentSeasonGameweeksQuery { }));

        [HttpGet("getGameweeksBySeasonId/{id}")]
        public async Task<IActionResult> GetGameweeksBySeasonIdQuery([FromRoute] int id)
            => Ok(await _mediator.Send(new GetGameweeksBySeasonIdQuery
            {
                SeasonId = id
            }));
    }
}