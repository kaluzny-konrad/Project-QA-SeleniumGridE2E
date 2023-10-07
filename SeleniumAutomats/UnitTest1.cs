using NUnit.Framework.Interfaces;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;

namespace SeleniumAutomats;

[TestFixture]
[Parallelizable(ParallelScope.All)]
public class Tests
{
    protected readonly string BaseUrl = "http://books.toscrape.com";
    protected readonly string HubUrl = "http://localhost:4444/wd/hub";
    protected readonly ChromeOptions ChromeOptions;

    public Tests()
    {
        ChromeOptions = new ChromeOptions();
        ChromeOptions.AddArgument("--headless");
        ChromeOptions.AddArgument("--no-sandbox");
        ChromeOptions.AddArgument("--disable-dev-shm-usage");
        ChromeOptions.AddArgument("--start-maximized");
    }

    [Test]
    public void AllProductsShouldBe20()
    {
        var driver = GetDriver();
        try
        {
            var pageBody = driver.FindElement(By.XPath("//div[@class='container-fluid page']"));
            var allProducts = pageBody.FindElements(By.XPath("//article[@class='product_pod']"));
            Assert.That(allProducts.Count, Is.EqualTo(20));
        }
        catch (Exception ex)
        {
            TearDown(driver);
            Assert.Fail(ex.Message);
        }
    }

    [Test]
    public void AllProductsShouldHavePrice()
    {
        var driver = GetDriver();
        try
        {
            var pageBody = driver.FindElement(By.XPath("//div[@class='container-fluid page']"));
            var allProducts = pageBody.FindElements(By.XPath("//article[@class='product_pod']"));
            foreach (var product in allProducts)
            {
                var price = product.FindElement(By.XPath(".//p[@class='price_color']"));
                Assert.That(price.Text, Does.Match(@"£\d+\.\d+"));
            }
        }
        catch (Exception ex)
        {
            TearDown(driver);
            Assert.Fail(ex.Message);
        }
    }

    [Test]
    public void AllProductsShouldHaveRating()
    {
        var driver = GetDriver();
        try
        {
            var pageBody = driver.FindElement(By.XPath("//div[@class='container-fluid page']"));
            var allProducts = pageBody.FindElements(By.XPath("//article[@class='product_pod']"));
            foreach (var product in allProducts)
            {
                var rating = product.FindElement(By.XPath(".//p[contains(@class, 'star-rating')]"));
                Assert.That(rating.GetAttribute("class"), Does.Match(@"star-rating (\w+)"));
            }
        }
        catch (Exception ex)
        {
            TearDown(driver);
            Assert.Fail(ex.Message);
        }
    }

    [Test]
    public void AllProductsShouldHaveTitle()
    {
        var driver = GetDriver();
        try
        {
            var pageBody = driver.FindElement(By.XPath("//div[@class='container-fluid page']"));
            var allProducts = pageBody.FindElements(By.XPath("//article[@class='product_pod']"));
            foreach (var product in allProducts)
            {
                var title = product.FindElement(By.XPath(".//h3/a"));
                Assert.That(title.Text, Is.Not.Empty);
            }
        }
        catch (Exception ex)
        {
            TearDown(driver);
            Assert.Fail(ex.Message);
        }
    }

    [Test]
    public void AllProductsShouldHaveLink()
    {
        var driver = GetDriver();
        try
        {
            var pageBody = driver.FindElement(By.XPath("//div[@class='container-fluid page']"));
            var allProducts = pageBody.FindElements(By.XPath("//article[@class='product_pod']"));
            foreach (var product in allProducts)
            {
                var link = product.FindElement(By.XPath(".//h3/a"));
                Assert.That(link.GetAttribute("href"), Does.Match(@"http://books\.toscrape\.com/catalogue/.*"));
            }
        }
        catch (Exception ex)
        {
            TearDown(driver);
            Assert.Fail(ex.Message);
        }
    }

    [Test]
    public void AllProductsShouldHaveImage()
    {
        var driver = GetDriver();
        try
        {
            var pageBody = driver.FindElement(By.XPath("//div[@class='container-fluid page']"));
            var allProducts = pageBody.FindElements(By.XPath("//article[@class='product_pod']"));
            foreach (var product in allProducts)
            {
                var image = product.FindElement(By.XPath(".//div[@class='image_container']/a/img"));
                Assert.That(image.GetAttribute("src"), Does.Match(@"http://books\.toscrape\.com/media/cache/.*"));
            }
        }
        catch (Exception ex)
        {
            TearDown(driver);
            Assert.Fail(ex.Message);
        }
    }

    [Test]
    public void AllProductsShouldHaveAddToBasketButton()
    {
        var driver = GetDriver();
        try
        {
            var pageBody = driver.FindElement(By.XPath("//div[@class='container-fluid page']"));
            var allProducts = pageBody.FindElements(By.XPath("//article[@class='product_pod']"));
            foreach (var product in allProducts)
            {
                var button = product.FindElement(By.XPath(".//form/button"));
                Assert.That(button.Text, Is.EqualTo("Add to basket"));
            }
        }
        catch (Exception ex)
        {
            TearDown(driver);
            Assert.Fail(ex.Message);
        }
    }

    [Test]
    public void AllProductsShouldHaveInStockLabel()
    {
        var driver = GetDriver();
        try
        {
            var pageBody = driver.FindElement(By.XPath("//div[@class='container-fluid page']"));
            var allProducts = pageBody.FindElements(By.XPath("//article[@class='product_pod']"));
            foreach (var product in allProducts)
            {
                var label = product.FindElement(By.XPath(".//p[@class='instock availability']"));
                Assert.That(label.Text, Is.EqualTo("In stock"));
            }
        }
        catch (Exception ex)
        {
            TearDown(driver);
            Assert.Fail(ex.Message);
        }
    }

    [Test]
    public void AllResultsShouldBe1000()
    {
        var driver = GetDriver();
        try
        {
            var pageBody = driver.FindElement(By.XPath("//div[@class='container-fluid page']"));
            var results = pageBody.FindElement(By.XPath("//form/strong[1]"));
            Assert.That(results.Text, Is.EqualTo("1000"));
        }
        catch (Exception ex)
        {
            TearDown(driver);
            Assert.Fail(ex.Message);
        }
    }

    [Test]
    public void ClassicsBooksShouldBe19Results()
    {
        var driver = GetDriver();
        try
        {
            var pageBody = driver.FindElement(By.XPath("//div[@class='container-fluid page']"));

            var sideCategories = pageBody.FindElement(By.XPath("//div[@class='side_categories']"));
            var booksSubCategory = sideCategories.FindElement(By.XPath("//a[contains(text(), 'Classics')]"));
            booksSubCategory.Click();

            var results = pageBody.FindElement(By.XPath("//form/strong[1]"));
            Assert.That(results.Text, Is.EqualTo("19"));
        }
        catch (Exception ex)
        {
            TearDown(driver);
            Assert.Fail(ex.Message);
        }
    }

    [Test]
    public void MusicBookWithTitleHowMusicWorksShouldCanBeOpenedByClickInCover()
    {
        var expectedPageUrl = $"{BaseUrl}/catalogue/how-music-works_979/index.html";
        var driver = GetDriver();
        try
        {
            var pageBody = driver.FindElement(By.XPath("//div[@class='container-fluid page']"));

            var sideCategories = pageBody.FindElement(By.XPath("//div[@class='side_categories']"));
            var booksSubCategory = sideCategories.FindElement(By.XPath("//a[contains(text(), 'Music')]"));
            booksSubCategory.Click();

            pageBody = driver.FindElement(By.XPath("//div[@class='container-fluid page']"));
            var allProducts = pageBody.FindElements(By.XPath("//article[@class='product_pod']"));
            var product = allProducts.First(p => p.FindElement(By.XPath(".//h3/a")).Text == "How Music Works");
            var image = product.FindElement(By.XPath(".//div[@class='image_container']/a/img"));
            image.Click();
            Assert.That(driver.Url, Is.EqualTo(expectedPageUrl));
        }
        catch (Exception ex)
        {
            TearDown(driver);
            Assert.Fail(ex.Message);
        }
    }

    [Test]
    public void MusicBookWithTitleHowMusicWorksShould_HaveCorrectSamePriceOnMainPageAndProductPage()
    {
        var expectedPageUrl = $"{BaseUrl}/catalogue/how-music-works_979/index.html";
        var driver = GetDriver();
        try
        {
            var pageBody = driver.FindElement(By.XPath("//div[@class='container-fluid page']"));

            var sideCategories = pageBody.FindElement(By.XPath("//div[@class='side_categories']"));
            var booksSubCategory = sideCategories.FindElement(By.XPath("//a[contains(text(), 'Music')]"));
            booksSubCategory.Click();

            pageBody = driver.FindElement(By.XPath("//div[@class='container-fluid page']"));
            var allProducts = pageBody.FindElements(By.XPath("//article[@class='product_pod']"));
            var product = allProducts.First(p => p.FindElement(By.XPath(".//h3/a")).Text == "How Music Works");
            var image = product.FindElement(By.XPath(".//div[@class='image_container']/a/img"));
            var expectedProductPriceText = product.FindElement(By.XPath(".//p[@class='price_color']")).Text;
            decimal expectedProductPrice = GetParsedProductPrice(expectedProductPriceText);
            image.Click();

            pageBody = driver.FindElement(By.XPath("//div[@class='container-fluid page']"));
            var productPriceText = pageBody.FindElement(By.XPath("//p[@class='price_color']")).Text;
            var productPrice = GetParsedProductPrice(productPriceText);

            Assert.That(productPrice, Is.EqualTo(expectedProductPrice));
        }
        catch (Exception ex)
        {
            TearDown(driver);
            Assert.Fail(ex.Message);
        }
    }

    private decimal GetParsedProductPrice(string expectedProductPriceText)
    {
        // £37.32
        var priceText = expectedProductPriceText.Replace("£", string.Empty).Replace(".", ",");
        return decimal.Parse(priceText);
    }

    [Test]
    public void FailTestForScreenshot()
    {
        Assert.Fail();
    }

    private RemoteWebDriver GetDriver()
    {
        var driver = new RemoteWebDriver(new Uri("http://localhost:4444/wd/hub"), ChromeOptions);
        Console.WriteLine($"SessionId: {driver.SessionId}");
        driver.Navigate().GoToUrl(BaseUrl);
        return driver;
    }

    private static void TearDown(RemoteWebDriver? driver)
    {
        if (driver != null)
        {
            try
            {
                if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
                {
                    var screenshot = driver.GetScreenshot();
                    var fileName = TestContext.CurrentContext.Test.Name + ".png";
                    var filePath = Path.Combine(TestContext.CurrentContext.WorkDirectory, fileName);
                    screenshot.SaveAsFile(fileName, ScreenshotImageFormat.Png);
                    Console.WriteLine($"Screenshot saved: {filePath}");
                }
            }
            catch (Exception) { }

            driver.Quit();
            driver.Dispose();
        }
    }
}

/* Full InnerHtml of div container-fluid On MainPage:
    <div class="page_inner">
        
    <ul class="breadcrumb">
        <li>
            <a href="index.html">Home</a>
        </li>
        <li class="active">All products</li>
    </ul>

        <div class="row">

            <aside class="sidebar col-sm-4 col-md-3">
                
                <div id="promotions_left">
                    
                </div>
                
    
    
        
        <div class="side_categories">
            <ul class="nav nav-list">
                
                    <li>
                        <a href="catalogue/category/books_1/index.html">
                            
                                Books
                            
                        </a>

                        <ul>
                        
                
                    <li>
                        <a href="catalogue/category/books/travel_2/index.html">
                            
                                Travel
                            
                        </a>

                        </li>
                        
                
                    <li>
                        <a href="catalogue/category/books/mystery_3/index.html">
                            
                                Mystery
                            
                        </a>

                        </li>
                        
                
                    <li>
                        <a href="catalogue/category/books/historical-fiction_4/index.html">
                            
                                Historical Fiction
                            
                        </a>

                        </li>
                        
                
                    <li>
                        <a href="catalogue/category/books/sequential-art_5/index.html">
                            
                                Sequential Art
                            
                        </a>

                        </li>
                        
                
                    <li>
                        <a href="catalogue/category/books/classics_6/index.html">
                            
                                Classics
                            
                        </a>

                        </li>
                        
                
                    <li>
                        <a href="catalogue/category/books/philosophy_7/index.html">
                            
                                Philosophy
                            
                        </a>

                        </li>
                        
                
                    <li>
                        <a href="catalogue/category/books/romance_8/index.html">
                            
                                Romance
                            
                        </a>

                        </li>
                        
                
                    <li>
                        <a href="catalogue/category/books/womens-fiction_9/index.html">
                            
                                Womens Fiction
                            
                        </a>

                        </li>
                        
                
                    <li>
                        <a href="catalogue/category/books/fiction_10/index.html">
                            
                                Fiction
                            
                        </a>

                        </li>
                        
                
                    <li>
                        <a href="catalogue/category/books/childrens_11/index.html">
                            
                                Childrens
                            
                        </a>

                        </li>
                        
                
                    <li>
                        <a href="catalogue/category/books/religion_12/index.html">
                            
                                Religion
                            
                        </a>

                        </li>
                        
                
                    <li>
                        <a href="catalogue/category/books/nonfiction_13/index.html">
                            
                                Nonfiction
                            
                        </a>

                        </li>
                        
                
                    <li>
                        <a href="catalogue/category/books/music_14/index.html">
                            
                                Music
                            
                        </a>

                        </li>
                        
                
                    <li>
                        <a href="catalogue/category/books/default_15/index.html">
                            
                                Default
                            
                        </a>

                        </li>
                        
                
                    <li>
                        <a href="catalogue/category/books/science-fiction_16/index.html">
                            
                                Science Fiction
                            
                        </a>

                        </li>
                        
                
                    <li>
                        <a href="catalogue/category/books/sports-and-games_17/index.html">
                            
                                Sports and Games
                            
                        </a>

                        </li>
                        
                
                    <li>
                        <a href="catalogue/category/books/add-a-comment_18/index.html">
                            
                                Add a comment
                            
                        </a>

                        </li>
                        
                
                    <li>
                        <a href="catalogue/category/books/fantasy_19/index.html">
                            
                                Fantasy
                            
                        </a>

                        </li>
                        
                
                    <li>
                        <a href="catalogue/category/books/new-adult_20/index.html">
                            
                                New Adult
                            
                        </a>

                        </li>
                        
                
                    <li>
                        <a href="catalogue/category/books/young-adult_21/index.html">
                            
                                Young Adult
                            
                        </a>

                        </li>
                        
                
                    <li>
                        <a href="catalogue/category/books/science_22/index.html">
                            
                                Science
                            
                        </a>

                        </li>
                        
                
                    <li>
                        <a href="catalogue/category/books/poetry_23/index.html">
                            
                                Poetry
                            
                        </a>

                        </li>
                        
                
                    <li>
                        <a href="catalogue/category/books/paranormal_24/index.html">
                            
                                Paranormal
                            
                        </a>

                        </li>
                        
                
                    <li>
                        <a href="catalogue/category/books/art_25/index.html">
                            
                                Art
                            
                        </a>

                        </li>
                        
                
                    <li>
                        <a href="catalogue/category/books/psychology_26/index.html">
                            
                                Psychology
                            
                        </a>

                        </li>
                        
                
                    <li>
                        <a href="catalogue/category/books/autobiography_27/index.html">
                            
                                Autobiography
                            
                        </a>

                        </li>
                        
                
                    <li>
                        <a href="catalogue/category/books/parenting_28/index.html">
                            
                                Parenting
                            
                        </a>

                        </li>
                        
                
                    <li>
                        <a href="catalogue/category/books/adult-fiction_29/index.html">
                            
                                Adult Fiction
                            
                        </a>

                        </li>
                        
                
                    <li>
                        <a href="catalogue/category/books/humor_30/index.html">
                            
                                Humor
                            
                        </a>

                        </li>
                        
                
                    <li>
                        <a href="catalogue/category/books/horror_31/index.html">
                            
                                Horror
                            
                        </a>

                        </li>
                        
                
                    <li>
                        <a href="catalogue/category/books/history_32/index.html">
                            
                                History
                            
                        </a>

                        </li>
                        
                
                    <li>
                        <a href="catalogue/category/books/food-and-drink_33/index.html">
                            
                                Food and Drink
                            
                        </a>

                        </li>
                        
                
                    <li>
                        <a href="catalogue/category/books/christian-fiction_34/index.html">
                            
                                Christian Fiction
                            
                        </a>

                        </li>
                        
                
                    <li>
                        <a href="catalogue/category/books/business_35/index.html">
                            
                                Business
                            
                        </a>

                        </li>
                        
                
                    <li>
                        <a href="catalogue/category/books/biography_36/index.html">
                            
                                Biography
                            
                        </a>

                        </li>
                        
                
                    <li>
                        <a href="catalogue/category/books/thriller_37/index.html">
                            
                                Thriller
                            
                        </a>

                        </li>
                        
                
                    <li>
                        <a href="catalogue/category/books/contemporary_38/index.html">
                            
                                Contemporary
                            
                        </a>

                        </li>
                        
                
                    <li>
                        <a href="catalogue/category/books/spirituality_39/index.html">
                            
                                Spirituality
                            
                        </a>

                        </li>
                        
                
                    <li>
                        <a href="catalogue/category/books/academic_40/index.html">
                            
                                Academic
                            
                        </a>

                        </li>
                        
                
                    <li>
                        <a href="catalogue/category/books/self-help_41/index.html">
                            
                                Self Help
                            
                        </a>

                        </li>
                        
                
                    <li>
                        <a href="catalogue/category/books/historical_42/index.html">
                            
                                Historical
                            
                        </a>

                        </li>
                        
                
                    <li>
                        <a href="catalogue/category/books/christian_43/index.html">
                            
                                Christian
                            
                        </a>

                        </li>
                        
                
                    <li>
                        <a href="catalogue/category/books/suspense_44/index.html">
                            
                                Suspense
                            
                        </a>

                        </li>
                        
                
                    <li>
                        <a href="catalogue/category/books/short-stories_45/index.html">
                            
                                Short Stories
                            
                        </a>

                        </li>
                        
                
                    <li>
                        <a href="catalogue/category/books/novels_46/index.html">
                            
                                Novels
                            
                        </a>

                        </li>
                        
                
                    <li>
                        <a href="catalogue/category/books/health_47/index.html">
                            
                                Health
                            
                        </a>

                        </li>
                        
                
                    <li>
                        <a href="catalogue/category/books/politics_48/index.html">
                            
                                Politics
                            
                        </a>

                        </li>
                        
                
                    <li>
                        <a href="catalogue/category/books/cultural_49/index.html">
                            
                                Cultural
                            
                        </a>

                        </li>
                        
                
                    <li>
                        <a href="catalogue/category/books/erotica_50/index.html">
                            
                                Erotica
                            
                        </a>

                        </li>
                        
                
                    <li>
                        <a href="catalogue/category/books/crime_51/index.html">
                            
                                Crime
                            
                        </a>

                        </li>
                        
                            </ul></li>
                        
                
            </ul>
        </div>
    
    

            </aside>

            <div class="col-sm-8 col-md-9">
                
                <div class="page-header action">
                    <h1>All products</h1>
                </div>
                

                



<div id="messages">

</div>


                <div id="promotions">
                    
                </div>

                
    <form method="get" class="form-horizontal">
        
        <div style="display:none">
            
            
        </div>

        
            
                
                    <strong>1000</strong> results - showing <strong>1</strong> to <strong>20</strong>.
                
            
            
        
    </form>
    
        <section>
            <div class="alert alert-warning" role="alert"><strong>Warning!</strong> This is a demo website for web scraping purposes. Prices and ratings here were randomly assigned and have no real meaning.</div>

            <div>
                <ol class="row">
                    
                        <li class="col-xs-6 col-sm-4 col-md-3 col-lg-3">






    <article class="product_pod">
        
            <div class="image_container">
                
                    
                    <a href="catalogue/a-light-in-the-attic_1000/index.html"><img src="media/cache/2c/da/2cdad67c44b002e7ead0cc35693c0e8b.jpg" alt="A Light in the Attic" class="thumbnail"></a>
                    
                
            </div>
        

        
            
                <p class="star-rating Three">
                    <i class="icon-star"></i>
                    <i class="icon-star"></i>
                    <i class="icon-star"></i>
                    <i class="icon-star"></i>
                    <i class="icon-star"></i>
                </p>
            
        

        
            <h3><a href="catalogue/a-light-in-the-attic_1000/index.html" title="A Light in the Attic">A Light in the ...</a></h3>
        

        
            <div class="product_price">
                






    
        <p class="price_color">£51.77</p>
    

<p class="instock availability">
    <i class="icon-ok"></i>
    
        In stock
    
</p>

                
                    






    
    <form>
        <button type="submit" class="btn btn-primary btn-block" data-loading-text="Adding...">Add to basket</button>
    </form>


                
            </div>
        
    </article>

</li>
                    
                        <li class="col-xs-6 col-sm-4 col-md-3 col-lg-3">






    <article class="product_pod">
        
            <div class="image_container">
                
                    
                    <a href="catalogue/tipping-the-velvet_999/index.html"><img src="media/cache/26/0c/260c6ae16bce31c8f8c95daddd9f4a1c.jpg" alt="Tipping the Velvet" class="thumbnail"></a>
                    
                
            </div>
        

        
            
                <p class="star-rating One">
                    <i class="icon-star"></i>
                    <i class="icon-star"></i>
                    <i class="icon-star"></i>
                    <i class="icon-star"></i>
                    <i class="icon-star"></i>
                </p>
            
        

        
            <h3><a href="catalogue/tipping-the-velvet_999/index.html" title="Tipping the Velvet">Tipping the Velvet</a></h3>
        

        
            <div class="product_price">
                






    
        <p class="price_color">£53.74</p>
    

<p class="instock availability">
    <i class="icon-ok"></i>
    
        In stock
    
</p>

                
                    






    
    <form>
        <button type="submit" class="btn btn-primary btn-block" data-loading-text="Adding...">Add to basket</button>
    </form>


                
            </div>
        
    </article>

</li>
                    
                        <li class="col-xs-6 col-sm-4 col-md-3 col-lg-3">






    <article class="product_pod">
        
            <div class="image_container">
                
                    
                    <a href="catalogue/soumission_998/index.html"><img src="media/cache/3e/ef/3eef99c9d9adef34639f510662022830.jpg" alt="Soumission" class="thumbnail"></a>
                    
                
            </div>
        

        
            
                <p class="star-rating One">
                    <i class="icon-star"></i>
                    <i class="icon-star"></i>
                    <i class="icon-star"></i>
                    <i class="icon-star"></i>
                    <i class="icon-star"></i>
                </p>
            
        

        
            <h3><a href="catalogue/soumission_998/index.html" title="Soumission">Soumission</a></h3>
        

        
            <div class="product_price">
                






    
        <p class="price_color">£50.10</p>
    

<p class="instock availability">
    <i class="icon-ok"></i>
    
        In stock
    
</p>

                
                    






    
    <form>
        <button type="submit" class="btn btn-primary btn-block" data-loading-text="Adding...">Add to basket</button>
    </form>


                
            </div>
        
    </article>

</li>
                    
                        <li class="col-xs-6 col-sm-4 col-md-3 col-lg-3">






    <article class="product_pod">
        
            <div class="image_container">
                
                    
                    <a href="catalogue/sharp-objects_997/index.html"><img src="media/cache/32/51/3251cf3a3412f53f339e42cac2134093.jpg" alt="Sharp Objects" class="thumbnail"></a>
                    
                
            </div>
        

        
            
                <p class="star-rating Four">
                    <i class="icon-star"></i>
                    <i class="icon-star"></i>
                    <i class="icon-star"></i>
                    <i class="icon-star"></i>
                    <i class="icon-star"></i>
                </p>
            
        

        
            <h3><a href="catalogue/sharp-objects_997/index.html" title="Sharp Objects">Sharp Objects</a></h3>
        

        
            <div class="product_price">
                






    
        <p class="price_color">£47.82</p>
    

<p class="instock availability">
    <i class="icon-ok"></i>
    
        In stock
    
</p>

                
                    






    
    <form>
        <button type="submit" class="btn btn-primary btn-block" data-loading-text="Adding...">Add to basket</button>
    </form>


                
            </div>
        
    </article>

</li>
                    
                        <li class="col-xs-6 col-sm-4 col-md-3 col-lg-3">






    <article class="product_pod">
        
            <div class="image_container">
                
                    
                    <a href="catalogue/sapiens-a-brief-history-of-humankind_996/index.html"><img src="media/cache/be/a5/bea5697f2534a2f86a3ef27b5a8c12a6.jpg" alt="Sapiens: A Brief History of Humankind" class="thumbnail"></a>
                    
                
            </div>
        

        
            
                <p class="star-rating Five">
                    <i class="icon-star"></i>
                    <i class="icon-star"></i>
                    <i class="icon-star"></i>
                    <i class="icon-star"></i>
                    <i class="icon-star"></i>
                </p>
            
        

        
            <h3><a href="catalogue/sapiens-a-brief-history-of-humankind_996/index.html" title="Sapiens: A Brief History of Humankind">Sapiens: A Brief History ...</a></h3>
        

        
            <div class="product_price">
                






    
        <p class="price_color">£54.23</p>
    

<p class="instock availability">
    <i class="icon-ok"></i>
    
        In stock
    
</p>

                
                    






    
    <form>
        <button type="submit" class="btn btn-primary btn-block" data-loading-text="Adding...">Add to basket</button>
    </form>


                
            </div>
        
    </article>

</li>
                    
                        <li class="col-xs-6 col-sm-4 col-md-3 col-lg-3">






    <article class="product_pod">
        
            <div class="image_container">
                
                    
                    <a href="catalogue/the-requiem-red_995/index.html"><img src="media/cache/68/33/68339b4c9bc034267e1da611ab3b34f8.jpg" alt="The Requiem Red" class="thumbnail"></a>
                    
                
            </div>
        

        
            
                <p class="star-rating One">
                    <i class="icon-star"></i>
                    <i class="icon-star"></i>
                    <i class="icon-star"></i>
                    <i class="icon-star"></i>
                    <i class="icon-star"></i>
                </p>
            
        

        
            <h3><a href="catalogue/the-requiem-red_995/index.html" title="The Requiem Red">The Requiem Red</a></h3>
        

        
            <div class="product_price">
                






    
        <p class="price_color">£22.65</p>
    

<p class="instock availability">
    <i class="icon-ok"></i>
    
        In stock
    
</p>

                
                    






    
    <form>
        <button type="submit" class="btn btn-primary btn-block" data-loading-text="Adding...">Add to basket</button>
    </form>


                
            </div>
        
    </article>

</li>
                    
                        <li class="col-xs-6 col-sm-4 col-md-3 col-lg-3">






    <article class="product_pod">
        
            <div class="image_container">
                
                    
                    <a href="catalogue/the-dirty-little-secrets-of-getting-your-dream-job_994/index.html"><img src="media/cache/92/27/92274a95b7c251fea59a2b8a78275ab4.jpg" alt="The Dirty Little Secrets of Getting Your Dream Job" class="thumbnail"></a>
                    
                
            </div>
        

        
            
                <p class="star-rating Four">
                    <i class="icon-star"></i>
                    <i class="icon-star"></i>
                    <i class="icon-star"></i>
                    <i class="icon-star"></i>
                    <i class="icon-star"></i>
                </p>
            
        

        
            <h3><a href="catalogue/the-dirty-little-secrets-of-getting-your-dream-job_994/index.html" title="The Dirty Little Secrets of Getting Your Dream Job">The Dirty Little Secrets ...</a></h3>
        

        
            <div class="product_price">
                






    
        <p class="price_color">£33.34</p>
    

<p class="instock availability">
    <i class="icon-ok"></i>
    
        In stock
    
</p>

                
                    






    
    <form>
        <button type="submit" class="btn btn-primary btn-block" data-loading-text="Adding...">Add to basket</button>
    </form>


                
            </div>
        
    </article>

</li>
                    
                        <li class="col-xs-6 col-sm-4 col-md-3 col-lg-3">






    <article class="product_pod">
        
            <div class="image_container">
                
                    
                    <a href="catalogue/the-coming-woman-a-novel-based-on-the-life-of-the-infamous-feminist-victoria-woodhull_993/index.html"><img src="media/cache/3d/54/3d54940e57e662c4dd1f3ff00c78cc64.jpg" alt="The Coming Woman: A Novel Based on the Life of the Infamous Feminist, Victoria Woodhull" class="thumbnail"></a>
                    
                
            </div>
        

        
            
                <p class="star-rating Three">
                    <i class="icon-star"></i>
                    <i class="icon-star"></i>
                    <i class="icon-star"></i>
                    <i class="icon-star"></i>
                    <i class="icon-star"></i>
                </p>
            
        

        
            <h3><a href="catalogue/the-coming-woman-a-novel-based-on-the-life-of-the-infamous-feminist-victoria-woodhull_993/index.html" title="The Coming Woman: A Novel Based on the Life of the Infamous Feminist, Victoria Woodhull">The Coming Woman: A ...</a></h3>
        

        
            <div class="product_price">
                






    
        <p class="price_color">£17.93</p>
    

<p class="instock availability">
    <i class="icon-ok"></i>
    
        In stock
    
</p>

                
                    






    
    <form>
        <button type="submit" class="btn btn-primary btn-block" data-loading-text="Adding...">Add to basket</button>
    </form>


                
            </div>
        
    </article>

</li>
                    
                        <li class="col-xs-6 col-sm-4 col-md-3 col-lg-3">






    <article class="product_pod">
        
            <div class="image_container">
                
                    
                    <a href="catalogue/the-boys-in-the-boat-nine-americans-and-their-epic-quest-for-gold-at-the-1936-berlin-olympics_992/index.html"><img src="media/cache/66/88/66883b91f6804b2323c8369331cb7dd1.jpg" alt="The Boys in the Boat: Nine Americans and Their Epic Quest for Gold at the 1936 Berlin Olympics" class="thumbnail"></a>
                    
                
            </div>
        

        
            
                <p class="star-rating Four">
                    <i class="icon-star"></i>
                    <i class="icon-star"></i>
                    <i class="icon-star"></i>
                    <i class="icon-star"></i>
                    <i class="icon-star"></i>
                </p>
            
        

        
            <h3><a href="catalogue/the-boys-in-the-boat-nine-americans-and-their-epic-quest-for-gold-at-the-1936-berlin-olympics_992/index.html" title="The Boys in the Boat: Nine Americans and Their Epic Quest for Gold at the 1936 Berlin Olympics">The Boys in the ...</a></h3>
        

        
            <div class="product_price">
                






    
        <p class="price_color">£22.60</p>
    

<p class="instock availability">
    <i class="icon-ok"></i>
    
        In stock
    
</p>

                
                    






    
    <form>
        <button type="submit" class="btn btn-primary btn-block" data-loading-text="Adding...">Add to basket</button>
    </form>


                
            </div>
        
    </article>

</li>
                    
                        <li class="col-xs-6 col-sm-4 col-md-3 col-lg-3">






    <article class="product_pod">
        
            <div class="image_container">
                
                    
                    <a href="catalogue/the-black-maria_991/index.html"><img src="media/cache/58/46/5846057e28022268153beff6d352b06c.jpg" alt="The Black Maria" class="thumbnail"></a>
                    
                
            </div>
        

        
            
                <p class="star-rating One">
                    <i class="icon-star"></i>
                    <i class="icon-star"></i>
                    <i class="icon-star"></i>
                    <i class="icon-star"></i>
                    <i class="icon-star"></i>
                </p>
            
        

        
            <h3><a href="catalogue/the-black-maria_991/index.html" title="The Black Maria">The Black Maria</a></h3>
        

        
            <div class="product_price">
                






    
        <p class="price_color">£52.15</p>
    

<p class="instock availability">
    <i class="icon-ok"></i>
    
        In stock
    
</p>

                
                    






    
    <form>
        <button type="submit" class="btn btn-primary btn-block" data-loading-text="Adding...">Add to basket</button>
    </form>


                
            </div>
        
    </article>

</li>
                    
                        <li class="col-xs-6 col-sm-4 col-md-3 col-lg-3">






    <article class="product_pod">
        
            <div class="image_container">
                
                    
                    <a href="catalogue/starving-hearts-triangular-trade-trilogy-1_990/index.html"><img src="media/cache/be/f4/bef44da28c98f905a3ebec0b87be8530.jpg" alt="Starving Hearts (Triangular Trade Trilogy, #1)" class="thumbnail"></a>
                    
                
            </div>
        

        
            
                <p class="star-rating Two">
                    <i class="icon-star"></i>
                    <i class="icon-star"></i>
                    <i class="icon-star"></i>
                    <i class="icon-star"></i>
                    <i class="icon-star"></i>
                </p>
            
        

        
            <h3><a href="catalogue/starving-hearts-triangular-trade-trilogy-1_990/index.html" title="Starving Hearts (Triangular Trade Trilogy, #1)">Starving Hearts (Triangular Trade ...</a></h3>
        

        
            <div class="product_price">
                






    
        <p class="price_color">£13.99</p>
    

<p class="instock availability">
    <i class="icon-ok"></i>
    
        In stock
    
</p>

                
                    






    
    <form>
        <button type="submit" class="btn btn-primary btn-block" data-loading-text="Adding...">Add to basket</button>
    </form>


                
            </div>
        
    </article>

</li>
                    
                        <li class="col-xs-6 col-sm-4 col-md-3 col-lg-3">






    <article class="product_pod">
        
            <div class="image_container">
                
                    
                    <a href="catalogue/shakespeares-sonnets_989/index.html"><img src="media/cache/10/48/1048f63d3b5061cd2f424d20b3f9b666.jpg" alt="Shakespeare's Sonnets" class="thumbnail"></a>
                    
                
            </div>
        

        
            
                <p class="star-rating Four">
                    <i class="icon-star"></i>
                    <i class="icon-star"></i>
                    <i class="icon-star"></i>
                    <i class="icon-star"></i>
                    <i class="icon-star"></i>
                </p>
            
        

        
            <h3><a href="catalogue/shakespeares-sonnets_989/index.html" title="Shakespeare's Sonnets">Shakespeare's Sonnets</a></h3>
        

        
            <div class="product_price">
                






    
        <p class="price_color">£20.66</p>
    

<p class="instock availability">
    <i class="icon-ok"></i>
    
        In stock
    
</p>

                
                    






    
    <form>
        <button type="submit" class="btn btn-primary btn-block" data-loading-text="Adding...">Add to basket</button>
    </form>


                
            </div>
        
    </article>

</li>
                    
                        <li class="col-xs-6 col-sm-4 col-md-3 col-lg-3">






    <article class="product_pod">
        
            <div class="image_container">
                
                    
                    <a href="catalogue/set-me-free_988/index.html"><img src="media/cache/5b/88/5b88c52633f53cacf162c15f4f823153.jpg" alt="Set Me Free" class="thumbnail"></a>
                    
                
            </div>
        

        
            
                <p class="star-rating Five">
                    <i class="icon-star"></i>
                    <i class="icon-star"></i>
                    <i class="icon-star"></i>
                    <i class="icon-star"></i>
                    <i class="icon-star"></i>
                </p>
            
        

        
            <h3><a href="catalogue/set-me-free_988/index.html" title="Set Me Free">Set Me Free</a></h3>
        

        
            <div class="product_price">
                






    
        <p class="price_color">£17.46</p>
    

<p class="instock availability">
    <i class="icon-ok"></i>
    
        In stock
    
</p>

                
                    






    
    <form>
        <button type="submit" class="btn btn-primary btn-block" data-loading-text="Adding...">Add to basket</button>
    </form>


                
            </div>
        
    </article>

</li>
                    
                        <li class="col-xs-6 col-sm-4 col-md-3 col-lg-3">






    <article class="product_pod">
        
            <div class="image_container">
                
                    
                    <a href="catalogue/scott-pilgrims-precious-little-life-scott-pilgrim-1_987/index.html"><img src="media/cache/94/b1/94b1b8b244bce9677c2f29ccc890d4d2.jpg" alt="Scott Pilgrim's Precious Little Life (Scott Pilgrim #1)" class="thumbnail"></a>
                    
                
            </div>
        

        
            
                <p class="star-rating Five">
                    <i class="icon-star"></i>
                    <i class="icon-star"></i>
                    <i class="icon-star"></i>
                    <i class="icon-star"></i>
                    <i class="icon-star"></i>
                </p>
            
        

        
            <h3><a href="catalogue/scott-pilgrims-precious-little-life-scott-pilgrim-1_987/index.html" title="Scott Pilgrim's Precious Little Life (Scott Pilgrim #1)">Scott Pilgrim's Precious Little ...</a></h3>
        

        
            <div class="product_price">
                






    
        <p class="price_color">£52.29</p>
    

<p class="instock availability">
    <i class="icon-ok"></i>
    
        In stock
    
</p>

                
                    






    
    <form>
        <button type="submit" class="btn btn-primary btn-block" data-loading-text="Adding...">Add to basket</button>
    </form>


                
            </div>
        
    </article>

</li>
                    
                        <li class="col-xs-6 col-sm-4 col-md-3 col-lg-3">






    <article class="product_pod">
        
            <div class="image_container">
                
                    
                    <a href="catalogue/rip-it-up-and-start-again_986/index.html"><img src="media/cache/81/c4/81c4a973364e17d01f217e1188253d5e.jpg" alt="Rip it Up and Start Again" class="thumbnail"></a>
                    
                
            </div>
        

        
            
                <p class="star-rating Five">
                    <i class="icon-star"></i>
                    <i class="icon-star"></i>
                    <i class="icon-star"></i>
                    <i class="icon-star"></i>
                    <i class="icon-star"></i>
                </p>
            
        

        
            <h3><a href="catalogue/rip-it-up-and-start-again_986/index.html" title="Rip it Up and Start Again">Rip it Up and ...</a></h3>
        

        
            <div class="product_price">
                






    
        <p class="price_color">£35.02</p>
    

<p class="instock availability">
    <i class="icon-ok"></i>
    
        In stock
    
</p>

                
                    






    
    <form>
        <button type="submit" class="btn btn-primary btn-block" data-loading-text="Adding...">Add to basket</button>
    </form>


                
            </div>
        
    </article>

</li>
                    
                        <li class="col-xs-6 col-sm-4 col-md-3 col-lg-3">






    <article class="product_pod">
        
            <div class="image_container">
                
                    
                    <a href="catalogue/our-band-could-be-your-life-scenes-from-the-american-indie-underground-1981-1991_985/index.html"><img src="media/cache/54/60/54607fe8945897cdcced0044103b10b6.jpg" alt="Our Band Could Be Your Life: Scenes from the American Indie Underground, 1981-1991" class="thumbnail"></a>
                    
                
            </div>
        

        
            
                <p class="star-rating Three">
                    <i class="icon-star"></i>
                    <i class="icon-star"></i>
                    <i class="icon-star"></i>
                    <i class="icon-star"></i>
                    <i class="icon-star"></i>
                </p>
            
        

        
            <h3><a href="catalogue/our-band-could-be-your-life-scenes-from-the-american-indie-underground-1981-1991_985/index.html" title="Our Band Could Be Your Life: Scenes from the American Indie Underground, 1981-1991">Our Band Could Be ...</a></h3>
        

        
            <div class="product_price">
                






    
        <p class="price_color">£57.25</p>
    

<p class="instock availability">
    <i class="icon-ok"></i>
    
        In stock
    
</p>

                
                    






    
    <form>
        <button type="submit" class="btn btn-primary btn-block" data-loading-text="Adding...">Add to basket</button>
    </form>


                
            </div>
        
    </article>

</li>
                    
                        <li class="col-xs-6 col-sm-4 col-md-3 col-lg-3">






    <article class="product_pod">
        
            <div class="image_container">
                
                    
                    <a href="catalogue/olio_984/index.html"><img src="media/cache/55/33/553310a7162dfbc2c6d19a84da0df9e1.jpg" alt="Olio" class="thumbnail"></a>
                    
                
            </div>
        

        
            
                <p class="star-rating One">
                    <i class="icon-star"></i>
                    <i class="icon-star"></i>
                    <i class="icon-star"></i>
                    <i class="icon-star"></i>
                    <i class="icon-star"></i>
                </p>
            
        

        
            <h3><a href="catalogue/olio_984/index.html" title="Olio">Olio</a></h3>
        

        
            <div class="product_price">
                






    
        <p class="price_color">£23.88</p>
    

<p class="instock availability">
    <i class="icon-ok"></i>
    
        In stock
    
</p>

                
                    






    
    <form>
        <button type="submit" class="btn btn-primary btn-block" data-loading-text="Adding...">Add to basket</button>
    </form>


                
            </div>
        
    </article>

</li>
                    
                        <li class="col-xs-6 col-sm-4 col-md-3 col-lg-3">






    <article class="product_pod">
        
            <div class="image_container">
                
                    
                    <a href="catalogue/mesaerion-the-best-science-fiction-stories-1800-1849_983/index.html"><img src="media/cache/09/a3/09a3aef48557576e1a85ba7efea8ecb7.jpg" alt="Mesaerion: The Best Science Fiction Stories 1800-1849" class="thumbnail"></a>
                    
                
            </div>
        

        
            
                <p class="star-rating One">
                    <i class="icon-star"></i>
                    <i class="icon-star"></i>
                    <i class="icon-star"></i>
                    <i class="icon-star"></i>
                    <i class="icon-star"></i>
                </p>
            
        

        
            <h3><a href="catalogue/mesaerion-the-best-science-fiction-stories-1800-1849_983/index.html" title="Mesaerion: The Best Science Fiction Stories 1800-1849">Mesaerion: The Best Science ...</a></h3>
        

        
            <div class="product_price">
                






    
        <p class="price_color">£37.59</p>
    

<p class="instock availability">
    <i class="icon-ok"></i>
    
        In stock
    
</p>

                
                    






    
    <form>
        <button type="submit" class="btn btn-primary btn-block" data-loading-text="Adding...">Add to basket</button>
    </form>


                
            </div>
        
    </article>

</li>
                    
                        <li class="col-xs-6 col-sm-4 col-md-3 col-lg-3">






    <article class="product_pod">
        
            <div class="image_container">
                
                    
                    <a href="catalogue/libertarianism-for-beginners_982/index.html"><img src="media/cache/0b/bc/0bbcd0a6f4bcd81ccb1049a52736406e.jpg" alt="Libertarianism for Beginners" class="thumbnail"></a>
                    
                
            </div>
        

        
            
                <p class="star-rating Two">
                    <i class="icon-star"></i>
                    <i class="icon-star"></i>
                    <i class="icon-star"></i>
                    <i class="icon-star"></i>
                    <i class="icon-star"></i>
                </p>
            
        

        
            <h3><a href="catalogue/libertarianism-for-beginners_982/index.html" title="Libertarianism for Beginners">Libertarianism for Beginners</a></h3>
        

        
            <div class="product_price">
                






    
        <p class="price_color">£51.33</p>
    

<p class="instock availability">
    <i class="icon-ok"></i>
    
        In stock
    
</p>

                
                    






    
    <form>
        <button type="submit" class="btn btn-primary btn-block" data-loading-text="Adding...">Add to basket</button>
    </form>


                
            </div>
        
    </article>

</li>
                    
                        <li class="col-xs-6 col-sm-4 col-md-3 col-lg-3">






    <article class="product_pod">
        
            <div class="image_container">
                
                    
                    <a href="catalogue/its-only-the-himalayas_981/index.html"><img src="media/cache/27/a5/27a53d0bb95bdd88288eaf66c9230d7e.jpg" alt="It's Only the Himalayas" class="thumbnail"></a>
                    
                
            </div>
        

        
            
                <p class="star-rating Two">
                    <i class="icon-star"></i>
                    <i class="icon-star"></i>
                    <i class="icon-star"></i>
                    <i class="icon-star"></i>
                    <i class="icon-star"></i>
                </p>
            
        

        
            <h3><a href="catalogue/its-only-the-himalayas_981/index.html" title="It's Only the Himalayas">It's Only the Himalayas</a></h3>
        

        
            <div class="product_price">
                






    
        <p class="price_color">£45.17</p>
    

<p class="instock availability">
    <i class="icon-ok"></i>
    
        In stock
    
</p>

                
                    






    
    <form>
        <button type="submit" class="btn btn-primary btn-block" data-loading-text="Adding...">Add to basket</button>
    </form>


                
            </div>
        
    </article>

</li>
                    
                </ol>
                



    <div>
        <ul class="pager">
            
            <li class="current">
            
                Page 1 of 50
            
            </li>
            
                <li class="next"><a href="catalogue/page-2.html">next</a></li>
            
        </ul>
    </div>


            </div>
        </section>
    


            </div>

        </div><!-- /row -->
    </div><!-- /page_inner -->

*/


/*
 * Inner Html of div container-fluid at Product Page
 * 
 *  <div class="page_inner">
                
<ul class="breadcrumb">
    <li>
        <a href="../../index.html">Home</a>
    </li>
    
        
        <li>
            <a href="../category/books_1/index.html">Books</a>
        </li>
        
        <li>
            <a href="../category/books/music_14/index.html">Music</a>
        </li>
        
        <li class="active">How Music Works</li>

        
        
    
</ul>

                

                



<div id="messages">

</div>

                
                <div class="content">
                    

                    
                    <div id="promotions">
                        
                    </div>

                    
                    <div id="content_inner">

<article class="product_page"><!-- Start of product page -->

    <div class="row">

        
        <div class="col-sm-6">
            




    

    

        
        <div id="product_gallery" class="carousel">
            <div class="thumbnail">
                <div class="carousel-inner">
                    <div class="item active">
                    
                        
                            <img src="../../media/cache/1d/40/1d4087ff0a63f09fae9cd8433d21c2c4.jpg" alt="How Music Works">
                        
                    
                    </div>
                </div>
            </div>
        </div>

    


        </div>
        

        
        <div class="col-sm-6 product_main">
            
            
            <h1>How Music Works</h1>

            
                






    
        <p class="price_color">£37.32</p>
    

<p class="instock availability">
    <i class="icon-ok"></i>
    
        In stock (19 available)
    
</p>

            

            
                



    <p class="star-rating Two">
        <i class="icon-star"></i>
        <i class="icon-star"></i>
        <i class="icon-star"></i>
        <i class="icon-star"></i>
        <i class="icon-star"></i>

        <!-- <small><a href="/catalogue/how-music-works_979/reviews/">
        
                
                    0 customer reviews
                
        </a></small>
         -->&nbsp;


<!-- 
    <a id="write_review" href="/catalogue/how-music-works_979/reviews/add/#addreview" class="btn btn-success btn-sm">
        Write a review
    </a>

 --></p>

            

            <hr>

            <div class="alert alert-warning" role="alert"><strong>Warning!</strong> This is a demo website for web scraping purposes. Prices and ratings here were randomly assigned and have no real meaning.</div>


            
                






            
        </div><!-- /col-sm-6 -->
        

    </div><!-- /row -->

    
        
        <div id="product_description" class="sub-header">
            <h2>Product Description</h2>
        </div>
        <p>How Music Works is David Byrne’s remarkable and buoyant celebration of a subject he has spent a lifetime thinking about. In it he explores how profoundly music is shaped by its time and place, and he explains how the advent of recording technology in the twentieth century forever changed our relationship to playing, performing, and listening to music.Acting as historian an How Music Works is David Byrne’s remarkable and buoyant celebration of a subject he has spent a lifetime thinking about. In it he explores how profoundly music is shaped by its time and place, and he explains how the advent of recording technology in the twentieth century forever changed our relationship to playing, performing, and listening to music.Acting as historian and anthropologist, raconteur and social scientist, he searches for patterns—and shows how those patterns have affected his own work over the years with Talking Heads and his many collaborators, from Brian Eno to Caetano Veloso. Byrne sees music as part of a larger, almost Darwinian pattern of adaptations and responses to its cultural and physical context. His range is panoptic, taking us from Wagnerian opera houses to African villages, from his earliest high school reel-to-reel recordings to his latest work in a home music studio (and all the big studios in between).Touching on the joy, the physics, and even the business of making music, How Music Works is a brainy, irresistible adventure and an impassioned argument about music’s liberating, life-affirming power. ...more</p>
        
    

    
    <div class="sub-header">
        <h2>Product Information</h2>
    </div>
    <table class="table table-striped">
        
        <tbody><tr>
            <th>UPC</th><td>327f68a59745c102</td>
        </tr>
        
        <tr>
            <th>Product Type</th><td>Books</td>
        </tr>

        
        
            <tr>
                <th>Price (excl. tax)</th><td>£37.32</td>
            </tr>
            
                <tr>
                    <th>Price (incl. tax)</th><td>£37.32</td>
                </tr>
                <tr>
                    <th>Tax</th><td>£0.00</td>
                </tr>
            
            <tr>
                <th>Availability</th>
                <td>In stock (19 available)</td>
            </tr>
        
        
        
            <tr>
                <th>Number of reviews</th>
                <td>0</td>
            </tr>
        
    </tbody></table>
    

    
        
        <section>
            <div id="reviews" class="sub-header">
            </div>
        </section>
        
    

    
        
    

    



    
        <div class="sub-header">
            <h2>Products you recently viewed</h2>
        </div>

        <ul class="row">
            
                <li class="col-xs-6 col-sm-4 col-md-3 col-lg-3">






    <article class="product_pod">
        
            <div class="image_container">
                
                    
                    <a href="../so-youve-been-publicly-shamed_832/index.html"><img src="../../media/cache/6e/d4/6ed4991d97f60db29ec7b421e61a2cf3.jpg" alt="So You've Been Publicly Shamed" class="thumbnail"></a>
                    
                
            </div>
        

        
            
                <p class="star-rating Two">
                    <i class="icon-star"></i>
                    <i class="icon-star"></i>
                    <i class="icon-star"></i>
                    <i class="icon-star"></i>
                    <i class="icon-star"></i>
                </p>
            
        

        
            <h3><a href="../so-youve-been-publicly-shamed_832/index.html" title="So You've Been Publicly Shamed">So You've Been Publicly ...</a></h3>
        

        
            <div class="product_price">
                






    
        <p class="price_color">£12.23</p>
    

<p class="instock availability">
    <i class="icon-ok"></i>
    
        In stock
    
</p>

                
                    






    
    <form>
        <button type="submit" class="btn btn-primary btn-block" data-loading-text="Adding...">Add to basket</button>
    </form>


                
            </div>
        
    </article>

</li>
            
                <li class="col-xs-6 col-sm-4 col-md-3 col-lg-3">






    <article class="product_pod">
        
            <div class="image_container">
                
                    
                    <a href="../the-artists-way-a-spiritual-path-to-higher-creativity_839/index.html"><img src="../../media/cache/0e/6d/0e6dc2484322c5b9e7854ced66fdf62d.jpg" alt="The Artist's Way: A Spiritual Path to Higher Creativity" class="thumbnail"></a>
                    
                
            </div>
        

        
            
                <p class="star-rating Five">
                    <i class="icon-star"></i>
                    <i class="icon-star"></i>
                    <i class="icon-star"></i>
                    <i class="icon-star"></i>
                    <i class="icon-star"></i>
                </p>
            
        

        
            <h3><a href="../the-artists-way-a-spiritual-path-to-higher-creativity_839/index.html" title="The Artist's Way: A Spiritual Path to Higher Creativity">The Artist's Way: A ...</a></h3>
        

        
            <div class="product_price">
                






    
        <p class="price_color">£38.49</p>
    

<p class="instock availability">
    <i class="icon-ok"></i>
    
        In stock
    
</p>

                
                    






    
    <form>
        <button type="submit" class="btn btn-primary btn-block" data-loading-text="Adding...">Add to basket</button>
    </form>


                
            </div>
        
    </article>

</li>
            
                <li class="col-xs-6 col-sm-4 col-md-3 col-lg-3">






    <article class="product_pod">
        
            <div class="image_container">
                
                    
                    <a href="../the-genius-of-birds_843/index.html"><img src="../../media/cache/13/57/1357c6aa40c9e63d2f931927fbf81f3f.jpg" alt="The Genius of Birds" class="thumbnail"></a>
                    
                
            </div>
        

        
            
                <p class="star-rating One">
                    <i class="icon-star"></i>
                    <i class="icon-star"></i>
                    <i class="icon-star"></i>
                    <i class="icon-star"></i>
                    <i class="icon-star"></i>
                </p>
            
        

        
            <h3><a href="../the-genius-of-birds_843/index.html" title="The Genius of Birds">The Genius of Birds</a></h3>
        

        
            <div class="product_price">
                






    
        <p class="price_color">£17.24</p>
    

<p class="instock availability">
    <i class="icon-ok"></i>
    
        In stock
    
</p>

                
                    






    
    <form>
        <button type="submit" class="btn btn-primary btn-block" data-loading-text="Adding...">Add to basket</button>
    </form>


                
            </div>
        
    </article>

</li>
            
                <li class="col-xs-6 col-sm-4 col-md-3 col-lg-3">






    <article class="product_pod">
        
            <div class="image_container">
                
                    
                    <a href="../the-omnivores-dilemma-a-natural-history-of-four-meals_854/index.html"><img src="../../media/cache/14/f3/14f3d525e2a114cd71e27201a16af188.jpg" alt="The Omnivore's Dilemma: A Natural History of Four Meals" class="thumbnail"></a>
                    
                
            </div>
        

        
            
                <p class="star-rating Two">
                    <i class="icon-star"></i>
                    <i class="icon-star"></i>
                    <i class="icon-star"></i>
                    <i class="icon-star"></i>
                    <i class="icon-star"></i>
                </p>
            
        

        
            <h3><a href="../the-omnivores-dilemma-a-natural-history-of-four-meals_854/index.html" title="The Omnivore's Dilemma: A Natural History of Four Meals">The Omnivore's Dilemma: A ...</a></h3>
        

        
            <div class="product_price">
                






    
        <p class="price_color">£38.21</p>
    

<p class="instock availability">
    <i class="icon-ok"></i>
    
        In stock
    
</p>

                
                    






    
    <form>
        <button type="submit" class="btn btn-primary btn-block" data-loading-text="Adding...">Add to basket</button>
    </form>


                
            </div>
        
    </article>

</li>
            
                <li class="col-xs-6 col-sm-4 col-md-3 col-lg-3">






    <article class="product_pod">
        
            <div class="image_container">
                
                    
                    <a href="../the-power-of-now-a-guide-to-spiritual-enlightenment_855/index.html"><img src="../../media/cache/03/38/0338682e76bad3216cd4c6c28b2b625a.jpg" alt="The Power of Now: A Guide to Spiritual Enlightenment" class="thumbnail"></a>
                    
                
            </div>
        

        
            
                <p class="star-rating Two">
                    <i class="icon-star"></i>
                    <i class="icon-star"></i>
                    <i class="icon-star"></i>
                    <i class="icon-star"></i>
                    <i class="icon-star"></i>
                </p>
            
        

        
            <h3><a href="../the-power-of-now-a-guide-to-spiritual-enlightenment_855/index.html" title="The Power of Now: A Guide to Spiritual Enlightenment">The Power of Now: ...</a></h3>
        

        
            <div class="product_price">
                






    
        <p class="price_color">£43.54</p>
    

<p class="instock availability">
    <i class="icon-ok"></i>
    
        In stock
    
</p>

                
                    






    
    <form>
        <button type="submit" class="btn btn-primary btn-block" data-loading-text="Adding...">Add to basket</button>
    </form>


                
            </div>
        
    </article>

</li>
            
                <li class="col-xs-6 col-sm-4 col-md-3 col-lg-3">






    <article class="product_pod">
        
            <div class="image_container">
                
                    
                    <a href="../algorithms-to-live-by-the-computer-science-of-human-decisions_880/index.html"><img src="../../media/cache/23/b4/23b42e094c02d52b14b11a960d49610e.jpg" alt="Algorithms to Live By: The Computer Science of Human Decisions" class="thumbnail"></a>
                    
                
            </div>
        

        
            
                <p class="star-rating One">
                    <i class="icon-star"></i>
                    <i class="icon-star"></i>
                    <i class="icon-star"></i>
                    <i class="icon-star"></i>
                    <i class="icon-star"></i>
                </p>
            
        

        
            <h3><a href="../algorithms-to-live-by-the-computer-science-of-human-decisions_880/index.html" title="Algorithms to Live By: The Computer Science of Human Decisions">Algorithms to Live By: ...</a></h3>
        

        
            <div class="product_price">
                






    
        <p class="price_color">£30.81</p>
    

<p class="instock availability">
    <i class="icon-ok"></i>
    
        In stock
    
</p>

                
                    






    
    <form>
        <button type="submit" class="btn btn-primary btn-block" data-loading-text="Adding...">Add to basket</button>
    </form>


                
            </div>
        
    </article>

</li>
            
        </ul>
    



</article><!-- End of product page -->
</div>
                </div>
            </div>
*/