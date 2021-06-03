using NUnit.Framework;
using Moq.Dapper;
using Moq;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace SiGe.EbillingServiceTest
{
    public class Tests
    {

        [SetUp]
        public void Setup()
        {
           
        }

        [Test]
        public void Test1Async()
        {
            var mockPersonRepository = new Mock<IPersonRepository>();

            // Simulamos un comportamiento correcto
            mockPersonRepository.Setup(m => m.GetAllAsync().Result).Returns(new List<PersonModel>());

            // Simulamos un comportamiento correcto
            mockPersonRepository.Setup(m => m.AddAsync(It.IsAny<PersonModel>()).Result).Returns(1);

            // Simulamos un comportamiento correcto
            mockPersonRepository.Setup(m => m.UpdateAsync(It.IsAny<PersonModel>()).Result).Returns(1);

            // Simulamos un comportamiento incorrecto
            mockPersonRepository.Setup(m => m.AddAsync(It.Is<PersonModel>(p => p.FatherLastName.Equals(""))).Result).Returns(0);

            // Creamos una instancia del mock y la inyectamos a la capa superior
            var personService = new PersonService(mockPersonRepository.Object);

            // Probamos
            Assert.AreEqual(personService.UpdateAsync(new PersonModel()).Result,1);
            Assert.AreEqual(personService.AddAsync(new PersonModel { FatherLastName = "" }).Result,0);
        }

        [Test]
        public void Test2Async()
        {
            var mockPersonRepository = new Mock<IUserRepository>();

            // Simulamos un comportamiento correcto
            mockPersonRepository.Setup(m => m.GetAllAsync().Result).Returns(new List<UserModel>());

            // Simulamos un comportamiento correcto
            mockPersonRepository.Setup(m => m.AddAsync(It.IsAny<UserModel>()).Result).Returns(1);

            // Simulamos un comportamiento correcto
            mockPersonRepository.Setup(m => m.UpdateAsync(It.IsAny<UserModel>()).Result).Returns(1);

            // Simulamos un comportamiento incorrecto
            mockPersonRepository.Setup(m => m.AddAsync(It.Is<UserModel>(p => p.UserName.Equals(""))).Result).Returns(0);

            // Creamos una instancia del mock y la inyectamos a la capa superior
            var userService = new UserService(mockPersonRepository.Object);

            // Probamos
            Assert.AreEqual(userService.UpdateAsync(new UserModel()).Result, 1);
            Assert.AreEqual(userService.AddAsync(new UserModel { UserName = "" }).Result, 0);
        }

        //[Test]
        //public void Test3Async()
        //{
        //    var mockUserRepository = new Mock<IUserRepository>();

        //    // Simulamos un comportamiento correcto
        //    mockUserRepository.Setup(m => m.ValidateAsync(It.IsAny<string>(), It.IsAny<string>()).Result).Returns(new UserModel());


        //    // Creamos una instancia del mock y la inyectamos a la capa superior
        //    var userService = new UserService(mockUserRepository.Object);

        //    // Probamos
        //    Assert.AreEqual(userService.ValidateAsync("","").Result, new UserModel());
        //}

        [Test]
        public void Test4Async()
        {
            var cmd = new Mock<ISecurityCommandText>();
            var cnn = new Mock<IConfiguration>();


        // Simulamos un comportamiento correcto
            cmd.Setup(m => m.ValidateUser).Returns("Usp_Sec_S_Sec_User_Validate");
            cnn.Setup(m => m.GetSection("ConnectionStrings")["DefaultConnection"]).Returns("Database=db_test;server=willaqtecbd.cb77qxe6fm1t.us-east-2.rds.amazonaws.com;UID=root;PWD=3ncruz1j4d45;Connection Timeout=120");

            // Creamos una instancia del mock y la inyectamos a la capa superior
            var userService = new UserRepository(cnn.Object,cmd.Object);

            var userModel = userService.ValidateAsync("46412825","46412825").Result;

            // Probamos
            Assert.AreNotEqual(userModel.UserName, "46412826");
            
        }

        [Test]
        public void Test5Async()
        {
            var cmd = new Mock<ISecurityCommandText>();
            var cnn = new Mock<IConfiguration>();
            var userRepository = new Mock<IUserRepository>();



            // Simulamos un comportamiento correcto
            cmd.Setup(m => m.ValidateUser).Returns("Usp_Sec_S_Sec_User_Validate");
            cnn.Setup(m => m.GetSection("ConnectionStrings")["DefaultConnection"]).Returns("Database=db_test;server=willaqtecbd.cb77qxe6fm1t.us-east-2.rds.amazonaws.com;UID=root;PWD=3ncruz1j4d45;Connection Timeout=120");

            // Creamos una instancia del mock y la inyectamos a la capa superior
            var userService = new UserRepository(cnn.Object, cmd.Object);

            var userModel = userService.ValidateAsync("46412825", "46412825").Result;

            // Probamos
            Assert.AreEqual(userModel.UserName, "46412825");

        }
    }
}