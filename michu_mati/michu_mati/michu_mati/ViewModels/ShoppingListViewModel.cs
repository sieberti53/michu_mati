using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using michu_mati.Models;
using System.Collections.ObjectModel;
using System.Linq;

namespace michu_mati.ViewModels
{
    public partial class ShoppingListViewModel : ObservableObject
    {
        private readonly PlannerViewModel _plannerVM;

        public ObservableCollection<string> ShoppingList { get; } = new();

        public ShoppingListViewModel(PlannerViewModel plannerVM)
        {
            _plannerVM = plannerVM;
        }

        [RelayCommand]
        private void Refresh()
        {
            ShoppingList.Clear();

            var ingredients = new System.Collections.Generic.Dictionary<string, System.Collections.Generic.List<string>>();

            foreach (var recipe in _plannerVM.Plan.Meals.Values.Where(r => r != null))
            {
                foreach (var ing in recipe!.Ingredients)
                {
                    if (!ingredients.ContainsKey(ing.Name))
                        ingredients[ing.Name] = new();

                    ingredients[ing.Name].Add(ing.Amount);
                }
            }

            foreach (var kvp in ingredients.OrderBy(x => x.Key))
            {
                var amounts = kvp.Value.Distinct().OrderBy(x => x).ToList();

                string line = amounts.Count == 1
                    ? $"{kvp.Key} – {amounts[0]}"
                    : $"{kvp.Key} – {string.Join(" + ", amounts)}";

                ShoppingList.Add(line);
            }

            if (ShoppingList.Count == 0)
            {
                ShoppingList.Add("Brak zaplanowanych posiłków – dodaj coś w planerze!");
            }
            else
            {
                ShoppingList.Add("");
                ShoppingList.Add($"Razem dań w planie: {_plannerVM.Plan.Meals.Values.Count(r => r != null)}");
            }
        }
    }
}