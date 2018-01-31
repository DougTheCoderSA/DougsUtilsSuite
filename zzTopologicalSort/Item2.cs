
using System.Collections.Generic;

namespace zzTopologicalSort
{
    public class Item2
    {
        public string Name { get; private set; }
        public string[] Dependencies { get; private set; }
        public List<string> DependsOn { get; set; } // Doug added this

        public Item2(string name, params string[] dependencies)
        {
            Name = name;
            Dependencies = dependencies;
            DependsOn = new List<string>();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
