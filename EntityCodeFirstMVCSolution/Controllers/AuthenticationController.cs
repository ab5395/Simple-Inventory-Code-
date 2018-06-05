using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace EntityCodeFirstMVCSolution.Controllers
{
    public class AuthenticationController : Controller
    {
        // GET: Authentication
        public ActionResult Index()
        {
             ViewData["Text"] =  Abc("asfdfdfsdfsadfds");
            return View();
        }

        public Byte[] Abc(string userName)
        {
            //create the MD5CryptoServiceProvider object we will use to encrypt the password
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
            //create an array of bytes we will use to store the encrypted password
            //Create a UTF8Encoding object we will use to convert our password string to a byte array
            UTF8Encoding encoder = new UTF8Encoding();

            //encrypt the password and store it in the hashedBytes byte array
            Byte[] hashedBytes = md5Hasher.ComputeHash(encoder.GetBytes(userName));
            return hashedBytes;

        }
    }
}