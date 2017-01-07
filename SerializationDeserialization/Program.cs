using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System;
using Newtonsoft.Json;

namespace SerializationDeserialization
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Employee> employees = new List<Employee>()
            {
                new Employee() { Id=1, Name="Maung Maung", Title="Manager", DOB="12/1/1986", Address="Mandalay" },
                new Employee() { Id=2, Name="Kyaw Kyaw", Title="Programmer", DOB="3/4/1975", Address="Yangon" },
                new Employee() { Id=3, Name="Mya Mya", Title="System Analyst", DOB="8/5/1987", Address="Taung Gyi" },
                new Employee() { Id=3, Name="Tun Tun", Title="Team Lead", DOB="1/7/1982", Address="Sagaing" }
            };

            XMLSerializeDeserialize<List<Employee>> XMLSerializeEmployees = new XMLSerializeDeserialize<List<Employee>>();
            string xmlserializedData = XMLSerializeEmployees.SerializeData(employees);
            Console.WriteLine("----------XML Serialize Data----------");
            Console.WriteLine(xmlserializedData);

            List<Employee> xmldeserializedData = XMLSerializeEmployees.DeserializeData(xmlserializedData);
            Console.WriteLine("----------XML Deserialize Data----------");
            foreach (var emp in xmldeserializedData)
            {
                Console.WriteLine("Id : {0}\nName : {1}\nTitle : {2}\nDOB : {3}\nAddress : {4} ", emp.Id, emp.Name, emp.Title, emp.DOB, emp.Address);
                Console.WriteLine("------------------------------------------------------------");
            }

            JSOSerializeDeserialize<List<Employee>> JsonSerializeEmployees = new JSOSerializeDeserialize<List<Employee>>();
            string jsonserializedData = JsonSerializeEmployees.SerializeData(employees);
            Console.WriteLine("----------JSON Serialize Data----------");
            Console.WriteLine(jsonserializedData);

            List<Employee> jsodeserializedData = JsonSerializeEmployees.DeserializeData(jsonserializedData);
            Console.WriteLine("----------JSON Deserialize Data----------");
            foreach (var emp in xmldeserializedData)
            {
                Console.WriteLine("Id : {0}\nName : {1}\nTitle : {2}\nDOB : {3}\nAddress : {4} ", emp.Id, emp.Name, emp.Title, emp.DOB, emp.Address);
                Console.WriteLine("------------------------------------------------------------");
            }

            Console.ReadLine();
        }
    }

    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string DOB { get; set; }
        public string Address { get; set; }
    }

    public class XMLSerializeDeserialize<T>
    {
        //XML Class
        XmlSerializer xmlSerializer;
        XmlDocument xDocument;
        XmlNodeReader xNodeReader;
        //String Class
        StringBuilder sbData;
        StringWriter swTextWriter;

        //Serialize XML string (T = Generic Type Parameter)
        public string SerializeData(T data)
        {
            sbData = new StringBuilder();
            swTextWriter = new StringWriter(sbData);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            xmlSerializer.Serialize(swTextWriter, data);
            return sbData.ToString();
        }

        //Deserialize T object
        public T DeserializeData(string xmldata)
        {
            xDocument = new XmlDocument();
            xDocument.LoadXml(xmldata);
            xNodeReader = new XmlNodeReader(xDocument.DocumentElement);
            xmlSerializer = new XmlSerializer(typeof(T));
            var objData = xmlSerializer.Deserialize(xNodeReader);
            T deserializedData = (T)objData;
            return deserializedData;
        }
    }

    //Newtonsoft.Json NeNuGet Package need to install
    public class JSOSerializeDeserialize<T>
    {
        //Serialize JSON string (T = Generic Type Parameter)
        public string SerializeData(T data)
        {
            string jsonData = JsonConvert.SerializeObject(data);
            return jsonData;
        }

        //Deserialize T object
        public T DeserializeData(string jsondata)
        {
            var objData = JsonConvert.DeserializeObject<T>(jsondata);
            T deserializedData = (T)objData;
            return deserializedData;
        }
    }
}