﻿using Microsoft.AspNetCore.Mvc;
using SRSWebApi.DTO;
using SRSWebApi.Interfaces;

namespace SRSWebApi.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class ProfessorController : Controller
	{
		private readonly IProfessorRepository _professorRepository;

		public ProfessorController(IProfessorRepository professorRepository)
		{
			_professorRepository = professorRepository;
		}

		[HttpGet]
		[ProducesResponseType(200)]
		public IActionResult GetProfessors()
		{
			var professors = _professorRepository.GetProfessors();
			return Ok(professors);
		}

		[HttpGet("{id}")]
		[ProducesResponseType(200)]
		public IActionResult GetProfessorById(int id)
		{
			var professor = _professorRepository.GetProfessorById(id);
			if (professor == null) return NotFound();
			return Ok(professor);
		}

		[HttpPost]
		[ProducesResponseType(200)]
		public IActionResult CreateProfessor([FromBody] ProfessorCreateDTO professor)
		{
			var result = _professorRepository.CreateProfessor(professor);
			return Ok(result);
		}

		[HttpPut("{id}")]
		[ProducesResponseType(200)]
		public IActionResult UpdateProfessor(int id, [FromBody] ProfessorUpdateDTO professor)
		{
			var result = _professorRepository.UpdateProfessor(id, professor);
			return Ok(result);
		}

		[HttpDelete("{id}")]
		[ProducesResponseType(200)]
		public IActionResult DeleteProfessor(int id)
		{
			var result = _professorRepository.DeleteProfessor(id);
			return Ok(result);
		}
	}
}