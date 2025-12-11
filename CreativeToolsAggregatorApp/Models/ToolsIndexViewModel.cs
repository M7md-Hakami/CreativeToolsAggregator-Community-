using System.Collections.Generic;

namespace CreativeToolsAggregatorApp.Models
{
    public class ToolsGroupViewModel
    {
        public string Tag { get; set; } = string.Empty;
        public List<Tools> Items { get; set; } = new();
    }

    public class ToolsIndexViewModel
    {
        public List<ToolsGroupViewModel> Groups { get; set; } = new();
    }
}