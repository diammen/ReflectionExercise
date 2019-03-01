using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace AssemblyExer
{
    class Program
    {
        class Wall
        {

        }
        class Door
        {

        }
        class Walkable
        {

        }
        static void Main(string[] args)
        {
            object[,] data = new object[0,0];

            //This first section cache's the various strings we'll be using
            //this helps demonstrate that the classes & methods can all
            //be entirely data driven.
            string workingDir = System.IO.Directory.GetCurrentDirectory();
            string dllFileName = "//netstandard2.0/ExternalLib.dll";
            string extClass = "ExternalLib.MyExternalClass";
            string extMethod = "LoadFile";

            //First load the external assembly
            Assembly ass = Assembly.LoadFile(workingDir + dllFileName);

            //Get th type of the desired class (including the namespace)
            Type typ = ass.GetType(extClass);

            //Get the supporting method info so we can execute a method.
            MethodInfo meth = typ.GetMethod(extMethod);

            //Create an instance of the desired object
            object obj = Activator.CreateInstance(typ);

            //And invoke the method
            data = (object[,])meth.Invoke(obj, null);
            Console.ReadLine();//Wait for random keyboard input & CR
        }
    }
}
