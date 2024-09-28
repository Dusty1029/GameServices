﻿using CommonV2.Helpers.Controller;
using Game.Dto;
using GameService.API.BusinessLogics.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GameService.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class SerieController(IControllerExecutor controllerExecutor,
        ISerieBL serieBL) : ControllerBase
    {
        [HttpGet]
        public Task<IActionResult> GetAllSeries()
            => controllerExecutor.ExecuteAsync(this, () => serieBL.GetAllSeries());

        [HttpGet]
        [Route("{id}")]
        public Task<IActionResult> GetSerieById([FromRoute] Guid id)
            => controllerExecutor.ExecuteAsync(this, () => serieBL.GetSerieById(id));

        [HttpPost]
        public Task<IActionResult> CreateSerie([FromBody] CreateSerieDto createSerie)
            => controllerExecutor.ExecuteAsync(this, () => serieBL.CreateSerie(createSerie));
    }
}
