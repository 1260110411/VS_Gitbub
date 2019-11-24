using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
namespace XmlSerializer
{
    /// <summary>
    /// XmlSerializer的序列化方式
    /// </summary>
    [Serializable]
    //如果要想保存某个class中的字段,必须在class前面加个这样attribute(C#里面用中括号括起来的标志符)
    public class Person
    {
        public int age;
        public string name;
        [NonSerialized] //如果某个字段不想被保存,则加个这样的标志
        public string secret;
    }
    class Program
    {
        static void Main(string[] args)
        {
            ///序列化:
            Person person = new Person();
            person.age = 18;
            person.name = "tom";
            person.secret = "i will not tell you";
            FileStream stream = new FileStream(@"C:\Users\Public\Desktop\xmlFormat.xml", FileMode.Create);
            System.Xml.Serialization.XmlSerializer xmlserilize = new System.Xml.Serialization.XmlSerializer(typeof(Person));
            //XmlSerializer xmlserilize = new XmlSerializer(typeof(Person));
            xmlserilize.Serialize(stream, person);
            stream.Close();
            ///反序列化:
            Person person1 = new Person();
            FileStream stream1 = new FileStream(@"C:\Users\Public\Desktop\xmlFormat.xml", FileMode.Open);
            System.Xml.Serialization.XmlSerializer xmlserilize1 = new System.Xml.Serialization.XmlSerializer(typeof(Person));
            person1 = (Person)xmlserilize.Deserialize(stream1);
            stream1.Close();
            Console.WriteLine(person1.age + person1.name + person1.secret);

            Console.ReadKey();
        }
    }
}
