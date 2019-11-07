using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            IMyInterface iImp = new Class2();
            iImp.MethodToImplement();
            iImp.ParentInterfaceMethod();
            IMyInterface iImp1 = new Class1();
            iImp1.MethodToImplement();
            iImp1.ParentInterfaceMethod();
            Console.ReadKey();
        }
    }
}
