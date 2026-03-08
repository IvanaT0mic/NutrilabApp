using Microsoft.AspNetCore.Components;
using Nutrilab.Dtos.Recipes.RecipeOutgoingDto;
using NutrilabApp.Frontend.Services;
using System.Net.Http.Json;

namespace NutrilabApp.Frontend.Layout
{
    public class HomeBase : ComponentBase
    {
        [Inject] protected HttpClient Http { get; set; } = default!;
        [Inject] protected TokenService TokenService { get; set; } = default!;

        protected string Email { get; set; } = "";
        protected List<RecipeOutgoingDto> Recipes { get; set; } = [];
        protected bool IsLoading { get; set; } = true;

        protected override async Task OnInitializedAsync()
        {
            Email = await TokenService.GetEmailAsync() ?? "";
            var all = await Http.GetFromJsonAsync<List<RecipeOutgoingDto>>("recipes") ?? [];
            Recipes = all.Take(6).ToList();
            IsLoading = false;
        }
    }
}