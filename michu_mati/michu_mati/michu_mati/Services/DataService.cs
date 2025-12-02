using michu_mati.Models;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System;
using System.IO;
using System.Collections.Generic;

namespace michu_mati.Services
{
    // Services/DataService.cs
    public class DataService
    {
        private readonly string _recipesPath = "recipes.json";
        private readonly string _planPath = "weeklyplan.json";

        public async Task<List<Recipe>> LoadRecipesAsync()
        {
            if (!File.Exists(_recipesPath)) return new List<Recipe>();
            var json = await File.ReadAllTextAsync(_recipesPath);
            return JsonConvert.DeserializeObject<List<Recipe>>(json) ?? new();
        }

        public async Task SaveRecipesAsync(List<Recipe> recipes)
        {
            var json = JsonConvert.SerializeObject(recipes, Newtonsoft.Json.Formatting.Indented);
            await File.WriteAllTextAsync(_recipesPath, json);
        }

        public async Task<WeeklyMealPlan> LoadPlanAsync()
        {
            if (!File.Exists(_planPath)) return new WeeklyMealPlan();
            var json = await File.ReadAllTextAsync(_planPath);
            return JsonConvert.DeserializeObject<WeeklyMealPlan>(json) ?? new();
        }

        public async Task SavePlanAsync(WeeklyMealPlan plan)
        {
            var json = JsonConvert.SerializeObject(plan, Newtonsoft.Json.Formatting.Indented);
            await File.WriteAllTextAsync(_planPath, json);
        }
    }
}
