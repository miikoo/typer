﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Typer.Logic.Services;

namespace Typer.Logic.Commands.Season.CreateSeason
{
    public class CreateSeasonCommandHandler : IRequestHandler<CreateSeasonCommand, Unit>
    {
        private readonly ISeasonService _seasonService;

        public CreateSeasonCommandHandler(ISeasonService seasonService)
        {
            _seasonService = seasonService;
        }

        public async Task<Unit> Handle(CreateSeasonCommand request, CancellationToken cancellationToken)
        {
            await _seasonService.CreateSeason(request.startYear, request.endYear);
            return Unit.Value;
        }
    }
}
