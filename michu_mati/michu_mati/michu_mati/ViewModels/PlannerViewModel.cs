using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using michu_mati.Models;
using michu_mati.Services;
using System;
using System.Threading.Tasks;

namespace michu_mati.ViewModels
{
    public partial class PlannerViewModel : ObservableObject
    {
        private readonly DataService _dataService;

        private WeeklyMealPlan _plan = new();
        public WeeklyMealPlan Plan
        {
            get => _plan;
            set => SetProperty(ref _plan, value);
        }

        public RecipesViewModel RecipesVM { get; }

        private Recipe? _selectedRecipeToAssign;
        public Recipe? SelectedRecipeToAssign
        {
            get => _selectedRecipeToAssign;
            set => SetProperty(ref _selectedRecipeToAssign, value);
        }

        public PlannerViewModel(DataService dataService, RecipesViewModel recipesVM)
        {
            _dataService = dataService;
            RecipesVM = recipesVM;
            _ = LoadPlanAsync(); 
        }

        [RelayCommand]
        private async Task LoadPlanAsync()
        {
            Plan = await _dataService.LoadPlanAsync();
        }

        [RelayCommand]
        private async Task AssignRecipe(DayOfWeek day)
        {
            if (SelectedRecipeToAssign is null) return;
            Plan.Meals[day] = SelectedRecipeToAssign;
            await _dataService.SavePlanAsync(Plan);
        }

        [RelayCommand]
        private async Task ClearDay(DayOfWeek day)
        {
            Plan.Meals[day] = null;
            await _dataService.SavePlanAsync(Plan);
        }
    }
}