using Microsoft.AspNetCore.Mvc;

namespace Cooperchip.ITDeveloper.Mvc.Controllers
{
    public class IntelController : Controller
    {
        public IActionResult AnalyticsDashboard() => View();
        public IActionResult MarketingDashboard() => View();
    }
}
