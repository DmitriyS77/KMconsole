using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace KMconsole
{
    public class Program
    {
        static void Main(string[] args)
        {
            CkmModel model = new CkmModel();
            model.AddParameter("A", 8);
            model.AddParameter("B",5);
            model.AddParameter("C",3);

            model.AddRelationship("A", "B", 0.5);
            model.AddRelationship("A", "C", -0.3);
            model.AddRelationship("B", "C", 0.8);

            model.Print();

            model.RunImpulseSimulation(3);
            model.RemoveParameterAndRelationships("A");// удаление узла
            model.Print();
            model.AnalyzeStructuralStabilityEmpty();
            Console.ReadKey();

        }
    }
}
