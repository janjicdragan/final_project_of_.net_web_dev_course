using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Results;
using DraganJanjic.Controllers;
using DraganJanjic.Interfaces;
using DraganJanjic.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;



namespace DraganJanjic.Tests.Controllers
{
    [TestClass]
    public class ZaposleniControllerTest
    {
        [TestMethod]
        public void GetReturnsObjectAndOkStatusCode()
        {
            //Arrange
            var mockRepo = new Mock<IZaposleniRepository>();
            mockRepo.Setup(x => x.GetById(1)).Returns(new Zaposleni() { Id = 1, ImeIPrezime = "Pera Peric" });
            var controller = new ZaposleniController(mockRepo.Object);

            //Act
            IHttpActionResult actionResult = controller.Get(1);
            var contentResult = actionResult as OkNegotiatedContentResult<Zaposleni>;


            //Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(1, contentResult.Content.Id);
            Assert.IsInstanceOfType(actionResult, typeof(OkNegotiatedContentResult<Zaposleni>));
        }

        [TestMethod]
        public void PutReturnsBadRequest()
        {
            //Arrange
            var mockRepo = new Mock<IZaposleniRepository>();
            var controller = new ZaposleniController(mockRepo.Object);

            //Act
            IHttpActionResult actionResult = controller.Put(1, new Zaposleni() { Id = 2, ImeIPrezime = "Zika Zikic" });

            //Assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult));
        }

        [TestMethod]
        public void GetReturnsSortedCollectionOfObjects()
        {
            //Arrange
            List<Zaposleni> zaposleni = new List<Zaposleni>();
            zaposleni.Add(new Zaposleni() { Id = 1, ImeIPrezime = "Pera Peric", GodinaZaposlenja = 2010 });
            zaposleni.Add(new Zaposleni() { Id = 2, ImeIPrezime = "Mika Mikic", GodinaZaposlenja = 2011 });
            var mockRepo = new Mock<IZaposleniRepository>();
            mockRepo.Setup(x => x.GetAll()).Returns(zaposleni);
            var controller = new ZaposleniController(mockRepo.Object);

            //Act
            var result = controller.Get();

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.ToList().Count, zaposleni.Count);
            Assert.AreEqual(result.ElementAt(0), zaposleni.ElementAt(0));
            Assert.AreEqual(result.ElementAt(1), zaposleni.ElementAt(1));
        }

        [TestMethod]
        public void PostReturnsCollectionOfObjectsByFilter()
        {
            //Arrange
            List<Zaposleni> zaposleni = new List<Zaposleni>();
            zaposleni.Add(new Zaposleni() { Id = 3, ImeIPrezime = "Iva Ivic", Plata = 2000m });
            zaposleni.Add(new Zaposleni() { Id = 2, ImeIPrezime = "Mika Mikic", Plata = 1000m });
            PlataFilter2 filter = new PlataFilter2() { Najmanje = 1000m, Najvise = 2000m };

            var mockRepo = new Mock<IZaposleniRepository>();
            mockRepo.Setup(x => x.GetZaposleniByPlata(filter)).Returns(zaposleni);
            var controller = new ZaposleniController(mockRepo.Object);

            //Act
            var result = controller.PostFilterZaposleniByPlata(filter);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.ToList().Count, zaposleni.Count);
            Assert.AreEqual(result.ElementAt(0), zaposleni.ElementAt(0));
            Assert.AreEqual(result.ElementAt(1), zaposleni.ElementAt(1));
        }

    }
}
