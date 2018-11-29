using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.AspNetCore.Mvc;
using MvcWebApp.Models;
using xmlCore.Models;

namespace MvcWebApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            List<xmlModel> words = new List<xmlModel>();

            XmlDocument doc = new XmlDocument();
            try
            {
                doc.Load("D:\\wordlist.xml");
                

                foreach (XmlNode node in doc.SelectNodes("/words/word"))
                {
                    words.Add(new xmlModel
                    {
                        text = node.Attributes["text"].InnerText,
                        count = int.Parse(node.Attributes["count"].InnerText),

                    });
                }

                return View(words.Take(10));
            }
            catch
            {
                return View("XML FILE");
            }

        }
    }
}
