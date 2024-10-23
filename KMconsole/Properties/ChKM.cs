using System;
using System.Collections.Generic;
//using Microsoft.Msagl.Drawing;
//using Microsoft.Msagl.GraphViewerGdi;

namespace KMconsole
{
    public class Parameter
    {
        public string Name { get; set; }
        public double Value { get; set; }

        public Parameter(string name, double value)
        {
            Name = name;
            Value = value;
        }
    }

    public class Relationship
    {
        public Parameter From { get; set; }
        public Parameter To { get; set; }
        public double Weight { get; set; }

        public Relationship(Parameter from, Parameter to, double weight)
        {
            From = from;
            To = to;
            Weight = weight;
        }
    }

    public class CkmModel
    {
        private List<Parameter> parameters = new List<Parameter>();
        private List<Relationship> relationships = new List<Relationship>();

        public void AddParameter(string name, double value)
        {
            parameters.Add(new Parameter(name, value));
        }

        public void RemoveParameter(string name)
        {
            parameters.RemoveAll(p => p.Name == name);
        }

        public void AddRelationship(string from, string to, double weight)
        {
            var fromParam = parameters.Find(p => p.Name == from);
            var toParam = parameters.Find(p => p.Name == to);
            if (fromParam != null && toParam != null)
            {
                relationships.Add(new Relationship(fromParam, toParam, weight));
            }
        }

        public void RemoveRelationship(string from, string to)
        {
            relationships.RemoveAll(r => r.From.Name == from && r.To.Name == to);
        }
        public void RemoveParameterAndRelationships(string name)
        {
            // Удаляем все связи, которые содержат данный узел
            relationships.RemoveAll(r => r.From.Name == name || r.To.Name == name);
            // Удаляем сам узел
            parameters.RemoveAll(p => p.Name == name);
        }

        public List<Parameter> GetParameters() => parameters;
        public List<Relationship> GetRelationships() => relationships;


        public bool AnalyzeStructuralStability()
        {
            // Простая проверка циклов как индикатор  устойчивости
            foreach (var param in parameters)
            {
                var related = relationships.FindAll(r => r.From == param || r.To == param);
                if (related.Count > 5) return false; // Если циклов много,то модель неустойчива
            }
            return true;
        }
        public bool AnalyzeStructuralStabilityEmpty()// метод для проверки "пустых" узлов
        {
            foreach (var param in parameters)
            {
                var outgoing = relationships.Find(r => r.From == param);
                if (outgoing == null)
                {
                    Console.WriteLine($"Узел {param.Name} не имеет исходящих связей. Модель неустойчива.");
                    return false;
                }
            }

            foreach (var param in parameters)
            {
                var incoming = relationships.Find(r => r.To == param);
                if (incoming == null)
                {
                    Console.WriteLine($"Узел {param.Name} не имеет входящих связей.");
                }
            }

            Console.WriteLine("Модель структурно устойчива.");
            return true;
        }

        public void RunImpulseSimulation(int steps)
        {
            // Инициализация импульсов для всех узлов
            Dictionary<Parameter, double> impulses = new Dictionary<Parameter, double>();
            foreach (var param in parameters)
            {
                impulses[param] = 1.0; // начальный импульс для каждого узла
            }

            Console.WriteLine("Импульсное моделирование:");
            for (int step = 0; step < steps; step++)
            {
                Console.WriteLine($"Шаг {step + 1}:");

                // Создаём новый словарь для обновлённых импульсов
                Dictionary<Parameter, double> newImpulses = new Dictionary<Parameter, double>(impulses);

                // Для каждой связи пересчитываем импульс на основе весов
                foreach (var relationship in relationships)
                {
                    double fromImpulse = impulses[relationship.From];
                    double transfer = fromImpulse * relationship.Weight;
                    newImpulses[relationship.To] += transfer; // Передаём импульс с учётом веса
                }

                // Выводим новые импульсы на этом шаге
                foreach (var param in parameters)
                {
                    Console.WriteLine($"{param.Name}: {newImpulses[param]}");
                }

                // Обновляем импульсы для следующего шага
                impulses = newImpulses;
            }
        }


        public void Print()
        {
            Console.WriteLine();
            {
                if (relationships.Count != 0 && parameters.Count != 0)
                {
                    Console.WriteLine("Граф ЧКМ:");
                }
                else { Console.WriteLine("Граф Пуст"); }

                //Console.WriteLine("Граф пуст");
                foreach (var relationship in relationships)
                {
                    Console.WriteLine($"{relationship.From.Name} -> {relationship.To.Name} [weight: {relationship.Weight}]");
                }
            }
        }
    }
}

