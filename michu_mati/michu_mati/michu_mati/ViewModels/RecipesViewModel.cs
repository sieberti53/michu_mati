using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using michu_mati.Models;
using michu_mati.Services;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace michu_mati.ViewModels
{
    public partial class RecipesViewModel : ObservableObject
    {
        private readonly DataService _dataService;

        public ObservableCollection<Recipe> Recipes { get; } = new();

        private Recipe? _selectedRecipe;
        public Recipe? SelectedRecipe
        {
            get => _selectedRecipe;
            set => SetProperty(ref _selectedRecipe, value);
        }

        private string _searchText = "";
        public string SearchText
        {
            get => _searchText;
            set
            {
                if (SetProperty(ref _searchText, value))
                {
                    _ = FilterRecipesAsync(value);
                }
            }
        }

        public RecipesViewModel(DataService dataService)
        {
            _dataService = dataService;
            _ = LoadRecipesAsync(); 
        }

        [RelayCommand]
        private async Task LoadRecipesAsync()
        {
            var list = await _dataService.LoadRecipesAsync();
            Recipes.Clear();
            foreach (var r in list)
                Recipes.Add(r);
        }

        [RelayCommand]
        private async Task AddOrUpdateRecipe()
        {
            if (SelectedRecipe is null) return;

            var existing = Recipes.FirstOrDefault(r => r.Id == SelectedRecipe.Id);
            if (existing is null)
                Recipes.Add(SelectedRecipe);
            else
            {
                var index = Recipes.IndexOf(existing);
                Recipes[index] = SelectedRecipe;
            }

            await _dataService.SaveRecipesAsync(Recipes.ToList());
            await LoadRecipesAsync();
        }

        [RelayCommand]
        private async Task DeleteRecipe()
        {
            if (SelectedRecipe is null) return;

            Recipes.Remove(SelectedRecipe);
            await _dataService.SaveRecipesAsync(Recipes.ToList());
            SelectedRecipe = null;
        }
        private async Task FilterRecipesAsync(string search)
        {
            var all = await _dataService.LoadRecipesAsync();

            var filtered = string.IsNullOrWhiteSpace(search)
                ? all
                : all.Where(r => r.Name.Contains(search, System.StringComparison.OrdinalIgnoreCase))
                     .ToList();

            Recipes.Clear();
            foreach (var r in filtered)
                Recipes.Add(r);
        }
    }
}