using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;

namespace TurboAzScraper_ASP.NET.Services;



public class PageLinkGenerator
{
    public PageLinkGenerator() { }
    public ICollection<string> GeneratePageLink(int pagecount,bool vipped=false)
    {    
        ICollection<string> links=new List<string>();
        string mainPage = "https://turbo.az/autos?page=";
        if (vipped)
        {
            mainPage = "https://turbo.az/autos/vip?page=";
        }
        for (int i = 1; i < pagecount; i++)
        {
            links.Append(mainPage + i.ToString());
        }
























        return links;
    }


}
