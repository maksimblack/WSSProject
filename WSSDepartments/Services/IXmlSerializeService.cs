using System.IO;

namespace WSSDepartments.Services
{
	public interface IXmlSerializeService
	{
		MemoryStream SerializeToMemoryStream<T>(T model);
		T DeSerializeFromMemoryStream<T>(MemoryStream memoryStream);
	}
}
