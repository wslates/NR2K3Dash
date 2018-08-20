using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N2k3Dash.ViewModel
{
    enum PageTypes
    {
        DefaultDigital = 0,
        DefaultAnalog = 1,
        Hybrid_1 = 2
    }

    class PageFactory
    {
        public static DashboardViewModel CreateInstance(PageTypes type)
        {
            switch (type)
            {
                case PageTypes.DefaultDigital:
                    return new DefaultViewModel();
                case PageTypes.DefaultAnalog:
                    return new AnalogViewModel();
                case PageTypes.Hybrid_1:
                    return new Hybrid1ViewModel(); 
                default:
                    return null;
            }
        }
    }
}
