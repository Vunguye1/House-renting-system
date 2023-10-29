using Project1.Models;

namespace Project1.ViewModels
{
    public class RealestateListViewModel // view model. This model will return real estate list and customized current view name
    {
        public IEnumerable<Realestate> realestates;
        public string? CurrentViewName;

        public RealestateListViewModel(IEnumerable<Realestate> _realestates, string? currentViewName)
        {
            realestates = _realestates;
            CurrentViewName = currentViewName;
        }
    }
}
