using System;
using System.Net.Mail;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CAIRO.ElasticEmail.Tests
{
    [TestClass]
    public class ElasticemailWebApiTests
    {
        private string _apiKey = "4a56a464-7d80-44cc-8d78-4f3137c48c15";

        [TestMethod]
        public void SendUnauthorized()
        {
            var target = new ElasticemailWebApi("invalidApiKey");
            var mail = new ElasticemailMessage();
            mail.From = new MailAddress("carlos@amillionmonkeys.es", "John");
            mail.To.Add(new MailAddress("blogcorriente@gmail.com", "Anna"));
            var actual = target.Send(mail);

            Assert.AreEqual(ResultType.Error, actual.ResultType);
            Assert.IsTrue(actual.ErrorMessage.Contains("Unauthorized"));
        }

        [TestMethod]
        public void ErrorMessage_If_From_Address_Is_Missing()
        {
            var target = new ElasticemailWebApi(_apiKey);
            var actual = target.Send(new ElasticemailMessage());

            Assert.AreEqual(ResultType.Error, actual.ResultType);
            Assert.IsTrue(actual.ErrorMessage.Contains("sender address is missing"));
        }

        [TestMethod]
        public void ErrorMessage_If_To_Address_Is_Missing()
        {
            var target = new ElasticemailWebApi(_apiKey);
            var mail = new ElasticemailMessage();
            mail.From = new MailAddress("blogcorriente@gmail.com", "John");
            var actual = target.Send(mail);

            Assert.AreEqual(ResultType.Error, actual.ResultType);
            Assert.IsTrue(actual.ErrorMessage.Contains("recipient address is missing"));
        }

        [TestMethod]
        public void Send_Email_Returns_TransactionId()
        {
            var target = new ElasticemailWebApi(_apiKey);
            var mail = new ElasticemailMessage();
            mail.To.Add(new MailAddress("blogcorriente@gmail.com"));
            mail.From = new MailAddress("blogcorriente@gmail.com", "Marc");
            mail.ReplyTo = new MailAddress("blogcorriente@gmail.com", "Marc");
            mail.Subject = "Test";
            mail.Body = "Body";
            var actual = target.Send(mail);

            Assert.AreEqual(ResultType.Success, actual.ResultType);
            Assert.IsNotNull(actual.TransactionId);
        }

        [TestMethod]
        public void Send_Email_With_Attachment()
        {
            var target = new ElasticemailWebApi(_apiKey);
            var mail = new ElasticemailMessage();
            mail.To.Add(new MailAddress("blogcorriente@gmail.com", "Marc"));
            mail.From = new MailAddress("blogcorriente@gmail.com", "Marc");
            mail.ReplyTo = new MailAddress("blogcorriente@gmail.com", "Marc");
            mail.Subject = "Test";
            mail.Body = "Body";
            mail.AddAttachment("file.txt", new byte[100]);
            mail.AddAttachment("file2.txt", new byte[200]);
            var actual = target.Send(mail);

            Assert.AreEqual(ResultType.Success, actual.ResultType);
            Assert.IsNotNull(actual.TransactionId);
        }

        [TestMethod]
        public void GetDeliveryStatus_Valid_TransactionIdJson()
        {
            Guid id = Guid.Parse("	f088b2e9-5412-47f2-9cec-225b71ee667c");
            var target = new ElasticemailWebApi(_apiKey);

            DeliveryStatusResponseJson actual = target.GetDeliveryStatusJson(id);

            Assert.AreEqual(ResultType.Success, actual.ResultType);
            Assert.AreEqual(id, actual.DeliveryStatus.data.Id);
            Assert.AreEqual("complete", actual.DeliveryStatus.data.Status);
            Assert.AreEqual(1, actual.DeliveryStatus.data.DeliveredCount);
            
        }

        [TestMethod]
        public void GetDeliveryStatus_Valid_TransactionIdXml()
        {
            Guid id = Guid.Parse("f088b2e9-5412-47f2-9cec-225b71ee667c");
            var target = new ElasticemailWebApi(_apiKey);

            var actual = target.GetDeliveryStatusXml(id);

            Assert.AreEqual(ResultType.Success, actual.ResultType);
            Assert.AreEqual(id, actual.DeliveryStatus.Id);
            Assert.AreEqual(1, actual.DeliveryStatus.Delivered);
            Assert.AreEqual("complete", actual.DeliveryStatus.Status);
        }

        [TestMethod]
        public void GetDeliveryStatus_Invalid_TransactionIdJson()
        {
            var target = new ElasticemailWebApi(_apiKey);

            var actual = target.GetDeliveryStatusJson(Guid.Parse("f088b2e9-5412-47f2-9cec-225b71ee6672"));

            Assert.AreEqual(ResultType.Error, actual.ResultType);
            Assert.AreEqual("{\"success\":false,\"error\":\"No job with transactionId f088b2e9-5412-47f2-9cec-225b71ee6672 could be found.\"}", actual.ErrorMessage);
        }

        [TestMethod]
        public void GetDeliveryStatus_Invalid_TransactionIdXml()
        {
            var target = new ElasticemailWebApi(_apiKey);

            var actual = target.GetDeliveryStatusXml(Guid.Parse("f088b2e9-5412-47f2-9cec-225b71ee6672"));

            Assert.AreEqual(ResultType.Error, actual.ResultType);
            Assert.AreEqual("No job with transactionId f088b2e9-5412-47f2-9cec-225b71ee6672 could be found.", actual.ErrorMessage);
        }
    }
}
