using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace michu_mati.Models
{
    public class Recipe
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public List<Ingredient> Ingredients { get; set; } = new();
        public string Instructions { get; set; } = string.Empty;
    }
}
