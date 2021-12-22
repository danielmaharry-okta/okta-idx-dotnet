

namespace embedded_auth_with_sdk.Controllers
{
    using embedded_auth_with_sdk.Models;
    using Okta.Idx.Sdk;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    public class OktaVerifyController : Controller
    {
        public ActionResult Enroll()
        {
            var oktaVerifyEnrollOptions = (OktaVerifyEnrollOptions)Session[nameof(OktaVerifyEnrollOptions)];
            return View("EnrollWithQrCode", new OktaVerifyEnrollModel(oktaVerifyEnrollOptions));
        }

        [HttpGet]
        public ActionResult SelectEnrollmentChannel()
        {
            var oktaVerifyEnrollOptions = (OktaVerifyEnrollOptions)Session[nameof(OktaVerifyEnrollOptions)];
            var selectEnrollmentChannelModel = new OktaVerifySelectEnrollmentChannelModel(oktaVerifyEnrollOptions);
            return View(selectEnrollmentChannelModel);
        }

        [HttpPost]
        public async Task<ActionResult> SelectEnrollmentChannel(OktaVerifySelectEnrollmentChannelModel model)
        {
            var oktaVerifyEnrollOptions = (OktaVerifyEnrollOptions)Session[nameof(OktaVerifyEnrollOptions)];
            if (!ModelState.IsValid)
            {
                var selectEnrollmentChannelViewModel = new OktaVerifySelectEnrollmentChannelModel(oktaVerifyEnrollOptions)
                {
                    SelectedChannel = model.SelectedChannel,
                };
                return View(selectEnrollmentChannelViewModel);
            }

            await oktaVerifyEnrollOptions.SelectEnrollmentChannelAsync(model.SelectedChannel);

            switch (model.SelectedChannel)
            {
                case "email":
                    return View("EnrollWithEmail");
                case "sms":
                    return View("EnrollWithPhoneNumber", new OktaVerifyEnrollWithPhoneNumberModel { CountryCode = "+1" });
            }

            throw new ArgumentException($"Unrecognized Okta Verify channel: {model.SelectedChannel}");
        }

        [HttpPost]
        public ActionResult EnrollWithEmail(OktaVerifyEnrollWithEmailModel emailModel)
        {
            var oktaVerifyEnrollOptions = (OktaVerifyEnrollOptions)Session[nameof(OktaVerifyEnrollOptions)];
            _ = oktaVerifyEnrollOptions.SendLinkToEmailAsync(emailModel.Email);
            var oktaVerifyEnrollModel = new OktaVerifyEnrollModel(oktaVerifyEnrollOptions)
            {
                Message = "An activation link was sent to your email, use it to complete Okta Verify enrollment."
            };

            return View("EnrollPoll", oktaVerifyEnrollModel);
        }

        [HttpPost]
        public ActionResult EnrollWithPhoneNumber(OktaVerifyEnrollWithPhoneNumberModel phoneNumberModel)
        {
            var oktaVerifyEnrollOptions = (OktaVerifyEnrollOptions)Session[nameof(OktaVerifyEnrollOptions)];
            _ = oktaVerifyEnrollOptions.SendLinkToPhoneNumberAsync($"{phoneNumberModel.CountryCode}{phoneNumberModel.PhoneNumber}");
            var oktaVerifyEnrollModel = new OktaVerifyEnrollModel(oktaVerifyEnrollOptions)
            {
                Message = "A activation link was sent to your phone via sms, use it to complete Okta Verify enrollment."
            };

            return View("EnrollPoll", oktaVerifyEnrollModel);
        }

        public async Task<ActionResult> Poll()
        {
            var oktaVerifyEnrollmentModel = (OktaVerifyEnrollOptions)Session[nameof(OktaVerifyEnrollOptions)];

            var pollResponse = await oktaVerifyEnrollmentModel.PollOnceAsync();
            if (!pollResponse.ContinuePolling)
            {
                var authenticators = (List<AuthenticatorViewModel>)Session["authenticators"];
                var oktaVerifyAuthenticator = authenticators.Where(authenticatorViewModel => authenticatorViewModel.Name == "Okta Verify").First();
                authenticators.Remove(oktaVerifyAuthenticator);
                pollResponse.Next = "/Manage/SelectAuthenticator";
            }

            return Json(pollResponse, JsonRequestBehavior.AllowGet);
        }
    }
}
