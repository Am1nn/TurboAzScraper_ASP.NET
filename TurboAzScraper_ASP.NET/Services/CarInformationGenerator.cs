using HtmlAgilityPack;
using System.Xml;

namespace TurboAzScraper_ASP.NET.Services;

public class CarInformationGenerator
{


    static void Main()
    {


        //List<string> carLinks = GetCarLinks("https://turbo.az/autos?page=");


        List<string> pageLinks = CreatePages(0, 5);
        foreach (string link in pageLinks)
        {
            List<string> carLinks = GetCarLinks(link);
            foreach (string carlink in carLinks)
            {
                GetCarInformation(carlink);
            }
        }
    }



    static public List<string> GetCarLinks(string page) 
    {
        var links = new List<string>();
        string url = page;
        var httpClient = new HttpClient();
        var html = httpClient.GetStringAsync(url).Result;
        var htmlDocument = new HtmlDocument();
        htmlDocument.LoadHtml(html);

        HtmlNode carDiv = htmlDocument.DocumentNode.SelectSingleNode("//div[@class='products-i']");
        HtmlNodeCollection carlinks = carDiv.SelectNodes("//a[@class='products-i__link']");

        foreach (var carlink in carlinks)
        {
            string href = carlink.GetAttributeValue("href", "");
            Console.WriteLine("https://turbo.az" + href);
            links.Add("https://turbo.az" + href);
        }

        return links;
    }






    static public void GetCarInformation(string Carlink)
    {
        string url = Carlink;
        var httpClient = new HttpClient();
        var html = httpClient.GetStringAsync(url).Result;
        var htmlDocument = new HtmlDocument();
        htmlDocument.LoadHtml(html);



        // Price
        Console.ForegroundColor = ConsoleColor.Red;
        HtmlNode priceNode = htmlDocument.DocumentNode.SelectSingleNode("//div[@class='product-price__i product-price__i--bold']");
        Console.WriteLine("Price:" + priceNode.GetDirectInnerText().ToString());
        // Image
        Console.ForegroundColor = ConsoleColor.Green;
        HtmlNode imagediv = htmlDocument.DocumentNode.SelectSingleNode("//div[@class='product tz-container']");
        HtmlNodeCollection images = imagediv.SelectNodes("//img");
        foreach (var image in images)
        {
            string link = image.GetAttributeValue("src", "");
            if (link.IndexOf("/uploads/f660x496/") != -1)
            {
                Console.WriteLine(link); // Linkler
            }
        }


        HtmlNode carInfoDiv = htmlDocument.DocumentNode.SelectSingleNode("//div[@class='product-properties__column']");

        HtmlNodeCollection carInfo_1 = carInfoDiv.SelectNodes("//div[@class='product-properties__i']");

        Console.ForegroundColor = ConsoleColor.Magenta;
        foreach (var carInfo in carInfo_1)
        {
            //Console.WriteLine(carInfo.InnerText);
            string infoStr = carInfo.InnerText.ToString();
            int tempindex;
            if (infoStr.IndexOf("Şəhər") != -1)
            {
                tempindex = infoStr.IndexOf("Şəhər");
                Console.WriteLine("Seher:" + infoStr.Substring(tempindex + 5));  // Seher
            }
            else if (infoStr.IndexOf("Marka") != -1)
            {
                tempindex = infoStr.IndexOf("Marka");
                Console.WriteLine("Marka:" + infoStr.Substring(tempindex + 5)); // Marka
            }
            else if (infoStr.IndexOf("Model") != -1)
            {
                tempindex = infoStr.IndexOf("Model");
                Console.WriteLine("Model:" + infoStr.Substring(tempindex + 5)); // Model
            }
            else if (infoStr.IndexOf("Buraxılış ili") != -1)
            {
                tempindex = infoStr.IndexOf("Buraxılış ili");
                Console.WriteLine("Buraxılış ili:" + infoStr.Substring(tempindex + 13)); // Buraxılış ili
            }
            else if (infoStr.IndexOf("Ban növü") != -1)
            {
                tempindex = infoStr.IndexOf("Ban növü");
                Console.WriteLine("Ban növü:" + infoStr.Substring(tempindex + 8)); // Ban növü
            }
            else if (infoStr.IndexOf("Rəng") != -1)
            {
                tempindex = infoStr.IndexOf("Rəng");
                Console.WriteLine("Rəng:" + infoStr.Substring(tempindex + 4)); //Rəng
            }
            else if (infoStr.IndexOf("Mühərrik") != -1)
            {
                tempindex = infoStr.IndexOf("Mühərrik");
                Console.WriteLine("Mühərrik:" + infoStr.Substring(tempindex + 8)); //Mühərrik
            }
            else if (infoStr.IndexOf("Yürüş") != -1)
            {
                tempindex = infoStr.IndexOf("Yürüş");
                Console.WriteLine("Yürüş:" + infoStr.Substring(tempindex + 5)); //Yürüş
            }
            else if (infoStr.IndexOf("Sürətlər qutusu") != -1)
            {
                tempindex = infoStr.IndexOf("Sürətlər qutusu");
                Console.WriteLine("Sürətlər qutusu:" + infoStr.Substring(tempindex + 15)); //Sürətlər qutusu
            }
            else if (infoStr.IndexOf("Ötürücü") != -1)
            {
                tempindex = infoStr.IndexOf("Ötürücü");
                Console.WriteLine("Ötürücü:" + infoStr.Substring(tempindex + 7)); //Ötürücü
            }
            else if (infoStr.IndexOf("Yeni") != -1)
            {
                tempindex = infoStr.IndexOf("Yeni");
                Console.WriteLine("Yeni:" + infoStr.Substring(tempindex + 4)); //Yeni
            }
        }

        Console.ResetColor();
        //Additional
        HtmlNode additional = htmlDocument.DocumentNode.SelectSingleNode("//div[@class='product-description__content js-description-content']");
        if (additional != null)
        {
            Console.WriteLine("\nAdditional:" + additional.InnerText);
        }
        Console.ForegroundColor = ConsoleColor.Blue;
        //Atribute
        HtmlNodeCollection AtributtesList = htmlDocument.DocumentNode.SelectNodes("//li[@class='product-extras__i']");
        if (AtributtesList != null)
        {
            foreach (var atribute in AtributtesList)
            {
                Console.WriteLine(atribute.InnerText);
            }
        }
        Console.ForegroundColor = ConsoleColor.DarkMagenta;
        //Seller Name
        HtmlNode sellername = htmlDocument.DocumentNode.SelectSingleNode("//div[@class='product-owner__info-name']");
        if (sellername != null)
        {
            Console.WriteLine("\nSellerName:" + sellername.InnerText);
        }
        else
        {
            sellername = htmlDocument.DocumentNode.SelectSingleNode("//div[@class='product-shop__owner-name']");
            Console.WriteLine("\nSellerName:" + sellername.InnerText);
        }
        ////Seller Phone
        HtmlNode sellerPhone = htmlDocument.DocumentNode.SelectSingleNode("//a[@class='product-phones__list-i']");
        if (sellerPhone != null)
        {
            Console.WriteLine("SellerPhone:" + sellerPhone.InnerText);
        }
    }


    static List<string> CreatePages(int? startpage, int? endpage)
    {
        List<string> pages = new List<string>();
        int? counter = startpage;

        while (counter <= endpage)
        {
            pages.Add($"https://turbo.az/autos?page={counter}");
            counter += 1;
        }



        return pages;
    }
}
