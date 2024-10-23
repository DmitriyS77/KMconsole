using System;
using System.Collections.Generic;
using Microsoft.Msagl.Drawing;
using Microsoft.Msagl.GraphViewerGdi;

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

        public List<Parameter> GetParameters() => parameters;
        public List<Relationship> GetRelationships() => relationships;


        public bool AnalyzeStructuralStability()
        {
            // Простая проверка циклов как индикатор  устойчивости
            foreach (var param in parameters)
            {
                var related = relationships.FindAll(r => r.From == param || r.To == param);
                if (related.Count > 5) return false; // Пример простой проверки
            }
            return true;
        }
    }
}