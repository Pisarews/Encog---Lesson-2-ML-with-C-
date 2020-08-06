using Encog.ML.Bayesian;
using Encog.ML.Bayesian.Query.Enumeration;
using System;

namespace Encog
{
    class Program
    {
        static void Main(string[] args)
        {
            
              /*Przykład 4.2.3 Teoria Bayesa - Zakłady Produkcyjne Etap Manualny
               * Lesson 1 - Machine Learning in C# for Amazon ML Group created by me :-) 
               * PS. Kill me I use polish :-) 
               */

            BayesianNetwork network = new BayesianNetwork();
            BayesianEvent WadliwyElement = network.CreateEvent("wadliwy_element");
            BayesianEvent A1 = network.CreateEvent("wadliwy_element_zaklad_A1");
            BayesianEvent A2 = network.CreateEvent("wadliwy_element_zaklad_A2");
            BayesianEvent A3 = network.CreateEvent("wadliwy_element_zaklad_A3");
            BayesianEvent A4 = network.CreateEvent("wadliwy_element_zaklad_A4");
            network.CreateDependency(WadliwyElement, A1, A2, A3, A4);
            network.FinalizeStructure();

            WadliwyElement?.Table?.AddLine(0.1083, true);
            A1?.Table.AddLine(0.069, true, true);
            A1?.Table.AddLine(1 - 0.069, true, false);
            A2?.Table.AddLine(0.277, true, true);
            A2?.Table.AddLine(1 - 0.277, true, false);
            A3?.Table.AddLine(0.007, true, true);
            A3?.Table.AddLine(1 - 0.007, true, false);
            A4?.Table.AddLine(0.646, true, true);
            A4?.Table.AddLine(1 - 0.646, true, false);
            network.Validate();
            Console.WriteLine(network.ToString() + "\n");
            Console.WriteLine($"Liczba parametrów: {network.CalculateParameterCount()}");

            EnumerationQuery query = new EnumerationQuery(network);
            query.DefineEventType(WadliwyElement, EventType.Evidence);
            query.DefineEventType(A1, EventType.Outcome);
            query.DefineEventType(A2, EventType.Evidence);
            query.DefineEventType(A3, EventType.Evidence);
            query.DefineEventType(A4, EventType.Evidence);

            query.SetEventValue(WadliwyElement, false);
            query.SetEventValue(A1, false);
            query.SetEventValue(A2, false);
            query.SetEventValue(A3, false);
            query.SetEventValue(A4, false);
            query.Execute();

            Console.WriteLine(query.ToString()); 
            
        }


    }
}
