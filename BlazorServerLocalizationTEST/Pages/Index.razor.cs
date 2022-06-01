using Microsoft.AspNetCore.Components;

namespace BlazorServerLocalizationTEST.Pages
{
    public partial class Index
    {
        [CascadingParameter] public string? Text { get; set; }

    }
}
