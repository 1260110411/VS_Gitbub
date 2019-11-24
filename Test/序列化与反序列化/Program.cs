using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace 序列化与反序列化
{
    /// <summary>
    /// BinaryFormatter的序列化方式
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
            FileStream stream = new FileStream(@"C:\Users\Public\Desktop\person.dat", FileMode.Create);
            BinaryFormatter bFormat = new BinaryFormatter();
            bFormat.Serialize(stream, person);
            stream.Close();

            ///反序列化
            Person person1 = new Person();
            FileStream stream1 = new FileStream(@"C:\Users\Public\Desktop\person.dat", FileMode.Open);
            BinaryFormatter bFormat1 = new BinaryFormatter();
            person1 = (Person)bFormat.Deserialize(stream1);
            //反序列化得到的是一个object对象.必须做下类型转换
            stream1.Close();
            Console.WriteLine(person1.age + person1.name + person1.secret);
            //结果为18tom.因为secret没有有被序列化.

            Console.ReadKey();
        }
    }
    
}
