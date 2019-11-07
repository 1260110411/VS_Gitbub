using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Class1 : IMyInterface
    {
        public void MethodToImplement()
        {
            Console.WriteLine("MethodToImplement() called.");
        }
        public void ParentInterfaceMethod()
        {
            Console.WriteLine("ParentInterfaceMethod() called.");
        }
    }
    class Class2 : IMyInterface
    {
        public void MethodToImplement()
        {
            Console.WriteLine("jiekou");
        }
        public void ParentInterfaceMethod()
        {
            Console.WriteLine("zhecaishi");
        }
    }
}
