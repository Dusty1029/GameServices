using Microsoft.AspNetCore.Components;

namespace GameInterface.Extensions
{
    public static class NavigationManagerExtensions
    {
        public static void ReloadPage(this NavigationManager manager, string continuationOfUrl = "") => manager.NavigateTo($"{manager.Uri}{continuationOfUrl}", true);
    }
}
