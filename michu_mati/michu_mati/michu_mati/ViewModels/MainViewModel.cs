using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using michu_mati.Services;
using michu_mati.ViewModels;

namespace michu_mati.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        private object? _currentView;

        public RecipesViewModel RecipesVM { get; }
        public PlannerViewModel PlannerVM { get; }
        public ShoppingListViewModel ShoppingVM { get; }

        public MainViewModel()
        {
            var dataService = new DataService();

            RecipesVM = new RecipesViewModel(dataService);
            PlannerVM = new PlannerViewModel(dataService, RecipesVM);
            ShoppingVM = new ShoppingListViewModel(PlannerVM);

            PlannerVM.PropertyChanged += (s, e) => ShoppingVM.RefreshCommand.Execute(null);
            CurrentView = RecipesVM;
        }

        [RelayCommand]
        private void ShowRecipes() => CurrentView = RecipesVM;

        [RelayCommand]
        private void ShowPlanner() => CurrentView = PlannerVM;

        [RelayCommand]
        private void ShowShoppingList()
        {
            ShoppingVM.RefreshCommand.Execute(null);  // DZIAŁA ZAWSZE
            CurrentView = ShoppingVM;
        }
    }
}