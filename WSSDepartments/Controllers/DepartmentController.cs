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
	public class DepartmentController : ControllerBase
	{
		private readonly ILogger<CompanyController> _logger;
		private readonly IMapper _mapper;
		private readonly IDatabaseProvider _databaseProvider;

		public DepartmentController(ILogger<CompanyController> logger, IMapper mapper, IDatabaseProvider databaseProvider)
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
		public async Task<Department> Get()
		{
			throw new Exception("Not implemented");
		}

		/// <summary>
		/// Here I return all companies for update all data in UI
		/// </summary>
		/// <param name="department"></param>
		/// <returns></returns>
		[HttpPost]
		public async Task<List<Company>> Post(Department department)
		{
			throw new Exception("Not implemented");
		}

		[HttpDelete]
		public async Task<List<Company>> Delete(Department department)
		{
			throw new Exception("Not implemented");
		}
	}
}
