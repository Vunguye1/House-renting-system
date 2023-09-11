using Project1.Models;

namespace Project1.ViewModels
{
    public class PropertyListViewModel
    {
        public IEnumerable<Property> properties;
        public string? CurrentViewName;

        public PropertyListViewModel(IEnumerable<Property> _properties, string? currentViewName)
        {
            properties = _properties;
            CurrentViewName = currentViewName;
        }
    }
}
