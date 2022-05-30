using AutoMapper;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WSSDepartments.DatabaseLayer;
using WSSDepartments.Services;
using WSSDepartments.UILayer;

namespace WSSDepartments.Tests
{
	[TestFixture]
	public class XmlSerializeTests
	{
		private IXmlSerializeService _xmlSerializeService;
		private readonly IDatabaseProvider _databaseProvider;
		private IMapper _mapper;
		private string _fileName = "testData.xml";

		[SetUp]
		public void SetUp() {
			_xmlSerializeService = new XmlSerializeService();

			IConfigurationProvider configuration = new MapperConfiguration((cfg) =>
			{
				cfg.AddProfile(new AppMappingProfile());
			});
			_mapper = new Mapper(configuration);
		}

		[Test]
		public void PositiveXmlSerialize()
		{
			SerializeDataToFile();

			Assert.IsTrue(File.Exists(_fileName));
		}


		[Test]
		public void PositiveXmlDeSerialize()
		{
			SerializeDataToFile();			

			CompanyList companies;
			using (var stream = new MemoryStream())
			{
				using (FileStream file = new FileStream(_fileName, FileMode.Open, FileAccess.Read))
				{
					file.CopyTo(stream);
					stream.Position = 0;
					companies = _xmlSerializeService.DeSerializeFromMemoryStream<CompanyList>(stream);
				}
			}

			List<Company> companiesUI = _mapper.Map<List<Company>>(TestData.DT_Companies);

			//Тут следовало бы переопределить свойства Equals и GetHashCode у объектов и сравнивать коллекции напрямую 
			Assert.IsTrue(companiesUI.Count.Equals(companies.Companies.Count));
			Assert.IsTrue(companiesUI.FirstOrDefault().CompanyId.Equals(companies.Companies.FirstOrDefault().CompanyId));
		}

		private void SerializeDataToFile() {
			List<Company> companiesUI = _mapper.Map<List<Company>>(TestData.DT_Companies);
			MemoryStream memoryStream = _xmlSerializeService.SerializeToMemoryStream<CompanyList>(new CompanyList { Companies = companiesUI });
			memoryStream.Position = 0;

			using (FileStream file = new FileStream(_fileName, FileMode.Create, System.IO.FileAccess.Write))
			{
				byte[] bytes = new byte[memoryStream.Length];
				memoryStream.Read(bytes, 0, (int)memoryStream.Length);
				file.Write(bytes, 0, bytes.Length);
				memoryStream.Close();
			}
		}
	}
}
