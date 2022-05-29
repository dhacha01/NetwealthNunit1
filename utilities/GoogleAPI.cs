using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace NetwealthNunit.utilities
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Google.Apis.Auth.OAuth2;
    using Google.Apis.Gmail.v1;
    using Google.Apis.Gmail.v1.Data;
    using Google.Apis.Services;
    using Google.Apis.Util.Store;
    public class GoogleAPI
    {
        // If modifying these scopes, delete your previously saved credentials
        // at ~/.credentials/gmail-dotnet-quickstart.json
        private static string[] scopes = { GmailService.Scope.GmailReadonly };

        private static string applicationName = "Gmail API .NET Quickstart";

        public static async Task<string> GetNewUnreadEmail(string fromMail)
        {

            string body = "";
            UserCredential credential;
          string projectPath = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory + @"../../").FullName;

        string clientsecretPath = projectPath + "\\client_secret.json";
            using (var stream =
                new FileStream(clientsecretPath, FileMode.Open, FileAccess.Read))
            {
                string credPath = System.Environment.GetFolderPath(
                    System.Environment.SpecialFolder.Personal);
                credPath = Path.Combine(credPath, ".credentials\\gmail-dotnet-quickstart.json");

                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }

            // Create Gmail API service.
            var service = new GmailService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = applicationName,
            });
            // Reffer to stack over link below for the solution
            //https://stackoverflow.com/questions/36448193/how-to-retrieve-my-gmail-messages-using-gmail-api
            UsersResource.MessagesResource.ListRequest request =
            service.Users.Messages.List("ddhanashrichavan@gmail.com");
            request.LabelIds = "INBOX";
            request.IncludeSpamTrash = false;
            request.Q = "from: " + fromMail + "is:unread";

            var response = request.Execute();
            Console.WriteLine(response);
            Console.WriteLine(response.Messages);
            if (response != null && response.Messages != null)
            {
                DateTime previousDate = DateTime.MinValue; ;
                foreach (var email in response.Messages)
                {
                    var emailInfoRequest = service.Users.Messages.Get("ddhanashrichavan@gmail.com", email.Id);

                    //make another request for that email id...
                    var emailInfoResponse = await emailInfoRequest.ExecuteAsync();

                    if (emailInfoResponse != null)
                    {
                        string from = "";
                        string date = "";
                        string subject = "";
                        //string body = "";
                        DateTime oDate = DateTime.MinValue;
                        //loop through the headers and get the fields we need...
                        foreach (var mParts in emailInfoResponse.Payload.Headers)
                        {
                            //Console.WriteLine("mParts: " + mParts);
                            if (mParts.Name == "Date")
                            {
                                string rawDate = mParts.Value;
                                Console.WriteLine(" --------- mParts: " + mParts);
                                Console.WriteLine(" --------- mParts.Value: " + mParts.Value);
                                int dateStartIndex = 0;
                                int dateEndIndex = rawDate.IndexOf("-") - 1;
                                int dateEndIndex2 = rawDate.IndexOf("+") - 1;
                                if (dateEndIndex > 0)
                                {
                                    date = rawDate.Substring(dateStartIndex, dateEndIndex);
                                }
                                else if (dateEndIndex2 > 0)
                                {
                                    date = rawDate.Substring(dateStartIndex, dateEndIndex2);
                                }
                                else
                                {
                                    date = rawDate;
                                }
                                Console.WriteLine(" --------- date: " + date);
                                if (date != string.Empty)
                                {
                                    oDate = DateTime.ParseExact(date, "ddd, d MMM yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                                    if (previousDate == DateTime.MinValue)
                                    {
                                        previousDate = oDate;
                                    }
                                }

                            }
                            else if (mParts.Name == "From")
                            {
                                from = mParts.Value;
                                Console.WriteLine("from: " + from);
                            }
                            else if (mParts.Name == "Subject")
                            {
                                subject = mParts.Value;
                                Console.WriteLine("subject: " + subject);
                            }

                            if (date != string.Empty && from != string.Empty && DateTime.Compare(oDate, previousDate) >= 0)
                            {

                                if (emailInfoResponse.Payload.Parts == null && emailInfoResponse.Payload.Body != null)
                                {
                                    body = DecodeBase64String(emailInfoResponse.Payload.Body.Data);
                                    //Console.WriteLine("Decode base 64: " + body);
                                }
                                else
                                {
                                    body = GetNestedBodyParts(emailInfoResponse.Payload.Parts, "");
                                    //Console.WriteLine("Get nested body: " + body);
                                }

                                // need to replace some characters as the data for the email's body is base64
                                //string codedBody = body.Replace("-", "+");
                                //codedBody = codedBody.Replace("_", "/");
                                //byte[] data = Convert.FromBase64String(codedBody);
                                // body = Encoding.UTF8.GetString(data);
                                //Console.WriteLine("Decoded body***************: " + body);

                            }
                        }
                    }
                }
            }

            return body;
        }

        private static string DecodeBase64String(string s)
        {
            var ts = s.Replace("-", "+");
            ts = ts.Replace("_", "/");
            var bc = Convert.FromBase64String(ts);
            var tts = Encoding.UTF8.GetString(bc);

            return tts;
        }

        private static string GetNestedBodyParts(IList<MessagePart> part, string curr)
        {
            string str = curr;
            if (part == null)
            {
                return str;
            }
            else
            {
                foreach (var parts in part)
                {
                    if (parts.Parts == null)
                    {
                        if (parts.Body != null && parts.Body.Data != null)
                        {
                            var ts = DecodeBase64String(parts.Body.Data);
                            str += ts;
                        }
                    }
                    else
                    {
                        return GetNestedBodyParts(parts.Parts, str);
                    }
                }

                return str;
            }
        }
    }
}

