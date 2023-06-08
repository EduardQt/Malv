using System.Text.RegularExpressions;
using HtmlAgilityPack;
using Malv.Data.EF.Entity;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using Newtonsoft.Json;

namespace Malv.MobileDe;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class Tests : PageTest
{
    [Test]
    public async Task HomepageHasPlaywrightInTitleAndGetStartedLinkLinkingtoTheIntroPage()
    {
        var chromium = await Playwright.Chromium.LaunchAsync();
        var context = await chromium.NewContextAsync(new BrowserNewContextOptions()
        {
            UserAgent =
                "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_7) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/111.0.0.0 Safari/537.36"
        });
        var page = await context.NewPageAsync();
        await page.GotoAsync("https://www.mobile.de/");
        var content = await page.ContentAsync();
        var doc = new HtmlDocument();
        doc.LoadHtml(content);
        HtmlNode selectNode = doc.DocumentNode.SelectSingleNode("//select[@data-testid='qs-select-make']");
        var alleMarken = selectNode.ChildNodes.Single(x => x.Attributes.Any(attribute => attribute.Value == "Alle Marken"));

        IList<Brand> carBrands = new List<Brand>();
        foreach (HtmlNode option in alleMarken.ChildNodes)
        {
            var name = option.InnerHtml;
            var optionValue = option.GetAttributeValue("value", "-1");
            var newPage = await context.NewPageAsync();
            await newPage.GotoAsync($"https://m.mobile.de/consumer/api/search/reference-data/models/{optionValue}");
            var carModelsContent = await newPage.ContentAsync();
            var carModelsDocument = new HtmlDocument();
            carModelsDocument.LoadHtml(carModelsContent);
            var carModelsJsonString = carModelsDocument.DocumentNode.SelectSingleNode("//pre").InnerHtml;
            carBrands.Add(new Brand()
            {
                Name = name,
                Models = carModelsJsonString
            });
        }

        string output = JsonConvert.SerializeObject(carBrands);
        output.Replace("Andere", "Other");
        await File.WriteAllTextAsync("bs.json", output);
    }
    
    [Test]
    public async Task Parser()
    {
        IList<Brand> oldBrands = JsonConvert.DeserializeObject<IList<Brand>>(await File.ReadAllTextAsync("bs.json"));

        IList<FinalBrand> newBrands = new List<FinalBrand>();
        IList<Category> categories = new List<Category>();
        foreach (var oldBrand in oldBrands)
        {
            Category category = new Category()
            {
                Name = oldBrand.Name
            };
            FinalModels models = JsonConvert.DeserializeObject<FinalModels>(oldBrand.Models);
            var test = RecursiveSearch(models.Data);
            category.Children = test;
            categories.Add(category);
        }
        
        string output = JsonConvert.SerializeObject(categories);
        await File.WriteAllTextAsync("newbs.json", output);
    }

    static List<Category> RecursiveSearch(IList<FinalModel> models)
    {
        List<Category> categories = new List<Category>();
        if (models == null || models.Count == 0) return categories;

        foreach (FinalModel model in models)
        {
            Category category = new Category();
            category.Name = model.Label ?? model.OptgroupLabel;
            //if (category.Name.Contains("Alle") && model.Items == null) continue;
            category.Children = RecursiveSearch(model.Items);
            categories.Add(category);
        }
        
        return categories;
    }
}