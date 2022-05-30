using AutoMapper;
using WSSDepartments.BusinessLayer.DataTransferObjects;
using WSSDepartments.DatabaseLayer.Models;

namespace WSSDepartments
{
	public class AppMappingProfile : Profile
	{
		public AppMappingProfile() 
		{
			Company_DT_Mapping();
			Department_DT_Mapping();
			Division_DT_Mapping();

			Company_UI_Mapping();
			Department_UI_Mapping();
			Division_UI_Mapping();
		}

		private void Company_DT_Mapping() {
			CreateMap<Company, DT_Company>()
				.ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => src.CompanyId))
				.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
				.ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
				.ForMember(dest => dest.Departments, opt => opt.MapFrom(src => src.Departments)).ReverseMap();				
		}

		private void Department_DT_Mapping()
		{
			CreateMap<Department, DT_Department>()
				.ForMember(dest => dest.DepartmentId, opt => opt.MapFrom(src => src.DepartmentId))
				.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
				.ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
				.ForMember(dest => dest.Company, opt => opt.MapFrom(src => src.Company))
				.ForMember(dest => dest.Divisions, opt => opt.MapFrom(src => src.Divisions));

			CreateMap<DT_Department, Department>()
				.ForMember(dest => dest.DepartmentId, opt => opt.MapFrom(src => src.DepartmentId))
				.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
				.ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
				.ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => src.Company.CompanyId))
				.ForMember(dest => dest.Divisions, opt => opt.MapFrom(src => src.Divisions));
		}

		private void Division_DT_Mapping() {
			CreateMap<Division, DT_Division>()
				.ForMember(dest => dest.DivisionId, opt => opt.MapFrom(src => src.DivisionId))
				.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
				.ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
				.ForMember(dest => dest.Department, opt => opt.MapFrom(src => src.Department));

			CreateMap<DT_Division, Division>()
				.ForMember(dest => dest.DivisionId, opt => opt.MapFrom(src => src.DivisionId))
				.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
				.ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
				.ForMember(dest => dest.DepartmentId, opt => opt.MapFrom(src => src.Department.DepartmentId));
		}

		private void Company_UI_Mapping()
		{
			CreateMap<UILayer.Company, DT_Company>()
				.ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => src.CompanyId))
				.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
				.ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
				.ForMember(dest => dest.Departments, opt => opt.MapFrom(src => src.Departments)).ReverseMap();
		}

		private void Department_UI_Mapping()
		{
			CreateMap<UILayer.Department, DT_Department>()
				.ForMember(dest => dest.DepartmentId, opt => opt.MapFrom(src => src.DepartmentId))
				.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
				.ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
				.ForMember(dest => dest.Company, opt => opt.MapFrom(src => new DT_Company { CompanyId = src.CompanyId }))
				.ForMember(dest => dest.Divisions, opt => opt.MapFrom(src => src.Divisions));

			CreateMap<DT_Department, UILayer.Department>()
				.ForMember(dest => dest.DepartmentId, opt => opt.MapFrom(src => src.DepartmentId))
				.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
				.ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
				.ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => src.Company.CompanyId))
				.ForMember(dest => dest.Divisions, opt => opt.MapFrom(src => src.Divisions));
		}

		private void Division_UI_Mapping()
		{
			CreateMap<UILayer.Division, DT_Division>()
				.ForMember(dest => dest.DivisionId, opt => opt.MapFrom(src => src.DivisionId))
				.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
				.ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
				.ForMember(dest => dest.Department, opt => opt.MapFrom(src => new DT_Department { DepartmentId = src.DepartmentId }));

			CreateMap<DT_Division, UILayer.Division>()
				.ForMember(dest => dest.DivisionId, opt => opt.MapFrom(src => src.DivisionId))
				.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
				.ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
				.ForMember(dest => dest.DepartmentId, opt => opt.MapFrom(src => src.Department.DepartmentId));
		}
	}
}
