using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using TCNX.Models;

namespace TCNX.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Test()
        {
            return View();
        }
        [System.Web.Http.HttpPost]
        public IActionResult CryptoWallet()
        {
            return Ok("Ok");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //[HttpPost]
        //public System.Net.Http.HttpResponseMessage PostMethod(string name)
        //{
        //    PersonModel person = new PersonModel
        //    {
        //        Name = name,
        //        DateTime = DateTime.Now.ToString()
        //    };
        //    return Ok(person);
        //}
       
      
    }
}


public class TextResult : IHttpActionResult
{
    string _value;
    HttpRequestMessage _request;

    public TextResult(string value, HttpRequestMessage request)
    {
        _value = value;
        _request = request;
    }

    public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
    {
        var response = new HttpResponseMessage()
        {
            Content = new StringContent(_value),
            RequestMessage = _request
        };
        return Task.FromResult(response);
    }
}


public class PersonModel
{
    ///<summary>
    /// Gets or sets Name.
    ///</summary>
    public string Name { get; set; }

    ///<summary>
    /// Gets or sets DateTime.
    ///</summary>
    public string DateTime { get; set; }
}