using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WSSDepartments.BusinessLayer.DataTransferObjects;
using WSSDepartments.DatabaseLayer;
using WSSDepartments.Tests;
using WSSDepartments.UILayer;

namespace WSSDepartments.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class CompanyController : ControllerBase
	{
		private readonly ILogger<CompanyController> _logger;
		private readonly IMapper _mapper;
		private readonly IDatabaseProvider _databaseProvider;

		public CompanyController(ILogger<CompanyController> logger, IMapper mapper, IDatabaseProvider databaseProvider)
		{
			_logger = logger;
			_mapper = mapper;
			_databaseProvider = databaseProvider;
		}

		[HttpGet]
		public async Task<IEnumerable<Company>> Get()
		{
			//for test you can use it for return test data
			//return _mapper.Map<List<Company>>(TestData.DT_Companies);

			return _mapper.Map<List<Company>>(await _databaseProvider.GetCompaniesAsync());
		}

		[HttpPost]
		public async Task<List<Company>> Post(Company company) {
			return _mapper.Map<List<Company>>(await _databaseProvider.SaveCompanyAsync(_mapper.Map<DT_Company>(company)));
		}

		[HttpDelete]
		public async Task<List<Company>> Delete(Company company)
		{
			throw new Exception("Not implemented");
		}
	}
}
