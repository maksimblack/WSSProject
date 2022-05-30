using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSSDepartments.BusinessLayer.DataTransferObjects;
using WSSDepartments.DatabaseLayer.Models;

namespace WSSDepartments.DatabaseLayer
{
	public class DatabaseProvider: IDatabaseProvider
	{
		private readonly IDbContextFactory<WSSDepartmentsContext> _contextFactory;
		private readonly IMapper _mapper;

		public DatabaseProvider(IDbContextFactory<WSSDepartmentsContext> contextFactory, IMapper mapper) {
			_contextFactory = contextFactory;
			_mapper = mapper;
		}

		public async Task<List<DT_Company>> GetCompaniesAsync() {
			using (var dbContext = _contextFactory.CreateDbContext()) 
			{
				List<Company> companies = await dbContext.Companies
				.Include(c => c.Departments)
				.ThenInclude(c => c.Divisions)
				.ToListAsync();

				return _mapper.Map<List<DT_Company>>(companies);
			}
		}

		#region SaveCompaniesAsync

		/// <summary>
		/// This method use for update departments structure(CRUD)
		/// </summary>
		/// <param name="dtCompanies"></param>
		/// <returns></returns>
		public async Task<List<DT_Company>> SaveCompaniesAsync(List<DT_Company> dtCompanies)
		{
			using (var dbContext = _contextFactory.CreateDbContext())
			{
				using (var transaction = await dbContext.Database.BeginTransactionAsync(System.Data.IsolationLevel.ReadCommitted)) 
				{
					int[] companyForUpdate = dtCompanies.Where(x => x.CompanyId != 0)
								.Select(x => x.CompanyId)
								.ToArray();

					List<Company> companiesForRemove = dbContext.Companies.Include(x => x.Departments).ThenInclude(x => x.Divisions)
						.Where(x => !companyForUpdate.Contains(x.CompanyId))
						.ToList();

					RemoveCompany(dbContext, companiesForRemove);
					foreach (var dtCompanyItem in dtCompanies)
					{
						await UpdateCompanyAsync(dbContext, dtCompanyItem);
					}
					dbContext.SaveChanges();
					transaction.Commit();
				}

				List<Company> resultCompanies = await dbContext.Companies
					.Include(c => c.Departments)
					.ThenInclude(c => c.Divisions)
					.ToListAsync();

				return _mapper.Map<List<DT_Company>>(resultCompanies);
			}
		}

		private async Task UpdateCompanyAsync(WSSDepartmentsContext dbContext, DT_Company dtCompanyItem) 
		{
			Company company = _mapper.Map<Company>(dtCompanyItem);
			var companyDatabase = await dbContext.Companies
							.FirstOrDefaultAsync(x => x.CompanyId == company.CompanyId);

			if (companyDatabase != null)
			{
				companyDatabase.Name = company.Name;
				companyDatabase.Description = company.Description;

				int[] departmentsForUpdate = dtCompanyItem.Departments.Where(x => x.DepartmentId != 0)
									.Select(x => x.DepartmentId)
									.ToArray();

				List<Department> departmentsForRemove = dbContext.Departments.Include(x => x.Divisions)
						.Where(x => x.CompanyId == dtCompanyItem.CompanyId
							&& !departmentsForUpdate.Contains(x.DepartmentId))
						.ToList();

				RemoveDepartment(dbContext, departmentsForRemove);

				foreach (var dtDepartmentItem in dtCompanyItem.Departments)
				{
					await UpdateDepartmentAsync(dbContext, dtDepartmentItem);
				}
			}
			else
			{
				await dbContext .Companies.AddAsync(company);
			}
		}

		private async Task UpdateDepartmentAsync(WSSDepartmentsContext dbContext, DT_Department dtDepartmentItem) 
		{
			Department department = _mapper.Map<Department>(dtDepartmentItem);

			var departmentDatabase = await dbContext.Departments
						.FirstOrDefaultAsync(x => x.DepartmentId == dtDepartmentItem.DepartmentId);

			if (departmentDatabase != null)
			{
				departmentDatabase.Name = department.Name;
				departmentDatabase.Description = department.Description;
				departmentDatabase.CompanyId = department.CompanyId;

				int[] divisionsForUpdate = dtDepartmentItem.Divisions.Where(x => x.DivisionId != 0)
					.Select(x => x.DivisionId)
					.ToArray();

				List<Division> divisionsForRemove = await dbContext.Divisions
						.Where(x => dtDepartmentItem.DepartmentId == x.DepartmentId
								&& !divisionsForUpdate.Contains(x.DivisionId))
						.ToListAsync();

				divisionsForRemove.ForEach(x => { x.Department = null; x.DepartmentId = 0; });
				dbContext.Divisions.RemoveRange(divisionsForRemove);

				foreach (var dtDivision in dtDepartmentItem.Divisions)
				{
					await UpdateDivisionAsync(dbContext, dtDivision);
				}
			}
			else
			{
				await dbContext.AddAsync(department);
			}
		}

		private async Task UpdateDivisionAsync(WSSDepartmentsContext dbContext, DT_Division dtDivision)
		{
			Division division = _mapper.Map<Division>(dtDivision);

			var divisionFromDatabase = await dbContext.Divisions
					.FirstOrDefaultAsync(x => x.DivisionId == dtDivision.DivisionId);

			if (divisionFromDatabase != null)
			{
				divisionFromDatabase.Name = division.Name;
				divisionFromDatabase.Description = division.Description;
				divisionFromDatabase.DepartmentId = division.DepartmentId;
			}
			else
			{
				await dbContext.AddAsync(division);
			}
		}

		private void RemoveCompany(WSSDepartmentsContext dbContext, List<Company> companiesForRemove) {
			foreach (var companyItem in companiesForRemove)
			{
				RemoveDepartment(dbContext, companyItem.Departments);
				dbContext.Remove(companyItem);
			};
		}

		private void RemoveDepartment(WSSDepartmentsContext dbContext, ICollection<Department> departments)
		{
			foreach (var departmentItem in departments)
			{
				foreach (var division in departmentItem.Divisions)
				{
					dbContext.Remove(division);
				}
				dbContext.Remove(departmentItem);
			}
		}
		#endregion

		#region Save Company

		public async Task<List<DT_Company>> SaveCompanyAsync(DT_Company company)
		{
			using (var dbContext = _contextFactory.CreateDbContext())
			{
				using (var transaction = await dbContext.Database.BeginTransactionAsync(System.Data.IsolationLevel.ReadCommitted))
				{
					if (company.CompanyId != 0)
					{
						Company companyDatabase = await dbContext.Companies.FirstOrDefaultAsync(x => x.CompanyId == company.CompanyId);
						if (companyDatabase != null)
						{
							companyDatabase.Description = company.Description;
							companyDatabase.Name = company.Name;
						}else{
							throw new Exception($"There is no company with id{company.CompanyId}");
						}
					}
					else{
						dbContext.Companies.Add(_mapper.Map<Company>(company));
					}

					dbContext.SaveChanges();
					transaction.Commit();
				}

				List<Company> resultCompanies = await dbContext.Companies
					.Include(c => c.Departments)
					.ThenInclude(c => c.Divisions)
					.ToListAsync();

				return _mapper.Map<List<DT_Company>>(resultCompanies);
			}
		}

		#endregion
	}
}
