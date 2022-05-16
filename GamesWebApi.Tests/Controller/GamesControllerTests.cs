using System.Collections.Generic;
using System.Threading.Tasks;
using FakeItEasy;
using FluentAssertions;
using GamesApi.Controllers;
using GamesApi.Models;
using GamesApi.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace GamesWebApi.Tests.Controller;

public class GamesControllerTests
{
	private readonly IGamesServices _services;
	private readonly GamesController _sut;
	
	public GamesControllerTests()
	{
		_services = A.Fake<IGamesServices>();
		
		// SUT - System Under Test
		_sut = new GamesController(_services);
	}

	[Fact]
	public void GamesController_GetGames_ReturnsGames()
	{
		// Arrange
		var response = A.Fake<Response<IEnumerable<Game>>>();
		A.CallTo(() => _services.GetGamesByLimit(10, 0)).Returns(response);

		// Act 
		var result = _sut.GetGames();

		// Assert
		result
			.Should()
			.BeOfType<Task<ActionResult>>();
	}
	
	[Fact]
	public void GamesController_GetGameNames_ReturnsGames()
	{
		// Arrange
		var response = A.Fake<Response<IEnumerable<Game>>>();
		A.CallTo(() => _services.GetGamesByLimit(10, 0)).Returns(response);

		// Act 
		var result = _sut.GetGameNames();

		// Assert
		result
			.Should()
			.BeOfType<Task<ActionResult>>();
	}
	
}