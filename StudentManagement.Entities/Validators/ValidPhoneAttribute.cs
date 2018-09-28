using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Globalization;
using Twilio;
using Twilio.Rest.Lookups.V1;

namespace StudentManagement.Entities.Validators
{
    class ValidPhoneAttribute : ValidationAttribute
    {
        //Custom Data Annotation Attribute for Phone validation
        public override string FormatErrorMessage(string name)
        {
            return String.Format(CultureInfo.CurrentCulture,
              ErrorMessageString, name);
        }

        public ValidPhoneAttribute()
        {
        }

        public override bool IsValid(object value)
        {
            //Get the configuration from App.config
            string accountSid = ConfigurationManager.AppSettings["Twilio.accountSid"].ToString();
            string authToken = ConfigurationManager.AppSettings["Twilio.authToken"].ToString();
            string validCultures = ConfigurationManager.AppSettings["Twilio.validCultures"].ToString();

            //Obtain an array of string with the supported cultures
            string[] cultureList = validCultures.Split(',');

            TwilioClient.Init(accountSid, authToken);
            bool result = false;

            foreach (string culture in cultureList)
            {
                try
                {
                    //Try to validate the number with the current cultures
                    var phoneNumber = PhoneNumberResource.Fetch(
                        countryCode: culture,
                        pathPhoneNumber: new Twilio.Types.PhoneNumber(value.ToString())
                    );

                    result = true;
                    break;
                }
                catch { }
            }

            return result;
        }
    }
}
