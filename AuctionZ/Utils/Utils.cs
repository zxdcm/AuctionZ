using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using AuctionZ.Controllers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.DependencyInjection;

namespace AuctionZ.Utils
{
    public static class Utils
    {
        public static string SaveImage(IFormFile image, IHostingEnvironment env)
        {
            if (image == null)
                return "no_image.jpg";
            var fileName = GetUniqueName(image.FileName);

            var uploads = Path.Combine(env.WebRootPath, "images");
            var filePath = Path.Combine(uploads, fileName);
            image.CopyTo(new FileStream(filePath, FileMode.Create));

            return fileName;
        }

        private static string GetUniqueName(string fileName)
        {
            return Path.GetFileNameWithoutExtension(fileName)
                   + "_" + Guid.NewGuid().ToString().Substring(0, 4)
                   + Path.GetExtension(fileName);
        }


        public static int? GetUserId(this ClaimsPrincipal claims)
        {
            int.TryParse(claims?.FindFirst(ClaimTypes.NameIdentifier)?.Value, 
                out var res);
            return res;
        }

    }
}
