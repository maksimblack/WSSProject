using System;
using System.Collections.Generic;
using WSSDepartments.BusinessLayer.DataTransferObjects;

namespace WSSDepartments.Tests
{
	public static class TestData
	{
		public static List<DT_Company> DT_Companies;

		static TestData() {
			DT_Companies = new List<DT_Company> { 
				new DT_Company{ 
					CompanyId = 0,
					Name = "Some Company one",
					Departments = new List<DT_Department>{ 
						new DT_Department{ 
							Name = "Some department name one",
							Divisions = new List<DT_Division>(){ 
								new DT_Division{
									Name = "Some division name one"
								},
								new DT_Division{
									Name = "Some division name second"
								},
								new DT_Division{
									Name = "Some division name third"
								},
								new DT_Division{
									Name = "Some division name fourth"
								},
								new DT_Division{
									Name = "Some division name fifth"
								}
							}
						},
						new DT_Department{
							Name = "Some department name second",
							Divisions = new List<DT_Division>(){
								new DT_Division{
									Name = "Some division name one"
								},
								new DT_Division{
									Name = "Some division name second"
								},
								new DT_Division{
									Name = "Some division name third"
								},
								new DT_Division{
									Name = "Some division name fourth"
								},
								new DT_Division{
									Name = "Some division name fifth"
								}
							}
						},
						new DT_Department{
							Name = "Some department name third",
							Divisions = new List<DT_Division>(){
								new DT_Division{
									Name = "Some division name one"
								},
								new DT_Division{
									Name = "Some division name second"
								},
								new DT_Division{
									Name = "Some division name third"
								},
								new DT_Division{
									Name = "Some division name fourth"
								},
								new DT_Division{
									Name = "Some division name fifth"
								}
							}
						}

					}					
				},
								new DT_Company{
					CompanyId = 0,
					Name = "Some Company second",
					Departments = new List<DT_Department>{
						new DT_Department{
							Name = "Some department name one",
							Divisions = new List<DT_Division>(){
								new DT_Division{
									Name = "Some division name one"
								},
								new DT_Division{
									Name = "Some division name second"
								},
								new DT_Division{
									Name = "Some division name third"
								},
								new DT_Division{
									Name = "Some division name fourth"
								},
								new DT_Division{
									Name = "Some division name fifth"
								}
							}
						},
						new DT_Department{
							Name = "Some department name second",
							Divisions = new List<DT_Division>(){
								new DT_Division{
									Name = "Some division name one"
								},
								new DT_Division{
									Name = "Some division name second"
								},
								new DT_Division{
									Name = "Some division name third"
								},
								new DT_Division{
									Name = "Some division name fourth"
								},
								new DT_Division{
									Name = "Some division name fifth"
								}
							}
						},
						new DT_Department{
							Name = "Some department name third",
							Divisions = new List<DT_Division>(){
								new DT_Division{
									Name = "Some division name one"
								},
								new DT_Division{
									Name = "Some division name second"
								},
								new DT_Division{
									Name = "Some division name third"
								},
								new DT_Division{
									Name = "Some division name fourth"
								},
								new DT_Division{
									Name = "Some division name fifth"
								}
							}
						}
					}
				}
			};
		}
	}
}
