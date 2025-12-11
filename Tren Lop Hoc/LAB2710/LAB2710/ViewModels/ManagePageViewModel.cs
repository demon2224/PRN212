using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LAB2710.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using System.Windows;

namespace LAB2710.ViewModels
{
    public partial class ManagePageViewModel : BaseViewModel
    {
        [ObservableProperty]
        private ObservableCollection<Game> games = new();

        [ObservableProperty]
        private Game? selectedGame;

        [ObservableProperty]
        private string searchText = string.Empty;

        [ObservableProperty]
        private string title = string.Empty;

        [ObservableProperty]
        private string genre = string.Empty;

        [ObservableProperty]
        private double? price;

        [ObservableProperty]
        private DateTime? releaseDate;

        [ObservableProperty]
        private bool isEditing = false;

        [ObservableProperty]
        private string editButtonText = "Edit";

        [ObservableProperty]
        private string totalGamesText = "Total: 0 game(s)";

        private int editingGameId = 0;

        public ManagePageViewModel()
        {
            _ = LoadGamesAsync();
        }

        [RelayCommand]
        private async Task LoadGamesAsync()
        {
            try
            {
                using var context = new GameDbContext();
                var gameList = await context.Games.ToListAsync();
                Games.Clear();
                foreach (var game in gameList)
                {
                    Games.Add(game);
                }
                UpdateTotalGamesText();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading games: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        [RelayCommand]
        private async Task SearchAsync()
        {
            try
            {
                using var context = new GameDbContext();
                List<Game> searchResults;

                if (string.IsNullOrWhiteSpace(SearchText))
                {
                    searchResults = await context.Games.ToListAsync();
                }
                else
                {
                    searchResults = await context.Games
                        .Where(g => g.Title.Contains(SearchText))
                        .ToListAsync();
                }

                Games.Clear();
                foreach (var game in searchResults)
                {
                    Games.Add(game);
                }
                UpdateTotalGamesText();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error searching games: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        [RelayCommand]
        private async Task SaveAsync()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Title))
                {
                    MessageBox.Show("Title is required!", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                using var context = new GameDbContext();
                var game = new Game
                {
                    Title = Title,
                    Genre = Genre,
                    Price = Price,
                    ReleaseDate = ReleaseDate
                };

                context.Games.Add(game);
                await context.SaveChangesAsync();

                MessageBox.Show("Game added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                ClearForm();
                await LoadGamesAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving game: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        [RelayCommand]
        private async Task EditAsync()
        {
            if (!IsEditing)
            {
                // Bước 1: Bấm Edit lần đầu - Load dữ liệu vào form
                if (SelectedGame != null)
                {
                    Title = SelectedGame.Title;
                    Genre = SelectedGame.Genre ?? string.Empty;
                    Price = SelectedGame.Price;
                    ReleaseDate = SelectedGame.ReleaseDate;
                    IsEditing = true;
                    EditButtonText = "Save";
                    editingGameId = SelectedGame.GameId;
                }
                else
                {
                    MessageBox.Show("Please select a game to edit!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                // Bước 2: Bấm Save - Lưu dữ liệu đã chỉnh sửa
                try
                {
                    if (string.IsNullOrWhiteSpace(Title))
                    {
                        MessageBox.Show("Title is required!", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    using var context = new GameDbContext();
                    var game = new Game
                    {
                        GameId = editingGameId,
                        Title = Title,
                        Genre = Genre,
                        Price = Price,
                        ReleaseDate = ReleaseDate
                    };

                    context.Games.Update(game);
                    await context.SaveChangesAsync();

                    MessageBox.Show("Game updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    ClearForm();
                    await LoadGamesAsync();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error updating game: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        [RelayCommand]
        private async Task DeleteAsync()
        {
            if (SelectedGame != null)
            {
                var result = MessageBox.Show($"Are you sure you want to delete '{SelectedGame.Title}'?", 
                    "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);
                
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        using var context = new GameDbContext();
                        var game = await context.Games.FindAsync(SelectedGame.GameId);
                        if (game != null)
                        {
                            context.Games.Remove(game);
                            await context.SaveChangesAsync();
                            
                            MessageBox.Show("Game deleted successfully!", "Success", 
                                MessageBoxButton.OK, MessageBoxImage.Information);
                            ClearForm();
                            await LoadGamesAsync();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error deleting game: {ex.Message}", "Error", 
                            MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a game to delete!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        [RelayCommand]
        private void Clear()
        {
            ClearForm();
        }

        [RelayCommand]
        private void ExportToJson()
        {
            try
            {
                var saveFileDialog = new SaveFileDialog
                {
                    Filter = "JSON files (*.json)|*.json",
                    DefaultExt = "json",
                    FileName = "games.json"
                };

                if (saveFileDialog.ShowDialog() == true)
                {
                    var options = new JsonSerializerOptions
                    {
                        WriteIndented = true
                    };

                    var json = JsonSerializer.Serialize(Games.ToList(), options);
                    File.WriteAllText(saveFileDialog.FileName, json);
                    
                    MessageBox.Show("Games exported successfully!", "Success", 
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error exporting games: {ex.Message}", "Error", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ClearForm()
        {
            Title = string.Empty;
            Genre = string.Empty;
            Price = null;
            ReleaseDate = null;
            IsEditing = false;
            EditButtonText = "Edit";
            editingGameId = 0;
        }

        private void UpdateTotalGamesText()
        {
            var count = Games.Count;
            TotalGamesText = $"Total: {count} game{(count != 1 ? "s" : "")}";
        }
    }
}