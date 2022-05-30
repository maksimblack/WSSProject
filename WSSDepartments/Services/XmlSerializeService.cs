using System;
using System.IO;

namespace WSSDepartments.Services
{
	public class XmlSerializeService : IXmlSerializeService
	{
		public MemoryStream SerializeToMemoryStream<T>(T model) {
			MemoryStream stream = new MemoryStream();
			System.Xml.Serialization.XmlSerializer xmlSerializer = new System.Xml.Serialization.XmlSerializer(model.GetType());
			xmlSerializer.Serialize(stream, model);
			return stream;
		}

		public T DeSerializeFromMemoryStream<T>(MemoryStream memoryStream)
		{
			System.Xml.Serialization.XmlSerializer reader = new System.Xml.Serialization.XmlSerializer(typeof(T));
			return (T)reader.Deserialize(memoryStream);			
		}
	}
}
