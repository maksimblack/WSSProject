using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WSSDepartments.DatabaseLayer;
using WSSDepartments.UILayer;

namespace WSSDepartments.Controllers
{
	public class DivisionController : ControllerBase
	{
		private readonly ILogger<CompanyController> _logger;
		private readonly IMapper _mapper;
		private readonly IDatabaseProvider _databaseProvider;

		public DivisionController(ILogger<CompanyController> logger, IMapper mapper, IDatabaseProvider databaseProvider)
		{
			_logger = logger;
			_mapper = mapper;
			_databaseProvider = databaseProvider;
		}

		/// <summary>
		/// Get additional info
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public async Task<Division> Get()
		{
			throw new Exception("Not implemented");
		}

		/// <summary>
		/// Here I return all companies for update all data in UI
		/// </summary>
		/// <param name="division"></param>
		/// <returns></returns>
		[HttpPost]
		public async Task<List<Company>> Post(Division division)
		{
			throw new Exception("Not implemented");
		}

		[HttpDelete]
		public async Task<List<Company>> Delete(Division division)
		{
			throw new Exception("Not implemented");
		}
	}
}
