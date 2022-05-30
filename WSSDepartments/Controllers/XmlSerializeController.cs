using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using WSSDepartments.BusinessLayer.DataTransferObjects;
using WSSDepartments.DatabaseLayer;
using WSSDepartments.Services;
using WSSDepartments.Tests;
using WSSDepartments.UILayer;

namespace WSSDepartments.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class XmlSerializeController : ControllerBase
	{
		private readonly IXmlSerializeService _xmlSerializeService;
		private readonly IDatabaseProvider _databaseProvider;
		private readonly IMapper _mapper;

		public XmlSerializeController(IXmlSerializeService xmlSerializeService, IDatabaseProvider databaseProvider, IMapper mapper) {
			_xmlSerializeService = xmlSerializeService;
			_databaseProvider = databaseProvider;
			_mapper = mapper;
		}
		
		[HttpGet]
		public async Task<IActionResult> Get()
		{
			//List<DT_Company> companies = TestData.DT_Companies;//
			List<DT_Company> companies = await _databaseProvider.GetCompaniesAsync();
			List<Company> companiesUI = _mapper.Map<List<Company>>(companies);
			MemoryStream memoryStream = _xmlSerializeService.SerializeToMemoryStream<CompanyList>(new CompanyList { Companies = companiesUI });

			return File(memoryStream.ToArray(), contentType: "text/xml", "Departments.xml");
		}

		[HttpPost]
		public async Task<List<Company>> Post(IFormFile xmlFile)
		{
			CompanyList companiesUI;
			using (var stream = new MemoryStream()) {
				xmlFile.CopyTo(stream);
				stream.Position = 0;
				companiesUI = _xmlSerializeService.DeSerializeFromMemoryStream<CompanyList>(stream);
			}					

			List<DT_Company> resultCompanyUI = await _databaseProvider.SaveCompaniesAsync(_mapper.Map<List<DT_Company>>(companiesUI.Companies));
			return _mapper.Map<List<Company>>(resultCompanyUI);
		}
	}
}
