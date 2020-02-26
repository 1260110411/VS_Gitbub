using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selenium
{
    class Program
    {
        static void Main(string[] args)
        {
            //using (IWebDriver driver = new OpenQA.Selenium.Chrome.ChromeDriver())
            //new OpenQA.Selenium.IE.InternetExplorerDriver(@"E:\Visual Studio 2015\VS_Github\VS_Gitbub\Selenium\packages\Selenium.WebDriver.ChromeDriver.80.0.3987.1600\driver\win32"))
            {
                //driver.Navigate().GoToUrl("http://www.baidu.com");//driver.Url = "http://www.baidu.com";是一样的
                //var source = driver.PageSource;
                //Console.WriteLine(source);
                #region 定位方法
                //var byID = driver.FindElement(By.Id("cards"));//通过id获取元素
                //var byClassName = driver.FindElements(By.ClassName("menu")); //通过类名获取元素
                //var byTagName = driver.FindElement(By.TagName("iframe"));//通过标签名获取元素
                //var byLinkText = driver.FindElement(By.LinkText("linkedtext"));//通过链接文本获取元素
                //var byName = driver.FindElement(By.Name("__VIEWSTATE"));//通过名字获取元素
                //var byPartialLinkText = driver.FindElement(By.PartialLinkText("text"));//通过部分链接文本获取元素
                //var byCss = driver.FindElement(By.CssSelector("#header .content .logo"));//通过CSS选择器获取元素
                //var byXPath = driver.FindElements(By.XPath("//div"));//通过XPath来获取元素(XPath使用可以参考上一篇博客)
                //var jsReturnValue = (IWebElement)((IJavaScriptExecutor)driver).ExecuteScript("jsfunname");//执行JS
                ////获取元素的值和属性
                //var byIDText = byID.Text;
                //var byIDAttributeText = byID.GetAttribute("id");
                ////模拟鼠标点击元素
                //driver.FindElement(By.Id("copyright")).Click();
                ////页面导航 
                //driver.Navigate().Forward();
                //driver.Navigate().Back();
                ////拖拽操作(可以实现滑动验证码的验证)
                //var element = driver.FindElement(By.Name("source"));
                //IWebElement target = driver.FindElement(By.Name("target"));
                //(new Actions(driver)).DragAndDrop(element, target).Perform();
                //driver.FindElement(By.Id("tranAmtText")).Clear();//清空文本框clear()
                //driver.FindElement(By.Id("kw")).SendKeys("Hello world!");//在文本框中输入指定的字符串sendkeys()
                //移动光标到指定的元素上perform
                //Actions action = new Actions(driver);
                //action.MoveToElement(driver.FindElement(By.XPath("//input[@id='submit' and @value='确定']"))).Perform();
                //driver.FindElement(By.Id("su")).Click();
                //各方法使用优先原则：
                //优先使用id,name,classname,link；次之使用CssSelector()；最后使用Xpath()；
                //因为Xpath()方法的性能和效率最低下。
                #endregion
                int i = 0;
                i++;
                int c = 0;
                int j = c++;
                Console.WriteLine("i={0},j={1}",i,j);
                
                //ID：driver.findElement(By.id(< elementID >))
                //Name：driver.findElement(By.name(< elementName >))
                //className：driver.findElement(By.className(< elementClassName >))
                //tagName：driver.findElement(By.tagName(< htmlTagName >))
                //linkText：driver.findElement(By.linkText(< linkText >))
                //partialLinkText：driver.findElement(By.partialLinkText(< partialLinkText >))
                //css：driver.findElement(By.cssSelector(< cssSelector >))
                //xpath：driver.findElement(By.xpath(< xpathQuery >))
                //driver.FindElement(By.XPath("//*[@id=\"kw\"]")).SendKeys("python");

                //var byID = driver.FindElement(By.Id("su"));
                Console.ReadKey();
            }
        }
    }
}
