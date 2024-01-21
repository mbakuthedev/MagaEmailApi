using Dna;
using MagaEmailApi.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace MagaEmailApi.Services
{
    /// <summary>
    /// The transaction based notifications throughout the application
    /// </summary>
    public static class TransactionNotifications
    {
        /// <summary>
        /// The DI instance of the <see cref="IEmailService"/>
        /// </summary>
        public static IEmailService EmailService => Dna.Framework.Service<IEmailService>();

        /// <summary>
        /// The DI instance of the <see cref="IConfiguration"/>
        /// </summary>
        public static IConfiguration Configuration => Framework.Service<IConfiguration>();

        /// <summary>
        /// The DI instance of the <see cref="ILogger"/>
        /// </summary>
        public static ILogger Logger => Framework.Service<ILogger>();

        //public static async Task SendComplaintByMail(Microsoft.AspNetCore.Mvc.Controller controller, UserDetails details)
        //{
        //    try
        //    {
        //        // Process the html content
        //        var htmlContent = await RenderViewAsync("~/Views/Email/UserDetailsTemplate.cshtml", reservationResult, controller);

        //        // Set the email credentials
        //        var emailCredentials = new EmailDetails
        //        {
        //            SenderEmail = Configuration["Email:Address"],
        //            SenderName = Configuration["Email:Title"],
        //            ReciepientEmail = Configuration["Email:Reciepient"],
        //            Bcc = new List<string> { Configuration["Email:Address"] },
        //            Subject = $"New Message",
        //            Content = htmlContent
        //        };

        //        // Send the email
        //        await EmailService.SendAsync(details);
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the error
        //        Logger.LogError($"Failed to Send Email: {ex.Message}");
        //    }
        //}

        private static async Task<string> RenderViewAsync<TModel>(string viewName, TModel model, Microsoft.AspNetCore.Mvc.Controller controller, bool isPartial = false)
        {
            if (string.IsNullOrEmpty(viewName))
            {
                viewName = controller.ControllerContext.ActionDescriptor.ActionName;
            }

            controller.ViewData.Model = model;

            using (var writer = new StringWriter())
            {
                IViewEngine? viewEngine = controller.HttpContext.RequestServices.GetService(typeof(ICompositeViewEngine)) as ICompositeViewEngine;
                ViewEngineResult viewResult = GetViewEngineResult(controller, viewName, isPartial, viewEngine);

                if (viewResult.Success == false)
                {
                    throw new System.Exception($"A view with the name {viewName} could not be found");
                }

                ViewContext viewContext = new ViewContext(
                    controller.ControllerContext,
                    viewResult.View,
                    controller.ViewData,
                    controller.TempData,
                    writer,
                    new HtmlHelperOptions()
                );

                await viewResult.View.RenderAsync(viewContext);

                return writer.GetStringBuilder().ToString();
            }
        }

        private static ViewEngineResult GetViewEngineResult(Microsoft.AspNetCore.Mvc.Controller controller, string viewName, bool isPartial, IViewEngine viewEngine)
        {
            if (viewName.StartsWith("~/"))
            {
                var hostingEnv = controller.HttpContext.RequestServices.GetService(typeof(IWebHostEnvironment)) as IWebHostEnvironment;
                return viewEngine.GetView(hostingEnv?.WebRootPath, viewName, !isPartial);
            }
            else
            {
                return viewEngine.FindView(controller.ControllerContext, viewName, !isPartial);

            }
        }

    }

}